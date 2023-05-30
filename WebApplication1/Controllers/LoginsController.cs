using System;
using System.Collections.Generic;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net.Mail;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginsController : ControllerBase
    {
        private readonly WebapiContext _context;
        private readonly JwtAuthenticationManager _jwtAuthenticationManager;
        
        public LoginsController(WebapiContext context, JwtAuthenticationManager jwtAuthenticationManager)
        {
            _context = context;
            this._jwtAuthenticationManager = jwtAuthenticationManager;
        }


        [AllowAnonymous]
        [HttpGet("AUthenticateUser")]
        public async Task<ActionResult<Login>> GetUSerDetails(string password, string emailaddress)
        {
            var customers = _context.Logins.FromSqlRaw("EXECUTE uspGetLogin @email_address={0}, @password={1}", emailaddress, password).ToList();
            if(customers.Count>0)
            {
                var token = _jwtAuthenticationManager.Authenticate(password);
                return Ok(token);
            }
            else
            {
                return BadRequest("User Not Found");
            }
        }
    }
}
