using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using identity.fitness_pro.ru.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Security.Claims;

namespace identity.fitness_pro.ru.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        //private readonly ILogger<RegisterModel> _logger;
        //private readonly IEmailSender _emailSender;

        public RegisterModel(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            RoleManager<IdentityRole> roleManager
            //ILogger<RegisterModel> logger,
            //IEmailSender emailSender
            )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;

            //_logger = logger;
            //_emailSender = emailSender;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public class InputModel
        {
            [Display(Name = "BirthDay")]
            public DateTime Birthday { get; set; }

            [Required]
            [Display(Name = "UserName")]
            public string UserName { get; set; }

            [Display(Name = "Adress")]
            public string Adress { get; set; }

            [Display(Name = "Picture")]
            public string Picture { get; set; }

            [Display(Name = "Skype")]
            public string Skype { get; set; }

            [Required]
            [Display(Name = "Phone")]
            public string PhoneNumber { get; set; }

            [Required]
            [EmailAddress]
            [Display(Name = "Email")]
            public string Email { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }

        public void OnGet(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            //await _roleManager.CreateAsync(new IdentityRole("kld-admin"));

            //var role = await _roleManager.FindByNameAsync("kld-admin");

            //await _roleManager.AddClaimAsync(role, new Claim("Permission", "projects.view"));

            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                //var user = new ApplicationUser { UserName = Input.Email, Email = Input.Email };

                var user = new ApplicationUser
                {
                    UserName = Input.UserName,
                    Email = Input.Email,
                    Birthdate = Input.Birthday,
                    Address = Input.Adress,
                    Picture = Input.Picture,
                    PhoneNumber = Input.PhoneNumber
                };

                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    if (!String.IsNullOrEmpty(Input.Skype))
                        await _userManager.AddClaimAsync(user, new Claim("Messanger.Skype", Input.Skype, ClaimValueTypes.String, "SelfIssuer"));

                    //var rrs = await _userManager.AddToRoleAsync(user, "kld-admin");

                    //_logger.LogInformation("User created a new account with password.");

                    //var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);
                    //var callbackUrl = Url.Page(
                    //    "/Account/ConfirmEmail",
                    //    pageHandler: null,
                    //    values: new { userId = user.Id, code = code },
                    //    protocol: Request.Scheme);

                    //await _emailSender.SendEmailAsync(Input.Email, "Confirm your email",
                    //    $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl)}'>clicking here</a>.");

                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
