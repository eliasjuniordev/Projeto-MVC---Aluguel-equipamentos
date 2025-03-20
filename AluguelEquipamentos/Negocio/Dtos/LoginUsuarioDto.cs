using System.ComponentModel.DataAnnotations;

namespace AluguelEquipamentos.Negocio.Dtos
{
    public class LoginUsuarioDto
    {
        [Required(ErrorMessage ="Digite o Email!")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Digite a Senha!")]
        public string Senha { get; set; }
    }
}
