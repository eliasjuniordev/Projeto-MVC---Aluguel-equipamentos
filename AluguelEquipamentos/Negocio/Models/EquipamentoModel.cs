using System.ComponentModel.DataAnnotations;

namespace AluguelEquipamentos.Negocio.Models
{
    public class EquipamentoModel
    {
        
        public int Id { get; set; }
        [Required (ErrorMessage ="Digite o nome do cliente")] 
        public string Cliente { get; set; }
        [Required(ErrorMessage = "Digite o CNPJ")]
        public string CNPJ { get; set;}
        [Required(ErrorMessage = "Digite qual equipamento está sendo alugado")]
        public string EquipamentoAlugado { get; set; }
        public DateTime DateInicial{ get; set; } = DateTime.Now;

    }
}
