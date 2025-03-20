using AluguelEquipamentos.Negocio.Models;
using Microsoft.EntityFrameworkCore;

namespace AluguelEquipamentos.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext>options): base(options)
        {
            
        }
        public DbSet<EquipamentoModel> Equipamentos { get; set; }
        public DbSet<EnderecoModel> Endereco { get; set; }
        public DbSet<UsuarioModel> Usuarios { get; set; }
    }
}
