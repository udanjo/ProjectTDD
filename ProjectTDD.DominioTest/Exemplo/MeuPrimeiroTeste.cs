using System;
using Xunit;

namespace ProjectTDD.DominioTest
{
    public class MeuPrimeiroTeste
    {
        //Utilizado para midar o nome do teste
        //[Fact(DisplayName ="Test"]
        [Fact]
        public void DeveVariavel1SerIgualVariavel2()
        {
            //Arrange (Organiza��o / Ajusta)
            var variavel1 = 1;
            var variavel2 = 1;


            //Act (A��o)
            //var result = variavel1 == variavel2;

            //Assert (Confirma��o ou Valida��o da a��o)
            Assert.Equal(variavel1, variavel2);
        }
    }
}
