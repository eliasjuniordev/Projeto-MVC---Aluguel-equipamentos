using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Interfaces;
using AutoMapper;

namespace AluguelEquipamentos.Services
{
    public class EnderecoService:IEnderecoService
    {
        private readonly IMapper _mapper;
        private readonly IApiCep _ApiCep;

        public EnderecoService(IMapper mapper, IApiCep cepApi)
        {
            _mapper = mapper;
            _ApiCep = cepApi;
        }

        public async Task<ResponseGenerico<EnderecoResponse>> BuscarEndereco(string cep)
        {
            var endereco = await _ApiCep.BuscarEnderecoPorCEP(cep);
            return _mapper.Map<ResponseGenerico<EnderecoResponse>>(endereco);
        }
    }
}
