using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Dev.Api.Controllers;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using DevIO.Business.Services;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Api.Controllers
{
    [Route("api/[controller]")]
    public class FornecedorController : MainController
    {

        private readonly IFornecedorRepository _fornecedorRepository;
        private readonly IFornecedorService _fornecedorService;
        private readonly IMapper _mapper;

        public FornecedorController(IFornecedorRepository fornecedorRepository,
                                    IFornecedorService fornecedorService,
                                    IMapper mapper,
                                    INotificador notificador) : base(notificador)
        {
            _fornecedorService = fornecedorService;
            _mapper = mapper;
            _fornecedorRepository = fornecedorRepository;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<FornecedorViewModel>>> ObterTodos()
        {
            var fornecedor = _mapper.Map<IEnumerable<FornecedorViewModel>>(await _fornecedorRepository.ObterTodos());
            return Ok(fornecedor);
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> ObterPorId(Guid id)
        {
            var fornecedor = _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterPorId(id));

            if (fornecedor == null) return NotFound();
            return Ok(fornecedor);
        }

        [HttpPost]
        public async Task<ActionResult<FornecedorViewModel>> Post(FornecedorViewModel fornecedorVM)
        {
            if (!ModelState.IsValid) return CustomResponse(ModelState);

            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(fornecedorVM);
                await _fornecedorService.Adicionar(fornecedor);
                return CustomResponse(fornecedorVM);
            }
            catch (Exception ex)
            {
                return CustomResponse(fornecedorVM);
            }
        }
        [HttpPut]
        public async Task<ActionResult<FornecedorViewModel>> Post(Guid id, FornecedorViewModel fornecedorVM)
        {
            if (id != fornecedorVM.Id) return BadRequest();

            try
            {
                var fornecedor = _mapper.Map<Fornecedor>(fornecedorVM);
                await _fornecedorRepository.Atualizar(fornecedor);
                return Ok(fornecedorVM);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<FornecedorViewModel>> Delete(Guid id)
        {
            var fornecedor = await ObterFornecedorPorId(id);

            if (fornecedor == null) return NotFound();

            try
            {
                var retorno = await _fornecedorService.Remover(fornecedor.Id);
                return Ok(retorno);
            }
            catch (Exception ex)
            {
                throw new Exception(ex.ToString());
            }
        }

        [NonAction]
        public async Task<FornecedorViewModel> ObterFornecedorPorId(Guid id)
        {
            return _mapper.Map<FornecedorViewModel>(await _fornecedorRepository.ObterFornecedorEndereco(id));
        }
    }
}