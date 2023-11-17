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
using Challenge.Application.Common.Models;
using Challenge.Application.Interfaces;
using System;

namespace Challenge.Application.UseCase.V1.PersonOperation.Queries.GetList
{

    public record struct ListPermission : IRequest<Response<List<PermissionsDto>>>
    {
    }

    public class ListPermissionsHandler : IRequestHandler<ListPermission, Response<List<PermissionsDto>>>
    {
        private readonly IPermissionsRepository _repository;
        private readonly IMapper _mapper;
        private readonly IProducerService _producerService;

        public ListPermissionsHandler(IPermissionsRepository repository, IMapper mapper, IProducerService producerService)
        {
            _repository = repository;
            _mapper = mapper;
            _producerService = producerService;
        }

        public async Task<Response<List<PermissionsDto>>> Handle(ListPermission request, CancellationToken cancellationToken)
        {
            var result = await _repository.GetAllAsync();

            var content = _mapper.Map<List<PermissionsDto>>(result);
            await SendKafkaMessage();
            return new Response<List<PermissionsDto>>
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
