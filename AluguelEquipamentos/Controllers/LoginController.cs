using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace AluguelEquipamentos.Controllers
{
    public class LoginController : Controller
    {
        private readonly Ilogin _login;  
        private readonly ISessao _sessao;
        public LoginController(Ilogin ilogin,ISessao sessao)
        {
            _login = ilogin;
            _sessao = sessao;
        }
        public IActionResult Login()
        {
            var usuario = _sessao.BuscarSessao();

            if (usuario != null)
            {
                return RedirectToAction("Index", "Home");
            }
            return View();
        }

        public IActionResult Logout()
        {
            _sessao.RemoverSessao();
            return RedirectToAction("Login");
                
        }


        public IActionResult Registrar()
        {

            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registrar(RegistroUsuarioDto registroUsuarioDto)
        {
           if(ModelState.IsValid)
           {
              var usuario = await _login.RegistrarUsuario(registroUsuarioDto);
                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                }
                else
                {
                    TempData["MensagemError"] = usuario.Mensagem;
                    return View(registroUsuarioDto);
                }
                return RedirectToAction("Index");
            }
            else
            {
                return View(registroUsuarioDto);
            }
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUsuarioDto loginUsuarioDto)
        {
            if (ModelState.IsValid) 
            { 
               var usuario = await _login.Login(loginUsuarioDto);

                if (usuario.Status)
                {
                    TempData["MensagemSucesso"] = usuario.Mensagem;
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    TempData["MensagemError"] = usuario.Mensagem;
                    return View(loginUsuarioDto);
                }
               
            }
            else 
            { 
               
                return View(loginUsuarioDto);
            
            }
        }
    }
}
