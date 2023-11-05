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

public class EventService : BaseService
{
    public EventService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    public ResponseVM AddEvent(EventVM eventVM) {
        // var event = new Event();
        var newEvent = eventVM.Adapt<Event>();
        _db.Event.Add(newEvent);
        _db.SaveChanges();
        return new ResponseVM(true);
    }

    public ResponseVM UpdateEvent(EventVM eventVM)
    {
        var existingEvent = _db.Event.FirstOrDefault(x => x.Id == eventVM.Id);
        if (existingEvent != null)
        {
            _db.Event.Update(eventVM.Adapt<Event>());
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("The Event not found");

    }

    public ResponseVM RemoveEvent(int eventId)
    {
        var existingEvent = _db.Event.FirstOrDefault(x => x.Id == eventId);
        if (existingEvent != null)
        {
            _db.Event.Remove(existingEvent);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("The Event not found");
    }

    public ResponseVM<List<EventVM>> GetEvents()
    {
        var events = _db.Event.ToList().Adapt<List<EventVM>>();
        return new ResponseVM<List<EventVM>>(events);
    }

}
