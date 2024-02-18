namespace OperationsService.Controllers;

using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using OperationsService.Models.Roles;
using OperationsService.Authorization;
using OperationsService.Services;
using OperationsService.Helpers;

[Authorize]
[ApiController]
[Route("[controller]")]

public class RolesController : ControllerBase
{
    private IRoleService _roleService;
    private IMapper _mapper;
    private readonly AppSettings _appSettings;
    public RolesController(
        IRoleService roleService,
        IMapper mapper,
        IOptions<AppSettings> appSettings)
    {
        _roleService = roleService;
        _mapper = mapper;
        _appSettings = appSettings.Value;
    }
    [AllowAnonymous]
    [HttpPost("create-role")]
    public IActionResult CreateRole(CreateRole model)
    {
        _roleService.Create(model);
        
        return Ok(new { message = "Registration successful"} );
    }
}
