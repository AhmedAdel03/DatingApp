using System;
using Api.DTOs;
using FluentValidation;
namespace Api.Validators
{
    public class RegisterValidator : AbstractValidator<RegisterDTO>
    {
        public RegisterValidator()
        {
            RuleFor(x => x.Email).EmailAddress().NotEmpty().WithMessage("should be a valid EmailAddress");
            RuleFor(x => x.DisplayName).MinimumLength(4).MaximumLength(22).NotEmpty().WithMessage("should be a valid name");
            RuleFor(x => x.Password).Length(10, 16).NotEmpty().WithMessage("should be a between 10-16 char");
            RuleFor(x => x.Description).Length(4, 255).When(x => !string.IsNullOrEmpty(x.Description));
            RuleFor(x => x.Gender).NotEmpty();
            RuleFor(x => x.City).NotEmpty();
            RuleFor(x => x.Country).NotEmpty();
            RuleFor(x => x.Dateofbirth).NotEmpty();
        }
    }
}
