using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using System.Collections.Generic;

using rest_api.Models;

[ApiController]
[Route("users")]
public class UserController : ControllerBase {

    [HttpGet]
    [Route("")]
    public async Task<ActionResult<List<User>>> FindAll ([FromServices] DataContext context){
        var users = await context.Users.ToListAsync();

        return users;   
    }

    [HttpGet]
    [Route("{id:int}")]
    public async Task<ActionResult<User>> FindById ([FromServices] DataContext context, int id){
        var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

        return user;
    }

    [HttpPost]
    [Route("")]
    public async Task<ActionResult<User>> Create ([FromServices] DataContext context, [FromBody] User model){
        if (ModelState.IsValid){
            context.Users.Add(model);
            await context.SaveChangesAsync();

            return StatusCode(201, model);
        }
        
        return BadRequest(ModelState);
    }

    [HttpPut]
    [Route("{id:int}")]
    public async Task<ActionResult<User>> Update ([FromServices] DataContext context, int id, [FromBody] User model){
        if (ModelState.IsValid){
            var userToUpdate = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            userToUpdate.Name = model.Name;
            userToUpdate.Email = model.Email;
            context.Update(userToUpdate);
            await context.SaveChangesAsync();

            return userToUpdate;
       }

        return BadRequest(ModelState);
    }

    [HttpDelete]
    [Route("{id:int}")]
    public async Task<ActionResult<int>> Destroy([FromServices] DataContext context, int id){
        var userToDelete = await context.Users.FindAsync(id);
        context.Users.Remove(userToDelete);
        await context.SaveChangesAsync();

        return Ok();
    }
}