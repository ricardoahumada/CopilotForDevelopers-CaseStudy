using FluentValidation;
using PortalEmpleo.Application.DTOs.Auth;

namespace PortalEmpleo.Application.Validators;

/// <summary>
/// Validator for RegisterDto using FluentValidation.
/// Validates email format, password complexity, phone E.164 format, and minimum age requirement.
/// </summary>
public class RegisterDtoValidator : AbstractValidator<RegisterDto>
{
    public RegisterDtoValidator()
    {
        RuleFor(x => x.Email)
            .NotEmpty().WithMessage("El email es requerido.")
            .EmailAddress().WithMessage("Formato de email inválido.")
            .MaximumLength(255).WithMessage("El email no puede exceder 255 caracteres.")
            .Matches(@"^[^@]+@[^@]+\.[^@]+$").WithMessage("Debe cumplir con el formato RFC 5322.");
        
        RuleFor(x => x.Password)
            .NotEmpty().WithMessage("La contraseña es requerida.")
            .MinimumLength(8).WithMessage("La contraseña debe tener al menos 8 caracteres.")
            .MaximumLength(100).WithMessage("La contraseña no puede exceder 100 caracteres.")
            .Matches(@"[A-Z]").WithMessage("Debe contener al menos una letra mayúscula.")
            .Matches(@"[a-z]").WithMessage("Debe contener al menos una letra minúscula.")
            .Matches(@"\d").WithMessage("Debe contener al menos un dígito.");
        
        RuleFor(x => x.FirstName)
            .NotEmpty().WithMessage("El nombre es requerido.")
            .MinimumLength(1).WithMessage("El nombre debe tener al menos 1 carácter.")
            .MaximumLength(100).WithMessage("El nombre no puede exceder 100 caracteres.");
        
        RuleFor(x => x.LastName)
            .NotEmpty().WithMessage("El apellido es requerido.")
            .MinimumLength(1).WithMessage("El apellido debe tener al menos 1 carácter.")
            .MaximumLength(100).WithMessage("El apellido no puede exceder 100 caracteres.");
        
        RuleFor(x => x.PhoneNumber)
            .NotEmpty().WithMessage("El número de teléfono es requerido.")
            .Matches(@"^\+[1-9]\d{1,14}$").WithMessage("Debe cumplir con el formato E.164 internacional (ej: +34612345678).")
            .MaximumLength(15).WithMessage("El número de teléfono no puede exceder 15 caracteres.");
        
        RuleFor(x => x.DateOfBirth)
            .NotEmpty().WithMessage("La fecha de nacimiento es requerida.")
            .LessThan(DateTime.UtcNow.AddYears(-16)).WithMessage("Debe ser mayor de 16 años.")
            .GreaterThan(DateTime.UtcNow.AddYears(-120)).WithMessage("La fecha de nacimiento no es válida.");
    }
}
