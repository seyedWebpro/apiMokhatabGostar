using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace allAPIs.SimoAPI.Controller.Telegram
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramApiController : ControllerBase
    {

        private readonly HttpClient _httpClient;

        public TelegramApiController(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        // یکی کردن api ها

        [HttpGet("getChannelInfo")]
        public async Task<IActionResult> GetChannelInfo(string channelName)
        {
            // توکن ربات تلگرام خود را اینجا قرار دهید
            string token = "7351126944:AAG1OdkcScBxO8PNS8WHi5JgsytRISEePfw";

            // تبدیل نام کانال به فرمت @channelName
            string chatId = $"@{channelName.TrimStart('@')}";
            string url = $"https://api.telegram.org/bot{token}/getChat?chat_id={chatId}";
            var response = await _httpClient.GetAsync(url);

            if (response.IsSuccessStatusCode)
            {
                var content = await response.Content.ReadAsStringAsync();
                var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

                if (jsonResponse != null && jsonResponse.ContainsKey("result"))
                {
                    var result = jsonResponse["result"] as JObject;

                    if (result != null)
                    {
                        // استخراج اطلاعات کانال
                        var channelInfo = new
                        {
                            // Id = result["id"]?.ToString(),
                            Title = result["title"]?.ToString(),
                            Username = result["username"]?.ToString(),
                            Description = result["description"]?.ToString(),
                            PhotoUrl = (string)null // مقدار پیش فرض PhotoUrl را null قرار می‌دهیم
                        };

                        // اگر عکس وجود دارد، درخواست getFile را برای گرفتن URL عکس ارسال کنید
                        string photoUrl = result["photo"]?["big_file_id"]?.ToString();
                        if (!string.IsNullOrEmpty(photoUrl))
                        {
                            // دریافت URL عکس از getFile
                            string getFileUrl = $"https://api.telegram.org/bot{token}/getFile?file_id={photoUrl}";
                            var fileResponse = await _httpClient.GetAsync(getFileUrl);

                            if (fileResponse.IsSuccessStatusCode)
                            {
                                var fileContent = await fileResponse.Content.ReadAsStringAsync();
                                var fileJsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContent);

                                if (fileJsonResponse != null && fileJsonResponse.ContainsKey("result"))
                                {
                                    var fileResult = fileJsonResponse["result"] as JObject;
                                    string filePath = fileResult["file_path"]?.ToString();
                                    if (!string.IsNullOrEmpty(filePath))
                                    {
                                        // ساخت URL کامل تصویر
                                        string fileUrl = $"https://api.telegram.org/file/bot{token}/{filePath}";
                                        // افزودن PhotoUrl به پاسخ
                                        channelInfo = new
                                        {
                                            // Id = result["id"]?.ToString(),
                                            Title = result["title"]?.ToString(),
                                            Username = result["username"]?.ToString(),
                                            Description = result["description"]?.ToString(),
                                            PhotoUrl = fileUrl // اضافه کردن URL عکس به پاسخ
                                        };
                                    }
                                }
                            }
                        }

                        return Ok(channelInfo);
                    }
                    else
                    {
                        return NotFound("Channel not found");
                    }
                }
                else
                {
                    return NotFound("Channel not found");
                }
            }
            else
            {
                return NotFound("Channel not found");
            }
        }


        // [HttpGet("getChannelInfo")]
        // public async Task<IActionResult> GetChannelInfo(string channelName)
        // {
        //     // توکن ربات تلگرام خود را اینجا قرار دهید
        //     string token = "7351126944:AAG1OdkcScBxO8PNS8WHi5JgsytRISEePfw";

        //     // تبدیل نام کانال به فرمت @channelName
        //     string chatId = $"@{channelName.TrimStart('@')}";
        //     string url = $"https://api.telegram.org/bot{token}/getChat?chat_id={chatId}";
        //     var response = await _httpClient.GetAsync(url);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         var content = await response.Content.ReadAsStringAsync();
        //         var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

        //         if (jsonResponse != null && jsonResponse.ContainsKey("result"))
        //         {
        //             var result = jsonResponse["result"] as JObject;

        //             if (result != null)
        //             {
        //                 // استخراج اطلاعات مورد نیاز
        //                 var channelInfo = new
        //                 {
        //                     Id = result["id"]?.ToString(),
        //                     Title = result["title"]?.ToString(),
        //                     Username = result["username"]?.ToString(),
        //                     Description = result["description"]?.ToString(),
        //                     PhotoUrl = result["photo"]?["big_file_id"]?.ToString() // یا هر فیلد دیگری که نیاز دارید
        //                 };

        //                 return Ok(channelInfo);
        //             }
        //             else
        //             {
        //                 return NotFound("Channel not found");
        //             }
        //         }
        //         else
        //         {
        //             return NotFound("Channel not found");
        //         }
        //     }
        //     else
        //     {
        //         return NotFound("Channel not found");
        //     }
        // }

        // [HttpGet("getPhotoUrl")]
        // public async Task<IActionResult> GetPhotoUrl(string photoId)
        // {
        //     // توکن ربات تلگرام خود را اینجا قرار دهید
        //     string token = "7351126944:AAG1OdkcScBxO8PNS8WHi5JgsytRISEePfw";
        //     string getFileUrl = $"https://api.telegram.org/bot{token}/getFile?file_id={photoId}";

        //     var response = await _httpClient.GetAsync(getFileUrl);

        //     if (response.IsSuccessStatusCode)
        //     {
        //         var content = await response.Content.ReadAsStringAsync();
        //         var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);

        //         if (jsonResponse != null && jsonResponse.ContainsKey("result"))
        //         {
        //             var result = jsonResponse["result"] as JObject;
        //             if (result != null)
        //             {
        //                 string filePath = result["file_path"]?.ToString();
        //                 if (!string.IsNullOrEmpty(filePath))
        //                 {
        //                     string fileUrl = $"https://api.telegram.org/file/bot{token}/{filePath}";
        //                     return Ok(new { FileUrl = fileUrl });
        //                 }
        //             }
        //         }
        //     }

        //     return NotFound("File not found or invalid file_id");
        // }
    }
}