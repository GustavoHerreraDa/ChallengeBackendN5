using FluentValidation;
using System;

namespace Challenge.Application.UseCase.V1.PersonOperation.Commands.Update;

public class UpdatePermissionValidation : AbstractValidator<UpdatePermissionCommand>
{
    public UpdatePermissionValidation()
    {
        RuleFor(x => x.Id)
            .Cascade(CascadeMode.Stop)
            .NotNull()
            .WithMessage("PermissionId is required");
        RuleFor(x => x.EmployeeForename)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("EmployeeForename is invalid")
            .MaximumLength(50)
            .WithMessage("EmployeeForename can only have 50 characters");
        RuleFor(x => x.EmployeeSurename)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("EmployeeSurename is invalid")
            .MaximumLength(50).WithMessage("EmployeeForename can only have 50 characters");
        RuleFor(x => x.PermissionType)
            .Cascade(CascadeMode.Stop)
            .NotEmpty()
            .WithMessage("PermissionType is invalid")
            .MaximumLength(50).WithMessage("PermissionType can only have 50 characters");
        RuleFor(x => x.PermissionDate)
            .NotEmpty()
            .WithMessage("PermissionDate is invalid")
            .LessThan(p => DateTime.Now).WithMessage("PermissionDate can only be before today");
    }
}
