using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Models;

namespace AluguelEquipamentos.Negocio.Interfaces
{
    public interface Ilogin
    {
        Task<ResponseModel<UsuarioModel>> RegistrarUsuario(RegistroUsuarioDto registroUsuarioDto);
        Task<ResponseModel<UsuarioModel>> Login(LoginUsuarioDto loginUsuarioDto);
    }
}
