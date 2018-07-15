using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.Entity;
using System.ComponentModel.DataAnnotations;


namespace App.Models
{
    public class UserAccount
    {
        [Key]   
        public int UserID { get; set; }

        [Required(ErrorMessage = "Введите имя")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "Введите фамилию")]
        public string LastName { get; set; }

        [Required(ErrorMessage = "Введите e-mail")]
        [RegularExpression(@"^(([\w-]+\.)+[\w-]+|([a-zA-Z]{1}|[\w-]{2,}))@"
     + @"((([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])\."
     + @"([0-1]?[0-9]{1,2}|25[0-5]|2[0-4][0-9])\.([0-1]?
				[0-9]{1,2}|25[0-5]|2[0-4][0-9])){1}|"
     + @"([a-zA-Z0-9]+[\w-]+\.)+[a-zA-Z]{1}[a-zA-Z0-9-]{1,23})$", ErrorMessage = "Введите правильный e-mail")]
        public string Email { get; set; }

        [Required(ErrorMessage ="Введите имя пользователя")]
        [RegularExpression(@"^[A-Z][a-zA-Z]*$", ErrorMessage ="Имя пользователя должно состоять из латинских букв и начинаться с заглавной")]
        public string Username { get; set; }

        [Required(ErrorMessage = "Введите пароль")]
        [DataType(DataType.Password)]
        public string Password { get; set; }

        [Compare("Password", ErrorMessage ="Пожалуйста, подтвердите ваш пароль")]
        [DataType(DataType.Password)]
        public string ConfirmPassword { get; set; }

        public int RoleId { get; set; }
        public Role Role { get; set; }

        public virtual ICollection<Protocol> Protocols { get; set; }
        public virtual ICollection<Assignment> Assignments { get; set; }

        public UserAccount()
        {
            Protocols = new List<Protocol>();
            Assignments = new List<Assignment>();
        }

    }

    public class Role
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }

}