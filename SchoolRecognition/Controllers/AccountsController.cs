using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SchoolRecognition.Classes;
using SchoolRecognition.Models;
using SchoolRecognition.Services;
using SchoolRecognition.Helpers;
using Vereyon.Web;
using SchoolRecognition.ResourceParameters;
using Marvin.Cache.Headers;
using Microsoft.AspNetCore.Http;

namespace SchoolRecognition.Controllers
{
    public class AccountsController : Controller
    {
        private readonly IAccountsRepository _accountRepository;
        private readonly IFlashMessage _flashMessage;
        private readonly IEmailSender _emailSender;

        public AccountsController(IFlashMessage flashMessage, IAccountsRepository accountRepository, IEmailSender emailSender)
        {
            _accountRepository = accountRepository ??
               throw new ArgumentNullException(nameof(accountRepository));

            _flashMessage = flashMessage ?? throw new ArgumentNullException(nameof(flashMessage));
            _emailSender = emailSender ?? throw new ArgumentNullException(nameof(emailSender));
        }
        //[Route("")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Index(string orderBy, string searchQuery, int? pageNumber)
        {
            try
            {

                //if (HttpContext.Session.Get<string>("UserId") != null)
                //{
                    IEnumerable<int> pages = new List<int>();

                    var resourceParams = new UserResourceParameters()
                    {
                        PageNumber = pageNumber != null ? pageNumber.Value : 1,
                        SearchQuery = !String.IsNullOrWhiteSpace(searchQuery) ? searchQuery : null,
                        OrderBy = !String.IsNullOrWhiteSpace(orderBy) ? searchQuery : "FullNames",
                    };
                    //Instantiate CustomPagedList
                    PagedList<AccountsDto> objData = PagedList<AccountsDto>
                             .Create(Enumerable.Empty<AccountsDto>().AsQueryable(),
                                 resourceParams.PageNumber,
                                 resourceParams.PageSize);
                    switch (orderBy)
                    {
                        case "full_names":
                            resourceParams.OrderBy = "FullNames";
                            break;
                        case "full_names_desc":
                            resourceParams.OrderBy = "FullNames";
                            break;
                        case "email_address":
                            resourceParams.OrderBy = "EmailAddress";
                            break;
                        case "phone_no":
                            resourceParams.OrderBy = "PhoneNo";
                            break;
                        case "lp_no":
                            resourceParams.OrderBy = "LpNo";
                            break;
                        default:
                            resourceParams.OrderBy = "FullNames";
                            break;
                    }

                    var result = await _accountRepository.GetAccounts(resourceParams);

                    if (result != null)
                    {
                        var totalPages = result.TotalPages;

                        pages = Enumerable.Range(1, totalPages);

                        if (totalPages > 5)
                        {

                            if ((resourceParams.PageNumber + 2) >= totalPages)
                            {
                                pages = pages.Skip(totalPages - 5).Take(5).ToList();
                            }
                            else if ((resourceParams.PageNumber - 2) <= 1)
                            {
                                pages = pages.Take(5).ToList();
                            }
                            else
                            {
                                pages = pages.Skip(resourceParams.PageNumber - 2).Take(5).ToList();
                            }
                        }
                    }

                    objData = result;

                    ViewData["Pages"] = pages;
                    ViewData["OrderBy"] = orderBy == null ? "" : orderBy;
                    ViewData["SearchQuery"] = searchQuery == null ? "" : searchQuery;


                    return PartialView(objData);
                //} else
                //{
                //    return StatusCode(StatusCodes.Status403Forbidden);
                //}
            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }


        [Route("account_details/{id?}")]
        [HttpCacheExpiration(CacheLocation = CacheLocation.Public, MaxAge = 100)]
        [HttpCacheValidation(MustRevalidate = false)]
        public async Task<IActionResult> Details(Guid? id)
        {
            try
            {
                if (id == null || id.Value == Guid.Empty)
                {
                    return BadRequest();
                }
                var result = await _accountRepository.GetAccount(id.Value);
                if (result == null)
                {
                    return NotFound();
                }
                return PartialView(result);

            }
            catch (Exception)
            {
                return StatusCode(StatusCodes.Status500InternalServerError);
            }
        }

        public IActionResult Register()
        {
            return View();
        }

        

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Register(RegisterViewModel model)
        {
            if (ModelState.IsValid)
            {
                //check if the email address is in use
                bool checkEmail = await _accountRepository.GetAccountByMail(model.Email.Trim());

                if (checkEmail)
                {
                    _flashMessage.Danger("Account exists", "The Email Address is in ue by another user " +
                        "Please use the login page. You can use the forgot password page if you forgot your password");
                } else
                {

                    var result = await _accountRepository.CreateAccount(new AccountsCreateDto
                    {
                        Surname = model.Surname,
                        Othernames = model.OtherName,
                        PhoneNo = model.PhoneNo,
                        EmailAddress = model.Email,
                        Password = model.Password
                    });

                    if (result != null)
                    {
                        //get the email message 
                        string message = GetEmailMessage(result);
                        try
                        {
                            await _emailSender.SendEmailAsync(result.EmailAddress, "Account Created", message);
                            _flashMessage.Info("Account created successfully. " +
                            "An email has been sent to you containing your activation information");
                        }
                        catch (Exception)
                        {
                            return StatusCode(StatusCodes.Status408RequestTimeout);
                        }




                    }
                    else
                    {
                        _flashMessage.Danger("Account failed", "Account failed to create. " +
                            "Please try again later");
                    }
                }
            }
            return View();
        }

        public string GetEmailMessage(AccountsDto user)
        {
            //compose the message
            string message = "<p>Thank you for registering on the WAEC School Recognition System.</p>";
            message += "<p>The Email address used to register on this platform is:</p>";
            message += "<p>Email Address: " + user.EmailAddress + "</p>";

            string siteLink = GetBaseUrl();

            string tempLink = (siteLink.Substring(siteLink.Length - 1, 1) == "/") ? siteLink : siteLink + "/";
            tempLink += "Account/verify?Id=" + user.Id;
            message += "<p>Please <a href='" + siteLink + "'>Click here</a> to verify your registration</p>";
            message += "<p></p>"; ;

            //retur nthe message
            return message;
        }

        public string GetBaseUrl()
        {
            return $"{ Request.Scheme}://{ Request.Host }{ Request.PathBase}";
        }

    }
}