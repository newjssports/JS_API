﻿using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SportsOrderApp.Services;

namespace SportsOrderApp.Controllers
{
    [Authorize(Policy = "FullAccess")] // Requires full-access token
    [Route("api/[controller]")]
    [ApiController]
    public class FlowStepNameController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IFlowStepNameService _flowStepNameService;
        public FlowStepNameController(IMapper mapper, IFlowStepNameService flowStepNameService) {
            _mapper = mapper;
            _flowStepNameService = flowStepNameService;
        }
        #region

        #endregion

        #region

        #endregion

        #region

        #endregion
    }
}
