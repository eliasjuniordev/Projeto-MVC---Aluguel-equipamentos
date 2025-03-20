using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using Newtonsoft.Json;

namespace AluguelEquipamentos.Services.Sessao
{
    public class SessaoService : ISessao

    {
        private readonly IHttpContextAccessor _contextAccessor;
        public SessaoService(IHttpContextAccessor accessor)
        {
          
            _contextAccessor = accessor;
         
        }
        public UsuarioModel BuscarSessao()
        {
            var sessaoUsuario = _contextAccessor.HttpContext.Session.GetString("SessaoUsuario");
            if (string.IsNullOrEmpty(sessaoUsuario))
            {
                return null;
            }
            return JsonConvert.DeserializeObject<UsuarioModel>(sessaoUsuario);
        }

        public void CriarSessao(UsuarioModel usuarioModel)
        {
            var usuarioJson = JsonConvert.SerializeObject(usuarioModel);
            _contextAccessor.HttpContext.Session.SetString("SessaoUsuario",usuarioJson);
        }

        public void RemoverSessao()
        {
            _contextAccessor.HttpContext.Session.Remove("SessaoUsuario");
        }
    }
}
