namespace API.Controllers;

using Microsoft.AspNetCore.Mvc;


[ApiController]
[Route("api/[controller]")]
public class ErrorController : ControllerBase
{

    [HttpGet("not-found")]
    public IActionResult NotFoundError()
    {
        return NotFound();  //404
    }   
    [HttpGet("bad-request")]
    public IActionResult BadRequestError()
    {
        return BadRequest(); //400
    }   
    [HttpGet("unauthorized")]
    public IActionResult UnauthorizedError()
    {
        return Unauthorized(); //401
    }   
    [HttpGet("server-error")]
    public IActionResult ServerError()
    {
        throw new Exception("Sever Erroer"); //500  
    }   
    [HttpGet("validation-error")]
    public IActionResult ValidationError()
    {
       ModelState.AddModelError("Validation Error 1", "Validation error details");
        ModelState.AddModelError("Validation Error 2", "Validation error details");
       return ValidationProblem(); 
    }   
}
