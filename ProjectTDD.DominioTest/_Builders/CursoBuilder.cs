using ProjectTDD.Dominio.Cursos;
using ProjectTDD.DominioTest.Cursos;

namespace ProjectTDD.DominioTest._Builders
{
    public class CursoBuilder
    {
        private string _nome = "Informatica básica";
        private double _cargaHoraria = 80;
        private PublicoAlvo _publicoAlvo = PublicoAlvo.Estudante;
        private double _valor = 950;
        private string _descricao = "Uma Descrição";

        public static CursoBuilder Novo()
        {
            return new CursoBuilder();
        }

        public CursoBuilder ComNome(string nome)
        {
            _nome = nome;
            return this;
        }

        public CursoBuilder ComCargaHoraria(double cargaHoraria)
        {
            _cargaHoraria = cargaHoraria;
            return this;
        }

        public CursoBuilder ComValor(double valor)
        {
            _valor = valor;
            return this;
        }

        public Curso Build()
        {
            return new Curso(_nome, _cargaHoraria, _publicoAlvo, _valor, _descricao);
        }
    }
}