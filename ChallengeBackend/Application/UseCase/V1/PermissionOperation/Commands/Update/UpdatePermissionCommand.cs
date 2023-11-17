using Challenge.Domain.Common;
using Challenge.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.Common.Interfaces;
using UnitTest.Domain.Common;
using Challenge.Application.Common.Models;
using Challenge.Application.Interfaces;

namespace Challenge.Application.UseCase.V1.PersonOperation.Commands.Update;

public class UpdatePermissionCommand : IRequest<Response<string>>
{
    public int Id { get; set; }
    public string EmployeeForename { get; set; }
    public string EmployeeSurename { get; set; }
    public string PermissionType { get; set; }
    public DateTime PermissionDate { get; set; }
}
public class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, Response<string>>
{
    private readonly IPermissionsRepository _repository;
    private readonly ILogger<UpdatePermissionHandler> _logger;
    private readonly IProducerService _producerService;
    public UpdatePermissionHandler(IPermissionsRepository repository, ILogger<UpdatePermissionHandler> logger, IProducerService producerService)
    {
        _repository = repository;
        _logger = logger;
        _producerService = producerService;
    }

    public async Task<Response<string>> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
    {
        var permission = await _repository.GetByIdAsync(request.Id);
        var response = new Response<string>();
        if (permission is null)
        {
            _logger.LogError(string.Format(ErrorMessage.NOT_FOUND_RECORD, "Permission", request.Id));
            response.AddNotification("#3123", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Permission", request.Id));
            response.StatusCode = System.Net.HttpStatusCode.NotFound;
            return response;
        }

        permission.EmployeeForename = request.EmployeeForename;
        permission.EmployeeSurename = request.EmployeeSurename;
        permission.PermissionType = request.PermissionType;
        permission.PermissionDate = request.PermissionDate;

        _repository.Update(permission);
        await _repository.SaveChangeAsync();
        await SendKafkaMessage();
        return response;
    }

    public async Task SendKafkaMessage()
    {
        Guid entityId = Guid.NewGuid();
        
        var kafkaMessage = new PermisionEvent
        {
            Id = entityId,
            OperationName = "Modify_permissions" // O "request" o "get" según sea necesario
        };

        await _producerService.SendEventAsync(kafkaMessage);
    }
}
