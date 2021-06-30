using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using DevIO.Business.Models;

namespace DevIO.Business.Intefaces
{
    public interface IFornecedorRepository : IRepository<Fornecedor>
    {
        Task<Fornecedor> ObterFornecedorEndereco(Guid id);
        
        Task<List<Fornecedor>> ObterFornecedorProduto();

        Task<Fornecedor> ObterFornecedorProdutosEndereco(Guid id);
    }
}