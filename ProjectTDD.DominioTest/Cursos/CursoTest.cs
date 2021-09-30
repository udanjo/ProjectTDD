using Bogus;
using ExpectedObjects;
using ProjectTDD.Dominio.Cursos;
using ProjectTDD.DominioTest._Builders;
using ProjectTDD.DominioTest._Util;
using System;
using Xunit;

namespace ProjectTDD.DominioTest.Cursos
{
    public class CursoTest : IDisposable
    {
        private readonly string _nome;
        private readonly double _cargaHoraria;
        private readonly PublicoAlvo _publicoAlvo;
        private readonly double _valor;
        private readonly string _descricao;

        public CursoTest()
        {
            //Usando Bogus
            var faker = new Faker();
            _nome = faker.Random.Word();
            _cargaHoraria = faker.Random.Double(50, 200);
            _publicoAlvo = PublicoAlvo.Estudante;
            _valor = faker.Random.Double(100, 1000);
            _descricao = faker.Lorem.Paragraph();
        }

        public void Dispose()
        {
            // Roda após o construtor para cada teste
            //throw new NotImplementedException();
        }

        [Fact]
        public void DeveCriarCurso()
        {
            var CursoEsperado = new
            {
                Nome = _nome,
                CargaHoraria = _cargaHoraria,
                PublicoAlvo = _publicoAlvo,
                Valor = _valor,
                Descricao = _descricao,
            };

            //const string nome = "Informatica básica";
            //const double cargaHoraria = 80;
            //const string publicoAlvo = "Estudantes";
            //const double valor = 950;

            var curso = new Curso(CursoEsperado.Nome, CursoEsperado.CargaHoraria, CursoEsperado.PublicoAlvo, CursoEsperado.Valor, CursoEsperado.Descricao);

            //Assert.Equal(nome, curso.Nome);
            //Assert.Equal(cargaHoraria, curso.CargaHoraria);
            //Assert.Equal(publicoAlvo, curso.PublicoAlvo);
            //Assert.Equal(valor, curso.Valor);
            CursoEsperado.ToExpectedObject().ShouldMatch(curso);
        }

        [Theory]
        [InlineData("")]
        [InlineData(null)]
        public void NaoDeveCursoTerUmNomeInvalido(string nomeInvalido)
        {
            //Sem usar Extension
            //var message = Assert.Throws<ArgumentException>(() =>
            //              new Curso(nomeInvalido, CursoEsperado.CargaHoraria, CursoEsperado.PublicoAlvo, CursoEsperado.Valor))
            //              .Message;
            //Assert.Equal("Nome de campo obrigatório", message);

            Assert.Throws<ArgumentException>(() =>
                           CursoBuilder.Novo().ComNome(nomeInvalido).Build())
                          .ComMensagem("Nome de campo obrigatório");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmaCargaHorariaMenorQueUm(double cargaHorariaInvalida)
        {
            Assert.Throws<ArgumentException>(() =>
                          CursoBuilder.Novo().ComCargaHoraria(cargaHorariaInvalida).Build())
                          .ComMensagem("Carga Horária deveria ser maior que 0");
        }

        [Theory]
        [InlineData(0)]
        [InlineData(-2)]
        [InlineData(-100)]
        public void NaoDeveCursoTerUmValorMenorQueUm(double valorInvalido)
        {
            Assert.Throws<ArgumentException>(() =>
                         CursoBuilder.Novo().ComValor(valorInvalido).Build())
                         .ComMensagem("Valor deveria ser maior que 0");
        }

        // Nesse padrão é muito Código e replicado
        // Melhor situação é usar o Theory e fazer igual acima
        // --------------------------------------------------
        //public void NaoDeveCursoTerUmNomeVazio()
        //{
        //    var CursoEsperado = new
        //    {
        //        Nome = "Informatica básica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)950
        //    };

        //    Assert.Throws<ArgumentException>(() =>
        //        new Curso(string.Empty, CursoEsperado.CargaHoraria, CursoEsperado.PublicoAlvo, CursoEsperado.Valor));
        //}

        //[Fact]
        //public void NaoDeveCursoTerUmNomeNulo()
        //{
        //    var CursoEsperado = new
        //    {
        //        Nome = "Informatica básica",
        //        CargaHoraria = (double)80,
        //        PublicoAlvo = PublicoAlvo.Estudante,
        //        Valor = (double)950
        //    };

        //    Assert.Throws<ArgumentException>(() =>
        //        new Curso(null, CursoEsperado.CargaHoraria, CursoEsperado.PublicoAlvo, CursoEsperado.Valor));
        //}
    }
}