using NUnit.Framework;
using FluentAssertions;
using Moq;
using AluguelEquipamentos.Controllers;
using AluguelEquipamentos.Data;
using AluguelEquipamentos.Negocio.Interfaces;
using AluguelEquipamentos.Negocio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Collections.Generic;
using System.Linq;

namespace AluguelEquipamentos.Testes.Unit
{
    [TestFixture]
    public class AluguelControllerTests
    {
        private ApplicationDbContext _dbContext;
        private Mock<ISessao> _mockSessao;
        private Mock<RabbitMQService> _mockRabbit;
        private AluguelController _controller;

        [SetUp]
        public void Setup()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: "Db_Aluguel_Test")
                .Options;

            _dbContext = new ApplicationDbContext(options);
            _dbContext.Equipamentos.AddRange(new List<EquipamentoModel>
            {
                new EquipamentoModel { Id = 1, Cliente = "João" , CNPJ ="1232323", DateInicial =DateTime.Today , EquipamentoAlugado = "sim" },
                new EquipamentoModel { Id = 2, Cliente = "Maria",CNPJ = "1232323",DateInicial =DateTime.Today ,EquipamentoAlugado = "sim"}
            });
            _dbContext.SaveChanges();

            _mockSessao = new Mock<ISessao>();
            _mockSessao.Setup(s => s.BuscarSessao()).Returns(new  UsuarioModel { Nome = "UsuarioTeste" });

            _mockRabbit = new Mock<RabbitMQService>();

            _controller = new AluguelController(_dbContext, _mockSessao.Object, _mockRabbit.Object);
        }

        [Test]
        public void Index_DeveRetornarViewComEquipamentos_QuandoUsuarioLogado()
        {

            var resultado = _controller.Index();

       
            var viewResult = resultado.Should().BeOfType<ViewResult>().Subject;
            var modelo = viewResult.Model.Should().BeAssignableTo<IEnumerable<EquipamentoModel>>().Subject;

            modelo.Should().HaveCount(2);
            modelo.First().Cliente.Should().Be("João");
        }

        [Test]
        public void Index_DeveRetornarViewComMensagemDeErro_QuandoNaoHouverEquipamentos()
        {
  
            _dbContext.Equipamentos.RemoveRange(_dbContext.Equipamentos);
            _dbContext.SaveChanges();
       
            var resultado = _controller.Index();
  
            var viewResult = resultado.Should().BeOfType<ViewResult>().Subject;
            viewResult.ViewData.ModelState.IsValid.Should().BeTrue();
        }

        [TearDown]
        public void Cleanup()
        {
            _dbContext.Database.EnsureDeleted();
            _dbContext.Dispose();
        }
    }
}
