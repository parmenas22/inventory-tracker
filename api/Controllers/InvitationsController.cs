using System.Security.Claims;
using api.DTOs.Auth;
using api.Helpers;
using api.Services.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace api.Controllers
{
    [ApiController]
    [Route("api/invitations")]
    public class InvitationsController : ControllerBase
    {
        private readonly IInvitationService _invitationService;

        public InvitationsController(IInvitationService invitationService)
        {
            _invitationService = invitationService;
        }

        [Authorize(Roles = "Admin")]
        [HttpGet("roles")]
        public async Task<IActionResult> GetRoles()
        {
            var result = await _invitationService.GetRolesAsync();
            return StatusCode((int)result.StatusCode, result);
        }

        [Authorize(Roles = "Admin")]
        [HttpPost]
        public async Task<IActionResult> CreateInvitation(
            [FromBody] InviteUserRequestDto request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid || request.RoleId == Guid.Empty)
            {
                return BadRequest(ApiResponse.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid invitation request."));
            }

            var userIdClaim = User.FindFirstValue("UserId");
            if (!Guid.TryParse(userIdClaim, out var invitedBy))
            {
                return Unauthorized(ApiResponse.Fail(System.Net.HttpStatusCode.Unauthorized, "The current user could not be identified."));
            }

            var result = await _invitationService.CreateInvitationAsync(request, invitedBy, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }

        [AllowAnonymous]
        [HttpPost("accept")]
        public async Task<IActionResult> AcceptInvitation(
            [FromBody] AcceptInvitationRequestDto request,
            CancellationToken cancellationToken)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ApiResponse.Fail(System.Net.HttpStatusCode.BadRequest, "Invalid invitation acceptance request."));
            }

            var result = await _invitationService.AcceptInvitationAsync(request, cancellationToken);
            return StatusCode((int)result.StatusCode, result);
        }
    }
}
