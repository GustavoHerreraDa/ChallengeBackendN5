using System;

namespace Challenge.Domain.Entities;


public class Permission
{
    public int Id { get; set; }
    public string EmployeeForename { get; set; }
    public string EmployeeSurename { get; set; }
    public string PermissionType { get; set; }
    public DateTime PermissionDate { get; set; }
}
