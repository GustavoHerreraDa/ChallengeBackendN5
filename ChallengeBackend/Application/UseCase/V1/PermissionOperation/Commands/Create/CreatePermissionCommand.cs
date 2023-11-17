using Challenge.Domain.Entities;
using MediatR;
using Microsoft.Extensions.Logging;
using System.Threading;
using System.Threading.Tasks;
using Challenge.Application.UseCase.V1.PersonOperation.Commands.Create;
using Challenge.Domain.Common;
using Challenge.Application.Common.Interfaces;

namespace Challenge.Application.UseCase.V1.PermissionsOperation.Commands.Create
{
    public class CreatePermissionsCommand : IRequest<Response<CreatePermissionResponse>>
    {
        /// <summary>
        /// 
        /// </summary>
        /// <example>Lucas</example>
        public string ForeName { get; set; }
        /// <summary>
        /// 
        /// </summary>
        /// <example>Olivera</example>
        public string SureName { get; set; }
    }

    public class CreatePermissionsCommandHandler : IRequestHandler<CreatePermissionsCommand, Response<CreatePermissionResponse>>
    {
        private readonly IPermissionsRepository _repository;
        private readonly ILogger<CreatePermissionsCommandHandler> _logger;

        public CreatePermissionsCommandHandler(IPermissionsRepository repository, ILogger<CreatePermissionsCommandHandler> logger)
        {
            _repository = repository;
            _logger = logger;
        }

        public async Task<Response<CreatePermissionResponse>> Handle(CreatePermissionsCommand request, CancellationToken cancellationToken)
        {
            var entity = new Permission
            {
                EmployeeForename = request.ForeName,
                EmployeeSurename = request.SureName
            };
            _repository.Insert(entity);
            await _repository.SaveChangeAsync();
            _logger.LogDebug("the Permissions was add correctly");

            return new Response<CreatePermissionResponse>
            {
                Content = new CreatePermissionResponse
                {
                    Message = "Success",
                    PersmissionId = entity.Id
                },
                StatusCode = System.Net.HttpStatusCode.Created
            };
        }
    }
}
