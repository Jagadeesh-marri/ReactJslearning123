using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;
using Microsoft.AspNetCore.Authorization;

namespace WebApplication1.Controllers
{
    [Route("api/[controller]")]
    [ApiController]


    public class UsersController : ControllerBase
    {
        private readonly WebapiContext _context;

        public UsersController(WebapiContext context)
        {
            _context = context;
        }

        // GET: api/Users
        [HttpGet]
        [Route("GetUsers")]
        //The IEnumerable<T> interface provides a way to access the elements in the List<int> 
        //Task is a type in the.NET framework that represents an asynchronous operation.The Task type is used to return a value from an asynchronous method and to communicate the status of the operation.
        public async Task<ActionResult<IEnumerable<User>>> GetUsers()
        {
            //ToListAsync, on the other hand, is a method that takes an IAsyncEnumerable and converts it to a List.
            return await _context.Users.ToListAsync();
        }

        // GET: api/Users/5
        [HttpGet("{id}")]
        public async Task<ActionResult<User>> GetUser(int id)
        {
            //FindAsync is used to retrieve a single entity from the database based on its primary key value.
            var user = await _context.Users.FindAsync(id);

            if (user == null)
            {
                return NotFound();
            }

            return user;
        }

        //Whenever an action has multiple return paths and needs to support returning multiple ActionResult types, 
        // PUT: api/Users/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutUser(int id, User user)
        {
            if (id != user.UserId)
            {
                return BadRequest();
            }
            //entry refers to a single record in the context that can be updated, deleted, or inserted in the database. It provides access to the current property values, original values, and entity state.
            //The EntityState enumeration is used to track the state of an entity within a context.It specifies the state of an entity, such as Added, Modified, Deleted, or Unchanged, in relation to the database. It helps the Entity Framework to determine what actions need to be taken
            _context.Entry(user).State = EntityState.Modified;
            try
            {
                //Here the data will insert to db
                await _context.SaveChangesAsync();
            }
            //DbUpdateConcurrencyException
            //For example, if two users are editing the same record in a web application, and one user saves the changes while the other user is still editing the record, a concurrency conflict may occur. In this scenario, Entity Framework Core will throw a DbUpdateConcurrencyException when the second user tries to save their changes, to indicate that the data they are trying to update has changed since they retrieved it.
            catch (DbUpdateConcurrencyException)
            {
                if (!UserExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }
            return NoContent();
        }

        // POST: api/Users
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<User>> PostUser(User user)
        {
            try
            {
                _context.Users.Add(user);
                await _context.SaveChangesAsync();
                //CreatedAtAction  
                //CreatedAtAction result is returned, which sends a 201 Created response to the client
                return CreatedAtAction("GetUser", new { id = user.UserId }, user);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        // DELETE: api/Users/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteUser(int id)
        {
            //FindAsync is used to retrieve a single entity based on a primary key, 
            //    User class is an entity that represents a user in a database
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            _context.Users.Remove(user);
            await _context.SaveChangesAsync();

            return NoContent();
        }
        [HttpGet("SP")]
        public async Task<ActionResult<List<User>>> getListofusers()
        {
            try
            {
                var output = await _context.Users.FromSqlRaw("uspGetAllUserslist").ToListAsync();
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("source")]
        public async Task<ActionResult<User>> GetUSerBySource(string source)
        {
            try
            {
                var output = await _context.Users.FromSqlRaw($"uspgettest {source}").ToListAsync();
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpGet("userid")]
        public async Task<ActionResult<User>> GetUserByid(int userid)
        {
            try
            {
                var output = await _context.Users.FromSqlRaw($"uspGetUserByUserid {userid}").ToListAsync();
                return Ok(output);
            }
            catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }


        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.UserId == id);
        }

        [HttpPut("userid")]
        public async Task<IActionResult> updateUser(int userid)
        {
            var output = await _context.Database.ExecuteSqlRawAsync($"uspUpdateuser {userid}  ");

            return Ok(output);
        }

    }
}
