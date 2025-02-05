using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using allAPIs.SimoAPI.Models.InstagramView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller.Instagram
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageCotroller : ControllerBase
    {
        private readonly apiContext _context;
        private readonly HttpClient _httpClient;
        private readonly IConfiguration _configuration;
        private readonly ILogger<PageCotroller> _logger;

        public PageCotroller(apiContext context, HttpClient httpClient, IConfiguration configuration, ILogger<PageCotroller> logger)
        {
            _context = context;
            _httpClient = httpClient;
            _configuration = configuration;
            _logger = logger;
        }

        // [HttpPost("create")]
        // public async Task<IActionResult> CreatePage([FromBody] CreateInstagramPageDto pageDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(new { StatusCode = 400, Message = "اطلاعات صفحه نامعتبر است." });
        //     }

        //     try
        //     {
        //         // دریافت اطلاعات صفحه از API اینستاگرام
        //         var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
        //         var instagramBusinessId = _configuration.GetValue<string>("Instagram:BusinessId");
        //         var url = $"https://graph.facebook.com/v21.0/{instagramBusinessId}?fields=business_discovery.username({pageDto.PageId}){{id,username,followers_count,profile_picture_url,name,media_count}}&access_token={accessToken}";

        //         var response = await _httpClient.GetAsync(url);

        //         if (!response.IsSuccessStatusCode)
        //         {
        //             var errorContent = await response.Content.ReadAsStringAsync();
        //             _logger.LogError($"Error from Instagram API: {errorContent}");

        //             // بررسی خطای نام کاربری نامعتبر
        //             if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
        //             {
        //                 var errorResponse = JsonDocument.Parse(errorContent).RootElement.GetProperty("error");
        //                 string errorMessage = errorResponse.GetProperty("error_user_msg").GetString();
        //                 return BadRequest(new { StatusCode = 400, Message = errorMessage });
        //             }

        //             return BadRequest(new { StatusCode = 400, Message = "خطا در دریافت اطلاعات صفحه از اینستاگرام.", Error = errorContent });
        //         }

        //         var content = await response.Content.ReadAsStringAsync();
        //         var jsonResponse = JsonDocument.Parse(content).RootElement.GetProperty("business_discovery");

        //         // استخراج اطلاعات از API
        //         string username = jsonResponse.GetProperty("username").GetString(); // نام کاربری (مثلاً cristiano)
        //         int followers = jsonResponse.GetProperty("followers_count").GetInt32(); // تعداد فالوورها
        //         string profilePictureUrl = jsonResponse.GetProperty("profile_picture_url").GetString(); // URL عکس پروفایل
        //         string name = jsonResponse.GetProperty("name").GetString(); // نام نمایشی پیج

        //         // ذخیره عکس پروفایل
        //         string photoPath = await SaveProfilePicture(profilePictureUrl, username);

        //         // ایجاد مدل صفحه
        //         var page = new PagesModel
        //         {
        //             PageId = username, // نام کاربری (مثلاً cristiano)
        //             ShowName = name, // نام نمایشی پیج (از API اینستاگرام)
        //             PersianName = pageDto.PersianName, // نام فارسی (از کاربر)
        //             Followesrs = followers, // تعداد فالوورها (از API اینستاگرام)
        //             ImgUrl = photoPath, // مسیر عکس ذخیره شده
        //             Description = pageDto.Description, // توضیحات (از کاربر)
        //             TelegramID = pageDto.TelegramID, // آیدی تلگرام (از کاربر)
        //             WhatsappNumber = pageDto.WhatsappNumber, // شماره واتس‌اپ (از کاربر)
        //             sex = pageDto.Sex, // جنسیت (از کاربر)
        //         };

        //         _context.Pages.Add(page);
        //         await _context.SaveChangesAsync();

        //         return Ok(new
        //         {
        //             StatusCode = 200,
        //             Message = "صفحه با موفقیت ایجاد شد.",
        //             PageId = page.Id
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "خطا در ایجاد صفحه اینستاگرام");
        //         return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
        //     }
        // }

        // اضافه کردن دسته بندی و نوع دسته بندی

//         [HttpPost("create")]
// public async Task<IActionResult> CreatePage([FromBody] CreateInstagramPageDto pageDto)
// {
//     if (!ModelState.IsValid)
//     {
//         return BadRequest(new { StatusCode = 400, Message = "اطلاعات صفحه نامعتبر است." });
//     }

//     try
//     {
//         // دریافت اطلاعات صفحه از API اینستاگرام
//         var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
//         var instagramBusinessId = _configuration.GetValue<string>("Instagram:BusinessId");
//         var url = $"https://graph.facebook.com/v21.0/{instagramBusinessId}?fields=business_discovery.username({pageDto.PageId}){{id,username,followers_count,profile_picture_url,name,media_count}}&access_token={accessToken}";

//         var response = await _httpClient.GetAsync(url);

//         if (!response.IsSuccessStatusCode)
//         {
//             var errorContent = await response.Content.ReadAsStringAsync();
//             _logger.LogError($"Error from Instagram API: {errorContent}");

//             if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
//             {
//                 var errorResponse = JsonDocument.Parse(errorContent).RootElement.GetProperty("error");
//                 string errorMessage = errorResponse.GetProperty("error_user_msg").GetString();
//                 return BadRequest(new { StatusCode = 400, Message = errorMessage });
//             }

//             return BadRequest(new { StatusCode = 400, Message = "خطا در دریافت اطلاعات صفحه از اینستاگرام.", Error = errorContent });
//         }

//         var content = await response.Content.ReadAsStringAsync();
//         var jsonResponse = JsonDocument.Parse(content).RootElement.GetProperty("business_discovery");

//         // استخراج اطلاعات از API
//         string username = jsonResponse.GetProperty("username").GetString();
//         int followers = jsonResponse.GetProperty("followers_count").GetInt32();
//         string profilePictureUrl = jsonResponse.GetProperty("profile_picture_url").GetString();
//         string name = jsonResponse.GetProperty("name").GetString();

//         // ذخیره عکس پروفایل
//         string photoPath = await SaveProfilePicture(profilePictureUrl, username);

//         // پیدا کردن PageTypeCategory بر اساس نام
//         var pageTypeCategory = await _context.pageTypeCategories
//             .FirstOrDefaultAsync(c => c.CategoryName == pageDto.PageTypeCategoryName);

//         if (pageTypeCategory == null)
//         {
//             return BadRequest(new { StatusCode = 400, Message = "دسته‌بندی مورد نظر یافت نشد." });
//         }

//         // پیدا کردن PageType بر اساس نام
//         var pageType = await _context.pageTypes
//             .FirstOrDefaultAsync(t => t.Name == pageDto.PageTypeName);

//         if (pageType == null)
//         {
//             return BadRequest(new { StatusCode = 400, Message = "نوع دسته‌بندی مورد نظر یافت نشد." });
//         }

//         // ایجاد مدل صفحه
//         var page = new PagesModel
//         {
//             PageId = username,
//             ShowName = name,
//             PersianName = pageDto.PersianName,
//             Followesrs = followers,
//             ImgUrl = photoPath,
//             Description = pageDto.Description,
//             TelegramID = pageDto.TelegramID,
//             WhatsappNumber = pageDto.WhatsappNumber,
//             sex = pageDto.Sex,

//             // اضافه کردن دسته‌بندی و نوع دسته‌بندی
//             PageTypeCategoryId = pageTypeCategory.Id,
//             PageTypeCategory = pageTypeCategory,
//             PageTypeId = pageType.Id,
//             PageType = pageType
//         };

//         _context.Pages.Add(page);
//         await _context.SaveChangesAsync();

//         return Ok(new
//         {
//             StatusCode = 200,
//             Message = "صفحه با موفقیت ایجاد شد.",
//             PageId = page.Id
//         });
//     }
//     catch (Exception ex)
//     {
//         _logger.LogError(ex, "خطا در ایجاد صفحه اینستاگرام");
//         return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
//     }
// }

// اضافه کردن تعرفه ها

[HttpPost("create")]
public async Task<IActionResult> CreatePage([FromBody] CreateInstagramPageDto pageDto)
{
    if (!ModelState.IsValid)
    {
        return BadRequest(new { StatusCode = 400, Message = "اطلاعات صفحه نامعتبر است." });
    }

    try
    {
        // دریافت اطلاعات صفحه از API اینستاگرام
        var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
        var instagramBusinessId = _configuration.GetValue<string>("Instagram:BusinessId");
        var url = $"https://graph.facebook.com/v21.0/{instagramBusinessId}?fields=business_discovery.username({pageDto.PageId}){{id,username,followers_count,profile_picture_url,name,media_count}}&access_token={accessToken}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Error from Instagram API: {errorContent}");

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                var errorResponse = JsonDocument.Parse(errorContent).RootElement.GetProperty("error");
                string errorMessage = errorResponse.GetProperty("error_user_msg").GetString();
                return BadRequest(new { StatusCode = 400, Message = errorMessage });
            }

            return BadRequest(new { StatusCode = 400, Message = "خطا در دریافت اطلاعات صفحه از اینستاگرام.", Error = errorContent });
        }

        var content = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonDocument.Parse(content).RootElement.GetProperty("business_discovery");

        // استخراج اطلاعات از API
        string username = jsonResponse.GetProperty("username").GetString();
        int followers = jsonResponse.GetProperty("followers_count").GetInt32();
        string profilePictureUrl = jsonResponse.GetProperty("profile_picture_url").GetString();
        string name = jsonResponse.GetProperty("name").GetString();

        // ذخیره عکس پروفایل
        string photoPath = await SaveProfilePicture(profilePictureUrl, username);

        // پیدا کردن PageTypeCategory بر اساس نام
        var pageTypeCategory = await _context.pageTypeCategories
            .FirstOrDefaultAsync(c => c.CategoryName == pageDto.PageTypeCategoryName);

        if (pageTypeCategory == null)
        {
            return BadRequest(new { StatusCode = 400, Message = "دسته‌بندی مورد نظر یافت نشد." });
        }

        // پیدا کردن PageType بر اساس نام
        var pageType = await _context.pageTypes
            .FirstOrDefaultAsync(t => t.Name == pageDto.PageTypeName);

        if (pageType == null)
        {
            return BadRequest(new { StatusCode = 400, Message = "نوع دسته‌بندی مورد نظر یافت نشد." });
        }

        // ایجاد مدل صفحه
        var page = new PagesModel
        {
            PageId = username,
            ShowName = name,
            PersianName = pageDto.PersianName,
            Followesrs = followers,
            ImgUrl = photoPath,
            Description = pageDto.Description,
            TelegramID = pageDto.TelegramID,
            WhatsappNumber = pageDto.WhatsappNumber,
            sex = pageDto.Sex,

            // اضافه کردن دسته‌بندی و نوع دسته‌بندی
            PageTypeCategoryId = pageTypeCategory.Id,
            PageTypeCategory = pageTypeCategory,
            PageTypeId = pageType.Id,
            PageType = pageType
        };

        _context.Pages.Add(page);
        await _context.SaveChangesAsync();

        // دریافت تعرفه‌های مرتبط با این پیج
        var tariffs = await _context.PageTariffModels
            .Where(t => t.PageId == page.Id)
            .Include(t => t.Tariff)
            .Select(t => new
            {
                t.TariffId,
                t.Tariff.Name,
                t.Price
            })
            .ToListAsync();

        return Ok(new
        {
            StatusCode = 200,
            Message = "صفحه با موفقیت ایجاد شد.",
            PageId = page.Id,
            Tariffs = tariffs
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "خطا در ایجاد صفحه اینستاگرام");
        return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
    }
}



        
        // [HttpGet("all")]
        // public IActionResult GetAllPages()
        // {
        //     try
        //     {
        //         var pages = _context.Pages.ToList();
        //         var result = pages.Select(page => new
        //         {
        //             page.Id,
        //             page.PageId,
        //             page.ShowName,
        //             page.PersianName,
        //             page.Followesrs,
        //             page.ImgUrl,
        //             page.Description,
        //             page.TelegramID,
        //             page.WhatsappNumber,
        //             page.sex,
        //             page.PageTypeId, // اضافه شده
        //             page.PageTypeCategoryId // اضافه شده
        //         }).ToList();

        //         return Ok(new
        //         {
        //             StatusCode = 200,
        //             Pages = result
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "خطا در دریافت همه پیج‌ها");
        //         return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
        //     }
        // }

        // اضافه کردن تعرفه ها
        [HttpGet("all")]
public IActionResult GetAllPages()
{
    try
    {
        var pages = _context.Pages
            .Include(p => p.PageTariffModels) // شامل کردن تعرفه‌های پیج
            .ThenInclude(pt => pt.Tariff) // شامل کردن جزئیات تعرفه
            .ToList();

        var result = pages.Select(page => new
        {
            page.Id,
            page.PageId,
            page.ShowName,
            page.PersianName,
            page.Followesrs,
            page.ImgUrl,
            page.Description,
            page.TelegramID,
            page.WhatsappNumber,
            page.sex,
            page.PageTypeId,
            page.PageTypeCategoryId,

            // اضافه کردن تعرفه‌ها
            Tariffs = page.PageTariffModels.Select(tariff => new
            {
                tariff.TariffId,
                tariff.Tariff.Name,
                tariff.Price
            }).ToList()
        }).ToList();

        return Ok(new
        {
            StatusCode = 200,
            Pages = result
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "خطا در دریافت همه پیج‌ها");
        return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
    }
}


      [HttpGet("{id}")]
public async Task<IActionResult> GetPageById(int id)
{
    try
    {
        var page = await _context.Pages
            .Include(p => p.PageTypeCategory)
            .Include(p => p.PageType)
            .FirstOrDefaultAsync(p => p.Id == id);

        if (page == null)
        {
            return NotFound(new { StatusCode = 404, Message = "صفحه مورد نظر پیدا نشد." });
        }

        // دریافت تعرفه‌های مرتبط با این صفحه
        var tariffs = await _context.PageTariffModels
            .Where(t => t.PageId == page.Id)
            .Include(t => t.Tariff)
            .Select(t => new
            {
                t.TariffId,
                t.Tariff.Name,
                t.Price
            })
            .ToListAsync();

        return Ok(new
        {
            StatusCode = 200,
            Page = new
            {
                page.Id,
                page.PageId,
                page.ShowName,
                page.PersianName,
                page.Followesrs,
                page.ImgUrl,
                page.Description,
                page.TelegramID,
                page.WhatsappNumber,
                page.sex,

                // اضافه کردن دسته‌بندی و نوع دسته‌بندی
                PageTypeCategory = page.PageTypeCategory != null ? new
                {
                    page.PageTypeCategory.Id,
                    page.PageTypeCategory.CategoryName
                } : null,

                PageType = page.PageType != null ? new
                {
                    page.PageType.Id,
                    page.PageType.Name
                } : null,

                // اضافه کردن تعرفه‌ها
                Tariffs = tariffs
            }
        });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "خطا در دریافت پیج با آیدی");
        return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
    }
}



        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeletePage(int id)
        {
            var page = await _context.Pages.FindAsync(id);
            if (page == null)
            {
                return NotFound(new { StatusCode = 404, Message = "صفحه مورد نظر پیدا نشد." });
            }

            // حذف عکس پروفایل
            DeleteProfilePicture(page.ImgUrl);

            _context.Pages.Remove(page);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Message = "صفحه با موفقیت حذف شد." });
        }


//         [HttpPost("updatewithapi/{id}")]
// public async Task<IActionResult> UpdatePageWithApi(int id)
// {
//     var page = await _context.Pages.FindAsync(id);
//     if (page == null)
//     {
//         return NotFound(new { StatusCode = 404, Message = "صفحه مورد نظر پیدا نشد." });
//     }

//     try
//     {
//         // دریافت اطلاعات جدید از API اینستاگرام
//         var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
//         var instagramBusinessId = _configuration.GetValue<string>("Instagram:BusinessId");
//         var url = $"https://graph.facebook.com/v21.0/{instagramBusinessId}?fields=business_discovery.username({page.PageId}){{id,username,followers_count,profile_picture_url,name,media_count}}&access_token={accessToken}";

//         var response = await _httpClient.GetAsync(url);

//         if (!response.IsSuccessStatusCode)
//         {
//             var errorContent = await response.Content.ReadAsStringAsync();
//             _logger.LogError($"Error from Instagram API: {errorContent}");
//             return BadRequest(new { StatusCode = 400, Message = "خطا در دریافت اطلاعات صفحه از اینستاگرام.", Error = errorContent });
//         }

//         var content = await response.Content.ReadAsStringAsync();
//         var jsonResponse = JsonDocument.Parse(content).RootElement.GetProperty("business_discovery");

//         // استخراج اطلاعات جدید از API
//         string pageId = jsonResponse.GetProperty("id").GetString();  // شناسه صفحه اینستاگرام
//         string username = jsonResponse.GetProperty("username").GetString();
//         int followers = jsonResponse.GetProperty("followers_count").GetInt32();
//         string profilePictureUrl = jsonResponse.GetProperty("profile_picture_url").GetString();
//         string name = jsonResponse.GetProperty("name").GetString();

//         // به‌روزرسانی فیلدهای صفحه
//         page.PageId = pageId;  // به‌روزرسانی شناسه صفحه
//         page.ShowName = name;  // به‌روزرسانی نام
//         page.Followesrs = followers;
//         page.ImgUrl = await UpdateProfilePicture(profilePictureUrl, page.ImgUrl);  // به‌روزرسانی عکس پروفایل

//         _context.Pages.Update(page);
//         await _context.SaveChangesAsync();

//         return Ok(new { StatusCode = 200, Message = "صفحه با موفقیت به‌روزرسانی شد." });
//     }
//     catch (Exception ex)
//     {
//         _logger.LogError(ex, "خطا در به‌روزرسانی صفحه اینستاگرام");
//         return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
//     }
// }

// درست کردن مشکل پیج ایدی 

[HttpPost("updatewithapi/{id}")]
public async Task<IActionResult> UpdatePageWithApi(int id)
{
    var page = await _context.Pages.FindAsync(id);
    if (page == null)
    {
        return NotFound(new { StatusCode = 404, Message = "صفحه مورد نظر پیدا نشد." });
    }

    try
    {
        // دریافت اطلاعات جدید از API اینستاگرام
        var accessToken = _configuration.GetValue<string>("Instagram:AccessToken");
        var instagramBusinessId = _configuration.GetValue<string>("Instagram:BusinessId");
        var url = $"https://graph.facebook.com/v21.0/{instagramBusinessId}?fields=business_discovery.username({page.PageId}){{username,followers_count,profile_picture_url,name}}&access_token={accessToken}";

        var response = await _httpClient.GetAsync(url);

        if (!response.IsSuccessStatusCode)
        {
            var errorContent = await response.Content.ReadAsStringAsync();
            _logger.LogError($"Error from Instagram API: {errorContent}");
            return BadRequest(new { StatusCode = 400, Message = "خطا در دریافت اطلاعات صفحه از اینستاگرام.", Error = errorContent });
        }

        var content = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonDocument.Parse(content).RootElement.GetProperty("business_discovery");

        // استخراج اطلاعات مورد نیاز از API
        string username = jsonResponse.GetProperty("username").GetString();  // username به جای PageId
        int followers = jsonResponse.GetProperty("followers_count").GetInt32();
        string profilePictureUrl = jsonResponse.GetProperty("profile_picture_url").GetString();
        string name = jsonResponse.GetProperty("name").GetString();

        // به‌روزرسانی فیلدهای صفحه
        page.PageId = username;  // تغییر PageId به username
        page.ShowName = name;
        page.Followesrs = followers;
        page.ImgUrl = await UpdateProfilePicture(profilePictureUrl, page.ImgUrl);  // به‌روزرسانی عکس پروفایل

        _context.Pages.Update(page);
        await _context.SaveChangesAsync();

        return Ok(new { StatusCode = 200, Message = "صفحه با موفقیت به‌روزرسانی شد." });
    }
    catch (Exception ex)
    {
        _logger.LogError(ex, "خطا در به‌روزرسانی صفحه اینستاگرام");
        return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
    }
}


        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdatePage(int id, [FromBody] UpdateInstagramPageDto pageDto)
        {
            try
            {
                // پیدا کردن صفحه مورد نظر
                var page = await _context.Pages.FindAsync(id);
                if (page == null)
                {
                    return NotFound(new { StatusCode = 404, Message = "صفحه مورد نظر پیدا نشد." });
                }

                // به‌روزرسانی فیلدها (فقط فیلدهایی که مقدار دارند)
                if (!string.IsNullOrEmpty(pageDto.PersianName))
                    page.PersianName = pageDto.PersianName;

                if (!string.IsNullOrEmpty(pageDto.Description))
                    page.Description = pageDto.Description;

                if (!string.IsNullOrEmpty(pageDto.TelegramID))
                    page.TelegramID = pageDto.TelegramID;

                if (!string.IsNullOrEmpty(pageDto.WhatsappNumber))
                    page.WhatsappNumber = pageDto.WhatsappNumber;

                if (!string.IsNullOrEmpty(pageDto.Sex))
                    page.sex = pageDto.Sex;

                if (pageDto.PageTypeId.HasValue) // اضافه شده
                    page.PageTypeId = pageDto.PageTypeId;

                if (pageDto.PageTypeCategoryId.HasValue) // اضافه شده
                    page.PageTypeCategoryId = pageDto.PageTypeCategoryId;


                // ذخیره تغییرات در دیتابیس
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "صفحه با موفقیت به‌روزرسانی شد.",
                    Page = new
                    {
                        page.Id,
                        page.PageId,
                        page.ShowName,
                        page.PersianName,
                        page.Followesrs,
                        page.ImgUrl,
                        page.Description,
                        page.TelegramID,
                        page.WhatsappNumber,
                        page.sex,
                        page.PageTypeId, // اضافه شده
                        page.PageTypeCategoryId // اضافه شده
                    }
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در به‌روزرسانی صفحه اینستاگرام");
                return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور." });
            }
        }

        // helper method

        private void DeleteProfilePicture(string photoPath)
        {
            try
            {
                if (!string.IsNullOrEmpty(photoPath))
                {
                    string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", photoPath);
                    if (System.IO.File.Exists(fullPath))
                    {
                        System.IO.File.Delete(fullPath);
                    }
                }
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در حذف عکس پروفایل");
            }
        }

        private async Task<string> UpdateProfilePicture(string profilePictureUrl, string existingPhotoPath)
        {
            try
            {
                // حذف عکس قبلی (اگر وجود دارد)
                if (!string.IsNullOrEmpty(existingPhotoPath))
                {
                    string oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", existingPhotoPath);
                    if (System.IO.File.Exists(oldPhotoPath))
                    {
                        System.IO.File.Delete(oldPhotoPath);
                    }
                }

                // ذخیره عکس جدید
                return await SaveProfilePicture(profilePictureUrl, Guid.NewGuid().ToString());
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در به‌روزرسانی عکس پروفایل");
                return null;
            }
        }

        private async Task<string> SaveProfilePicture(string profilePictureUrl, string pageId)
        {
            try
            {
                // دانلود عکس از URL
                byte[] imageBytes = await _httpClient.GetByteArrayAsync(profilePictureUrl);

                // مسیر ذخیره عکس
                string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "profilePictures");
                if (!Directory.Exists(saveDirectory))
                {
                    Directory.CreateDirectory(saveDirectory);
                }

                // نام فایل
                string fileName = $"{pageId}_{Guid.NewGuid()}.jpg";
                string filePath = Path.Combine(saveDirectory, fileName);

                // ذخیره عکس
                await System.IO.File.WriteAllBytesAsync(filePath, imageBytes);

                // بازگرداندن مسیر نسبی
                return Path.Combine("profilePictures", fileName);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "خطا در ذخیره عکس پروفایل");
                return null;
            }
        }
    }
}