using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PageTypeCategoryController : ControllerBase
    {
       private readonly apiContext _context;

        public PageTypeCategoryController(apiContext context)
        {
            _context = context;
        }
        // ایجاد دسته‌بندی
        [HttpPost("create-category")]
        public async Task<IActionResult> CreateCategory([FromBody] CreatePageTypeCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "داده‌های ورودی معتبر نیستند."
                });
            }

            var category = new PageTypeCategoryModel
            {
                CategoryName = dto.CategoryName
            };

            _context.pageTypeCategories.Add(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "دسته‌بندی با موفقیت ایجاد شد.",
                CategoryId = category.Id
            });
        }

        // دریافت همه دسته‌بندی‌ها
      [HttpGet("get-all-categories")]
public async Task<IActionResult> GetAllCategories()
{
    var categories = await _context.pageTypeCategories
        .Select(c => new
        {
            c.Id,
            c.CategoryName
        })
        .ToListAsync();

    return Ok(new
    {
        StatusCode = 200,
        Message = "دسته‌بندی‌ها با موفقیت دریافت شدند.",
        Categories = categories
    });
}

        // دریافت یک دسته‌بندی با شناسه
      [HttpGet("get-category/{id}")]
public async Task<IActionResult> GetCategoryById(int id)
{
    var category = await _context.pageTypeCategories
        .Where(c => c.Id == id)
        .Select(c => new
        {
            c.Id,
            c.CategoryName
        })
        .FirstOrDefaultAsync();

    if (category == null)
    {
        return NotFound(new
        {
            StatusCode = 404,
            Message = "دسته‌بندی یافت نشد."
        });
    }

    return Ok(new
    {
        StatusCode = 200,
        Message = "دسته‌بندی با موفقیت دریافت شد.",
        Category = category
    });
}

        // ویرایش دسته‌بندی
        [HttpPost("edit-category/{id}")]
        public async Task<IActionResult> EditCategory(int id, [FromBody] UpdatePageTypeCategoryDto dto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "داده‌های ورودی معتبر نیستند."
                });
            }

            var category = await _context.pageTypeCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "دسته‌بندی یافت نشد."
                });
            }

            // به‌روزرسانی فیلدها
            if (dto.CategoryName != null)
            {
                category.CategoryName = dto.CategoryName;
            }

            _context.pageTypeCategories.Update(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "دسته‌بندی با موفقیت ویرایش شد."
            });
        }

        // حذف دسته‌بندی
        [HttpPost("delete-category/{id}")]
        public async Task<IActionResult> DeleteCategory(int id)
        {
            var category = await _context.pageTypeCategories.FindAsync(id);

            if (category == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "دسته‌بندی یافت نشد."
                });
            }

            _context.pageTypeCategories.Remove(category);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "دسته‌بندی با موفقیت حذف شد."
            });
        }
    }
}