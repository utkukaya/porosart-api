using System;
using System.Linq;
using porosartapi.model.ViewModel;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using System.Text;
using System.Linq;
using porosartapi.model;
using porosartapi.Services;
using Mapster;

using System.IdentityModel.Tokens.Jwt;
using Microsoft.IdentityModel.Tokens;
using porosartapi.model.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace porosartapi.Services;

public class WorkshopService : BaseService
{
    public WorkshopService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

   
    public async Task<ResponseVM> ChangeWorkshop(UploadWorkshopVM uploadWorkshopVM)
    {
        try
        {
            var existingWorkshop = _db.Workshop.FirstOrDefault(x => x.Id == uploadWorkshopVM.Id);

            if (existingWorkshop == null)
            {
                return new ResponseVM("Game card not found");
            }

            existingWorkshop.Title = uploadWorkshopVM.Title;
            existingWorkshop.Description = uploadWorkshopVM.Description;

            if (uploadWorkshopVM.ImageFile != null)
            {
                if (uploadWorkshopVM.ImageFile.Length > 0)
                {
                    if (!Directory.Exists(_appSetting.ImageFilesPath))
                    {
                        Directory.CreateDirectory(_appSetting.ImageFilesPath);
                    }

                    string filePath = Path.Combine(
                        _appSetting.ImageFilesPath,
                        uploadWorkshopVM.ImageFile.FileName
                    );

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadWorkshopVM.ImageFile.CopyToAsync(stream);
                    }

                    existingWorkshop.ImageName = uploadWorkshopVM.ImageFile.FileName;
                    existingWorkshop.ImageRatio = uploadWorkshopVM.ImageRatio;
                }
            }

            _db.Workshop.Update(existingWorkshop);
            _db.SaveChanges();

            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public async Task<ResponseVM> AddWorkshop(UploadWorkshopVM uploadWorkshopVM)
    {
        try
        {
            var file = uploadWorkshopVM.ImageFile;
            if (file == null || file.Length == 0)
                return new ResponseVM("No file uploaded");

            if (!Directory.Exists(_appSetting.ImageFilesPath))
                Directory.CreateDirectory(_appSetting.ImageFilesPath);

            string filePath = Path.Combine(_appSetting.ImageFilesPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var newWorkshop = new Workshop();
            newWorkshop.Title = uploadWorkshopVM.Title;
            newWorkshop.Description = uploadWorkshopVM.Description;
            newWorkshop.ImageName = file.FileName;
            newWorkshop.ImageRatio = uploadWorkshopVM.ImageRatio;
            _db.Workshop.Add(newWorkshop);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public async Task<ResponseVM> AddWorkshopImage(UploadWorkshopImagesVM uploadWorkshopImagesVM)
    {
        try
        {
            var workshopImages = _db.WorkshopImages.FirstOrDefault(x => x.WorkshopId == uploadWorkshopImagesVM.WorkshopId);
            if(workshopImages != null){
                return new ResponseVM("Image already available");
            }
            var file = uploadWorkshopImagesVM.ImageFile;
            if (file == null || file.Length == 0)
                return new ResponseVM("No file uploaded");

            if (!Directory.Exists(_appSetting.ImageFilesPath))
                Directory.CreateDirectory(_appSetting.ImageFilesPath);

            string filePath = Path.Combine(_appSetting.ImageFilesPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var newWorkshopImage = new WorkshopImages();
            newWorkshopImage.WorkshopId = uploadWorkshopImagesVM.WorkshopId;
            newWorkshopImage.ImageName = file.FileName;
            _db.WorkshopImages.Add(newWorkshopImage);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public ResponseVM RemoveWorkshopImages(RemoveWorkshopImagesVM removeWorkshopImagesVM)
    {
        foreach (var id in removeWorkshopImagesVM.ImageIds)
        {
            var workshopImages = _db.WorkshopImages.FirstOrDefault(x => x.Id == id);
            if (workshopImages != null)
            {
                _db.WorkshopImages.Remove(workshopImages);
                _db.SaveChanges();
            }
        }
        return new ResponseVM(true);
    }

public ResponseVM RemoveWorkshopImage(int workshopId)
    {
        var existingWorkshopImage = _db.WorkshopImages.FirstOrDefault(x => x.WorkshopId == workshopId);
        if(existingWorkshopImage != null){
            _db.WorkshopImages.Remove(existingWorkshopImage);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("The Workshop Image not found");
    }

    public ResponseVM<List<WorkshopVM>> GetWorkshops()
    {
        var workshops = _db.Workshop.ToList().Adapt<List<WorkshopVM>>();
        return new ResponseVM<List<WorkshopVM>>(workshops);
    }

    public ResponseVM<List<WorkshopImages>> GetWorkshopImages(int workshopId)
    {
        return new ResponseVM<List<WorkshopImages>>(
            _db.WorkshopImages.Where(x => x.WorkshopId == workshopId).ToList()
        );
    }

    public ResponseVM<WorkshopContentVM> GetWorkshopContent(int workshopId)
    {
        var workshopContent = _db.WorkshopContent.FirstOrDefault(x => x.WorkshopId == workshopId);
        if (workshopContent != null)
            return new ResponseVM<WorkshopContentVM>(workshopContent.Adapt<WorkshopContentVM>());
        else
            return new ResponseVM<WorkshopContentVM>("Workshop content cannot be found.");
    }

    public ResponseVM RemoveWorkshop(int workshopId)
    {
        var workshop = _db.Workshop.FirstOrDefault(x => x.Id == workshopId);
        if (workshop != null)
        {
            var workshopContent = _db.WorkshopContent.FirstOrDefault(x => x.WorkshopId == workshop.Id);
            if (workshopContent != null)
            {
                _db.WorkshopContent.Remove(workshopContent);
                _db.SaveChanges();
            }
            _db.Workshop.Remove(workshop);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("The Workshop Card not found");
    }

    public ResponseVM AddWorkshopContent(WorkshopContentVM workshopContentVM)
    {
        var newWorkshopContent = workshopContentVM.Adapt<WorkshopContent>();
        _db.WorkshopContent.Add(newWorkshopContent);
        _db.SaveChanges();
        return new ResponseVM(true);
    }

    public ResponseVM UpdateWorkshopContent(WorkshopContentVM workshopContentVM)
    {
        var workshopContent = _db.WorkshopContent.FirstOrDefault(x => x.WorkshopId == workshopContentVM.WorkshopId);
        if (workshopContent != null)
        {
            workshopContent.HTMLCode = workshopContentVM.HTMLCode;
            _db.WorkshopContent.Update(workshopContent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        else
        {
            var newWorkshopContent = workshopContentVM.Adapt<WorkshopContent>();
            _db.WorkshopContent.Add(newWorkshopContent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
    }
}
