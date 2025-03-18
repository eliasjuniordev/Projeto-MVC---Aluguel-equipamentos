using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Models;
using AutoMapper;

namespace AluguelEquipamentos.Data.Mappings
{
    public class EnderecoMapping : Profile
    {
        public EnderecoMapping()
        {
            CreateMap(typeof(ResponseGenerico<>), typeof(ResponseGenerico<>));
            CreateMap<EnderecoResponse, EnderecoModel>();
            CreateMap<EnderecoModel, EnderecoResponse>();
        }
    }
}
