using ASI.Basecode.KnowledgeSiteAdminApp.Mvc;
using ASI.Basecode.Services.Interfaces;
using ASI.Basecode.Services.ServiceModels;
using AutoMapper;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace ASI.Basecode.KnowledgeSiteAdminApp.Controllers
{
    /// <summary>
    /// Home Controller
    /// </summary>
    public class HomeController : ControllerBase<HomeController>
    {
        private readonly IUserService _userService;
        /// <summary>
        /// Constructor
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="loggerFactory"></param>
        /// <param name="configuration"></param>
        /// <param name="localizer"></param>
        /// <param name="mapper"></param>
        public HomeController(IUserService userService,IHttpContextAccessor httpContextAccessor,
                              ILoggerFactory loggerFactory,
                              IConfiguration configuration,
                              IMapper mapper = null) : base(httpContextAccessor, loggerFactory, configuration, mapper)
        {
            _userService= userService;
        }

        /// <summary>
        /// Returns Home View.
        /// </summary>
        /// <returns> Home View </returns>

        public IActionResult Index()
        {
            return View();
        }

        public IActionResult UserMasterAdmin()
        {
            var data = _userService.GetUsers();
            return View("UserMasterAdmin", data);
        }
        public IActionResult AddModalUser()
        {
            return View("AddModalUser");
        }
        [HttpPost]
        public IActionResult AddModalUser(UserViewModel user)
        {
            _userService.AddUser(user);
            return RedirectToAction("UserMasterAdmin");
        }

        [HttpGet]
        public IActionResult UpdateModalUser(int Id)
        {
            var data = _userService.GetUser(Id);
            return View(data);
            
        }
        [HttpPost]
        public IActionResult UpdateModalUser(UserViewModel user)
        {
            _userService.UpdateUser(user, this.UserName);
            return RedirectToAction("UserMasterAdmin");

        }
        [HttpPost]
        public IActionResult DeleteModalUser(UserViewModel user)
        {
            _userService.DeleteUser(user);
            return RedirectToAction("UserMasterAdmin");
        }
    }
}
