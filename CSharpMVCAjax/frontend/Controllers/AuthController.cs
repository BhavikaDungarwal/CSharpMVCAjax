
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
        public IActionResult register(authModel auth)
        {
            _authRepository.signup(auth);
            return Json(new{success=true});
        }

        [HttpPost]
        public IActionResult login(authModel auth)
        {
            var data = _authRepository.login(auth);
            // HttpContext.Session.SetInt32("userId",data.c_userid);
            // return Json(new {isAdmin = data.c_role != 0});
            return Json(new {success = true});
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}
