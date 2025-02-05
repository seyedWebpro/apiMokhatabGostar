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
    public class TarefeController : ControllerBase
    {
        private readonly apiContext _context;

        public TarefeController(apiContext context)
        {
            _context = context;
        }

        // ایجاد تعرفه جدید
        [HttpPost("create-tariff")]
        public async Task<IActionResult> CreateTariff([FromBody] string tariffName)
        {
            if (string.IsNullOrWhiteSpace(tariffName))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "نام تعرفه نمی‌تواند خالی باشد."
                });
            }

            var tariff = new TarefeModel
            {
                Name = tariffName
            };

            _context.tarefeModels.Add(tariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت ایجاد شد.",
                TariffId = tariff.Id
            });
        }

        // دریافت همه تعرفه‌ها
        [HttpGet("get-all-tariffs")]
        public async Task<IActionResult> GetAllTariffs()
        {
            var tariffs = await _context.tarefeModels.ToListAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه‌ها با موفقیت دریافت شدند.",
                Tariffs = tariffs
            });
        }

        // دریافت تعرفه با شناسه خاص
        [HttpGet("get-tariff/{id}")]
        public async Task<IActionResult> GetTariffById(int id)
        {
            var tariff = await _context.tarefeModels.FindAsync(id);

            if (tariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه پیدا نشد."
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت دریافت شد.",
                Tariff = tariff
            });
        }

        // ویرایش اسم تعرفه
        [HttpPost("edit-tariff/{id}")]
        public async Task<IActionResult> EditTariff(int id, [FromBody] string tariffName)
        {
            if (string.IsNullOrWhiteSpace(tariffName))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "نام تعرفه نمی‌تواند خالی باشد."
                });
            }

            var tariff = await _context.tarefeModels.FindAsync(id);

            if (tariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه پیدا نشد."
                });
            }

            // ویرایش فقط اسم تعرفه
            tariff.Name = tariffName;

            _context.tarefeModels.Update(tariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت ویرایش شد."
            });
        }

        // حذف تعرفه
        [HttpPost("delete-tariff/{id}")]
        public async Task<IActionResult> DeleteTariff(int id)
        {
            var tariff = await _context.tarefeModels.FindAsync(id);

            if (tariff == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "تعرفه پیدا نشد."
                });
            }

            _context.tarefeModels.Remove(tariff);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "تعرفه با موفقیت حذف شد."
            });
        }
    }
}
