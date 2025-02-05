// using System;
// using System.Collections.Generic;
// using System.Linq;
// using System.Text.Json;
// using System.Threading.Tasks;
// using Microsoft.AspNetCore.Authentication;
// using Microsoft.AspNetCore.Mvc;

// namespace allAPIs.SimoAPI.Controller
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class InstagramApiController : ControllerBase
//     {
//         private readonly HttpClient _httpClient;
//         private readonly IConfiguration _configuration;
//         private readonly ILogger<InstagramApiController> _logger;

//         private readonly string _accessToken;

//         public apiContext Con { get; }

//         public InstagramApiController(HttpClient httpClient, IConfiguration configuration, apiContext context, ILogger<InstagramApiController> logger)
//         {
//             _httpClient = httpClient;
//             _configuration = configuration;
//             Con = context;
//             _logger = logger;
//             _accessToken = "EAAOxQUaHhHUBO6V66K3JUQNutZBYmkRRdsIs6P1xEgXZB6ZAZBtgZCnAaZA9ZBrPtZAzT846Lw5pNsVMZBOBVRYS610pWhf819piJUaHnuOTQD714kj20yzVuGTiive5UiwymSI88UM6uHAZCJUJtTfAkZAzfEkPCDbwiwlJKGZBkssMmnZBUJTpZCrFRdY0zNAxnkAJBb";
//         }

//         [HttpPost("[action]")]
//         public async Task<IActionResult> ا(string username)
//         {
//             try
//             {
//                 await Task.Delay(2000);

//                 var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
//                 var url = $"https://graph.facebook.com/v20.0/17841469689053529?fields=business_discovery.username({username}){{id,username,followers_count,profile_picture_url,name}}&access_token={accessToken}";

//                 var response = await _httpClient.GetAsync(url);
//                 response.EnsureSuccessStatusCode();

//                 var jsonString = await response.Content.ReadAsStringAsync();
//                 var data = JsonDocument.Parse(jsonString).RootElement
//                     .GetProperty("business_discovery");

//                 // استخراج اطلاعات
//                 var pageId = data.GetProperty("id").GetString();
//                 var followersCount = data.GetProperty("followers_count").GetInt32();
//                 var profilePictureUrl = data.GetProperty("profile_picture_url").GetString();
//                 var name = data.GetProperty("name").GetString();

//                 // جستجوی صفحه در دیتابیس با استفاده از username
//                 var existPage = Con.Pages.FirstOrDefault(x => x.PageId == username); // جستجو با username

//                 if (existPage == null)
//                 {
//                     return NotFound("پیجی با این نام کاربری وجود ندارد");
//                 }

//                 // به‌روزرسانی اطلاعات صفحه
//                 existPage.Followesrs = followersCount;
//                 existPage.ShowName = name;
//                 existPage.ImgUrl = profilePictureUrl; // اگر می‌خواهید تصویر پروفایل را ذخیره کنید

//                 Con.SaveChanges();

//                 return Ok(new
//                 {
//                     Message = "پیج با موفقیت تغییر یافت",
//                     Page = existPage
//                 });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
//             }
//         }


//         // اضافه کردن انگیجمنت به متد اپدیت
//         [HttpPost("[action]")]
//         public async Task<IActionResult> update2(string username)
//         {
//             try
//             {
//                 await Task.Delay(2000);

//                 var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
//                 var url = $"https://graph.facebook.com/v21.0/17841469689053529?fields=business_discovery.username({username}){{id,username,followers_count,profile_picture_url,name,media_count,media{{id,media_type,timestamp,media_url,permalink,thumbnail_url,caption,like_count,comments_count}}}}&access_token={accessToken}";
//                 var response = await _httpClient.GetAsync(url);

//                 // بررسی وضعیت پاسخ
//                 if (!response.IsSuccessStatusCode)
//                 {
//                     var errorContent = await response.Content.ReadAsStringAsync();
//                     return BadRequest(new { Error = response.StatusCode, Message = errorContent });
//                 }

//                 // خواندن داده‌های JSON
//                 var jsonString = await response.Content.ReadAsStringAsync();
//                 var data = JsonDocument.Parse(jsonString).RootElement.GetProperty("business_discovery");

//                 // ایجاد Dictionary برای نتایج
//                 var result = new Dictionary<string, object>
//         {
//             {"id", data.GetProperty("id").GetString()},
//             {"username", data.GetProperty("username").GetString()},
//             {"followers", data.GetProperty("followers_count").GetInt32()},
//             {"ProfilePictureUrl", data.GetProperty("profile_picture_url").GetString()},
//             {"name", data.GetProperty("name").GetString()},
//             {"media_count", data.GetProperty("media_count").GetInt32()},
//             {"posts", new List<Dictionary<string, object>>()},
//         };

//                 // استخراج پست‌ها و نمایش id، media_type، timestamp، media_url و permalink
//                 foreach (var post in data.GetProperty("media").GetProperty("data").EnumerateArray())
//                 {
//                     var postData = new Dictionary<string, object>
//             {
//                 {"id", post.GetProperty("id").GetString()},
//                 {"media_type", post.GetProperty("media_type").GetString()},
//                 {"timestamp", post.GetProperty("timestamp").GetString()},
//                 {"media_url", post.GetProperty("media_url").GetString()},
//                 {"permalink", post.GetProperty("permalink").GetString()},
//                 {"thumbnail_url", post.TryGetProperty("thumbnail_url", out var thumbnail) ? thumbnail.GetString() : null},
//                 {"caption", post.TryGetProperty("caption", out var caption) ? caption.GetString() : null},
//                 {"likes_count", post.TryGetProperty("like_count", out var likes) ? likes.GetInt32() : 0},
//                 {"comments_count", post.TryGetProperty("comments_count", out var comments) ? comments.GetInt32() : 0},
//             };
//                     ((List<Dictionary<string, object>>)result["posts"]).Add(postData);
//                 }

//                 // جستجوی صفحه در دیتابیس با استفاده از username
//                 var existPage = Con.Pages.FirstOrDefault(x => x.PageId == username); // جستجو با username

//                 if (existPage == null)
//                 {
//                     return NotFound("پیجی با این نام کاربری وجود ندارد");
//                 }

//                 // به‌روزرسانی اطلاعات صفحه
//                 existPage.Followesrs = (int)result["followers"];
//                 existPage.ShowName = (string)result["name"];
//                 existPage.ImgUrl = (string)result["ProfilePictureUrl"]; // اگر می‌خواهید تصویر پروفایل را ذخیره کنید
//                 existPage.ShowName = (string)result["name"];

//                 // محاسبه انگیجمنت
//                 var totalLikes = 0;
//                 var totalComments = 0;
//                 var postsList = (List<Dictionary<string, object>>)result["posts"];
//                 var recentPosts = postsList.Take(10); // 10 پست اخیر

//                 foreach (var post in recentPosts)
//                 {
//                     totalLikes += (int)post["likes_count"];
//                     totalComments += (int)post["comments_count"];
//                 }

//                 var followersCount = (int)result["followers"];
//                 double engagement = (followersCount > 0) ? ((totalLikes + totalComments) / (double)followersCount * 10) : 0;

//                 // گرد کردن به دو رقم اعشار
//                 engagement = Math.Round(engagement, 2);

//                 // ذخیره انگیجمنت در مدل و دیتابیس
//                 existPage.Engagement = engagement;
//                 Con.SaveChanges();

//                 return Ok(new
//                 {
//                     Message = "پیج با موفقیت تغییر یافت",
//                     Page = existPage,
//                     Posts = result["posts"],
//                     Engagement = engagement
//                 });
//             }
//             catch (Exception ex)
//             {
//                 return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
//             }
//         }

//         [HttpGet("[action]")]
//         public async Task<IActionResult> getInstaUser(string username)
//         {

//             await Task.Delay(2000);
            
//             var instagramBusinessId = "17841469689053529"; // شناسه بیزینس اینستاگرام
//             var url = $"https://graph.facebook.com/v21.0/{instagramBusinessId}?fields=business_discovery.username({username}){{followers_count,id,username,profile_picture_url,name,media_count,media{{id,media_type,timestamp,media_url,permalink,thumbnail_url,caption,like_count,comments_count}}}}&access_token={_accessToken}";
//             var response = await _httpClient.GetAsync(url);

//             // بررسی وضعیت پاسخ
//             if (!response.IsSuccessStatusCode)
//             {
//                 var errorContent = await response.Content.ReadAsStringAsync();
//                 return BadRequest(new { Error = response.StatusCode, Message = errorContent });
//             }

//             // خواندن داده‌های JSON
//             var jsonString = await response.Content.ReadAsStringAsync();
//             var data = JsonDocument.Parse(jsonString).RootElement.GetProperty("business_discovery");

//             // ایجاد Dictionary برای نتایج
//             var result = new Dictionary<string, object>
//     {
//         {"id", data.GetProperty("id").GetString()},
//         {"username", data.GetProperty("username").GetString()},
//         {"followers", data.GetProperty("followers_count").GetInt32()},
//         {"ProfilePictureUrl", data.GetProperty("profile_picture_url").GetString()},
//         {"name", data.GetProperty("name").GetString()},
//         {"media_count", data.GetProperty("media_count").GetInt32()},
//         {"posts", new List<Dictionary<string, object>>()},
//     };

//             // استخراج پست‌ها و نمایش id، media_type، timestamp، media_url و permalink
//             foreach (var post in data.GetProperty("media").GetProperty("data").EnumerateArray())
//             {
//                 var postData = new Dictionary<string, object>
//         {
//             {"id", post.GetProperty("id").GetString()},
//             {"media_type", post.GetProperty("media_type").GetString()},
//             {"timestamp", post.GetProperty("timestamp").GetString()},
//             {"media_url", post.GetProperty("media_url").GetString()},
//             {"permalink", post.GetProperty("permalink").GetString()},
//             {"thumbnail_url", post.TryGetProperty("thumbnail_url", out var thumbnail) ? thumbnail.GetString() : null},
//             {"caption", post.TryGetProperty("caption", out var caption) ? caption.GetString() : null},
//             {"likes_count", post.TryGetProperty("like_count", out var likes) ? likes.GetInt32() : 0},
//             {"comments_count", post.TryGetProperty("comments_count", out var comments) ? comments.GetInt32() : 0},
//         };
//                 ((List<Dictionary<string, object>>)result["posts"]).Add(postData);
//             }

//             // بازگشت پاسخ
//             return Ok(result);
//         }

//     }
// }