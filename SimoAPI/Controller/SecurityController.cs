using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace api.Controllers;


[Route("[controller]")]
[ApiController]
public class SecurityController : ControllerBase
{
    #region ctors
    public SecurityController(apiContext context,IHttpContextAccessor httpContextAccessor)
    {
        Con = context;
        Con.httpContext = httpContextAccessor.HttpContext;
    }
    public apiContext Con { get; }
    #endregion

    #region services
    [HttpPost("[action]")]
    public serviceresult Login(userview_login userInfo)
    {
        serviceresult result = new serviceresult();
        try
        {
            if(Con.users.Where(u=>u.roles.Contains("admin")).Count() == 0)
            {
                //create user admin
                user u = new user{
                    id = 0,
                    username = "admin",
                    password = "123".Encrypt(),
                    roles = "admin",
                };

                Con.users.Add(u);
                Con.SaveChanges();
            }


           userInfo.password = userInfo.password.Encrypt(); // رمز عبور وارد شده توسط کاربر را رمزگذاری کنید
        var entity = Con.users.Where(x => x.username == userInfo.username && x.password == userInfo.password).FirstOrDefault();
if (entity == null)
            {
                result.messageType = messageType.error;
                result.message = "نام کاربری یا کلمه عبور اشتباه است";
            }

            if (result.messageType == messageType.error)
                return result;

            var token = CreateToken(entity??new user());

            result.message = "";
            result.messageType = messageType.ok;
            result.objectResult = new
            {
                userId = entity?.id,
                username = entity?.username,
                roles = entity?.roles,
                startDate = token.ValidFrom,
                toDate = token.ValidTo,
                displayName = entity?.displayName,
                pic = entity?.pic,
                token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }
        catch (Exception ex)
        {
            result.messageType = messageType.error;
            result.message = ex.Message;
            result.objectResult = ex;
        }

        return result;
    }

    [Authorize]
    [HttpGet("[action]")]
    public serviceresult CheckToken()
    {
        serviceresult result = new serviceresult()
        {
            message = "OK",
            messageType = messageType.ok,
            objectResult = SimoFunctions.GetCurrentUserName(HttpContext) + "::" + SimoFunctions.GetCurrentUserId(HttpContext) + "::" + Newtonsoft.Json.JsonConvert.SerializeObject(Con.users.Select(u=>u.username.Encrypt()).ToList())
        };

        return result;

    }


    [Authorize]
    [HttpGet("[action]")]
    public serviceresult GetClientIp()
    {
        var remoteIp = HttpContext?.Connection?.RemoteIpAddress?.ToString();
        var userId = HttpContext?.User.Claims.Select(x => x.Value).ToArray()[0];
        serviceresult result = new serviceresult()
        {
            message = "OK",
            messageType = messageType.ok,
            objectResult = new
            {
                remoteIp = remoteIp,
                userId = userId
            }
        };

        return result;

    }
    #endregion

    #region functions 
    //create token
    private JwtSecurityToken CreateToken(user userInfo)
    {
        var securityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Con.Config["Jwt:Key"]??""));
        var credentials = new SigningCredentials(securityKey, SecurityAlgorithms.HmacSha256);

        List<Claim> claims = new List<Claim>
                {
                new Claim(JwtRegisteredClaimNames.Sub, userInfo.id.ToString()),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

        claims.Add(new Claim(ClaimTypes.NameIdentifier, userInfo.username));


        var rols = userInfo.roles ?? "";
        foreach (var rol in rols.Split(','))
        {
            claims.Add(new Claim(ClaimTypes.Role, rol));
        }

        //expire date
        var expiredt = DateTime.UtcNow.AddMinutes(180);
        if (userInfo.username == "admin")
        {
            expiredt = DateTime.UtcNow.AddDays(3);
        }

        var token = new JwtSecurityToken(Con.Config["Jwt:Issuer"],
            Con.Config["Jwt:Issuer"],
            claims,
            expires: expiredt,
            signingCredentials: credentials);

        return token;
    }
    #endregion
}
