using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace allAPIs.SimoAPI.Jops
{
    public class AuotoDeletePageVersions : BackgroundService
    {
        private readonly IServiceScopeFactory _scopeFactory;

        public AuotoDeletePageVersions(IServiceScopeFactory scopeFactory)
        {
            _scopeFactory = scopeFactory;
        }

        protected override async Task ExecuteAsync(CancellationToken stoppingToken)
        {
            while (!stoppingToken.IsCancellationRequested)
            {
                using (var scope = _scopeFactory.CreateScope())
                {
                    var context = scope.ServiceProvider.GetRequiredService<apiContext>(); // YourDbContext را با نام DbContext خود جایگزین کنید

                    // یافتن PageId های منحصر به فرد 
                    var pageIds = context.pageVersions
                        .Select(pv => pv.PageId)
                        .Distinct()
                        .ToList();

                    foreach (var pageId in pageIds)
                    {
                        // یافتن نسخه های مربوط به هر PageId
                        var pageVersions = context.pageVersions
                            .Where(pv => pv.PageId == pageId)
                            .OrderByDescending(pv => pv.CreatedDateTime)
                            .ToList();

                        // حذف همه نسخه های قدیمی تر از جدیدترین نسخه
                        for (int i = 1; i < pageVersions.Count; i++)
                        {
                            context.pageVersions.Remove(pageVersions[i]);
                        }

                        await context.SaveChangesAsync();
                    }

                    // انتظار به مدت 7 روز (168 ساعت) برای اجرای بعدی
                    await Task.Delay(TimeSpan.FromHours(168), stoppingToken); // زمان را به دلخواه خود تنظیم کنید
                }
            }
        }
    }
}