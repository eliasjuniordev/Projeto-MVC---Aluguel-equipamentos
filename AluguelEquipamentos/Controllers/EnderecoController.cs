using AluguelEquipamentos.Data;
using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using Microsoft.AspNetCore.Mvc;
using System.Text.Json;

public class EnderecoController : Controller
{
    private readonly IEnderecoService _enderecoService;
    readonly private ApplicationDbContext _context;

    public EnderecoController(IEnderecoService enderecoService, ApplicationDbContext dbContext)
    {
        _context = dbContext;
        _enderecoService = enderecoService;
    }

    public IActionResult Index()
    {
        IEnumerable<EnderecoModel> enderecos = _context.Endereco;
        return View(enderecos);
    }

    public IActionResult Cadastrar()
    {

        var enderecos = TempData["Enderecos"] != null
            ? JsonSerializer.Deserialize<List<EnderecoModel>>(TempData["Enderecos"].ToString())
            : new List<EnderecoModel>();

        return View(enderecos);
    }

    [HttpPost]
    public async Task<IActionResult> BuscarEndereco(string cep)
    {
        if (string.IsNullOrWhiteSpace(cep) || cep.Length != 8)
        {
            TempData["MensagemError"] = "CEP inválido.";
            return RedirectToAction("Cadastrar");
        }

        var response = await _enderecoService.BuscarEndereco(cep);

        if (response?.DadosRetorno == null)
        {
            TempData["MensagemError"] = "Endereço não encontrado.";
            return RedirectToAction("Cadastrar");
        }

        var enderecos = TempData["Enderecos"] != null
            ? JsonSerializer.Deserialize<List<EnderecoModel>>(TempData["Enderecos"].ToString())
            : new List<EnderecoModel>();


        enderecos.Add(new EnderecoModel
        {
            cep = response.DadosRetorno.CEP,
            street = response.DadosRetorno.Street,
            neighborhood = response.DadosRetorno.Neighborhood,
            city = response.DadosRetorno.City,
            state = response.DadosRetorno.State
        });

        TempData["Enderecos"] = JsonSerializer.Serialize(enderecos);

        return RedirectToAction("Cadastrar");
    }

    [HttpPost]
    public async Task<IActionResult> Salvar(EnderecoModel endereco)
    {
        if (endereco == null)
        {
            TempData["MensagemError"] = "Dados inválidos.";
            return RedirectToAction("Index");
        }

        try
        {
          
            var enderecoExistente = _context.Endereco
                .FirstOrDefault(e => e.cep == endereco.cep);

            if (enderecoExistente == null)
            {
                await _context.Endereco.AddAsync(endereco);
                await _context.SaveChangesAsync();
                TempData["MensagemSucesso"] = "Endereço salvo com sucesso!";
            }
            else
            {
                TempData["MensagemError"] = "O endereço já existe.";
            }
        }
        catch (Exception)
        {
            TempData["MensagemError"] = "Erro ao salvar o endereço.";
        }

        return RedirectToAction("Index");
    }


    [HttpGet]
    public IActionResult Editar(int? id)
    {
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


}
