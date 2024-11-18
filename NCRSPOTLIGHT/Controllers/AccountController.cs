using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using EntitiesLayer.Models.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

using System.Runtime.CompilerServices;
using System.Security.Claims;
using System.Text.Encodings.Web;
using Microsoft.EntityFrameworkCore;
using Microsoft.VisualStudio.Web.CodeGenerators.Mvc.Templates.BlazorIdentity.Pages.Manage;

namespace NCRSPOTLIGHT.Controllers
{
    [Authorize]
    public class AccountController : Controller
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender emailSender;
        private readonly UrlEncoder _urlEncoder;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender, UrlEncoder urlEncoder,
            RoleManager<IdentityRole> roleManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            this.emailSender = emailSender;
            _urlEncoder = urlEncoder;
            _roleManager = roleManager;
        }
        #region Register & Confirm
        [AllowAnonymous]
        public async Task<IActionResult> Register(string returnUrl = null)
        {
            if (!_roleManager.RoleExistsAsync(SD.Admin).GetAwaiter().GetResult())
            {
                await _roleManager.CreateAsync(new IdentityRole(SD.Admin));
                await _roleManager.CreateAsync(new IdentityRole(SD.User));
                await _roleManager.CreateAsync(new IdentityRole(SD.QualityAssurance));
            }



            ViewData["ReturnUrl"] = returnUrl;
            var roles = await _roleManager.Roles.ToListAsync();
            RegisterViewModel registerViewModel = new();

            foreach(var item in roles)
            {
                registerViewModel.RolesList.Add(
                    new RoleSelection
                    {
                        RoleName = item.Name,
                        IsSelected = false
                    }
                    );
            }
            return View(registerViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Register(RegisterViewModel registerViewModel, string returnUrl = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            var user = new ApplicationUser();
            IdentityResult result;
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                user = new ApplicationUser
                {
                    UserName = registerViewModel.Email,
                    Email = registerViewModel.Email,
                    FirstName = registerViewModel.FirstName,
                    LastName = registerViewModel.LastName,
                };
                result = await _userManager.CreateAsync(user, registerViewModel.Password);
                if (result.Succeeded)
                {
                    foreach (var role in registerViewModel.RolesList)
                    {

                        if (registerViewModel.RolesList != null)
                        {
                            await _userManager.AddToRolesAsync(user, registerViewModel.RolesList.Where(r => r.IsSelected == true).Select(y => y.RoleName));
                        }
                        else
                        {
                            await _userManager.AddToRoleAsync(user, SD.User);

                        }
                    }
                    var code = await _userManager.GenerateEmailConfirmationTokenAsync(user);

                    var callbackUrl = Url.Action("ConfirmEmail", "Account", new
                    {
                        userid = user.Id,
                        code
                    }, protocol: HttpContext.Request.Scheme);


                    await emailSender.SendEmailAsync(registerViewModel.Email, "Confirm - Identity Manager",
                        $"Please confirm your email by clicking this link: <a href='{callbackUrl}'>Click Here</a>");
                    //await _signInManager.SignInAsync(user, isPersistent: false);
                    return LocalRedirect(returnUrl);
                }
                AddErrors(result);
            }
            result = await _userManager.AddToRolesAsync(user,
                registerViewModel.RolesList.Where(r => r.IsSelected == true).Select(y => y.RoleName)); ;

            return View(registerViewModel);
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> ConfirmEmail(string code, string userId)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByIdAsync(userId);
                if (user == null)
                {
                    return View("Error");
                }
                var result = await _userManager.ConfirmEmailAsync(user, code);

                if (result.Succeeded)
                {
                    return View();
                }
            }
            return View("Error");
        }
        #endregion



        #region Forgot Password
        [HttpGet]
        [AllowAnonymous]

        public IActionResult ForgotPassword()
        {
            return View();
        }

        [HttpPost]
        [AllowAnonymous]

        public async Task<IActionResult> ForgotPassword(ForgotPasswordViewModel forgotPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(forgotPasswordViewModel.Email);
                if (user == null)
                {
                    return RedirectToAction("ForgotPasswordConfirmation");
                }
                var code = await _userManager.GeneratePasswordResetTokenAsync(user);
                var callbackUrl = Url.Action("ResetPassword", "Account", new
                {
                    userid = user.Id,
                    code
                }, protocol: HttpContext.Request.Scheme);

                await emailSender.SendEmailAsync(forgotPasswordViewModel.Email, "Reset Password - Identity Manager",
                    $"Please reset your password by clicking this link: <a href='{callbackUrl}'>Click Here</a>");

                return RedirectToAction(nameof(ForgotPasswordConfirmation));
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult ResetPassword(string code = null)
        {
            return code == null ? View("Error") : View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> ResetPassword(ResetPasswordViewModel resetPasswordViewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.FindByEmailAsync(resetPasswordViewModel.Email);
                if (user == null)
                {
                    return RedirectToAction(nameof(ResetPassword));
                }
                var result = await _userManager.ResetPasswordAsync(
                            user, resetPasswordViewModel.Code, resetPasswordViewModel.Password);

                if (result.Succeeded)
                {
                    return RedirectToAction(nameof(ResetPasswordConfirmation));
                }
                AddErrors(result);
            }
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult ResetPasswordConfirmation()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult ForgotPasswordConfirmation()
        {
            return View();
        }
        #endregion

        #region Login
        [AllowAnonymous]

        public async Task<IActionResult> Login(string returnUrl = null)
        {
            
            ViewData["ReturnUrl"] = returnUrl;

            LoginViewModel loginViewModel = new();
            return View(loginViewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> Login(LoginViewModel loginViewModel, string returnUrl = null,  string email = null)
        {
            ViewData["ReturnUrl"] = returnUrl;
            returnUrl = returnUrl ?? Url.Content("~/");

            if (email != null)
            {
                ApplicationUser user = await _userManager.FindByEmailAsync(email);

                if (user is null)
                {
                    return NotFound();
                }
                var result = await _signInManager.PasswordSignInAsync(user.UserName, password: "password", isPersistent: false, lockoutOnFailure: false);
                if (result.Succeeded && returnUrl != null)
                {
                    if(user.Email == "admin@email.com" && user.Role != "Admin")
                    {
                        _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (user.Email == "qa@email.com" && user.Role != "QualityAssurance")
                    {
                        _userManager.AddToRoleAsync(user, "QualityAssurance");
                    }
                    else if (user.Email == "engineer@email.com" && user.Role != "Engineer")
                    {
                        _userManager.AddToRoleAsync(user, "Engineer");
                    }
                    else if (user.Email == "superadmin@email.com" && user.Role != "SuperAdmin")
                    {
                        _userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                    else
                    {
                        _userManager.AddToRoleAsync(user, "BasicUser");
                    }

                    return LocalRedirect(returnUrl);
                }
                else if (result.Succeeded && returnUrl is null)
                {
                    if (user.Email == "admin@email.com" && user.Role != "Admin")
                    {
                        _userManager.AddToRoleAsync(user, "Admin");
                    }
                    else if (user.Email == "qa@email.com" && user.Role != "QualityAssurance")
                    {
                        _userManager.AddToRoleAsync(user, "QualityAssurance");
                    }
                    else if (user.Email == "engineer@email.com" && user.Role != "Engineer")
                    {
                        _userManager.AddToRoleAsync(user, "Engineer");
                    }
                    else if (user.Email == "superAdmin@email.com" && user.Role != "SuperAdmin")
                    {
                        _userManager.AddToRoleAsync(user, "SuperAdmin");
                    }
                    else
                    {
                        _userManager.AddToRoleAsync(user, "BasicUser");
                    }
                    return RedirectToAction("Index", "Home");
                }
                return Error();
            }

            if (ModelState.IsValid)
            {

                var result = await _signInManager.PasswordSignInAsync(loginViewModel.Email, loginViewModel.Password,
                    loginViewModel.RememberMe, lockoutOnFailure: true);
                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }
                if (result.RequiresTwoFactor)
                {
                    return RedirectToAction(nameof(VerifyAuthenticatorCode), new { returnUrl, loginViewModel.RememberMe });
                }
                if (result.IsLockedOut)
                {
                    return View("Lockout");
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Invalid login attempt");
                }
            }
            return View(loginViewModel);
        }

        [HttpGet]
        [AllowAnonymous]

        public async Task<IActionResult> VerifyAuthenticatorCode(bool rememberMe, string returnUrl = null)
        {
            var user = await _signInManager.GetTwoFactorAuthenticationUserAsync();
            if (user == null)
            {
                return View("Error");
            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(new VerifyAuthenticatorViewModel
            {
                ReturnUrl = returnUrl,
                RememberMe = rememberMe,

            });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [AllowAnonymous]

        public async Task<IActionResult> VerifyAuthenticatorCode(VerifyAuthenticatorViewModel verifyAuthenticatorViewModel)
        {
            verifyAuthenticatorViewModel.ReturnUrl = verifyAuthenticatorViewModel.ReturnUrl ?? Url.Content("~/");
            if (!ModelState.IsValid)
            {
                return View(verifyAuthenticatorViewModel);
            }
            var result = await _signInManager.TwoFactorAuthenticatorSignInAsync(
                verifyAuthenticatorViewModel.Code, verifyAuthenticatorViewModel.RememberMe,
                rememberClient: false);
            if (result.Succeeded)
            {
                return LocalRedirect(verifyAuthenticatorViewModel.ReturnUrl);
            }
            if (result.IsLockedOut)
            {
                return View("Lockout");
            }
            else
            {
                ModelState.AddModelError(string.Empty, "Invalid login attempt");
                return View(verifyAuthenticatorViewModel);
            }
        }
        #endregion

        #region TFA
        [HttpGet]
        [Authorize]
        public async Task<IActionResult> EnableAuthentication()
        {
            string AuthenticatorUriFormat = "otpauth://totp/{0}:{1}?secret={2}&issuer={0}&digits=6";
            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            var token = await _userManager.GetAuthenticatorKeyAsync(user);
            string AuthUri = string.Format(AuthenticatorUriFormat, _urlEncoder.Encode("IdentityManager-RA"),
                _urlEncoder.Encode(user.Email), token);
            var model = new TwoFactorAuthenticationVIewModel() { Token = token, QRCodeUrl = AuthUri };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> EnableAuthentication(TwoFactorAuthenticationVIewModel twoFactorAuthenticationVIewModel)
        {
            if (ModelState.IsValid)
            {
                var user = await _userManager.GetUserAsync(User);
                var succeeded = await _userManager.VerifyTwoFactorTokenAsync(user, _userManager.Options.Tokens.AuthenticatorTokenProvider,
                    twoFactorAuthenticationVIewModel.Code);
                if (succeeded)
                {
                    await _userManager.SetTwoFactorEnabledAsync(user, enabled: true);
                }
                else
                {
                    ModelState.AddModelError(string.Empty, "Unable to verify your two factor authentication code");
                }
                return RedirectToAction(nameof(AutheticatorConfirmation));
            }
            return View("Error");
        }

        [HttpGet]
        public IActionResult AutheticatorConfirmation()
        {
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> RemoveAuthenticator()
        {
            var user = await _userManager.GetUserAsync(User);
            await _userManager.ResetAuthenticatorKeyAsync(user);
            await _userManager.SetTwoFactorEnabledAsync(user, false);
            return RedirectToAction("Index", "Home");
        }
        #endregion

        #region External Login
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLogin(string provider, string returnUrl = null)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account", new { returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return Challenge(properties, provider);
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");
                return View(nameof(Login));
            }

            var info = await _signInManager.GetExternalLoginInfoAsync();
            if (info == null)
            {
                return RedirectToAction(nameof(Login));
            }

            var result = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey,
                isPersistent: false, bypassTwoFactor: true);
            if (result.Succeeded)
            {
                await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                return LocalRedirect(returnUrl);
            }
            if (result.RequiresTwoFactor)
            {
                return RedirectToAction(nameof(VerifyAuthenticatorCode), new { returnUrl });
            }
            else
            {
                ViewData["ReturnUrl"] = returnUrl;
                ViewData["ProviderDisplayName"] = info.ProviderDisplayName;
                return View("ExternalLoginConfirmation", new ExternalLoginConfirmationViewModel
                {
                    Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                    Name = info.Principal.FindFirstValue(ClaimTypes.Name),

                });
            }
        }
        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> ExternalLoginConfirmation(ExternalLoginConfirmationViewModel model, string returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var info = await _signInManager.GetExternalLoginInfoAsync();
                if (info == null)
                {
                    return View("Error");
                }
                var user = new ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email
                };


                var result = await _userManager.CreateAsync(user);
                if (result.Succeeded)
                {

                    await _userManager.AddToRoleAsync(user, SD.User);
                    result = await _userManager.AddLoginAsync(user, info);
                    if (result.Succeeded)
                    {
                        await _signInManager.SignInAsync(user, isPersistent: false);
                        await _signInManager.UpdateExternalAuthenticationTokensAsync(info);
                        return LocalRedirect(returnUrl);
                    }
                }

                AddErrors(result);

            }
            ViewData["ReturnUrl"] = returnUrl;
            return View(model);
        }

        #endregion

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Lockout()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult NoAccess()
        {
            return View();
        }

        [HttpGet]
        [AllowAnonymous]

        public IActionResult Error()
        {
            return View();
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        private void AddErrors(IdentityResult result)
        {
            foreach (var error in result.Errors)
            {
                ModelState.AddModelError(string.Empty, error.Description);
            }
        }
    }
}
