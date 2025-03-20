using AluguelEquipamentos.Negocio.Models;

namespace AluguelEquipamentos.Negocio.Interfaces
{
    public interface ISessao
    {
        UsuarioModel BuscarSessao();
        void CriarSessao(UsuarioModel usuarioModel);
        void RemoverSessao();
    }
}
