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

namespace porosartapi.Services;

public class UserService : BaseService
{
    public UserService(AppDbContext dbContext)
    {
        _db = dbContext;
    }

    public ResponseVM<UserVM> Login(string username, string password)
    {
        var user = _db.Users.Include(y => y.Role).SingleOrDefault(x => x.Username == username);
        if (user == null)
        {
            return new ResponseVM<UserVM>("User not found.");
        }

        var hasher = new Hasher(Convert.FromBase64String(user.Password));
        if (!hasher.Verify(password))
        {
            return new ResponseVM<UserVM>("Wrong password.");
        }

        var userVM = user.Adapt<UserVM>();
        AuthorizeUser(userVM);
        return new ResponseVM<UserVM>(userVM);
    }

    public void AuthorizeUser(UserVM userVM)
    {
        var tokenHandler = new JwtSecurityTokenHandler();
        var key = Encoding.ASCII.GetBytes("De944Li20vE20Co*19!VidArKu37id3AioR38912*?!-65");
        var tokenDescriptor = new SecurityTokenDescriptor
        {
            Subject = new ClaimsIdentity(new[]
            {
                new Claim("UserId", userVM.Id.ToString()),
                new Claim("Username", userVM.Username),
                new Claim("RoleId", userVM.RoleId.ToString())
            }),
            Expires = DateTime.UtcNow.AddDays(7),
            SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key),
                SecurityAlgorithms.HmacSha256Signature)
        };
        var token = tokenHandler.CreateToken(tokenDescriptor);
        userVM.Token = tokenHandler.WriteToken(token);
    }

    public ResponseVM SaveUser(UserFormVM registerVM)
    {
        if (!_db.Users.Any(x => x.Username == registerVM.Username))
        {
            Hasher hasher = new Hasher(registerVM.Password);
            registerVM.Password = Convert.ToBase64String(hasher.ToArray());
            var user = registerVM.Adapt<User>();
            _db.Users.Add(user);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("This username has been used before.");
    }

    public ResponseVM<UserVM> GetUser(int Id)
    {
        var user = _db.Users.Include(y => y.Role).FirstOrDefault(x => x.Id == Id);
        var userVM = user.Adapt<UserVM>();
        return new ResponseVM<UserVM>(userVM);
    }

    public ResponseVM<List<UserVM>> GetUsers()
    {
        var users = _db.Users.Include(y => y.Role).OrderBy(z => z.Id).ToList();
        var usersVM = users.Adapt<List<UserVM>>();
        return new ResponseVM<List<UserVM>>(usersVM);
    }

    public ResponseVM UpdateUser(UserFormVM userVM)
    {
        var user = _db.Users.Find(userVM.Id);
        if (user != null)
        {
            userVM.UpdateUser(user);
            _db.Users.Update(user);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("User not found");
    }

    public ResponseVM DeleteUser(int Id)
    {
        var user = _db.Users.Find(Id);
        if (user != null)
        {
            _db.Users.Remove(user);
            _db.SaveChanges();
            return new ResponseVM(true);
        }
        return new ResponseVM("User not found");
    }

}