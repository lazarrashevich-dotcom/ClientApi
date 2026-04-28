using Microsoft.AspNetCore.Mvc;
using ClientApi.Models;
using ClientApi.Services;

namespace ClientApi.Controllers;

[ApiController]
[Route("api/[controller]")]
[Produces("application/json")]
public class ClientsController : ControllerBase
{
    private readonly IClientService _svc;

    public ClientsController(IClientService svc) => _svc = svc;

    [HttpGet]
    public ActionResult<IEnumerable<ClientProfile>> GetAll()
        => Ok(_svc.GetAll());

    [HttpGet("{id:int}")]
    public ActionResult<ClientProfile> GetById(int id)
    {
        var c = _svc.GetById(id);
        return c is null ? NotFound() : Ok(c);
    }

    [HttpPost]
    public ActionResult<ClientProfile> Add([FromBody] ClientRequest req)
    {
        if (!ModelState.IsValid) return BadRequest(ModelState);
        var c = _svc.Add(req);
        return CreatedAtAction(nameof(GetById), new { id = c.Id }, c);
    }

    // POST /api/clients/analyze — вычисляемый ответ
    [HttpPost("analyze")]
    public ActionResult<ClientRecommendation> Analyze([FromBody] ClientRequest req)
        => Ok(_svc.Analyze(req));
}
