using ChocolateFanClub.Data;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using ChocolateFanClub.Data;
using ChocolateFanClub.Models;
using ChocolateFanClub.ViewModels;

namespace ChocolateFanClub.Controllers
{
    public class AccountController : Controller
    {
        // manager class provides extensions and code to handle authentication controllers
        private readonly UserManager<AppUser> _userManager;
        private readonly SignInManager<AppUser> _signInManager;
        
        // adding in the parameters which relate to the above classes is called dependency injection
        public AccountController(UserManager<AppUser> userManager, SignInManager<AppUser> signInManager)
        {
            _userManager = userManager;
            _signInManager = signInManager;
        }
        public IActionResult Login()
        {
            // this is to provide login credentials in case the user refreshes the page while logging in, this prevents them from having to rewrite credentials
            // not required but useful
            var response = new LoginViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel loginViewModel)
        {
            if (!ModelState.IsValid) return View();
            // this finds a user based on the email provided
            var user = await _userManager.FindByEmailAsync(loginViewModel.EmailAddress);
            if (user != null)
            {
                // this checks the password provided by the user
                var passwordCheck = await _userManager.CheckPasswordAsync(user, loginViewModel.Password);
                if (passwordCheck)
                {
                    // password correct and user is signed in
                    var result = await _signInManager.PasswordSignInAsync(user, loginViewModel.Password, false, false);
                    if (result.Succeeded)
                    {
                        // redirects to the homepage
                        return RedirectToAction("Index", "Home");
                    }
                }
                TempData["Error"] = "Wrong credentials. Please try again.";
                return View(loginViewModel);
            }
            // user not found
            TempData["Error"] = "Wrong credentials. Please try again.";
            return View(loginViewModel);

        }
        public IActionResult Register()
        {
            // this is to provide login credentials in case the user refreshes the page while logging in, this prevents them from having to rewrite credentials
            // not required but useful
            var response = new RegisterViewModel();
            return View(response);
        }
        [HttpPost]
        public async Task<IActionResult> Register(RegisterViewModel registerViewModel)
        {
            if (!ModelState.IsValid) return View(registerViewModel);
            // this is to check whether the user already exists
            var user = await _userManager.FindByEmailAsync(registerViewModel.EmailAddress);
            if (user != null)
            {
                TempData["Error"] = "This email address is already in use";
                return View(registerViewModel);
            }
            var newUser = new AppUser()
            {
                Email = registerViewModel.EmailAddress,
                UserName = registerViewModel.EmailAddress,
            };
            var newUserResponse = await _userManager.CreateAsync(newUser, registerViewModel.Password);

            if (newUserResponse.Succeeded)
            {
                // this adds the "user" role to the new user
                // admin role can be used when making larger scale applications
                await _userManager.AddToRoleAsync(newUser, UserRoles.User);
                return RedirectToAction("Login");
            }
            TempData["Error"] = "Wrong credentials. Please try again." +
             " Passwords must include non [A-Z,0-9] characters, at least 1 upper case character. and must be longer than 10 characters";
            return View(registerViewModel);



        }
        [HttpPost]
        public async Task<IActionResult> Logout()
        {
            // this performs the sign out action
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
    }
}
