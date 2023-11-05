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

public class GameService : BaseService
{
    public GameService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    // public async Task<ResponseVM> ChangeGameCard(UploadGameCardVM uploadGameCardVM)
    // {
    //     try
    //     {
    //         if (uploadGameCardVM.ImageFile != null)
    //         {
    //             var file = uploadGameCardVM.ImageFile;
    //             if (file == null || file.Length == 0)
    //                 return new ResponseVM("No file uploaded");

    //             if (!Directory.Exists(_appSetting.ImageFilesPath))
    //                 Directory.CreateDirectory(_appSetting.ImageFilesPath);

    //             string filePath = Path.Combine(_appSetting.ImageFilesPath, file.FileName);

    //             using (var stream = new FileStream(filePath, FileMode.Create))
    //             {
    //                 await file.CopyToAsync(stream);
    //             }
    //             var gameCard = _db.Games.FirstOrDefault(x => x.Id == uploadGameCardVM.Id);
    //             // var gameCard = new Game();
    //             if (gameCard != null)
    //             {
    //                 gameCard.Title = uploadGameCardVM.Title;
    //                 gameCard.Description = uploadGameCardVM.Description;
    //                 gameCard.ImageName = file.FileName;
    //                 gameCard.ImageRatio = uploadGameCardVM.ImageRatio;
    //                 _db.Games.Update(gameCard);
    //                 _db.SaveChanges();
    //                 return new ResponseVM(true);
    //             }
    //             else
    //             {
    //                 return new ResponseVM("Game card not found");
    //             }
    //         }
    //         else
    //         {
    //             var gameCard = _db.Games.FirstOrDefault(x => x.Id == uploadGameCardVM.Id);
    //             // var gameCard = new Game();
    //             if (gameCard != null)
    //             {
    //                 gameCard.Title = uploadGameCardVM.Title;
    //                 gameCard.Description = uploadGameCardVM.Description;
    //                 _db.Games.Update(gameCard);
    //                 _db.SaveChanges();
    //                 return new ResponseVM(true);
    //             }
    //             else
    //             {
    //                 return new ResponseVM("Game card not found");
    //             }
    //         }
    //     }
    //     catch (Exception ex)
    //     {
    //         return new ResponseVM(ex.Message);
    //     }
    // }

    public async Task<ResponseVM> ChangeGameCard(UploadGameCardVM uploadGameCardVM)
    {
        try
        {
            var gameCard = _db.Games.FirstOrDefault(x => x.Id == uploadGameCardVM.Id);

            if (gameCard == null)
            {
                return new ResponseVM("Game card not found");
            }

            gameCard.Title = uploadGameCardVM.Title;
            gameCard.Description = uploadGameCardVM.Description;

            if (uploadGameCardVM.ImageFile != null)
            {
                if (uploadGameCardVM.ImageFile.Length > 0)
                {
                    if (!Directory.Exists(_appSetting.ImageFilesPath))
                    {
                        Directory.CreateDirectory(_appSetting.ImageFilesPath);
                    }

                    string filePath = Path.Combine(
                        _appSetting.ImageFilesPath,
                        uploadGameCardVM.ImageFile.FileName
                    );

                    using (var stream = new FileStream(filePath, FileMode.Create))
                    {
                        await uploadGameCardVM.ImageFile.CopyToAsync(stream);
                    }

                    gameCard.ImageName = uploadGameCardVM.ImageFile.FileName;
                    gameCard.ImageRatio = uploadGameCardVM.ImageRatio;
                }
            }

            _db.Games.Update(gameCard);
            _db.SaveChanges();

            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public async Task<ResponseVM> AddGameCard(UploadGameCardVM uploadGameCardVM)
    {
        try
        {
            var file = uploadGameCardVM.ImageFile;
            if (file == null || file.Length == 0)
                return new ResponseVM("No file uploaded");

            if (!Directory.Exists(_appSetting.ImageFilesPath))
                Directory.CreateDirectory(_appSetting.ImageFilesPath);

            string filePath = Path.Combine(_appSetting.ImageFilesPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            // var gameCard = _db.Games.FirstOrDefault( x => uploadGameCardVM.Id);
            var gameCard = new Game();
            gameCard.Title = uploadGameCardVM.Title;
            gameCard.Description = uploadGameCardVM.Description;
            gameCard.ImageName = file.FileName;
            gameCard.ImageRatio = uploadGameCardVM.ImageRatio;
            _db.Games.Add(gameCard);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public async Task<ResponseVM> AddGameImage(UploadGameImagesVM uploadGameImagesVM)
    {
        try
        {
            var file = uploadGameImagesVM.ImageFile;
            if (file == null || file.Length == 0)
                return new ResponseVM("No file uploaded");

            if (!Directory.Exists(_appSetting.ImageFilesPath))
                Directory.CreateDirectory(_appSetting.ImageFilesPath);

            string filePath = Path.Combine(_appSetting.ImageFilesPath, file.FileName);

            using (var stream = new FileStream(filePath, FileMode.Create))
            {
                await file.CopyToAsync(stream);
            }

            var gameImage = new GameImages();
            gameImage.GameId = uploadGameImagesVM.GameId;
            gameImage.ImageName = file.FileName;
            _db.GameImages.Add(gameImage);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        catch (Exception ex)
        {
            return new ResponseVM(ex.Message);
        }
    }

    public ResponseVM RemoveGameImages(RemoveGameImagesVM removeGameImagesVM)
    {
        foreach (var id in removeGameImagesVM.ImageIds)
        {
            var gameImages = _db.GameImages.FirstOrDefault(x => x.Id == id);
            if (gameImages != null)
            {
                _db.GameImages.Remove(gameImages);
                _db.SaveChanges();
            }
        }
        return new ResponseVM(true);
    }

    public ResponseVM<List<GameVM>> GetGameCards()
    {
        var games = _db.Games.ToList().Adapt<List<GameVM>>();
        return new ResponseVM<List<GameVM>>(games);
    }

    public ResponseVM<List<GameImages>> GetGameImages(int gameId)
    {
        return new ResponseVM<List<GameImages>>(
            _db.GameImages.Where(x => x.GameId == gameId).ToList()
        );
    }

    public ResponseVM<GameContentVM> GetGameContent(int gameId)
    {
        var gameContent = _db.GameContent.FirstOrDefault(x => x.GameId == gameId);
        if (gameContent != null)
            return new ResponseVM<GameContentVM>(gameContent.Adapt<GameContentVM>());
        else
            return new ResponseVM<GameContentVM>("Game content cannot be found.");
    }

    public ResponseVM RemoveGameCard(int cardId)
    {
        var game = _db.Games.FirstOrDefault(x => x.Id == cardId);
        if (game != null)
        {
            var gameContent = _db.GameContent.FirstOrDefault(x => x.GameId == game.Id);
            if (gameContent != null)
            {
                _db.GameContent.Remove(gameContent);
                _db.SaveChanges();
            }
            _db.Games.Remove(game);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("The Game Card not found");
    }

    public ResponseVM AddGameContent(GameContentVM gameContentVM)
    {
        var newGameContent = gameContentVM.Adapt<GameContent>();
        _db.GameContent.Add(newGameContent);
        _db.SaveChanges();
        return new ResponseVM(true);
    }

    public ResponseVM UpdateGameContent(GameContentVM gameContentVM)
    {
        var gameContent = _db.GameContent.FirstOrDefault(x => x.GameId == gameContentVM.GameId);
        if (gameContent != null)
        {
            gameContent.HTMLCode = gameContentVM.HTMLCode;
            _db.GameContent.Update(gameContent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        else
        {
            var newGameContent = gameContentVM.Adapt<GameContent>();
            _db.GameContent.Add(newGameContent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
    }
    
}
