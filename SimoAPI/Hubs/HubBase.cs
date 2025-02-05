using Microsoft.AspNetCore.SignalR;

public class HubBase : Hub
{

    //SendDataToGroups
    public async Task SendDataToGroups(string baseurl, string token, string setionCode, string actionName, object data)
    {
        // ReceiveData
        // await Clients.All.SendAsync("ReceiveMessage", user, message);
        var w = await SimoFunctions.checktoken(baseurl, token);
        if (w == null || w == "undefined" || w == "")
        {
            await Clients.Client(Context.ConnectionId).SendAsync("checktoken", "خطای اعتبارسنجی");
        }
        else
        {

            var u = Newtonsoft.Json.JsonConvert.DeserializeObject<serviceresult>(w).objectResult.ToString();
            if (u.Contains(setionCode.Encrypt()))
            {
                await Clients.Client(Context.ConnectionId).SendAsync("checktoken", "خطای کد نشست");
            }
            else
            {
                // join
                await Groups.AddToGroupAsync(Context.ConnectionId, setionCode);

                //onClientsrecive
                if (actionName != "RToaUser" && actionName != ("RToaUser" + actionName) )
                    await Clients.Group(setionCode).SendAsync(actionName, data);
            }

        }

    }


    //SendDataToaUser
    public async Task SendDataToaUser(string baseurl, string token, string username , string actionName, object data)
    {
        // ReceiveData
        // await Clients.All.SendAsync("ReceiveMessage", user, message);
        var w = await SimoFunctions.checktoken(baseurl, token);
        if (w == null || w == "undefined" || w == "")
        {
            await Clients.Client(Context.ConnectionId).SendAsync("checktoken", "خطای اعتبارسنجی");
        }
        else
        {
            // current username and id
            var u = Newtonsoft.Json.JsonConvert.DeserializeObject<serviceresult>(w).objectResult.ToString();
            // username join
            await Groups.AddToGroupAsync(Context.ConnectionId, u.Split("::")[0]);

            //onClientsrecive
            if(username != u.Split("::")[0])
                await Clients.Group(username).SendAsync("RToaUser" + actionName , data);
        }

    }


}