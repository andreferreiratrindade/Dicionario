using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Dicionario.Client;
using Dicionario.WebServices;
using Moq;

using System.Collections.Generic;

namespace Dicionario.Test.Client
{
    [TestClass]
    public class DicionarioClientTest
    {

        private Mock<IDicionarioWebService> _dicionarioWS;

        [TestInitialize]
        public void Setup()
        {


            var dicPalavras = GetDicionario();

            _dicionarioWS = new Mock<IDicionarioWebService>();
            _dicionarioWS.Setup(x => x.RealizaPesquisaAtravesDaPosicao(It.IsAny<int>())).Returns((int pos) => VerificaPosicaoEmDicionario(pos, dicPalavras));

        }

        private string VerificaPosicaoEmDicionario(int pos, IDictionary<int, string> dicPalavras)
        {
            if (dicPalavras.ContainsKey(pos))
            {
                return dicPalavras[pos];
            }

            return "";
        }

        private IDictionary<int, string> GetDicionario()
        {
            var dic = new Dictionary<int, string>();

            dic.Add(1, "ABC");
            dic.Add(2, "ACD");
            dic.Add(3, "AFG");
            dic.Add(4, "BCDASDSA");
            dic.Add(5, "BGUAAAA");
            dic.Add(6, "CQWE");
            dic.Add(7, "DVAS");
            dic.Add(8, "ELEFANTE");
            dic.Add(9, "FORMIGA");
            dic.Add(10, "GARFANHOTO");
            dic.Add(11, "GIRAFA");
            dic.Add(12, "GOLEIRO");
            dic.Add(13, "INGUA");
            dic.Add(14, "JACARE");
            dic.Add(15, "MONTANHA");
            dic.Add(16, "NDFGDFG");
            dic.Add(17, "ODHJDGHD");
            dic.Add(18, "PXCVZXCVZXC");
            dic.Add(19, "QASDASDSA");
            dic.Add(21, "RASDWQSSDS");
            dic.Add(22, "SQQQQWAD");
            dic.Add(23, "TAAAAAA");
            dic.Add(24, "TAAAAAAP");
            dic.Add(25, "TAAAAAAUUU");
            dic.Add(26, "UAAAÃ");
            dic.Add(27, "USSDAÁ");
            dic.Add(28, "UASDA");
            dic.Add(29, "USDSS");
            dic.Add(30, "ZASDWW");
            return dic;
        }


        [TestMethod]
        public void DeveConsultarPalavraComTresLetras()
        {
            string palavra = "AFG";
            var dicClient = new DicionarioClient(_dicionarioWS.Object);
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            Assert.AreEqual(11, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarMelhorCaso()
        {
            string palavra = "abc";
            var dicClient = new DicionarioClient(_dicionarioWS.Object);
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra.ToUpper(), resultado.Palavra);
            Assert.AreEqual(12, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarMedioCaso()
        {
            string palavra = "INGUA";
            var dicClient = new DicionarioClient(_dicionarioWS.Object);
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            Assert.AreEqual(11, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPiorCaso()
        {
            string palavra = "ZASDWW";
            var dicClient = new DicionarioClient(_dicionarioWS.Object);
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            Assert.AreEqual(12, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPrimeiraPalavraWebSErvice()
        {
            string palavra = "AARÃO";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            Assert.AreEqual(13, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComOnzeLetrasWebSErvice()
        {
            string palavra = "CONSENTIRÃO";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //   Assert.AreEqual(18, resultado.QtdGatinho);
        }


        [TestMethod]
        public void DeveConsultarPalavraComQuatroLetrasWebSErvice()
        {
            string palavra = "ANTE";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //  Assert.AreEqual(14, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComDozeLetrasWebSErvice()
        {
            string palavra = "ACABRUNHADOR";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //     Assert.AreEqual(13, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComSeisLetrasWebSErvice()
        {
            string palavra = "CANADÁ";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //    Assert.AreEqual(14, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComPosicaoQuarentaMilWebSErvice()
        {
            string palavra = "SOLDADO";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //   Assert.AreEqual(23, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComPosicaoQuarentaEhTresMilWebSErvice()
        {
            string palavra = "USUFRUIRÃO";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //  Assert.AreEqual(26, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComPosicaoQuarentaEhTresMilEhTresSilabasWebSErvice()
        {
            string palavra = "VAGIRA";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //        Assert.AreEqual(26, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraComAcentuacaoWebSErvice()
        {
            string palavra = "VAGIRÁ";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //      Assert.AreEqual(22, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarUltimaPalavraComAcentuacaoWebSErvice()
        {
            string palavra = "ZARCÃO";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //      Assert.AreEqual(28, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarUltimaPalavraWebSErvice()
        {
            string palavra = "ZURRO";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //         Assert.AreEqual(29, resultado.QtdGatinho);
        }

        [TestMethod]
        public void DeveConsultarPalavraDoMeioWebSErvice()
        {
            string palavra = "GALHARDEAR";
            var dicClient = new DicionarioClient(new DicionarioWebService());
            var resultado = dicClient.ConsultaPalavra(palavra);
            Console.WriteLine(string.Format("Posição: {0}", resultado.PosicaoPalavra));
            Console.WriteLine(string.Format("Gatinhos mortos: {0}", resultado.QtdGatinho));

            Assert.IsTrue(resultado.Encontrou);
            Assert.AreEqual(palavra, resultado.Palavra);
            //     Assert.AreEqual(19, resultado.QtdGatinho);
        }


    }
}
