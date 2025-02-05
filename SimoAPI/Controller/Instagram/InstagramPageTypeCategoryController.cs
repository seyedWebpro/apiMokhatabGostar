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
    public class InstagramPageTypeCategoryController : ControllerBase
    {
        private readonly apiContext _context;

        public InstagramPageTypeCategoryController(apiContext context)
        {
            _context = context;
        }

        // اضافه کردن دسته‌بندی به پیج
        [HttpPost("add-category-to-page/{pageId}/{categoryId}")]
        public async Task<IActionResult> AddCategoryToPage(int pageId, int categoryId)
        {
            try
            {
                // پیدا کردن پیج و دسته‌بندی
                var page = await _context.Pages.FindAsync(pageId);
                var category = await _context.pageTypeCategories.FindAsync(categoryId);

                if (page == null || category == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "پیج یا دسته‌بندی مورد نظر پیدا نشد."
                    });
                }

                // اضافه کردن دسته‌بندی به پیج
                page.PageTypeCategoryId = categoryId;

                // ذخیره تغییرات
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "دسته‌بندی با موفقیت به پیج اضافه شد."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور.", Error = ex.Message });
            }
        }

        // حذف دسته‌بندی از پیج
        [HttpPost("remove-category-from-page/{pageId}")]
        public async Task<IActionResult> RemoveCategoryFromPage(int pageId)
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

                // حذف دسته‌بندی از پیج
                page.PageTypeCategoryId = null;

                // ذخیره تغییرات
                _context.Pages.Update(page);
                await _context.SaveChangesAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "دسته‌بندی با موفقیت از پیج حذف شد."
                });
            }
            catch (Exception ex)
            {
                return StatusCode(500, new { StatusCode = 500, Message = "خطای داخلی سرور.", Error = ex.Message });
            }
        }

        // دریافت همه پیج‌های یک دسته‌بندی خاص
        [HttpGet("pages-by-category/{categoryId}")]
        public async Task<IActionResult> GetPagesByCategory(int categoryId)
        {
            try
            {
                // پیدا کردن دسته‌بندی
                var category = await _context.pageTypeCategories.FindAsync(categoryId);

                if (category == null)
                {
                    return NotFound(new
                    {
                        StatusCode = 404,
                        Message = "دسته‌بندی مورد نظر پیدا نشد."
                    });
                }

                // دریافت همه پیج‌های مرتبط با این دسته‌بندی
                var pages = await _context.Pages
                    .Where(p => p.PageTypeCategoryId == categoryId)
                    .ToListAsync();

                return Ok(new
                {
                    StatusCode = 200,
                    Message = "پیج‌های مرتبط با دسته‌بندی با موفقیت دریافت شدند.",
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