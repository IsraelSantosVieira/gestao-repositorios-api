using Microsoft.AspNetCore.Mvc;
using RepositorioApp.Domain.ViewsModels.Emails;
namespace RepositorioApp.Api.Controllers
{
    [ApiExplorerSettings(IgnoreApi = true)]
    [Route("emails")]
    public class EmailsController : Controller
    {
        [HttpGet("new-user")]
        public IActionResult NewUser()
        {
            var newUserEmailVm = new NewUserEmailVm
            {
                Email = "admin@email.com.br",
                Name = "Admin App",
                Password = "123654"
            };

            return View(newUserEmailVm);
        }

        [HttpGet("reset-password")]
        public IActionResult PasswordRecoverRequest()
        {
            var passwordRecoverRequestEmailVm = new PasswordRecoverRequestEmailVm
            {
                Email = "admin@email.com.br",
                Name = "Admin App",
                Code = "123654"
            };

            return View(passwordRecoverRequestEmailVm);
        }
    }
}
