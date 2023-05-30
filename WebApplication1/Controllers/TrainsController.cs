using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
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
    [Authorize]
    //ControllerBase provides a common set of methods and properties for controllers in the API.
    public class TrainsController : ControllerBase
    {
        private readonly WebapiContext _context;

        public TrainsController(WebapiContext context)
        {
            _context = context;
        }

        [HttpGet("Id")]
        public async Task<ActionResult<Trains>> GetTrainById(int Id)
        {
            try
            {
                //FirstAsync return the 1st value from query output;
                //Here I want particular id data when I use firstasync it returns 1st value 
                var output = await _context.Trains.FindAsync(Id);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("Id")]
        public async Task<ActionResult<IEnumerable<Trains>>> UpdateTrainData(int Id, int Train_Number, string Train_Name, string Source, string Destination)
        {
            try
            {
                var output = await _context.Database.ExecuteSqlRawAsync("EXEC  usepUpdateTrainData @Id={0} , @Train_Number={1} , @Train_Name={2} , @Source ={3} , @Destination={4}", Id, Train_Number, Train_Name, Source, Destination);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost("Train_Number")]
        public async Task<ActionResult<IEnumerable<Trains>>> AddNewTrainData(int Train_Number, string Train_Name, string Source, string Destination)
        {
            try
            {
                var output = await _context.Database.ExecuteSqlRawAsync("EXEC uspInsertNewRecord  @Train_Number={0} , @Train_Name={1} ,@Source={2} , @Destination={3}", Train_Number, Train_Name, Source, Destination);
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("Id")]
        public void DeleteTrainData(int Id)
        {
            _context.Database.ExecuteSqlRawAsync("EXEC uspDeleteRecord @Id={0}", Id);
        }
    }
}
