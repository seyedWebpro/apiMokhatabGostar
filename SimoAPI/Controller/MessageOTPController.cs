using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace allAPIs.SimoAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class MessageOTPController : ControllerBase
    {
        public MessageOTPController(IConfiguration configuration, apiContext context, ILogger<MessageOTPController> logger) 
        {
            _configuration = configuration;
            Con = context;
            _logger = logger;
        }
        private readonly IConfiguration _configuration;
        private readonly apiContext Con;


        private readonly ILogger<MessageOTPController> _logger; // اضافه کردن ILogger


        // [HttpPost("[action]")]
        // public async Task<IActionResult> SendPhone([FromBody] OtpView request)
        // {
        //     // 1. Validate the request
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(ModelState);
        //     }

        //     // 2. Configure HttpClient
        //     using var httpClient = new HttpClient();
        //     // 3. Get API key from configuration
        //     var apiKey = _configuration.GetValue<string>("otp:securKey");
        //     httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

        //     // 4. Construct the payload
        //     var payload = JsonSerializer.Serialize(new
        //     {
        //         mobile = request.Mobile,
        //         templateId = request.TemplateId,
        //         parameters = new[]
        //         {
        //             // تغییر مقدار value به request.Code
        //             new { name = "NUMBER", value = request.Code } // کد کاربر را ارسال می کند
        //         }
        //     });

        //     // 5. Send the request
        //     var content = new StringContent(payload, Encoding.UTF8, "application/json");
        //     var response = await httpClient.PostAsync("https://api.sms.ir/v1/send/verify", content);

        //     // 6. Handle the response
        //     if (response.IsSuccessStatusCode)
        //     {
        //         var result = await response.Content.ReadAsStringAsync();
        //         return Ok(result);
        //     }
        //     else
        //     {
        //         return StatusCode((int)response.StatusCode, "Error sending SMS");
        //     }
        // }

        [HttpPost("[action]")]
        public async Task<IActionResult> SendPhone(string phone)
        {
            // 1. Validate the request
            if (string.IsNullOrEmpty(phone))
            {
                return BadRequest("لطفا شماره تلفن را وارد کنید.");
            }

            // 2. Find user
            var user = Con.users.FirstOrDefault(u => u.PhoneNumber == phone);

            if (user == null)
            {
                return NotFound("کاربری با این شماره تلفن یافت نشد.");
            }

            // 3. Generate a random new password
            string newPassword = GenerateRandomPassword();

            // 4. Update user's password in the database
            user.password = HashPassword(newPassword); // هش کردن رمز عبور جدید 
            await Con.SaveChangesAsync();

            // 5. Send SMS with new password
            using var httpClient = new HttpClient();
            var apiKey = _configuration.GetValue<string>("otp:securKey");
            httpClient.DefaultRequestHeaders.Add("x-api-key", apiKey);

            var payload = JsonSerializer.Serialize(new
            {
                mobile = phone,
                templateId = 690975, // TemplateId ثابت
                parameters = new[]
                {
            new { name = "NUMBER", value = newPassword } // کد کاربر را ارسال می کند
        }
            });

            var content = new StringContent(payload, Encoding.UTF8, "application/json");
            var response = await httpClient.PostAsync("https://api.sms.ir/v1/send/verify", content);

            if (response.IsSuccessStatusCode)
            {
                var result = await response.Content.ReadAsStringAsync();
                return Ok(result);
            }
            else
            {
                return StatusCode((int)response.StatusCode, "Error sending SMS");
            }
        }

        private static string GenerateRandomPassword()
        {
            string[] words = new string[] { "new", "send", "forget", "password", "reset", "test", "tabligh", "instagram", "gostar", "mokhatab" };

            Random random = new Random();
            int wordIndex1 = random.Next(words.Length);
            int wordIndex2 = random.Next(words.Length);

            string password = words[wordIndex1] + words[wordIndex2] + random.Next(1000).ToString("D3");

            return password;
        }

        private static string HashPassword(string password)
        {
            return password.Encrypt();
        }

    }
}

