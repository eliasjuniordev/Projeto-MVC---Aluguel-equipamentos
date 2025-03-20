using AluguelEquipamentos.Data;
using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class EnderecoController : Controller
{
    private readonly IEnderecoService _enderecoService;
    readonly private ApplicationDbContext _context;
    readonly private ISessao _sessao;


    public EnderecoController(IEnderecoService enderecoService, ApplicationDbContext dbContext,ISessao sessao)
    {
        _context = dbContext;
        _enderecoService = enderecoService;
        _sessao = sessao;
    }

    public IActionResult Index()
    {
        var usuario = _sessao.BuscarSessao();

        if (usuario == null)
        {
            return RedirectToAction("Login", "Login");
        }
        IEnumerable<EnderecoModel> enderecos = _context.Endereco;
        return View(enderecos);
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
    public IActionResult Cadastrar(EnderecoModel endereco)
    {
        if (ModelState.IsValid)
        {
            _context.Add(endereco);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Cadastro realizado com sucesso!";

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

       EnderecoModel endereco = _context.Endereco.FirstOrDefault(x => x.IdEndereco == id);

        if (endereco == null)
        {
            return NotFound();
        }

        return View(endereco);
    }

    [HttpPost]
    public IActionResult Editar(EnderecoModel endereco)
    {
        if (ModelState.IsValid)
        {
            _context.Endereco.Update(endereco);
            _context.SaveChanges();

            TempData["MensagemSucesso"] = "Alteração realizada com sucesso!";
            return RedirectToAction("Index");
        }

        TempData["MensagemError"] = "Erro ao Relizar Edição Tente Novamente.";
        return View(endereco);
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

        EnderecoModel endereco = _context.Endereco.FirstOrDefault(x => x.IdEndereco == id);

        if (endereco == null)
        {
            return NotFound();
        }

        return View(endereco);

    }

    [HttpPost]
    public IActionResult Excluir(EnderecoModel endereco)
    {
        if (endereco == null)
        {
            return NotFound();
        }

        _context.Remove(endereco);
        _context.SaveChanges();

        TempData["MensagemSucesso"] = "Exclusão realizada com sucesso!";

        return RedirectToAction("Index");

    }

    [HttpPost]
    public async Task<IActionResult> BuscarEndereco([FromBody] JsonElement jsonData)
    {
        var usuario = _sessao.BuscarSessao();

        if (usuario == null)
        {
            return RedirectToAction("Login", "Login");
        }
        if (!jsonData.TryGetProperty("cep", out JsonElement cepElement))
        {
            return BadRequest(new { mensagem = "CEP inválido." });
        }

        string cep = cepElement.GetString();

        if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
        {
            return BadRequest(new { mensagem = "CEP inválido." });
        }

        var response = await _enderecoService.BuscarEndereco(cep);

        if (response?.DadosRetorno == null)
        {
            return NotFound(new { mensagem = "Endereço não encontrado." });
        }

        var endereco = new
        {
            cep = response.DadosRetorno.CEP,
            street = response.DadosRetorno.Street,
            neighborhood = response.DadosRetorno.Neighborhood,
            city = response.DadosRetorno.City,
            state = response.DadosRetorno.State
        };

        return Json(endereco);
    }

}
