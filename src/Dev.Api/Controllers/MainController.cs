using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Api.Controllers
{
    [ApiController]
    public abstract class MainController: ControllerBase
    {
        //Validação de notificações, de erro
        //Validaçção de modelState
        //Validação da operação de negócios
    }

}