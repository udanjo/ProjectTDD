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
            //Arrange (Organização / Ajusta)
            var variavel1 = 1;
            var variavel2 = 1;


            //Act (Ação)
            //var result = variavel1 == variavel2;

            //Assert (Confirmação ou Validação da ação)
            Assert.Equal(variavel1, variavel2);
        }
    }
}
