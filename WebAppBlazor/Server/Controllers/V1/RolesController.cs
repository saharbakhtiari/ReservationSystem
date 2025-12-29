using Application.RoleManagers.Commands.AssignPermission;
using Application.RoleManagers.Commands.CreateRole;
using Application.RoleManagers.Commands.DeleteRole;
using Application.RoleManagers.Commands.EditRole;
using Application.RoleManagers.Queries.GetPermission;
using Application.RoleManagers.Queries.GetRoles;
using Application.RoleManagers.Queries.GetRoleUsers;
using Application_Backend.Common;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Text.Json;
using System.Threading.Tasks;
using WebAppBlazor.Server.Controllers.V1;

namespace WebAppBlazor.Server.Controllers
{
    [ApiVersion("1.0")]
    [Route("api/v{version:apiVersion}/[controller]")]
    public class RolesController : ApiController
    {
        [HttpPost]
        public async Task<IActionResult> CreateRole([FromBody] CreateRoleCommand dto)
        {
            var id = await Mediator.SendWithUow(dto);
            return Ok(id);
        }

        [HttpPut]
        public async Task<IActionResult> EditRole( [FromBody] EditRoleCommand dto)
        {
             await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> DeleteRole(Guid id)
        {
            var dto = new DeleteRoleCommand() { Id = id };
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpPost("assignpermission")]
        public async Task<IActionResult> AssignPermission([FromBody] AssignPermissionCommand dto)
        {
            await Mediator.SendWithUow(dto);
            return Ok(true);
        }

        [HttpGet("roles/{pageNumber:int}/{pageSize:int}/{sort}/{filter?}")]
        public async Task<IActionResult> GetRoles(int pageNumber, int pageSize, string sort, string filter = null)
        {
            var output = await Mediator.SendWithUow(new GetRolesQuery()
            {
                PageNumber = pageNumber,
                PageSize = pageSize,
                Filter = filter,
                Sort = sort
            });
            Response.Headers.Add("X-Pagination", JsonSerializer.Serialize(output.MetaData));

            return Ok(output);
        }

        [HttpGet("users/{roleId:Guid}")]
        public async Task<IActionResult> GetUsers(Guid roleId)
        {
            var output = await Mediator.SendWithUow(new GetRoleUsersQuery()
            {
                RoleId = roleId
            });
            return Ok(output);
        }

        [HttpGet("permissions/{roleId:Guid}")]
        public async Task<IActionResult> GetPermissions(Guid roleId)
        {
            var output = await Mediator.SendWithUow(new GetPermissionQuery()
            {
                RoleId = roleId
            });
            return Ok(output);
        }

        [HttpGet("permissions")]
        public async Task<IActionResult> GetAllPermissions()
        {
            var output = await Mediator.SendWithUow(new GetAllPermissionQuery());
            return Ok(output);
        }
    }
}
