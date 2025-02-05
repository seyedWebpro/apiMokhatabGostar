using allAPIs.SimoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageTypeController : ControllerBase
    {
private readonly apiContext _context;

        public PageTypeController(apiContext context)
        {
            _context = context;
        }

        // ایجاد نوع دسته‌بندی
        [HttpPost("create-page-type")]
        public async Task<IActionResult> CreatePageType([FromBody] CreatePageTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "داده‌های ورودی معتبر نیستند."
                });
            }

            var pageType = new PageTypeModel
            {
                Name = dto.Name
            };

            _context.pageTypes.Add(pageType);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "نوع دسته‌بندی با موفقیت ایجاد شد.",
                PageTypeId = pageType.Id
            });
        }

        // دریافت همه نوع دسته‌بندی‌ها
       [HttpGet("get-all-page-types")]
public async Task<IActionResult> GetAllpageTypes()
{
    var pageTypes = await _context.pageTypes
        .Select(pt => new
        {
            pt.Id,
            pt.Name
        })
        .ToListAsync();

    return Ok(new
    {
        StatusCode = 200,
        Message = "نوع دسته‌بندی‌ها با موفقیت دریافت شدند.",
        pageTypes = pageTypes
    });
}

        // دریافت یک نوع دسته‌بندی با شناسه
       [HttpGet("get-page-type/{id}")]
public async Task<IActionResult> GetPageTypeById(int id)
{
    var pageType = await _context.pageTypes
        .Where(pt => pt.Id == id)
        .Select(pt => new
        {
            pt.Id,
            pt.Name
        })
        .FirstOrDefaultAsync();

    if (pageType == null)
    {
        return NotFound(new
        {
            StatusCode = 404,
            Message = "نوع دسته‌بندی یافت نشد."
        });
    }

    return Ok(new
    {
        StatusCode = 200,
        Message = "نوع دسته‌بندی با موفقیت دریافت شد.",
        PageType = pageType
    });
}

        // ویرایش نوع دسته‌بندی
        [HttpPost("edit-page-type/{id}")]
        public async Task<IActionResult> EditPageType(int id, [FromBody] UpdatePageTypeDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "داده‌های ورودی معتبر نیستند."
                });
            }

            var pageType = await _context.pageTypes.FindAsync(id);

            if (pageType == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "نوع دسته‌بندی یافت نشد."
                });
            }

            // به‌روزرسانی فیلدها
            if (dto.Name != null)
            {
                pageType.Name = dto.Name;
            }

            _context.pageTypes.Update(pageType);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "نوع دسته‌بندی با موفقیت ویرایش شد."
            });
        }

        // حذف نوع دسته‌بندی
        [HttpPost("delete-page-type/{id}")]
        public async Task<IActionResult> DeletePageType(int id)
        {
            var pageType = await _context.pageTypes.FindAsync(id);

            if (pageType == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "نوع دسته‌بندی یافت نشد."
                });
            }

            _context.pageTypes.Remove(pageType);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "نوع دسته‌بندی با موفقیت حذف شد."
            });
        }
    }
}