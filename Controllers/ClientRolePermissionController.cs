using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize(Policy = "FullAccess")] // Requires full-access token
    [Route("api/[controller]")]
    [ApiController]
    public class ClientRolePermissionController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IClientRolePermissionService _clientRolePermissionService;
        public ClientRolePermissionController(IMapper mapper, IClientRolePermissionService clientRolePermissionService) {
            _mapper = mapper;
            _clientRolePermissionService = clientRolePermissionService;
        }
        #region

        #endregion

        #region

        #endregion

        #region

        #endregion
    }
}
