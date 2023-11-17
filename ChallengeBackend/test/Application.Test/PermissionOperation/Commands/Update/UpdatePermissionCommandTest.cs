
using AutoFixture;
using Challenge.Application.Common.Interfaces;
using Challenge.Application.Interfaces;
using Challenge.Application.UseCase.V1.PersonOperation.Commands.Update;
using Challenge.Domain.Entities;
using FluentAssertions;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using Xunit;

namespace Application.Test.PersonOperation.Commands.Update
{
    public class UpdatePermissionCommandTest
    {
        private readonly Mock<IPermissionsRepository> _repository;
        private readonly Mock<ILogger<UpdatePermissionHandler>> _logger;
        private readonly Mock<IProducerService> _producerService;
        private UpdatePermissionHandler _handler;
        private CancellationToken _cancellationToken;

        public UpdatePermissionCommandTest()
        {
            // Arrange
            _repository = new Mock<IPermissionsRepository>();
            _producerService = new Mock<IProducerService>();
            _logger = new Mock<ILogger<UpdatePermissionHandler>>();
            _producerService = new Mock<IProducerService>();
            _cancellationToken = CancellationToken.None;

            _handler = new UpdatePermissionHandler(_repository.Object, _logger.Object, _producerService.Object);
        }
        [Fact]
        public async Task Handler_UpdatePermission_Success()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePermissionCommand>();
            var person = new Fixture().Create<Permission>();
            _repository.Setup(_ => _.GetByIdAsync(It.IsAny<int>())).ReturnsAsync(person);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.OK);
            result.Content.Should().BeNullOrEmpty();
        }

        [Fact]
        public async Task Handler_UpdatePerson_PermissionNotExist()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePermissionCommand>();
            _repository.Setup(_ => _.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync((Permission)null);

            // Act
            var result = await _handler.Handle(request, _cancellationToken);

            // Assert
            result.StatusCode.Should().Be(HttpStatusCode.NotFound);
            result.IsValid.Should().BeFalse();
        }

        [Fact]
        public async Task Handler_UpdatePerson_ThrowUpdateDatabase()
        {
            // Arrange
            var request = new Fixture().Create<UpdatePermissionCommand>();
            var person = new Fixture().Create<Permission>();
            _repository.Setup(_ => _.GetByIdAsync(It.IsAny<int>()))
                .ReturnsAsync(person);
            _repository.Setup(_ => _.SaveChangeAsync()).ThrowsAsync(new DbUpdateException());

            // Act
            // Assert
            await Assert.ThrowsAsync<DbUpdateException>(() => _handler.Handle(request, _cancellationToken));

        }
    }
}
