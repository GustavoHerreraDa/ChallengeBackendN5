using FluentValidation;

namespace Challenge.Application.UseCase.V1.PermissionsOperation.Commands.Create
{
    public class CreatePermissionValidation : AbstractValidator<CreatePermissionsCommand>
    {

        public CreatePermissionValidation()
        {
            RuleFor(x => x.ForeName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Apellido is invalid")
                .MaximumLength(255)
                .WithMessage("Apellido solo puede tener 255 caracteres");
            RuleFor(x => x.SureName)
                .Cascade(CascadeMode.Stop)
                .NotEmpty()
                .WithMessage("Nombre is invalid")
                .MaximumLength(255)
                .WithMessage("Nombre solo puede tener 255 caracteres");
        }
    }
}
