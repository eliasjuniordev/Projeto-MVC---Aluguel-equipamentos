using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Models;

namespace AluguelEquipamentos.Negocio.Interfaces
{
    public interface IApiCep
    {
        Task<ResponseGenerico<EnderecoModel>> BuscarEnderecoPorCEP(string cep);
    }
}
