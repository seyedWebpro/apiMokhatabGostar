using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI.Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class ChartController : ControllerBase
    {
        public ChartController(apiContext context)
        {
            Con = context;
        }

        public apiContext Con { get; }

        [HttpGet("[action]")]
        public IActionResult GetChartByCampID(int campaignID)
        {
            try
            {
                var ExistCamp = Con.campaigns.FirstOrDefault(c => c.Id == campaignID);

                if (ExistCamp == null)
                {
                    return NotFound("کمپینی با این شناسه وجود ندارد");
                }

                 var existChart = Con.charts.Where(c => c.CampaignId == campaignID).ToList();

                if (existChart == null || !existChart.Any())
                {
                    return NotFound("هیچ نموداری برای این کمپین وجود ندارد.");
                }

                return Ok(existChart);
                
            }
             catch (DbUpdateException ex)
            {
                
                return StatusCode(500 , $"database update error : {ex.Message}");
            }

            catch (Exception ex)
            {
                
                return StatusCode(500 , $"internal server error : {ex.Message}");
            }
        }

        
    }
}