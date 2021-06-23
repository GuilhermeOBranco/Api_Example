using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dev.Api.Controllers;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedorController : MainController
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository,
                                      IMapper mapper)
        {
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }
        
        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return Ok(fornecedor);
        }
    }
}