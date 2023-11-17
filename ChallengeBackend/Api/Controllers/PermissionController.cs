using Challenge.Application.UseCase.V1.PermissionsOperation.Commands.Create;
using Challenge.Application.UseCase.V1.PersonOperation.Commands.Create;
using Challenge.Application.UseCase.V1.PersonOperation.Commands.Update;
using Challenge.Application.UseCase.V1.PersonOperation.Queries.GetList;
using Challenge.Domain.Common;
using Challenge.Domain.Dtos;
using ChallengeBackend.Model;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using PermissionApi.Base;

namespace ChallagenBackend.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class PermissionController : ApiControllerBase
    {
        ///// <summary>
        ///// Creación de nueva persona
        ///// </summary>
        ///// <param name="body"></param>
        ///// <returns></returns>
        //[HttpPost]
        //[ProducesResponseType(typeof(CreatePermissionResponse), StatusCodes.Status201Created)]
        //[ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        //public async Task<IActionResult> Create(CreatePermissionsCommand body) => Result(await Mediator.Send(body));

        /// <summary>
        /// Listado de persona de la base de datos
        /// </summary>
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpGet("Request/{id}")]
        [ProducesResponseType(typeof(List<PermissionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Request(int id) => Result(await Mediator.Send(new ListGetPermissions() { Id = id }));
        /// <summary>
        /// Listado de persona de la base de datos
        /// </summary>
        /// <remarks>en los remarks podemos documentar información más detallada</remarks>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(typeof(List<PermissionsDto>), StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        public async Task<IActionResult> Get() => Result(await Mediator.Send(new ListPermission()));

        [HttpPut("{id}")]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status400BadRequest)]
        [ProducesResponseType(typeof(List<Notify>), StatusCodes.Status404NotFound)]
        public async Task<IActionResult> Update(int id, UpdatePermissionVm body)
        {
            return Result(await Mediator.Send(new UpdatePermissionCommand
            {
                Id = id,
                EmployeeForename = body.EmployeeForename,
                EmployeeSurename = body.EmployeeSurename,
                PermissionType = body.PermissionType,
                PermissionDate = body.PermissionDate
            }));
        }
    }
}