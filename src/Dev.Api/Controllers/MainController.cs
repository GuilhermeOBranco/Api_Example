using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Notificacoes;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace Dev.Api.Controllers
{
    [ApiController]
    public abstract class MainController : ControllerBase
    {

        private readonly INotificador notificador;

        public MainController(INotificador Notificador)
        {
            notificador = Notificador;
        }

        protected bool OperacaoValida()
        {
            return !notificador.TemNotificacao();
        }

        protected ActionResult CustomResponse(object result = null)
        {
            if(OperacaoValida())
            {
              return Ok(new {
                  success = true,
                  data = result,
              });  
            }else
            {
                return BadRequest(new {
                    success = false,
                    errors = notificador.ObterNotificacoes().Select(n => n.Mensagem)
                });
            }
        }
        protected ActionResult CustomResponse(ModelStateDictionary modelState)
        {
            if(!modelState.IsValid) NotificarErroModelInvalida(modelState);
            return CustomResponse();
        }

        protected void NotificarErroModelInvalida(ModelStateDictionary modelState)
        {
            var erros = modelState.Values.SelectMany(e => e.Errors);

            foreach (var erro in erros)
            {
                var erroMessage = erro.Exception == null ? erro.ErrorMessage : erro.Exception.Message;
                NotificarErro(erroMessage);
            }
        }

        protected void NotificarErro(string mensagem)
        {
            notificador.Handle(new Notificacao(mensagem));
        }
    }

}