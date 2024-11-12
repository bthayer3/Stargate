﻿using Microsoft.AspNetCore.Mvc;
using StargateAPI.Business.Results;

namespace StargateAPI.Controllers
{
    public static class ControllerBaseExtensions
    {
        public static IActionResult GetResponse(this ControllerBase controllerBase, BaseResponse response)
        {
            var httpResponse = new ObjectResult(response)
            {
                StatusCode = response.ResponseCode
            };

            return httpResponse;
        }
    }
}