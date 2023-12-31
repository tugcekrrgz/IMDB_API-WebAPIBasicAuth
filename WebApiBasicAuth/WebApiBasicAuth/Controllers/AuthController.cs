﻿using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using WebApiBasicAuth.DTOs;

namespace WebApiBasicAuth.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AuthController : ControllerBase
    {
        private readonly UserManager<IdentityUser> _userManager;

        public AuthController(UserManager<IdentityUser> userManager)
        {
            _userManager = userManager;
        }

        [HttpPost]
        public async Task<IActionResult> Register(RegisterDTO registerDTO)
        {
            if (ModelState.IsValid)
            {
                var user = new IdentityUser
                {
                    UserName=registerDTO.Username,
                    Email=registerDTO.Email
                };

                var result= await _userManager.CreateAsync(user,registerDTO.Password);

                if (result.Succeeded)
                {
                    return Ok("Kullanıcı Oluşuruldu.");
                }
                else
                {
                    return BadRequest("Sorun meydana geldi!");
                }


            }
            else
            {
                return BadRequest();
            }
        }
    }
}
