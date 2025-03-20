using System.ComponentModel.DataAnnotations;

namespace AluguelEquipamentos.Negocio.Dtos
{
    public class RegistroUsuarioDto
    {
        [Required(ErrorMessage = "O campo nome é obrigatório")]
        public string Nome { get; set; }
        [Required(ErrorMessage = "O campo email é obrigatório")]
        public string Email { get; set; }
        [Required(ErrorMessage = "O campo senha é obrigatório")]
        public string Senha { get; set; }
        [Required(ErrorMessage = "O campo confirma senha é obrigatório"),
        Compare("Senha", ErrorMessage = "As senhas não estão iguais.")]
        public string ConfirmaSenha { get; set; }
    }

}

