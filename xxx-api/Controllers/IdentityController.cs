﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace xxx_api.Controllers
{
    [Route("feature/[action]")]
    [Authorize]
    public class IdentityController : Controller
    {
        public IActionResult Index()
        {
            return new JsonResult(new
            {
                AuthenticationType = User.Identity.AuthenticationType,
                IsAuthenticated = User.Identity.IsAuthenticated,
                Name = User.Identity.Name
            });
        }
    }
}