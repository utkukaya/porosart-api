using System.Text.RegularExpressions;
using System.ComponentModel.DataAnnotations;
using porosartapi.model;

namespace porosartapi.model.ViewModels;

public class UserFormVM
{
    public int Id { get; set; }

    [Required(AllowEmptyStrings = false)]
    public string FullName { get; set; }

    [MinLength(7)]
    [Required(AllowEmptyStrings = false)]
    public string Username { get; set; }
    public string Phone { get; set; }

    [Required(AllowEmptyStrings = false)]
    public int RoleId { get; set; }

    public string Password { get; set; }

    public bool isValidPassword()
    {
        if (isEmptyPassword()) return true;
        var mediumRegex = new Regex(@"^(?=.*?[A-Z])(?=.*?[a-z])(?=.*?[0-9]).{8,}$");
        return mediumRegex.IsMatch(Password);
    }
    public bool isEmptyPassword()
    {
        if (Password == null || Password.Length == 0) return true;
        else return false;
    }
    public void UpdateUser(User user)
    {
        user.FullName = FullName;
        user.Username = Username;
        user.Phone = Phone;
        if (!isEmptyPassword())
        {
            Hasher hasher = new Hasher(Password);
            user.Password = Convert.ToBase64String(hasher.ToArray());
        }
        user.RoleId = RoleId != 0 ? RoleId : user.RoleId;
        user.UpdateDate = DateTime.UtcNow;
    }
}