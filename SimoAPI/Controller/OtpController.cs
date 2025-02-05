using System.Diagnostics;
using System.Text.Json.Serialization;
using Dapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore.SqlServer;
using SmsIrRestfulNetCore;

namespace api.Controllers;

[Route("[controller]")]
[ApiController]
public class OtpController : ControllerBase
{
        #region ctors
        public OtpController(IConfiguration configuration, apiContext dbcontext)
        {
            Configuration = configuration;
            Con = dbcontext;
        }

        public IConfiguration Configuration { get; }
        public apiContext Con { get; }
        #endregion

        #region Services
        [HttpPost("[action]")]
        public serviceresult SendVerifyCode(string PhoneNumber)
        {
            Con.httpContext = HttpContext;
            serviceresult result;

            try
            {
                Random RandomCode = new Random();

                otp new_otp = new otp();

                result = new serviceresult()
                {
                    messageType = messageType.ok,
                    message = "کد برای شما ارسال شد",
                    objectResult = new_otp
                };

                new_otp.id = 0;
                new_otp.phone_number = PhoneNumber;
                new_otp.status = 1;
                new_otp.verify_code = RandomCode.Next(1000, 9999).ToString();

                new_otp.requiredValidation(ref result);
                if (result.messageType == messageType.error)
                    return result;

                new_otp.createTime = DateTime.Now;
                Con.otps.Add(new_otp);
                Con.SaveChanges();

                var token = new Token().GetToken(Configuration.GetSection("otp:userKey").Value, Configuration.GetSection("otp:securKey").Value);
                var ultraFastSend = new UltraFastSend()
                {
                    Mobile = Convert.ToInt64(new_otp.phone_number),
                    TemplateId = 54286,
                    ParameterArray = new List<UltraFastParameters>()
                                {
                                    new UltraFastParameters()
                                    {
                                        Parameter = "VerificationCode" , ParameterValue = new_otp.verify_code
                                    }
                                }.ToArray()
                };

                UltraFastSendRespone ultraFastSendRespone = new UltraFast().Send(token, ultraFastSend);
                if (ultraFastSendRespone.IsSuccessful)
                {
                    result = new serviceresult()
                    {
                        messageType = messageType.ok,
                        message = "کد برای شما ارسال شد",
                        //objectResult = new_otp
                        objectResult = null
                    };
                }
                else
                {
                    result = new serviceresult()
                    {
                        messageType = messageType.error,
                        message = "خطایی در ارسال پیامک کد تایید رخ داده است",
                        objectResult = null
                    };
                }
            }
            catch (Exception ex)
            {
                result = new serviceresult()
                {
                    messageType = messageType.error,
                    message = ex.Message,
                    objectResult = null
                };
            }

            return result;
        }

        [HttpPost("[action]")]
        public serviceresult VerifyCode(string PhoneNumber, string Code)
        {
            //code status after user action
            //if code status == 1 ==> sms has been send
            //if code status == 2 ==> this code has been verify by user
            //if code status == 3 ==> this code couldn't verify by user
            //if code status == 4 ==> this code has expired

            Con.httpContext = HttpContext;
            serviceresult result;

            try
            {
                var entity = Con.otps.Where(x => x.phone_number == PhoneNumber).OrderByDescending(x => x.createTime).FirstOrDefault() ?? null;

                result = new serviceresult()
                {
                    messageType = messageType.ok,
                    message = "OK",
                    objectResult = entity
                };

                if (entity != null)
                {
                    if (entity.createTime.Value.AddMinutes(2) >= DateTime.Now)
                    {
                        if (entity.verify_code == Code)
                        {
                            entity.status = 2;

                            Con.Entry(entity).State = EntityState.Modified;
                            Con.SaveChanges();

                            //برای ورود با پیامک
                            var user = Con.users.Where(x=>x.username == PhoneNumber).FirstOrDefault();
                            if(user != null){
                                user.password = SimoFunctions.Decrypt(user.password);
                            }

                            result = new serviceresult()
                            {
                                messageType = messageType.ok,
                                message = "OK",
                                objectResult =  user?.password
                            };
                        }
                        else
                        {
                            entity.status = 3;

                            Con.Entry(entity).State = EntityState.Modified;
                            Con.SaveChanges();

                            result = new serviceresult()
                            {
                                messageType = messageType.error,
                                message = "کد وارد شده صحیح نیست",
                                objectResult = entity
                            };
                        }
                    }
                    else
                    {
                        entity.status = 4;

                        Con.Entry(entity).State = EntityState.Modified;
                        Con.SaveChanges();

                        result = new serviceresult()
                        {
                            messageType = messageType.error,
                            message = "کد وارد شده منقضی شده است",
                            objectResult = entity
                        };
                    }
                }
            }
            catch (Exception ex)
            {
                result = new serviceresult()
                {
                    messageType = messageType.error,
                    message = ex.Message,
                    objectResult = null
                };
            }

            return result;
        }

        #endregion

        #region Functions

        #endregion
}
