using AluguelEquipamentos.Negocio.Dtos;

namespace AluguelEquipamentos.Negocio.Interfaces
{
    public interface IEnderecoService
    {
        Task<ResponseGenerico<EnderecoResponse>> BuscarEndereco(string cep);
    }
}
