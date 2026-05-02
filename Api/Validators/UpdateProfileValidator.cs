using System;
using Api.DTOs;
using FluentValidation;
namespace Api.Validators
{
    public class UpdateProfileValidator : AbstractValidator<UpdateProfileDTO>
{
    public UpdateProfileValidator()
    {
        RuleFor(x=>x.displayname).Length(4-10).NotEmpty();
        RuleFor(x=>x.City).MinimumLength(4).MaximumLength(22).NotEmpty();
        RuleFor(x=>x.Country).MinimumLength(4).MaximumLength(22).NotEmpty();
        RuleFor(x=>x.Description).Length(4-255);
    }
}
}
