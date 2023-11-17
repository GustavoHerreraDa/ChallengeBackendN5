using AutoFixture;
using Challenge.Application.UseCase.V1.PersonOperation.Commands.Update;
using FluentAssertions;
using FluentAssertions.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.PersonOperation.Commands.Update
{
    public  class UpdatePermissionValidationTest
    {
        [Fact]
        public async Task Validation_WithPropertyCorrect_IsValidFalse()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePermissionCommand>();
            request.Id = 1;
            var validator = new UpdatePermissionValidation();
            // Act
            var result = await validator.ValidateAsync(request);
            // Assert
            result.IsValid.Should().BeFalse();

        }

        [Theory]
        [MemberData(nameof(Data))]
        public async Task Validation_WithPropertyIncorrect_IsValidTrue(int permissionId, string employeeForename, string EmployeeSurename, DateTime permissionDate, string PermissionType)
        {
            // Arrange
            var request = new UpdatePermissionCommand
            {
                Id = permissionId,
                EmployeeForename = employeeForename,
                EmployeeSurename = EmployeeSurename,
                PermissionDate = permissionDate,
                PermissionType = PermissionType
            };
            var validator = new UpdatePermissionValidation();
            // Act
            var result = await validator.ValidateAsync(request);
            // Assert
            result.IsValid.Should().BeTrue();
        }

        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { 1, "Homer", "Simpson" ,DateTime.Now, "DBA" }
               

            };
    }
}
