using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AluguelEquipamentos.Negocio.Models
{
    public class EnderecoModel
    {
        [Key]
        public int IdEndereco { get; set; }
        [Column("cep")]
        [Required(ErrorMessage = "Digite o Cep")]
        public string? cep { get; set; }
        [Column("estado")]
        public string? state { get; set; }

        [Column("cidade")]
        public string? city { get; set; }

        [Column("bairro")]
        public string? neighborhood { get; set; }

        [Column("rua")]
        public string? street { get; set; }

        [Column("servico")]
        public string? service { get; set; }
    }
}
