using System.Runtime.InteropServices;
using Microsoft.AspNetCore.Mvc;

namespace API;

[ApiController]
[Route("[controller]")]
public class ConverterController : ControllerBase
{
    private readonly IConverter _converter;

    public ConverterController(IConverter converter)
    {
        this._converter = converter;
    }

    [HttpGet("value")]
    public ActionResult<string> GetResult([FromQuery] decimal value, bool allowRounding = false)
    {
        try
        {
            var convertedVal = _converter.Convert(value, allowRounding); 
            return Ok(convertedVal);
        }
        catch (System.Exception ex)
        {
            return BadRequest(ex.Message);
        }
    }

}
