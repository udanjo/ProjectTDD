using Bogus;
using Moq;
using ProjectTDD.Dominio.Cursos;
using ProjectTDD.DominioTest._Builders;
using ProjectTDD.DominioTest._Util;
using System;
using Xunit;

namespace ProjectTDD.DominioTest.Cursos
{
    public class ArmazenadorDeCursoTest
    {
        private readonly CursoDto _cursoDto;
        private readonly ArmazenadorDeCurso _armazenadorDeCurso;
        private readonly Mock<ICursoRepositorio> _cursoRepositorioMock;

        public ArmazenadorDeCursoTest()
        {
            var fake = new Faker();
            _cursoDto = new CursoDto
            {
                Nome = fake.Random.Words(),
                Descricao = fake.Lorem.Paragraph(),
                CargaHoraria = fake.Random.Double(20, 500),
                PublicoAlvo = "Estudante",
                Valor = fake.Random.Double(100, 2000),
            };

            _cursoRepositorioMock = new Mock<ICursoRepositorio>();
            _armazenadorDeCurso = new ArmazenadorDeCurso(_cursoRepositorioMock.Object);
        }

        [Fact]
        public void DeveAdicionarCurso()
        {
            _armazenadorDeCurso.Armazenar(_cursoDto);

            _cursoRepositorioMock.Verify(r => r.Adicionar(
                It.Is<Curso>(
                    c => c.Nome == _cursoDto.Nome &&
                    c.Descricao == _cursoDto.Descricao &&
                    c.Valor == _cursoDto.Valor
                    )
                ));
        }

        [Fact]
        public void NaoDeveInformarPublicoAlvoInvalido()
        {
            _cursoDto.PublicoAlvo = "Medico";

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
                .ComMensagem("Publico Alvo Invalido");
        }

        [Fact]
        public void NaoDeveAdicionarCursoComMesmoNomeDeOutroJaSalvo()
        {
            var cursoJaSalvo = CursoBuilder.Novo().ComNome(_cursoDto.Nome).Build();
            _cursoRepositorioMock.Setup(r => r.ObterPeloNome(_cursoDto.Nome)).Returns(cursoJaSalvo);

            Assert.Throws<ArgumentException>(() => _armazenadorDeCurso.Armazenar(_cursoDto))
               .ComMensagem("Nome do curso Já cosnta no banco de dados");
        }
    }

    public class ArmazenadorDeCurso
    {
        private readonly ICursoRepositorio _cursoRepositorio;

        public ArmazenadorDeCurso(ICursoRepositorio repositorio)
        {
            _cursoRepositorio = repositorio;
        }

        public void Armazenar(CursoDto cursoDto)
        {
            var _cursoJaSalvo = _cursoRepositorio.ObterPeloNome(cursoDto.Nome);
            if (_cursoJaSalvo != null)
                throw new ArgumentException("Nome do curso Já cosnta no banco de dados");

            Enum.TryParse(typeof(PublicoAlvo), cursoDto.PublicoAlvo, out var publicoAlvo);

            if (publicoAlvo == null)
                throw new ArgumentException("Publico Alvo Invalido");

            Curso curso = new(cursoDto.Nome, cursoDto.CargaHoraria, (PublicoAlvo)publicoAlvo, cursoDto.Valor, cursoDto.Descricao);
            _cursoRepositorio.Adicionar(curso);
        }
    }

    public interface ICursoRepositorio
    {
        void Adicionar(Curso curso);

        Curso ObterPeloNome(string nome);

        void Atualizar(Curso curso);
    }

    public class CursoDto
    {
        public string Nome { get; set; }
        public string Descricao { get; set; }
        public double CargaHoraria { get; set; }
        public string PublicoAlvo { get; set; }
        public double Valor { get; set; }
    }
}