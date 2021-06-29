using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using AutoMapper;
using DevIO.Api.ViewModels;
using DevIO.Business.Intefaces;
using DevIO.Business.Models;
using Microsoft.AspNetCore.Mvc;

namespace Dev.Api.Controllers
{
    [Route("api/produtos")]
    public class ProdutosController : MainController
    {
        private readonly IProdutoRepository _produtoRepository;
        private readonly IProdutoService _produtoService;
        private readonly IMapper _mapper;
        public ProdutosController(INotificador Notificador, 
                                  IProdutoRepository produtoRepository, 
                                  IProdutoService produtoService,
                                  IMapper mapper) : base(Notificador)
        {
            _produtoRepository = produtoRepository;
            _produtoService = produtoService;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<IEnumerable<ProdutoViewModel>> ObterTodos(){
            return _mapper.Map<IEnumerable<ProdutoViewModel>>(await _produtoRepository.ObterProdutosFornecedores());
        }

        [HttpGet("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> ObterPorId(Guid id)
        {
            var produto = await ObterProdutoPorId(id);

            if(produto == null) return NotFound();

            return Ok(produto);
        }

        [HttpPost]
        public async Task<ActionResult<ProdutoViewModel>> Adicionar(ProdutoViewModel produto)
        {
            if(!ModelState.IsValid) return CustomResponse(ModelState);

            var imgNome = Guid.NewGuid() + "_" + produto.Imagem;

            if(!UploadArquivo(produto.ImagemUpload, imgNome)) return CustomResponse();

            produto.Imagem = imgNome;

            await _produtoService.Adicionar(_mapper.Map<Produto>(produto));

            return CustomResponse(produto);
        }

        [HttpDelete("{id:guid}")]
        public async Task<ActionResult<ProdutoViewModel>> Excluir(Guid id)
        {
            if(id == null) return NotFound();

            var produto = _produtoRepository.ObterPorId(id);

            if(produto == null) return NotFound();

            await _produtoService.Remover(id);

            return CustomResponse(produto);
        }

        private async Task<ProdutoViewModel> ObterProdutoPorId(Guid id)
        {
           return _mapper.Map<ProdutoViewModel>(await _produtoRepository.ObterPorId(id));
        }

        private bool UploadArquivo(string arquivo, string imgNome)
        {
            var imageDataByteArray = Convert.FromBase64String(arquivo);

            if(string.IsNullOrEmpty(arquivo))
            {
                ModelState.AddModelError(string.Empty, "Forneça um arquivo válido");
                return false;
            }

            var filePath = Path.Combine(Directory.GetCurrentDirectory(),"wwwroot/imgs",imgNome);

            if(System.IO.File.Exists(filePath))
            {
                ModelState.AddModelError(string.Empty, "Imagem com mesmo nome já existe no servidor");
                return false;
            }

            System.IO.File.WriteAllBytes(filePath, imageDataByteArray);
            return true;

        }
    }
}