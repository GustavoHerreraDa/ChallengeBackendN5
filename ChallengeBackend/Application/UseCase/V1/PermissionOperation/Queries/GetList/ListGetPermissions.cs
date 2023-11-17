using Challenge.Domain.Dtos;
using Challenge.Domain.Entities;
using MediatR;
using System.Collections.Generic;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using System.Security;
using Challenge.Domain.Common;
using Challenge.Application.Common.Interfaces;
using AutoMapper;
using Challenge.Application.Interfaces;
using Challenge.Application.Common.Models;
using System;

namespace Challenge.Application.UseCase.V1.PersonOperation.Queries.GetList
{

    public record struct ListGetPermissions : IRequest<Response<PermissionsDto>>
    {
        public int Id { get; set; }
    }

    public class ListGetPermissionsHandler : IRequestHandler<ListGetPermissions, Response<PermissionsDto>>
    {
        private readonly IPermissionsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProducerService _producerService;

        public ListGetPermissionsHandler(IPermissionsRepository repository, IMapper mapper, IProducerService producerService)
        {
            _repository = repository;
            _mapper = mapper;
            _producerService = producerService;
        }

        public async Task<Response<PermissionsDto>> Handle(ListGetPermissions request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetByIdAsync(request.Id);

            if (result is null)
            {
                var response = new Response<PermissionsDto>();
                response.AddNotification("#3123", nameof(request.Id), string.Format(ErrorMessage.NOT_FOUND_RECORD, "Permission", request.Id));
                response.StatusCode = System.Net.HttpStatusCode.NotFound;
                return response;
            }

            var content = _mapper.Map<PermissionsDto>(result);

            await SendKafkaMessage();

            return new Response<PermissionsDto>
            {
                Content = content,
                StatusCode = System.Net.HttpStatusCode.OK
            };
        }

        public async Task SendKafkaMessage()
        {
            Guid entityId = Guid.NewGuid();

            var kafkaMessage = new PermisionEvent
            {
                Id = entityId,
                OperationName = "GetAll_Permissions"
            };

            await _producerService.SendEventAsync(kafkaMessage);
        }
    }
}
