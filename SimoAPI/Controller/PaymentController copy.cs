using System.Diagnostics;
using System.Text;
using System.Text.Json.Serialization;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using Newtonsoft.Json.Linq;
using SmsIrRestfulNetCore;

namespace api.Controllers;

[Route("[controller]")]
[ApiController]
public class PymentController : ControllerBase
{
    #region ctors
    public PymentController(IConfiguration configuration, apiContext dbcontext)
    {
        Configuration = configuration;
        Con = dbcontext;
    }

    public IConfiguration Configuration { get; }
    public apiContext Con { get; }
    #endregion

    #region Services

    [HttpPost("[action]")]
    public async Task<serviceresult> Payment(RequestParameters requestParametersinfo)
    {

        string merchant = "362102a5-a574-4a1e-92dd-663575de7417";
        string amount = requestParametersinfo.amount;
        string authority;
        string description = requestParametersinfo.description;
        string callbackurl = requestParametersinfo.callback_url;


        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();

        try
        {

            using (var client = new HttpClient())
            {
                RequestParameters parameters = new RequestParameters(merchant, amount, description, callbackurl, "", "");

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URLs.requestUrl, content);

                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jo = JObject.Parse(responseBody);
                string errorscode = jo["errors"].ToString();

                JObject jodata = JObject.Parse(responseBody);
                string dataauth = jodata["data"].ToString();


                if (dataauth != "[]")
                {

                    authority = jodata["data"]["authority"].ToString();

                    string gatewayUrl = URLs.gateWayUrl + authority;

                    result.messageType = messageType.ok;
                    result.message = gatewayUrl;

                }
                else
                {

                    result.messageType = messageType.error;
                    result.message = errorscode;

                }

            }

        }
        catch (Exception ex)
        {
            result = new serviceresult()
            {
                messageType = messageType.error,
                message = ex.Message,
                objectResult = null
            };
        }

        return result;
    }


    [HttpPost("[action]")]
    public async Task<serviceresult> Verify(VerifyParameters verifyParametersInfo)
    {

        string merchant = "362102a5-a574-4a1e-92dd-663575de7417";
        string amount = verifyParametersInfo.amount;
        string authority = verifyParametersInfo.authority;


        Con.httpContext = HttpContext;
        serviceresult result = new serviceresult();

        try
        {

            VerifyParameters parameters = new VerifyParameters();
            parameters.authority = authority;
            parameters.amount = amount;
            parameters.merchant_id = merchant;


            using (HttpClient client = new HttpClient())
            {

                var json = Newtonsoft.Json.JsonConvert.SerializeObject(parameters);

                HttpContent content = new StringContent(json, Encoding.UTF8, "application/json");

                HttpResponseMessage response = await client.PostAsync(URLs.verifyUrl, content);

                string responseBody = await response.Content.ReadAsStringAsync();

                JObject jodata = JObject.Parse(responseBody);

                string data = jodata["data"].ToString();

                JObject jo = JObject.Parse(responseBody);

                string errors = jo["errors"].ToString();

                if (data != "[]")
                {
                    string refid = jodata["data"]["ref_id"].ToString();

                    result.objectResult = refid;
                    result.messageType = messageType.ok;

                }
                else if (errors != "[]")
                {

                    string errorscode = jo["errors"]["code"].ToString();

                    result.messageType = messageType.error;
                    result.message = errorscode;

                }
            }

        }
        catch (Exception ex)
        {
            result = new serviceresult()
            {
                messageType = messageType.error,
                message = ex.Message,
                objectResult = null
            };
        }

        return result;
    }



    #endregion

    #region Functions

    #endregion
}
