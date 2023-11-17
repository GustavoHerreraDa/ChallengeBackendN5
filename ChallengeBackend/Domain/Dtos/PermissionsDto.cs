using Challenge.Domain.Entities;
using System;

namespace Challenge.Domain.Dtos;

public record struct PermissionsDto(int Id, string EmployeeForename, string EmployeeSurename, string PermissionType, DateTime PermissionDate) { }
