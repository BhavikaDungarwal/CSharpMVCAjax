
using Frontend.Repository;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using frontend.Models;

namespace Frontend.Controllers
{
    public class AuthController : Controller
    {
        private readonly ILogger<AuthController> _logger;
        protected IAuthRepository _authRepository;

        public AuthController(ILogger<AuthController> logger, IAuthRepository authRepository)
        {
            _logger = logger;
            _authRepository = authRepository;
        }

        [HttpGet]
        public IActionResult Index()
        {
            return View();
        }


        [HttpPost]
        public IActionResult Login([FromBody] authModel auth)
        {
            var user = _authRepository.login(auth);
            if (user != null)
            {
                HttpContext.Session.SetInt32("c_userid", user.c_userid);
                if (user.c_role == 1)
                {
                    return Json(new { redirectToAction = "Admin/Product" });
                }
                else
                {
                    return Json(new { redirectToAction = "Product/Index" });
                }
            }
            else
            {
                return Json(new { error = "Invalid username or password." });
            }
        }

     

        [HttpPost]
        public IActionResult Signup([FromBody] authModel auth)
        {
            if (ModelState.IsValid)
            {
                _authRepository.signup(auth);
                return Json(new { redirectToAction = "Login" });
            }

            return Json(new { error = "Invalid model state." });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
