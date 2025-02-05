// using System.Text.Json;
// using System.Text.Json.Serialization;
// using allAPIs.SimoAPI.Models;
// using allAPIs.SimoAPI.Models.pageView;
// using Microsoft.AspNetCore.Mvc;
// using Microsoft.EntityFrameworkCore;

// namespace allAPIs.SimoAPI.Controller
// {
//     [ApiController]
//     [Route("api/[controller]")]
//     public class PagesController : ControllerBase
//     {
//         public PagesController(apiContext context, IHttpContextAccessor httpContextAccessor, ILogger<PagesController> logger)
//         {
//             Con = context;
//             Con.httpContext = httpContextAccessor.HttpContext;
//             _logger = logger;
//         }
//         public apiContext Con { get; }
//         private readonly ILogger<PagesController> _logger;

//         [HttpPost("[action]")]
//         public async Task<IActionResult> CreatePage2(pageView pageCreateDto)
//         {
//             // 1. اعتبارسنجی اطلاعات ورودی
//             if (!ModelState.IsValid)
//             {
//                 return BadRequest(ModelState);
//             }

//             // 2. بررسی وجود پیج تکراری
//     var existingPage = await Con.Pages.FirstOrDefaultAsync(p => p.PageId == pageCreateDto.PageId);
//     if (existingPage != null)
//     {
//         return Conflict("این پیج از قبل وجود دارد."); // یا می‌توانید از NotFound استفاده کنید
//     }

//             // 2. ساخت پیج جدید
//             var page = new PagesModel
//             {
//                 PageId = pageCreateDto.PageId,
//                 Description = pageCreateDto.Description,
//                 PageTypeId = pageCreateDto.PageTypeId,
//                 PageCategoryId = pageCreateDto.PageCategoryId,
//                 sex = pageCreateDto.Sex,
//                 PersianName = pageCreateDto.PersianName,
//                 TelegramID = pageCreateDto.TelegramID,
//                 WhatsappNumber = pageCreateDto.WhatsappNumber
//             };

//             // 3. ذخیره پیج جدید در دیتابیس
//             Con.Pages.Add(page);
//             await Con.SaveChangesAsync(); // ذخیره پیج برای دریافت شناسه آن

//             // 4. تولید تگ‌ها
//             var tags = GenerateTags(page.PersianName, page.PageId); // حالا تگ‌ها به صورت TagModel هستند

//             // 5. ذخیره تگ‌ها و ایجاد ارتباط با پیج
//             foreach (var tag in tags)
//             {
//                 // بررسی وجود تگ در دیتابیس
//                 var existingTag = await Con.Tags.FirstOrDefaultAsync(t => t.Name == tag.Name);
//                 if (existingTag == null)
//                 {
//                     // اگر تگ وجود ندارد، آن را ایجاد کنید
//                     existingTag = new TagModel { Name = tag.Name };
//                     Con.Tags.Add(existingTag);
//                     await Con.SaveChangesAsync(); // ذخیره تگ جدید برای دریافت شناسه آن
//                 }

//                 // بررسی وجود ارتباط بین پیج و تگ
//                 var existingPageTag = await Con.PageTags
//                     .FirstOrDefaultAsync(pt => pt.PageId == page.Id && pt.TagId == existingTag.Id);

//                 if (existingPageTag == null)
//                 {
//                     // ایجاد ارتباط بین پیج و تگ
//                     var pageTag = new PageTag
//                     {
//                         PageId = page.Id, // شناسه پیج جدید
//                         TagId = existingTag.Id // شناسه تگ
//                     };
//                     Con.PageTags.Add(pageTag);
//                 }
//             }
//             await Con.SaveChangesAsync(); // ذخیره ارتباطات

//             // 6. ایجاد صفحه HTML
//             await CreateHtmlPage(page, tags);

//             return Ok("پیج جدید با موفقیت ایجاد شد.");
//         }


//         private List<TagModel> GenerateTags(string persianName, string PageId)
//         {
//             var tags = new List<TagModel>();

//             tags.Add(new TagModel { Name = "#تبلیغات_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#instagram_ads" });
//             tags.Add(new TagModel { Name = "#تبلیغ_در_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#تبلیغ_در_پیج_بلاگرها" });
//             tags.Add(new TagModel { Name = "#تبلیغ_پست_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#تبلیغ_استوری" });
//             tags.Add(new TagModel { Name = "#story_ads" });
//             tags.Add(new TagModel { Name = "#تبلیغ_ریلز" });
//             tags.Add(new TagModel { Name = "#همکاری_با_اینفلوئنسر" });
//             tags.Add(new TagModel { Name = "#تبلیغات_هدفمند" });
//             tags.Add(new TagModel { Name = "#تبلیغ_در_پیج_عمومی" });
//             tags.Add(new TagModel { Name = "#تبلیغ_در_پیجهای_عمومی" });
//             tags.Add(new TagModel { Name = "#تبلیغ_پست_و_استوری" });
//             tags.Add(new TagModel { Name = "#تبلیغ_محصول_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#اینفلوئنسر_مارکتینگ" });
//             tags.Add(new TagModel { Name = "#اینفلوئنسر_تبلیغات" });
//             tags.Add(new TagModel { Name = "#تبلیغات_کسب_و_کار" });
//             tags.Add(new TagModel { Name = "#تبلیغات_آنلاین" });
//             tags.Add(new TagModel { Name = "#blogger_ads" });
//             tags.Add(new TagModel { Name = "#پیج_اینستاگرام_تبلیغاتی" });
//             tags.Add(new TagModel { Name = "#بلاگر_ایرانی" });
//             tags.Add(new TagModel { Name = "#تبلیغ_صفحه_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#instagram_page_promotion" });
//             tags.Add(new TagModel { Name = "#تبلیغ_خلاقانه" });
//             tags.Add(new TagModel { Name = "#تبلیغات_24_ساعته" });
//             tags.Add(new TagModel { Name = "#تبلیغات_48_ساعته" });
//             tags.Add(new TagModel { Name = "#کمپین_تبلیغاتی_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#تبلیغ_هدفمند_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#public_page_promotion" });
//             tags.Add(new TagModel { Name = "#targeted_instagram_ads" });
//             tags.Add(new TagModel { Name = "#بازاریابی_در_اینستاگرام" });
//             tags.Add(new TagModel { Name = "#تبلیغ_تخفیف_ویژه" });
//             tags.Add(new TagModel { Name = "#تبلیغ_کالا" });
//             tags.Add(new TagModel { Name = "#بازاریابی_محتوا" });
//             tags.Add(new TagModel { Name = "#پست_و_ریلز" });
//             tags.Add(new TagModel { Name = "#تبلیغات_پابلیک_پیج" });
//             tags.Add(new TagModel { Name = "#تبلیغات_پیج_معروف" });
//             tags.Add(new TagModel { Name = "#بلاگرهای_ایرانی" });
//             tags.Add(new TagModel { Name = "#تبلیغات_موفق" });
//             tags.Add(new TagModel { Name = "#تبلیغات_ارزان" });
//             tags.Add(new TagModel { Name = "#تبلیغات_پربازده" });
//             tags.Add(new TagModel { Name = "#تبلیغات_اینترنتی" });
//             tags.Add(new TagModel { Name = "#تبلیغات_صفحات_پربازدید" });
//             tags.Add(new TagModel { Name = "#اینفلوئنسر_مارکتینگ_ایرانی" });

//             // تگ‌های بر اساس نام صفحه
//             tags.Add(new TagModel { Name = $"#تبلیغات_در_پیج_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_در_پیج_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#همکاری_با_{persianName}" });
//             tags.Add(new TagModel { Name = $"#همکاری_با_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصول_در_پیج_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصول_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_پست_و_استوری_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_پست_و_استوری_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_ویژه_در_پیج_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_ویژه_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_ریلز_در_پیج_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_ریلز_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_در_استوری_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_در_استوری_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصول_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصول_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_24_ساعته_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_24_ساعته_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_48_ساعته_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_48_ساعته_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_استوری_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_استوری_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_پربازده_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_پربازده_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_در_پیج_پابلیک_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_در_پیج_پابلیک_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_ارزان_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_ارزان_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#همکاری_تبلیغاتی_با_{persianName}" });
//             tags.Add(new TagModel { Name = $"#همکاری_تبلیغاتی_با_{PageId}" });
//             tags.Add(new TagModel { Name = $"#پیج_تبلیغاتی_{persianName}" });
//             tags.Add(new TagModel { Name = $"#پیج_تبلیغاتی_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_کسب_و_کار_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_کسب_و_کار_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_هدفمند_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_هدفمند_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_در_پیج_بلاگر_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_در_پیج_بلاگر_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصولات_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصولات_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#بازاریابی_اینستاگرام_با_{persianName}" });
//             tags.Add(new TagModel { Name = $"#بازاریابی_اینستاگرام_با_{PageId}" });
//             tags.Add(new TagModel { Name = $"#کمپین_تبلیغاتی_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#کمپین_تبلیغاتی_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصولات_آرایشی_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغ_محصولات_آرایشی_در_{PageId}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_فروشگاهی_در_{persianName}" });
//             tags.Add(new TagModel { Name = $"#تبلیغات_فروشگاهی_در_{PageId}" });


//             // ... سایر تگ‌های بر اساس نام صفحه

//             return tags;
//         }


//         private async Task CreateHtmlPage(PagesModel page, List<TagModel> tags)
//         {
//             var htmlContent = $"<html><head><title>{page.PersianName}</title></head><body>";
//             htmlContent += $"<h1>{page.PersianName}</h1>";
//             htmlContent += $"<p>{page.Description}</p>";

//             if (tags != null && tags.Any())
//             {
//                 htmlContent += "<h2>Tags:</h2><ul>";
//                 foreach (var tag in tags)
//                 {
//                     htmlContent += $"<li>{tag.Name}</li>"; // استفاده از نام تگ
//                 }
//                 htmlContent += "</ul>";
//             }

//             htmlContent += "</body></html>";

//             // var directoryPath = "path_to_save_html";
//             // تغییر مسیر به پوشه wwwroot
//             var directoryPath = Path.Combine("wwwroot");
//             if (!Directory.Exists(directoryPath))
//             {
//                 Directory.CreateDirectory(directoryPath);
//             }

//             // بررسی اینکه آیا page.PageId برابر با "index" است یا خیر
//             string fileName = page.PageId == "index" ? "default.html" : $"{page.PageId}.html";
//             var filePath = Path.Combine(directoryPath, fileName);
//             await System.IO.File.WriteAllTextAsync(filePath, htmlContent);

//         }


        // [HttpGet("[action]")]
        // public IActionResult GetAllPage()
        // {
        //     try
        //     {
        //         var page = Con.pages.ToList();

        //         // اگر هیچ پیجی وجود نداشت، کد وضعیت 404 (Not Found) را برمی‌گردانیم
        //         if (page.Count == 0)
        //         {
        //             return NotFound("هیچ پیجی وجود ندارد");
        //         }

        //         // اگر پیجی وجود داشته باشد، آنها را به همراه کد وضعیت 200 (OK) برمی‌گردانیم
        //         return Ok(page);

        //     }
        //     catch (DbUpdateException ex)
        //     {

        //         return StatusCode(500, $"database update error : {ex.Message}");
        //     }

        //     catch (Exception ex)
        //     {

        //         return StatusCode(500, $"internal server error : {ex.Message}");
        //     }
        // }
        // [HttpGet("[action]")]
        // public IActionResult GetAllPageAndAllPricePage()
        // {
        //     try
        //     {
        //         // Assuming you have a database context called "Con"
        //         var pages = Con.pages.ToList();
        //         var pricePages = Con.pricePages.ToList(); // Get the price pages from your database

        //         // Check if any pages exist, and if not, return a 404 Not Found response
        //         if (pages.Count == 0)
        //         {
        //             return NotFound("هیچ پیجی وجود ندارد");
        //         }

        //         // Return a 200 OK response with both pages and price pages as a combined object
        //         return Ok(new { Pages = pages, PricePages = pricePages });
        //     }
        //     catch (DbUpdateException ex)
        //     {
        //         return StatusCode(500, $"database update error : {ex.Message}");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"internal server error : {ex.Message}");
        //     }
        // }

        // // test
        // [HttpGet("[action]")]
        // public IActionResult GetAllPageAndAllPricePage2()
        // {
        //     try
        //     {
        //         var pages = Con.pages.Include(p => p.PricePages).ToList(); // بارگذاری صفحات به همراه تعرفه‌ها

        //         // Check if any pages exist, and if not, return a 404 Not Found response
        //         if (pages.Count == 0)
        //         {
        //             return NotFound("هیچ پیجی وجود ندارد");
        //         }

        //         // Return a 200 OK response with both pages and their related price pages
        //         return Ok(pages);
        //     }
        //     catch (DbUpdateException ex)
        //     {
        //         return StatusCode(500, $"database update error : {ex.Message}");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"internal server error : {ex.Message}");
        //     }
        // }


        // // متد بالا فقط یک پیج را برمیگرداند ولی این متد تمامی پیج ها
        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilterPricePage([FromQuery] string tariffName)
        // {
        //     try
        //     {
        //         // 1. دریافت تعرفه از دیتابیس
        //         var tariff = Con.pricePages.FirstOrDefault(t => t.Name == tariffName);

        //         // 2. بررسی وجود تعرفه
        //         if (tariff == null)
        //         {
        //             return NotFound($"تعرفه با نام '{tariffName}' یافت نشد.");
        //         }

        //         // 3. دریافت پیج‌های مرتبط با تعرفه
        //         var pages = Con.pages
        //             .Where(p => p.PricePagesId == tariff.Id) // بر اساس PricePagesId فیلتر می‌کنیم
        //             .ToList();

        //         // 4. بازگرداندن اطلاعات پیج‌ها
        //         return Ok(pages);
        //     }
        //     catch (DbUpdateException ex)
        //     {
        //         return StatusCode(500, $"خطای به روز رسانی دیتابیس: {ex.Message}");
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilterPageName([FromQuery] string pageTypeName)
        // {
        //     try
        //     {
        //         // 1. بررسی وجود نوع پیج 
        //         var pageType = Con.pageTypes.FirstOrDefault(t => t.Name == pageTypeName);

        //         // 2. بررسی وجود نوع پیج 
        //         if (pageType == null)
        //         {
        //             return NotFound($"نوع پیجی با این اسم پیدا نشد");
        //         }

        //         // 3. بررسی وجود پیجی با PageTypeId 
        //         var pageExists = Con.pages.Any(p => p.PageTypeId == pageType.Id);

        //         if (!pageExists)
        //         {
        //             return NotFound($"پیجی با این نوع ({pageTypeName}) پیدا نشد");
        //         }

        //         // 3. دریافت پیج‌هایی با نوع پیج مشخص
        //         var pages = Con.pages
        //             .Where(p => p.PageTypeId == pageType.Id) // فیلتر بر اساس PageTypeId
        //             .ToList();

        //         // 4. بازگرداندن اطلاعات پیج ها
        //         return Ok(pages);
        //     }

        //     catch (DbUpdateException ex)
        //     {
        //         return StatusCode(500, $"خطای به روز رسانی دیتابیس: {ex.Message}");
        //     }

        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // // مانند متد بالا فقط بدون دادن اسم تعرفه

        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilteNormalPrice([FromQuery] int? minNormalPrice, [FromQuery] int? maxNormalPrice)
        // {
        //     try
        //     {
        //         // 1. Check if minNormalPrice and maxNormalPrice are valid
        //         if (!minNormalPrice.HasValue || !maxNormalPrice.HasValue)
        //         {
        //             return BadRequest("حداقل و حداکثر قیمت نرمال باید وارد شوند.");
        //         }

        //         // 2. Retrieve pages based on the normal price range
        //         var pages = Con.pages
        //             .Join(Con.pricePages, // Join with PricePageModel
        //                    p => p.PricePagesId, // Field from PagesModel
        //                    pp => pp.Id, // Field from PricePageModel
        //                    (p, pp) => new { Page = p, PricePage = pp }) // Create a new object
        //             .Where(q => q.PricePage.Normalprice >= minNormalPrice && q.PricePage.Normalprice <= maxNormalPrice) // Filter based on Normalprice
        //             .Select(q => q.Page) // Select only the Page field
        //             .ToList();

        //         // 3. Check if any pages were found
        //         if (pages.Count == 0)
        //         {
        //             return NotFound("پیجی با این قیمت پیدا نشد");
        //         }

        //         // 4. Return the filtered pages
        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilteSpecialPrice([FromQuery] int? minSpecialPrice, [FromQuery] int? maxSpecialPrice)
        // {
        //     try
        //     {
        //         // 1. Check if minNormalPrice and maxNormalPrice are valid
        //         if (!minSpecialPrice.HasValue || !maxSpecialPrice.HasValue)
        //         {
        //             return BadRequest("حداقل و حداکثر قیمت نرمال باید وارد شوند.");
        //         }

        //         // 2. Retrieve pages based on the normal price range
        //         var pages = Con.pages
        //             .Join(Con.pricePages, // Join with PricePageModel
        //                    p => p.PricePagesId, // Field from PagesModel
        //                    pp => pp.Id, // Field from PricePageModel
        //                    (p, pp) => new { Page = p, PricePage = pp }) // Create a new object
        //             .Where(q => q.PricePage.SpecialPrice >= minSpecialPrice && q.PricePage.Normalprice <= maxSpecialPrice) // Filter based on Normalprice
        //             .Select(q => q.Page) // Select only the Page field
        //             .ToList();

        //         // 3. Check if any pages were found
        //         if (pages.Count == 0)
        //         {
        //             return NotFound("پیجی با این قیمت پیدا نشد");
        //         }

        //         // 4. Return the filtered pages
        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilterHamkarPrice([FromQuery] int? minHamkarPrice, [FromQuery] int? maxHamkarPrice)
        // {
        //     try
        //     {
        //         // 1. Check if minNormalPrice and maxNormalPrice are valid
        //         if (!minHamkarPrice.HasValue || !maxHamkarPrice.HasValue)
        //         {
        //             return BadRequest("حداقل و حداکثر قیمت نرمال باید وارد شوند.");
        //         }

        //         // 2. Retrieve pages based on the normal price range
        //         var pages = Con.pages
        //             .Join(Con.pricePages, // Join with PricePageModel
        //                    p => p.PricePagesId, // Field from PagesModel
        //                    pp => pp.Id, // Field from PricePageModel
        //                    (p, pp) => new { Page = p, PricePage = pp }) // Create a new object
        //             .Where(q => q.PricePage.HamkarPrice >= minHamkarPrice && q.PricePage.Normalprice <= maxHamkarPrice) // Filter based on Normalprice
        //             .Select(q => q.Page) // Select only the Page field
        //             .ToList();

        //         // 3. Check if any pages were found
        //         if (pages.Count == 0)
        //         {
        //             return NotFound("پیجی با این قیمت پیدا نشد");
        //         }

        //         // 4. Return the filtered pages
        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }


        // // بدون نام تعرفه 

        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilterAllPricePage([FromQuery] int? minPrice, [FromQuery] int? maxPrice)
        // {
        //     try
        //     {
        //         // 1. Check if minPrice and maxPrice are valid
        //         if (!minPrice.HasValue || !maxPrice.HasValue)
        //         {
        //             return BadRequest("حداقل و حداکثر قیمت باید وارد شوند.");
        //         }

        //         // 2. Retrieve pages based on the price range
        //         var pages = Con.pages
        //             .Join(Con.pricePages,
        //                    p => p.PricePagesId,
        //                    pp => pp.Id,
        //                    (p, pp) => new { Page = p, PricePage = pp })
        //             .Where(q =>
        //                 q.PricePage.Normalprice >= minPrice && q.PricePage.Normalprice <= maxPrice &&
        //                 q.PricePage.HamkarPrice >= minPrice && q.PricePage.HamkarPrice <= maxPrice &&
        //                 q.PricePage.SpecialPrice >= minPrice && q.PricePage.SpecialPrice <= maxPrice
        //             )
        //             .Select(q => q.Page)
        //             .ToList();

        //         // 3. Check if any pages were found
        //         if (pages.Count == 0)
        //         {
        //             return NotFound("پیجی با این قیمت ها پیدا نشد");
        //         }

        //         // 4. Return the filtered pages
        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpGet("[action]")]
        // public IActionResult GetPagesByFilterType(string pageTypeName)
        // {
        //     try
        //     {
        //         // 1. پیدا کردن نوع پیج
        //         var pageType = Con.pageTypes.FirstOrDefault(t => t.Name == pageTypeName);

        //         // 2. بررسی وجود نوع پیج
        //         if (pageType == null)
        //         {
        //             return NotFound($"نوع پیجی با این اسم پیدا نشد");
        //         }

        //         // 3. فیلتر کردن پیج ها بر اساس PageTypeId
        //         var pages = Con.pages
        //             .Where(p => p.PageTypeId == pageType.Id) // فیلتر بر اساس PageTypeId
        //             .ToList();

        //         // 4. بازگرداندن اطلاعات پیج ها
        //         if (pages.Count == 0)
        //         {
        //             return NotFound($"پیجی با این نوع ({pageTypeName}) پیدا نشد");
        //         }

        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpGet("[action]")]
        // public IActionResult GetPageByFilterCategory(string pageCategoryName)
        // {
        //     try
        //     {
        //         var pageTypeCategory = Con.pageTypeCategories.FirstOrDefault(t => t.CategoryName == pageCategoryName);

        //         if (pageTypeCategory == null)
        //         {
        //             return NotFound($"نوع دسته بندی پیجی با این اسم پیدا نشد");
        //         }

        //         var pages = Con.pages
        //             .Where(p => p.PageCategoryId == pageTypeCategory.Id)
        //             .ToList();

        //         // 4. بازگرداندن اطلاعات پیج ها
        //         if (pages.Count == 0)
        //         {
        //             return NotFound($"پیجی با این نوع ({pageCategoryName}) پیدا نشد");
        //         }

        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpPost("[action]")]
        // public IActionResult AddPriceToPage(int pageId, int pricePageId)
        // {
        //     try
        //     {
        //         // 1. پیدا کردن پیج
        //         var page = Con.pages.Include(p => p.PricePages).FirstOrDefault(p => p.Id == pageId);

        //         // 2. بررسی وجود پیج
        //         if (page == null)
        //         {
        //             return NotFound($"پیجی با این شناسه ({pageId}) پیدا نشد");
        //         }

        //         // 3. پیدا کردن تعرفه
        //         var pricePage = Con.pricePages.FirstOrDefault(p => p.Id == pricePageId);

        //         // 4. بررسی وجود تعرفه
        //         if (pricePage == null)
        //         {
        //             return NotFound($"تعرفه ای با این شناسه ({pricePageId}) پیدا نشد");
        //         }

        //         // 5. اضافه کردن تعرفه به پیج
        //         page.PricePages.Add(pricePage); // اضافه کردن تعرفه به لیست تعرفه‌ها

        //         // 6. ذخیره تغییرات در دیتابیس
        //         Con.SaveChanges();

        //         return Ok(new
        //         {
        //             Message = $"تعرفه با موفقیت به پیج با شناسه ({pageId}) اضافه شد",
        //             Page = page,
        //             Price = pricePage
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }


        // [HttpPost("[action]")]
        // public async Task<IActionResult> UpdatePageContent(int campaignId, int pageId, [FromBody] PageDataView updatedPage)
        // {
        //     try
        //     {
        //         // 1. یافتن پیج مورد نظر در کمپین
        //         var campaignPage = await Con.CampaignPages
        //             .Where(cp => cp.CampaignId == campaignId && cp.PageId == pageId)
        //             .Include(cp => cp.Page)
        //             .FirstOrDefaultAsync();

        //         // 2. بررسی وجود پیج و کمپین
        //         if (campaignPage == null)
        //         {
        //             return NotFound($"پیجی با شناسه {pageId} در کمپین {campaignId} یافت نشد.");
        //         }

        //         // 3. به‌روزرسانی اطلاعات پیج
        //         campaignPage.Page.Description = updatedPage.Description;
        //         campaignPage.Page.PostLikes = updatedPage.PostLikes;
        //         campaignPage.Page.PostLink = updatedPage.PostLink;
        //         campaignPage.Page.postComments = updatedPage.postComments;
        //         campaignPage.Page.PostViews = updatedPage.PostViews;
        //         campaignPage.Page.PostImpertion = updatedPage.PostImpertion;
        //         campaignPage.Page.StoryViews = updatedPage.StoryViews;
        //         campaignPage.Page.StoryImpertion = updatedPage.StoryImpertion;
        //         campaignPage.Page.sex = updatedPage.sex;
        //         campaignPage.Page.PersianName = updatedPage.PersianName;

        //         // 4. ذخیره نسخه جدید از اطلاعات پیج در جدول `PageVersion`
        //         var pageVersion = new PageVersion
        //         {
        //             PageId = pageId,
        //             CampaignId = campaignId,
        //             Description = campaignPage.Page.Description,
        //             PostLikes = campaignPage.Page.PostLikes,
        //             PostLink = campaignPage.Page.PostLink,
        //             postComments = campaignPage.Page.postComments,
        //             PostViews = campaignPage.Page.PostViews,
        //             PostImpertion = campaignPage.Page.PostImpertion,
        //             StoryViews = campaignPage.Page.StoryViews,
        //             StoryImpertion = campaignPage.Page.StoryImpertion,
        //             CreatedDateTime = DateTime.Now,
        //             sex = campaignPage.Page.sex,
        //             PersianName = campaignPage.Page.PersianName,

        //         };

        //         Con.pageVersions.Add(pageVersion);

        //         // 5. به‌روزرسانی زمان آخرین به‌روزرسانی در جدول `CampaignPages`
        //         campaignPage.LastUpdatedTime = DateTime.Now;

        //         // 6. ذخیره تغییرات در پایگاه داده
        //         await Con.SaveChangesAsync();

        //         return Ok($"اطلاعات پیج {pageId} در کمپین {campaignId} با موفقیت به‌روزرسانی شد.");
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "خطای داخلی در متد UpdatePageContent: {pageId}, {campaignId}", pageId, campaignId);
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpPost("[action]")]
        // public IActionResult EditPageName(int PageID, string NewPageId)
        // {
        //     try
        //     {
        //         var ExistPage = Con.pages.FirstOrDefault(x => x.Id == PageID);

        //         if (ExistPage == null)
        //         {
        //             return NotFound("پیجی با شناسه وارد شده وجود ندارد ");
        //         }

        //         var UniqPageNme = Con.pages.Any(x => x.PageId == NewPageId);

        //         if (UniqPageNme)
        //         {
        //             return BadRequest("پیجی با این نام از قبل وجود دارد ");
        //         }

        //         ExistPage.PageId = NewPageId;

        //         Con.SaveChanges();

        //         return Ok(new
        //         {
        //             Message = "نام پیج با موفقیت ویرایش شد",
        //             Page = ExistPage
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpPost("[action]")]
        // public IActionResult EditPageEngagement(int Engagement, int pageID)
        // {
        //     try
        //     {
        //         // پیدا کردن پیج با شناسه وارد شده
        //         var existingPage = Con.pages.FirstOrDefault(x => x.Id == pageID);

        //         if (existingPage == null)
        //         {
        //             return NotFound("پیجی با شناسه وارد شده وجود ندارد");
        //         }

        //         // به‌روزرسانی مقادیر موجودیت پیج با توجه به داده‌های ارسالی
        //         existingPage.Engagement = Engagement;
        //         // ذخیره تغییرات در پایگاه داده
        //         Con.SaveChanges();

        //         return Ok(new
        //         {
        //             Message = "اطلاعات نرخ مشارکت پیج با موفقیت به‌روزرسانی شد.",
        //             Page = existingPage
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // // edit Show name

        // [HttpPost("[action]")]
        // public IActionResult EditShowName(int pageID, string showName)
        // {
        //     try
        //     {
        //         var ExistPage = Con.pages.FirstOrDefault(x => x.Id == pageID);

        //         if (ExistPage == null)
        //         {
        //             return NotFound("پیجی با این شناسه وجود ندارد ");
        //         }

        //         ExistPage.ShowName = showName;

        //         Con.SaveChanges();

        //         return Ok(new
        //         {
        //             Message = "اسم نمایشی پیج با موفقیت تغییر یافت",
        //             Page = ExistPage
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }



        // [HttpGet("[action]")]
        // public IActionResult GetPageByPageId(int pageID)
        // {

        //     try
        //     {
        //         var existPage = Con.pages.FirstOrDefault(c => c.Id == pageID);

        //         if (existPage == null)
        //         {
        //             return NotFound("پیجی با این شناسه وجود ندارد");
        //         }

        //         return Ok(existPage);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }

        // }

        // [HttpGet("[action]")]
        // public async Task<IActionResult> GetPagesByCampaignId(int campaignId)
        // {
        //     try
        //     {
        //         // 1. بررسی وجود کمپین
        //         var campaign = await Con.campaigns.FindAsync(campaignId);
        //         if (campaign == null)
        //         {
        //             return NotFound("کمپینی با این شناسه وجود ندارد.");
        //         }

        //         // 2. گرفتن لیست پیج‌های مرتبط با کمپین
        //         var pages = await Con.CampaignPages
        //             .Where(cp => cp.CampaignId == campaignId)
        //             .Select(cp => cp.Page) // انتخاب پیج‌ها از جدول واسطه
        //             .ToListAsync();

        //         // 3. بازگرداندن لیست پیج‌ها
        //         return Ok(pages);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"internal server error: {ex.Message}");
        //     }
        // }




        // [HttpGet("[action]")]
        // public async Task<IActionResult> GetPageByCampaignIdAndPageId(int campaignId, int pageId)
        // {
        //     try
        //     {
        //         // 1. بررسی وجود کمپین
        //         var campaign = await Con.campaigns.FindAsync(campaignId);
        //         if (campaign == null)
        //         {
        //             return NotFound("کمپینی با این شناسه وجود ندارد.");
        //         }

        //         // 2. گرفتن پیج مرتبط با کمپین و شناسه 
        //         var page = await Con.CampaignPages
        //             .Where(cp => cp.CampaignId == campaignId && cp.PageId == pageId)
        //             .Select(cp => cp.Page)
        //             .FirstOrDefaultAsync();

        //         // 3. بررسی وجود پیج
        //         if (page == null)
        //         {
        //             return NotFound("پیجی با این شناسه در این کمپین وجود ندارد.");
        //         }

        //         // 4. بازگرداندن اطلاعات پیج
        //         return Ok(page);
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"internal server error: {ex.Message}");

        //     }
        // }
        // [HttpGet("[action]")]
        // public async Task<IActionResult> GetPageVersionByCampID(int campaignId)
        // {
        //     try
        //     {
        //         // 1. یافتن همه ورژن‌های پیج در کمپین مورد نظر
        //         var pageVersions = await Con.pageVersions
        //             .Where(pv => pv.CampaignId == campaignId)
        //             .Include(pv => pv.Page) // شامل اطلاعات پیج در هر ورژن
        //             .OrderByDescending(pv => pv.CreatedDateTime) // مرتب کردن ورژن‌ها بر اساس زمان ایجاد (جدیدترین در اول)
        //             .ToListAsync();

        //         // 2. بررسی وجود ورژن
        //         if (pageVersions == null || pageVersions.Count == 0)
        //         {
        //             return NotFound($"هیچ ورژنی برای کمپین با شناسه {campaignId} یافت نشد.");
        //         }

        //         // 3. برگرداندن لیست ورژن‌ها به عنوان پاسخ
        //         return Ok(pageVersions);
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "خطای داخلی در متد GetPageVersionByCampID: {campaignId}", campaignId);
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // // بدون نیاز به سریلیایز
        // [HttpGet("[action]")]
        // public async Task<IActionResult> GetEditedPageAndOriginalBuCampID(int campaignId)
        // {
        //     // 1. بارگیری اطلاعات اصلی کمپین و پیج‌های مرتبط با آن
        //     var campaignPages = await Con.CampaignPages
        //         .Where(cp => cp.CampaignId == campaignId)
        //         .Include(cp => cp.Page)
        //         .ToListAsync();

        //     // 2. بارگیری نسخه‌های پیج‌ها برای کمپین
        //     var pageVersions = await Con.pageVersions
        //         .Where(pv => pv.CampaignId == campaignId)
        //         .ToListAsync();

        //     // 3. ایجاد یک ViewModel برای نمایش اطلاعات پیج و نسخه‌ها
        //     var viewModel = new List<CampaignPageViewModel>();
        //     foreach (var campaignPage in campaignPages)
        //     {
        //         var versions = pageVersions
        //             .Where(pv => pv.PageId == campaignPage.PageId)
        //             .OrderByDescending(pv => pv.CreatedDateTime)
        //             .ToList();

        //         viewModel.Add(new CampaignPageViewModel
        //         {
        //             CampaignPage = campaignPage,
        //             PageVersions = versions
        //         });
        //     }

        //     // 4. ایجاد یک List جدید برای جمع آوری اطلاعات
        //     var result = new List<Dictionary<string, object>>();
        //     foreach (var item in viewModel)
        //     {
        //         // 4.1. ایجاد Dictionary برای اطلاعات پیج
        //         var campaignPageDict = new Dictionary<string, object>();
        //         campaignPageDict.Add("CampaignPageId", item.CampaignPage.CampaignPageId);
        //         campaignPageDict.Add("CampaignId", item.CampaignPage.CampaignId);
        //         campaignPageDict.Add("PageId", item.CampaignPage.Page.PageId);
        //         campaignPageDict.Add("Description", item.CampaignPage.Page.Description);
        //         campaignPageDict.Add("LastUpdatedTime", item.CampaignPage.LastUpdatedTime);

        //         // 4.2. اضافه کردن اطلاعات پیج به Dictionary
        //         campaignPageDict.Add("Page", new Dictionary<string, object>()
        //         {
        //             {"Id", item.CampaignPage.Page.Id},
        //             {"ShowName", item.CampaignPage.Page.ShowName},
        //             {"Followesrs", item.CampaignPage.Page.Followesrs},
        //             {"Following", item.CampaignPage.Page.Following},
        //             {"ImgUrl", item.CampaignPage.Page.ImgUrl},
        //             {"PostViews", item.CampaignPage.Page.PostViews},
        //             {"PostLink", item.CampaignPage.Page.PostLink},
        //             {"PostLikes", item.CampaignPage.Page.PostLikes},
        //             {"PostImpertion", item.CampaignPage.Page.PostImpertion},
        //             {"StoryViews", item.CampaignPage.Page.StoryViews},
        //             {"StoryImpertion", item.CampaignPage.Page.StoryImpertion},
        //             {"Engagement", item.CampaignPage.Page.Engagement},
        //             {"PageTypeId", item.CampaignPage.Page.PageTypeId},
        //             {"PageCategoryId", item.CampaignPage.Page.PageCategoryId},
        //             {"PricePagesId", item.CampaignPage.Page.PricePagesId},
        //             {"sex", item.CampaignPage.Page.sex},
        //             {"PersianName", item.CampaignPage.Page.PersianName},
        //             {"isAccept", item.CampaignPage.Page.IsAccepted}


        //         });

        //         // 4.3. ایجاد List برای اطلاعات نسخه‌ها
        //         var pageVersionsDict = new List<Dictionary<string, object>>();
        //         foreach (var pageVersion in item.PageVersions)
        //         {
        //             var versionDict = new Dictionary<string, object>();
        //             versionDict.Add("Id", pageVersion.Id);
        //             versionDict.Add("PageId", pageVersion.PageId);
        //             versionDict.Add("Description", pageVersion.Description);
        //             versionDict.Add("CreatedDateTime", pageVersion.CreatedDateTime);
        //             versionDict.Add("isAccept", pageVersion.IsAccepted);
        //             pageVersionsDict.Add(versionDict);
        //         }

        //         // 4.4. اضافه کردن اطلاعات نسخه‌ها به Dictionary
        //         campaignPageDict.Add("PageVersions", pageVersionsDict);
        //         result.Add(campaignPageDict);
        //     }

        //     // 5. بازگرداندن List به عنوان JSON
        //     return Ok(result);
        // }

        // [HttpPost("[action]")]
        // public IActionResult EditPageInformation(string pageID, int Followesrs, int Following, int Engagement, string showName, string sex, string persianName)
        // {
        //     try
        //     {
        //         var ExistPage = Con.pages.FirstOrDefault(x => x.PageId == pageID);

        //         if (ExistPage == null)
        //         {
        //             return NotFound("پیجی با این شناسه وجود ندارد ");
        //         }

        //         ExistPage.Followesrs = Followesrs;
        //         ExistPage.Following = Following;
        //         ExistPage.Engagement = Engagement;
        //         ExistPage.ShowName = showName;
        //         ExistPage.sex = sex;
        //         ExistPage.PersianName = persianName;



        //         Con.SaveChanges();

        //         return Ok(new
        //         {
        //             Message = " پیج با موفقیت تغییر یافت",
        //             Page = ExistPage
        //         });
        //     }
        //     catch (Exception ex)
        //     {
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }

        // [HttpPost("[action]")]
        // public async Task<IActionResult> DeletePage(string pageId)
        // {
        //     try
        //     {
        //         // 1. یافتن پیج در جدول PagesModel
        //         var page = await Con.pages
        //             .Include(p => p.CampaignPages) // شامل کمپین‌های مرتبط
        //             .FirstOrDefaultAsync(p => p.PageId == pageId);

        //         // 2. بررسی وجود پیج
        //         if (page == null)
        //         {
        //             return NotFound($"پیجی با شناسه {pageId} یافت نشد.");
        //         }

        //         // 3. حذف تمامی ارتباطات کمپین‌ها با این پیج
        //         var campaignPages = await Con.CampaignPages
        //             .Where(cp => cp.PageId == page.Id)
        //             .ToListAsync();

        //         if (campaignPages.Any())
        //         {
        //             Con.CampaignPages.RemoveRange(campaignPages);
        //         }

        //         // 4. حذف نسخه‌های مربوط به این پیج از جدول PageVersion
        //         var pageVersions = await Con.pageVersions
        //             .Where(v => v.PageId == page.Id)
        //             .ToListAsync();

        //         if (pageVersions.Any())
        //         {
        //             Con.pageVersions.RemoveRange(pageVersions);
        //         }

        //         // 5. حذف پیج از جدول PagesModel
        //         Con.pages.Remove(page);

        //         // 6. ذخیره تغییرات در پایگاه داده
        //         await Con.SaveChangesAsync();

        //         return Ok($"پیج با شناسه {pageId} با موفقیت حذف شد.");
        //     }
        //     catch (Exception ex)
        //     {
        //         _logger.LogError(ex, "خطای داخلی در متد DeletePage: {pageId}", pageId);
        //         return StatusCode(500, $"خطای داخلی سرور: {ex.Message}");
        //     }
        // }
//     }
// }
