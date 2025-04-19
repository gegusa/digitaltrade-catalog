using DigitalTrade.Catalog.Api.Contracts.Catalog;
using DigitalTrade.Catalog.Api.Contracts.Catalog.Command;
using DigitalTrade.Catalog.AppServices.Catalog;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace DigitalTrade.Catalog.Host.Controllers;

[Authorize]
[ApiController]
[Route(CatalogWebRoutes.BasePath)]
public class CatalogController : ControllerBase
{
    private readonly ICatalogHandler _handler;

    public CatalogController(ICatalogHandler handler)
    {
        _handler = handler;
    }

    [AllowAnonymous]
    [HttpPost(CatalogWebRoutes.CreateProduct)]
    public void CreateClient([FromBody] CreateProductCommand command)
    {
        _mediator.Send(command);

        Ok();
    }

    [AllowAnonymous]
    [HttpPost(CatalogWebRoutes.Authenticate)]
    public async Task<IActionResult> AuthenticateClient([FromBody] AuthenticateClientRequest request)
    {
        var response = await _mediator.Send(new AuthenticateClientCommand
        {
            Email = request.Email,
            Password = request.Password,
            IpAddress = IpAddress()!
        });

        SetTokenCookie(response.RefreshToken);

        return Ok(response);
    }
}