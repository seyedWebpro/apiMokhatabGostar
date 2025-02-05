using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using allAPIs.SimoAPI.Models;

namespace allAPIs.SimoAPI.Controller.Instagram
{
    [ApiController]
    [Route("api/[controller]")]
    public class InstagramPageTypeController : ControllerBase
    {
        private readonly apiContext _context;

        public InstagramPageTypeController(apiContext context)
        {
            _context = context;
        }

        // اضافه کردن نوع دسته‌بندی به پیج
        [HttpPost("add-page-type-to-page/{pageId}/{pageTypeId}")]
        public async Task<IActionResult> AddPageTypeToPage(int pageId, int pageTypeId)
        {
            try
            {
                // پیدا کردن پیج و نوع دسته‌بندی
                var page = await _context.Pages.FindAsync(pageId);
                var pageType = await _context.pageTypes.FindAsync(pageTypeId);

                if (page == null || pageType == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "پیج یا نوع دسته‌بندی مورد نظر پیدا نشد."
                    });
                }

                // اضافه کردن نوع دسته‌بندی به پیج
                page.PageTypeId = pageTypeId;

                // ذخیره تغییرات
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "نوع دسته‌بندی با موفقیت به پیج اضافه شد."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور.", Error = ex.Message });
            }
        }

        // حذف نوع دسته‌بندی از پیج
        [HttpPost("remove-page-type-from-page/{pageId}")]
        public async Task<IActionResult> RemovePageTypeFromPage(int pageId)
        {
            try
            {
                // پیدا کردن پیج
                var page = await _context.Pages.FindAsync(pageId);

                if (page == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "پیج مورد نظر پیدا نشد."
                    });
                }

                // حذف نوع دسته‌بندی از پیج
                page.PageTypeId = null;

                // ذخیره تغییرات
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "نوع دسته‌بندی با موفقیت از پیج حذف شد."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور.", Error = ex.Message });
            }
        }

        // دریافت همه پیج‌های یک نوع دسته‌بندی خاص
        [HttpGet("pages-by-page-type/{pageTypeId}")]
        public async Task<IActionResult> GetPagesByPageType(int pageTypeId)
        {
            try
            {
                // پیدا کردن نوع دسته‌بندی
                var pageType = await _context.pageTypes.FindAsync(pageTypeId);

                if (pageType == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "نوع دسته‌بندی مورد نظر پیدا نشد."
                    });
                }

                // دریافت همه پیج‌های مرتبط با این نوع دسته‌بندی
                var pages = await _context.Pages
                    .Where(p => p.PageTypeId == pageTypeId)
                    .ToListAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "پیج‌های مرتبط با نوع دسته‌بندی با موفقیت دریافت شدند.",
                    Pages = pages
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور.", Error = ex.Message });
            }
        }
    }
}