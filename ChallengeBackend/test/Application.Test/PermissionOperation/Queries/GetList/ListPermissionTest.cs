using AutoFixture;
using AutoMapper;
using Challenge.Application.Common.Interfaces;
using Challenge.Application.Common.Models;
using Challenge.Application.Interfaces;
using Challenge.Application.UseCase.V1.PersonOperation.Queries.GetList;
using Challenge.Domain.Dtos;
using Challenge.Domain.Entities;
using FluentAssertions;
using Moq;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading;
using System.Threading.Tasks;

using Xunit;

namespace Application.Test.PersonOperation.Queries.GetList
{
    public class ListPermissionsTest
    {
        [Fact]
        public async Task Handle_ReturnsResponseWithPermissions()
        {
            // Arrange
            var mockRepository = new Mock<IPermissionsRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockProducerService = new Mock<IProducerService>();

            var handler = new ListPermissionsHandler(mockRepository.Object, mockMapper.Object, mockProducerService.Object);

            var permissions = new List<Permission>(); // Populate with test data as needed
            mockRepository.Setup(repo => repo.GetAllAsync()).ReturnsAsync(permissions);

            var permissionsDto = new List<PermissionsDto>(); // Populate with test data as needed
            mockMapper.Setup(mapper => mapper.Map<List<PermissionsDto>>(permissions)).Returns(permissionsDto);

            // Act
            var request = new ListPermission();
            var response = await handler.Handle(request, CancellationToken.None);

            // Assert
            Assert.NotNull(response);
            Assert.Equal(System.Net.HttpStatusCode.OK, response.StatusCode);
            Assert.Equal(permissionsDto, response.Content);

            // Optionally, add more assertions based on your specific requirements
        }

        [Fact]
        public async Task SendKafkaMessage_CallsProducerService()
        {
            // Arrange
            var mockRepository = new Mock<IPermissionsRepository>();
            var mockMapper = new Mock<IMapper>();
            var mockProducerService = new Mock<IProducerService>();

            var handler = new ListPermissionsHandler(mockRepository.Object, mockMapper.Object, mockProducerService.Object);

            // Act
            await handler.SendKafkaMessage();

            // Assert
            mockProducerService.Verify(producer => producer.SendEventAsync(It.IsAny<PermisionEvent>()), Times.Once);
        }
    }
}
