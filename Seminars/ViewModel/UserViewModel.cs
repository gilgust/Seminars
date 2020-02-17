using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Seminars.Models;

namespace Seminars.ViewModel
{
    public class CreateUserModel
    {
        [Required]
        public string Name { get; set; }
        [Required]
        public string Email { get; set; }
        [Required]
        public string Password { get; set; }
    }
    public class LoginModel
    {
        [Required(ErrorMessage = "Укажите email")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Укажите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }
    }

    public class RoleEditModel
    {
        public AppRole Role { get; set; }
        public IEnumerable<AppUser> Members { get; set; }
        public IEnumerable<AppUser> NonMembers { get; set; }
    }

    public class RoleModificationModel
    {
        public string RoleName { get; set; }
        public string RoleId { get; set; }
        public string[] IdsToAdd{ get; set; }
        public string[] IdsToDelete{ get; set; }
    }

    public class LogupModel
    {
        [Required]
        [Remote(action: "NameIsAvailable", controller: "Account", ErrorMessage = "Name isn't available")]
        public string Name { get; set; }
        [Required]
        [DataType(DataType.EmailAddress)]
        [Remote(action: "EmailIsAvailable", controller: "Account", ErrorMessage = "Email is already in use")]
        public string Email { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [DataType(DataType.Password)]
        [Compare(nameof(Password), ErrorMessage = "password does not match")]
        public string PasswordConfirm { get; set; }
    }
}
