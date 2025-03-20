using AluguelEquipamentos.Data;
using AluguelEquipamentos.Negocio.Dtos;
using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using RabbitMQ.Client.Events;

namespace AluguelEquipamentos.Services.Login
{
    public class LoginService: Ilogin
    {
        private readonly ApplicationDbContext _context;
        private readonly ISenha _senha;
        private readonly ISessao _sessao;

        public LoginService(ApplicationDbContext context,ISenha senha,ISessao sessao)
        {
            _context = context;
            _senha = senha;
            _sessao = sessao;
        }

        public async Task<ResponseModel<UsuarioModel>> Login(LoginUsuarioDto loginUsuarioDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try
            {
                var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == loginUsuarioDto.Email);
                if (usuario == null)
                {
                    response.Mensagem = "Credencial inválida";
                    response.Status = false;
                    return response;

                }

                if (!_senha.VerificaSenha(loginUsuarioDto.Senha,usuario.SenhaHash,usuario.SenhaSolt))
                {
                    response.Mensagem = "Credencial inválida";
                    response.Status = false;
                    return response;
                }

                _sessao.CriarSessao(usuario);

                response.Mensagem = "Login efetuado com sucesso";
                return response;
            }
            catch (Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }
        public async Task<ResponseModel<UsuarioModel>> RegistrarUsuario(RegistroUsuarioDto registroUsuarioDto)
        {
            ResponseModel<UsuarioModel> response = new ResponseModel<UsuarioModel>();

            try 
            { 
                if(VerificarSeEmailExiste(registroUsuarioDto))
                {
                    response.Mensagem = "Email já cadastrado";
                    response.Status = false;
                    return response;

                }

                _senha.CriarSenhaHash(registroUsuarioDto.Senha, out byte[] senhaHash, out byte[] senhaSalt);

                var usuario = new UsuarioModel
                {
                    Nome = registroUsuarioDto.Nome,
                    Email = registroUsuarioDto.Email,
                    SenhaHash = senhaHash,
                    SenhaSolt = senhaSalt
                };

                _context.Usuarios.Add(usuario);
                await _context.SaveChangesAsync();

                response.Mensagem = "Usuário cadastrado com sucesso";
                return response;


            } catch(Exception ex)
            {
                response.Mensagem = ex.Message;
                response.Status = false;
                return response;
            }
        }

        private bool VerificarSeEmailExiste(RegistroUsuarioDto registroUsuarioDto)
        {
            var usuario = _context.Usuarios.FirstOrDefault(x => x.Email == registroUsuarioDto.Email);

            if (usuario != null)
            {
                return true;
            }

            return false;
        }
    }
}
