using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using allAPIs.SimoAPI.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.CodeAnalysis.CSharp.Syntax;
using Microsoft.EntityFrameworkCore;

namespace allAPIs.SimoAPI   .Controller
{
    [ApiController]
    [Route("api/[controller]")]
    public class PricePageController : ControllerBase
    {
        public PricePageController(apiContext context, IHttpContextAccessor httpContextAccessor)
        {
            Con = context;
            Con.httpContext = httpContextAccessor.HttpContext;
        }
        public apiContext Con { get; }

        //Create PricePage 
        [HttpPost("[action]")]
        public IActionResult CreatePricePage(PricePageModel pricePageModel)
        {
            try
            {

                var pricepage = new PricePageModel
                {
                    Name = pricePageModel.Name,
                    HamkarPrice = pricePageModel.HamkarPrice,
                    SpecialPrice = pricePageModel.SpecialPrice,
                    Normalprice = pricePageModel.Normalprice,
                };

                Con.pricePages.Add(pricepage);
                Con.SaveChanges();


                return Ok(new
                {
                    Message = "تعرفه با موفقیت اضافه شد",
                    Price = pricepage
                }
                );
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, $"database update error : {ex.Message}");
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"internal server error : {ex.Message}");
            }

        }


        [HttpGet("[action]")]
        public IActionResult GetAll()
        {
            try
            {
                var pricePage = Con.pricePages.ToList();

                // اگر هیچ پیجی وجود نداشت، کد وضعیت 404 (Not Found) را برمی‌گردانیم
                if (pricePage.Count == 0)
                {
                    return NotFound("هیچ تعرفه ای وجود ندارد");
                }

                // اگر پیجی وجود داشته باشد، آنها را به همراه کد وضعیت 200 (OK) برمی‌گردانیم
                return Ok(pricePage);

            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, $"database update error : {ex.Message}");
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"internal server error : {ex.Message}");
            }
        }

        [HttpPost("[action]")]
        public IActionResult AddTitlePricePage(PricePageTitleModel pricePageTitleModel)
        {
            try
            {

                var pricepageTitle = new PricePageTitleModel
                {
                    Title = pricePageTitleModel.Title,
                };

                Con.pricePageTitles.Add(pricepageTitle);
                Con.SaveChanges();


                return Ok(new
                {
                    Message = " تایتل تعرفه با موفقیت اضافه شد" ,
                    Price = pricepageTitle
                }
                );
            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, $"database update error : {ex.Message}");
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"internal server error : {ex.Message}");
            }

        }

         [HttpGet("[action]")]
        public IActionResult GetAllTitle()
        {
            try
            {
                var Title = Con.pricePageTitles.ToList();

                if (Title.Count == 0)
                {
                    return NotFound("هیچ تایتلی وجود ندارد");
                }

                return Ok(Title);

            }
            catch (DbUpdateException ex)
            {

                return StatusCode(500, $"database update error : {ex.Message}");
            }

            catch (Exception ex)
            {

                return StatusCode(500, $"internal server error : {ex.Message}");
            }
        }

    }
}