using System.Threading.Tasks;
using Dev.Api.Controllers;
using Dev.Api.ViewModels;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Api.Configurations
{
    [Route("api")]
    public class AuthController : MainController
    {
        private readonly SignInManager<IdentityUser> _signInManager;
        private readonly UserManager<IdentityUser> _userManager;
        public AuthController(INotificador Notificador,
                              SignInManager<IdentityUser> signInManager,
                              UserManager<IdentityUser> userManager) : base(Notificador)
        {
            _signInManager = signInManager;
            _userManager = userManager;
        }

        [HttpPost("registrar-conta")]
        public async Task<ActionResult> Registrar(RegisterUserViewModel usuario)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            var user = new IdentityUser
            {
                UserName = usuario.Email,
                Email = usuario.Email,
                EmailConfirmed = true
            };

            var result = await _userManager.CreateAsync(user);

            if (result.Succeeded)
            {
                await _signInManager.SignInAsync(user, false);
                return CustomResponse(user);
            }

            foreach (var erro in result.Errors)
            {
                System.Console.WriteLine(erro.Description);
            }

            return CustomResponse(usuario);
        }

        [HttpPost("login")]
        public async Task<ActionResult> Login(LoginUserViewModel usuario)
        {
            var result = _signInManager.PasswordSignInAsync(usuario.Email, usuario.Password, false, true);

            if(result.IsCompletedSuccessfully)
            {
                return CustomResponse(usuario);
            }

            return CustomResponse(usuario);
        }
    }
}