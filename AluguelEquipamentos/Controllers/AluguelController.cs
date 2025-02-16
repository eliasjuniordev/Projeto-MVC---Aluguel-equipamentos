using AluguelEquipamentos.Data;
using AluguelEquipamentos.Models;
using Microsoft.AspNetCore.Mvc;

namespace AluguelEquipamentos.Controllers
{
    public class AluguelController : Controller
    {
        readonly private ApplicationDbContext _context;

        public AluguelController(ApplicationDbContext dbContext)
        {
            _context = dbContext;
        }
        public IActionResult Index()
        {
            IEnumerable<EquipamentoModel> equipamentos = _context.Equipamentos;
            return View(equipamentos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            return View();  
        }

        [HttpPost]
        public IActionResult Cadastrar(EquipamentoModel equipamentos)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipamentos);
                _context.SaveChanges();

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            if(id == null || id == 0)
            {
                return NotFound();
            }

            EquipamentoModel aluguel = _context.Equipamentos.FirstOrDefault(x=>x.Id == id); 
            
            if (aluguel == null)
            {
                return NotFound();
            }


            return View(aluguel);
        }

        [HttpPost]
        public IActionResult Editar(EquipamentoModel equipamento)
        {
            if (ModelState.IsValid) 
            { 
              _context.Equipamentos.Update(equipamento);
              _context.SaveChanges();
              return RedirectToAction("Index");
            }

            return View(equipamento);
        }

        [HttpGet]
        public IActionResult Excluir(int? id) 
        {

            if (id == null || id == 0)
            {
                return NotFound();
            }

            EquipamentoModel equipamentoModel = _context.Equipamentos.FirstOrDefault(x=>x.Id == id);

            if (equipamentoModel == null)
            {
                return NotFound();
            }

            return View(equipamentoModel);

        }

        [HttpPost]
        public IActionResult Excluir(EquipamentoModel equipamento)
        {
            if (equipamento == null) 
            {
                return NotFound();
            }

            _context.Remove(equipamento);
            _context.SaveChanges();
            return RedirectToAction("Index");

        }


    }
}
