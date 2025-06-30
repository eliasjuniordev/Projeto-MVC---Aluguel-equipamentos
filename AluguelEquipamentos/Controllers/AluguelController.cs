using AluguelEquipamentos.Data;
using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using Microsoft.AspNetCore.Mvc;

namespace AluguelEquipamentos.Controllers
{
    public class AluguelController : Controller
    {
        readonly private ApplicationDbContext _context;
        private readonly RabbitMQService _rabbitMQService;
        private readonly ISessao _sessao;

        public AluguelController(ApplicationDbContext dbContext,ISessao sessao, RabbitMQService rabbitMQService)
        {

            _context = dbContext;
            _rabbitMQService = rabbitMQService;
            _sessao = sessao;
        }
        public IActionResult Index()
        {
            var usuario = _sessao.BuscarSessao();
            
            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }
            IEnumerable<EquipamentoModel> equipamentos = _context.Equipamentos;
            return View(equipamentos);
        }

        [HttpGet]
        public IActionResult Cadastrar()
        {
            var usuario = _sessao.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            return View();  
        }

        [HttpPost]
        public IActionResult Cadastrar(EquipamentoModel equipamento)
        {
            if (ModelState.IsValid)
            {
                _context.Add(equipamento);
                _context.SaveChanges();

                TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

                _rabbitMQService.SendMessage($"Novo aluguel cadastrado para o cliente {equipamento.Cliente}");

                return RedirectToAction("Index");
            }

            return View();
        }

        [HttpGet]
        public IActionResult Editar(int? id)
        {
            var usuario = _sessao.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

            if (id == null || id == 0)
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

                _rabbitMQService.SendMessage($"Alteração cadastro cliente {equipamento.Cliente}");

                TempData["MensagemSucesso"] = "Alteração realizada com sucesso!";
              return RedirectToAction("Index");
            }

            TempData["MensagemError"] = "Erro ao Relizar Edição Tente Novamente.";
            return View(equipamento);
        }

        [HttpGet]
        public IActionResult Excluir(int? id) 
        {
            var usuario = _sessao.BuscarSessao();

            if (usuario == null)
            {
                return RedirectToAction("Login", "Login");
            }

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

            _rabbitMQService.SendMessage($"Cadastro cliente Excluido {DateTime.Now} {equipamento.Cliente}");

            TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";

            return RedirectToAction("Index");

        }
    }
}
