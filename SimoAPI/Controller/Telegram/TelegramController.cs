using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using allAPIs.SimoAPI.Models.TelegramView;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace allAPIs.SimoAPI.Controller.Telegram
{
    [ApiController]
    [Route("api/[controller]")]
    public class TelegramController : ControllerBase
    {
        private readonly apiContext _context;
        private readonly HttpClient _httpClient;

        public TelegramController(apiContext context, HttpClient httpClient)
        {
            _context = context;
            _httpClient = httpClient;
        }

        // متد ایجاد کانال
        // [HttpPost("create")]
        // public async Task<IActionResult> CreateChannel([FromBody] TelegramChannelDto channelDto)
        // {
        //     if (!ModelState.IsValid)
        //     {
        //         return BadRequest(new
        //         {
        //             StatusCode = 400,
        //             Message = "اطلاعات کانال نامعتبر است."
        //         });
        //     }

        //     var channel = new TelegramChannelModel
        //     {
        //         Name = channelDto.Name,
        //         Topic = channelDto.Topic,
        //         ChannelId = channelDto.ChannelId,
        //         ManagerId = channelDto.ManagerId
        //     };

        //     _context.telegramChannels.Add(channel);
        //     await _context.SaveChangesAsync();

        //     return Ok(new
        //     {
        //         StatusCode = 200,
        //         Message = "کانال با موفقیت اضافه شد.",
        //         ChannelId = channel.Id
        //     });
        // }

        // ترکیب کردن با api های تلگرم
        [HttpPost("create")]
        public async Task<IActionResult> CreateChannel([FromBody] TelegramChannelDto channelDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "اطلاعات کانال نامعتبر است."
                });
            }

            // توکن ربات تلگرام
            string token = "7351126944:AAG1OdkcScBxO8PNS8WHi5JgsytRISEePfw";
            string chatId = channelDto.ChannelId;
            string url = $"https://api.telegram.org/bot{token}/getChat?chat_id={chatId}";

            // ارسال درخواست به API تلگرام
            var response = await _httpClient.GetAsync(url);
            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "خطا در دریافت اطلاعات کانال از تلگرام."
                });
            }

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            if (jsonResponse == null || !jsonResponse.ContainsKey("result"))
            {
                return BadRequest(new
                {
                    StatusCode = 400,
                    Message = "کانال یافت نشد."
                });
            }

            var result = jsonResponse["result"] as JObject;
            string channelName = result?["title"]?.ToString();
            string photoFileId = result?["photo"]?["big_file_id"]?.ToString();

            // دانلود و ذخیره عکس
            string photoPath = null;
            if (!string.IsNullOrEmpty(photoFileId))
            {
                string getFileUrl = $"https://api.telegram.org/bot{token}/getFile?file_id={photoFileId}";
                var fileResponse = await _httpClient.GetAsync(getFileUrl);

                if (fileResponse.IsSuccessStatusCode)
                {
                    var fileContent = await fileResponse.Content.ReadAsStringAsync();
                    var fileJsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContent);
                    var fileResult = fileJsonResponse?["result"] as JObject;
                    string filePath = fileResult?["file_path"]?.ToString();

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string fileDownloadUrl = $"https://api.telegram.org/file/bot{token}/{filePath}";
                        byte[] photoBytes = await _httpClient.GetByteArrayAsync(fileDownloadUrl);

                        // مسیر ذخیره عکس (نسبی به wwwroot)
                        string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "channelPhotos");
                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }

                        string fileName = $"{Guid.NewGuid()}.jpg";
                        photoPath = Path.Combine("channelPhotos", fileName); // مسیر نسبی برای ذخیره در دیتابیس

                        // مسیر کامل برای ذخیره فایل
                        string fullPath = Path.Combine(saveDirectory, fileName);
                        await System.IO.File.WriteAllBytesAsync(fullPath, photoBytes);
                    }
                }
            }

            var channel = new TelegramChannelModel
            {
                Name = channelName ?? channelDto.Name,
                Topic = channelDto.Topic,
                TelID = channelDto.ChannelId,
                ManagerId = channelDto.ManagerId,
                PhotoPath = photoPath, // ذخیره مسیر نسبی عکس
                SubscribersCount = channelDto.SubscribersCount // تعداد ساب‌اسکرایب‌ها
            };

            _context.telegramChannels.Add(channel);
            await _context.SaveChangesAsync();

            // دریافت تعرفه‌های مرتبط با این کانال
            var tariffs = await _context.telegramChannelTariffModels
                .Where(t => t.TelegramChannelId == channel.Id)
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
                Message = "کانال با موفقیت اضافه شد.",
                Channel = new
                {
                    channel.Id,
                    channel.Name,
                    channel.Topic,
                    channel.TelID,
                    channel.ManagerId,
                    channel.PhotoPath,
                    channel.SubscribersCount,
                    Tariffs = tariffs // اضافه کردن تعرفه‌ها در پاسخ
                }
            });
        }


        // بروز رسانی اطلاعات کانال (مثلا اگر عکس ان تغییر کرده باشد)

        [HttpPost("updatewithapi/{id}")]
        public async Task<IActionResult> UpdateChannelWithApi(int id)
        {
            var channel = await _context.telegramChannels.FindAsync(id);
            if (channel == null)
            {
                return NotFound(new { StatusCode = 404, Message = "کانال مورد نظر پیدا نشد." });
            }

            // دریافت اطلاعات جدید از API تلگرام
            string token = "7351126944:AAG1OdkcScBxO8PNS8WHi5JgsytRISEePfw";
            string url = $"https://api.telegram.org/bot{token}/getChat?chat_id={channel.TelID}";
            var response = await _httpClient.GetAsync(url);

            if (!response.IsSuccessStatusCode)
            {
                return BadRequest(new { StatusCode = 400, Message = "خطا در دریافت اطلاعات کانال از تلگرام." });
            }

            var content = await response.Content.ReadAsStringAsync();
            var jsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(content);
            var result = jsonResponse["result"] as JObject;

            // به‌روزرسانی اطلاعات
            channel.Name = result?["title"]?.ToString();
            string photoFileId = result?["photo"]?["big_file_id"]?.ToString();

            // به‌روزرسانی عکس (در صورت وجود)
            if (!string.IsNullOrEmpty(photoFileId))
            {
                string getFileUrl = $"https://api.telegram.org/bot{token}/getFile?file_id={photoFileId}";
                var fileResponse = await _httpClient.GetAsync(getFileUrl);

                if (fileResponse.IsSuccessStatusCode)
                {
                    var fileContent = await fileResponse.Content.ReadAsStringAsync();
                    var fileJsonResponse = JsonConvert.DeserializeObject<Dictionary<string, object>>(fileContent);
                    var fileResult = fileJsonResponse?["result"] as JObject;
                    string filePath = fileResult?["file_path"]?.ToString();

                    if (!string.IsNullOrEmpty(filePath))
                    {
                        string fileDownloadUrl = $"https://api.telegram.org/file/bot{token}/{filePath}";
                        byte[] photoBytes = await _httpClient.GetByteArrayAsync(fileDownloadUrl);

                        string saveDirectory = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "channelPhotos");
                        if (!Directory.Exists(saveDirectory))
                        {
                            Directory.CreateDirectory(saveDirectory);
                        }

                        string fileName = $"{Guid.NewGuid()}.jpg";
                        string photoPath = Path.Combine("channelPhotos", fileName);
                        string fullPath = Path.Combine(saveDirectory, fileName);
                        await System.IO.File.WriteAllBytesAsync(fullPath, photoBytes);

                        // حذف عکس قبلی (اگر وجود دارد)
                        if (!string.IsNullOrEmpty(channel.PhotoPath))
                        {
                            string oldPhotoPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", channel.PhotoPath);
                            if (System.IO.File.Exists(oldPhotoPath))
                            {
                                System.IO.File.Delete(oldPhotoPath);
                            }
                        }

                        channel.PhotoPath = photoPath;
                    }
                }
            }

            _context.telegramChannels.Update(channel);
            await _context.SaveChangesAsync();

            return Ok(new { StatusCode = 200, Message = "کانال با موفقیت به‌روزرسانی شد." });
        }

        // متد دریافت همه کانال‌ها
        [HttpGet("all")]
        public IActionResult GetAllChannels()
        {
            var channels = _context.telegramChannels
                .Include(c => c.TelegramChannelTariffModels)
                .ThenInclude(t => t.Tariff)
                .ToList();

            var result = channels.Select(channel => new
            {
                channel.Id,
                channel.Name,
                channel.Topic,
                channel.TelID,
                channel.ManagerId,
                channel.SubscribersCount, // تعداد ساب‌اسکرایب‌ها
                PhotoUrl = !string.IsNullOrEmpty(channel.PhotoPath)
                    ? $"/{channel.PhotoPath.Replace("\\", "/")}" // تبدیل مسیر به URL
                    : null,
                Tariffs = channel.TelegramChannelTariffModels.Select(t => new
                {
                    t.TariffId,
                    t.Tariff.Name,
                    t.Price
                }).ToList()
            }).ToList();

            return Ok(new
            {
                StatusCode = 200,
                Channels = result
            });
        }


        // متد دریافت کانال براساس آیدی
        // [HttpGet("{id}")]
        // public async Task<IActionResult> GetChannelById(int id)
        // {
        //     var channel = await _context.telegramChannels.FindAsync(id);
        //     if (channel == null)
        //     {
        //         return NotFound(new
        //         {
        //             StatusCode = 404,
        //             Message = "کانال مورد نظر پیدا نشد."
        //         });
        //     }

        //     return Ok(new
        //     {
        //         StatusCode = 200,
        //         Channel = channel
        //     });
        // }

        // ترکیب با api های تلگرام
        [HttpGet("{id}")]
        public async Task<IActionResult> GetChannelById(int id)
        {
            var channel = await _context.telegramChannels
                .Include(c => c.TelegramChannelTariffModels)
                .ThenInclude(t => t.Tariff)
                .FirstOrDefaultAsync(c => c.Id == id);

            if (channel == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "کانال مورد نظر پیدا نشد."
                });
            }

            return Ok(new
            {
                StatusCode = 200,
                Channel = new
                {
                    channel.Id,
                    channel.Name,
                    channel.Topic,
                    channel.TelID,
                    channel.ManagerId,
                    channel.SubscribersCount, // تعداد ساب‌اسکرایب‌ها
                    PhotoUrl = !string.IsNullOrEmpty(channel.PhotoPath)
                        ? $"/{channel.PhotoPath.Replace("\\", "/")}" // تبدیل مسیر به URL
                        : null,
                    Tariffs = channel.TelegramChannelTariffModels.Select(t => new
                    {
                        t.TariffId,
                        t.Tariff.Name,
                        t.Price
                    }).ToList()
                }
            });
        }


        // متد ویرایش کانال
        [HttpPost("update/{id}")]
        public async Task<IActionResult> UpdateChannel(int id, [FromBody] TelegramChannelUpdateDto channelUpdateDto)
        {
            var channel = await _context.telegramChannels.FindAsync(id);
            if (channel == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "کانال مورد نظر پیدا نشد."
                });
            }

            // به‌روزرسانی فقط فیلدهایی که مقدار داده شده‌اند
            if (!string.IsNullOrEmpty(channelUpdateDto.Name))
                channel.Name = channelUpdateDto.Name;

            if (!string.IsNullOrEmpty(channelUpdateDto.Topic))
                channel.Topic = channelUpdateDto.Topic;

            if (!string.IsNullOrEmpty(channelUpdateDto.ChannelId))
                channel.TelID = channelUpdateDto.ChannelId;

            if (!string.IsNullOrEmpty(channelUpdateDto.ManagerId))
                channel.ManagerId = channelUpdateDto.ManagerId;

            if (channelUpdateDto.SubscribersCount.HasValue)
            {
                channel.SubscribersCount = channelUpdateDto.SubscribersCount.Value;
            }

            _context.telegramChannels.Update(channel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "کانال با موفقیت به‌روزرسانی شد."
            });
        }

        // متد حذف کانال
        [HttpPost("delete/{id}")]
        public async Task<IActionResult> DeleteChannel(int id)
        {
            var channel = await _context.telegramChannels.FindAsync(id);
            if (channel == null)
            {
                return NotFound(new
                {
                    StatusCode = 404,
                    Message = "کانال مورد نظر پیدا نشد."
                });
            }

            // حذف فایل عکس از سرور (اگر وجود دارد)
            if (!string.IsNullOrEmpty(channel.PhotoPath))
            {
                string fullPath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", channel.PhotoPath);
                if (System.IO.File.Exists(fullPath))
                {
                    System.IO.File.Delete(fullPath);
                }
            }

            _context.telegramChannels.Remove(channel);
            await _context.SaveChangesAsync();

            return Ok(new
            {
                StatusCode = 200,
                Message = "کانال با موفقیت حذف شد."
            });
        }
    }
}