using System.Diagnostics;
using System.Text.Json.Serialization;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;

namespace api.Controllers;

[ApiController]
[Route("")]
public partial class ServicesController : ControllerBase
{

    public ServicesController(apiContext context,IHttpContextAccessor httpContextAccessor)
    {
        Con = context;
        Con.httpContext = httpContextAccessor.HttpContext;
    }

    public apiContext Con { get; }

}
