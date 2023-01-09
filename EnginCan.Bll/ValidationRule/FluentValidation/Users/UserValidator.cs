using FluentValidation;
using EnginCan.Entity.Models.Users;
using System;
using System.Collections.Generic;
using System.Text;

namespace EnginCan.Bll.ValidationRule.FluentValidation.Users
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(p => p.Name).NotEmpty().WithMessage("Lütfen kullanıcının adını boş geçmeyiniz.");
            RuleFor(p => p.Surname).NotEmpty().WithMessage("Lütfen kullanıcının soyadını boş geçmeyiniz.");
            RuleFor(p => p.PhoneNumber).NotEmpty().WithMessage("Lütfen kullanıcının telefon numarasını boş geçmeyiniz.");
            RuleFor(p => p.Email).NotEmpty().WithMessage("Lütfen kullanıcının emaril adresini boş geçmeyiniz.");
            RuleFor(p => p.Password).NotEmpty().WithMessage("Lütfen kullanıcının şifresini boş geçmeyiniz.");
        }
    }
}
