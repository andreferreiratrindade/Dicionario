using Dicionario.Client;
using Dicionario.WebServices;
using System;

namespace Dicionario
{
    public class DicionarioVirtual
    {

        public static void Main(string[] args)
        {
            
            int continuar = 0;
            while (continuar == 0)
            {
                Console.Clear();
                Console.WriteLine("                     >>>>>Bem Vindo ao Dicionário Virtual<<<<<");
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Informe a palavra: ");
                string palavra = Console.ReadLine();
                Console.WriteLine();
                Console.WriteLine("Consultando...");
                DicionarioClient dicCliente = new DicionarioClient(new DicionarioWebService());
                var dicionarioDto = dicCliente.ConsultaPalavra(palavra);

                string resultado;

                if (dicionarioDto.Encontrou)
                {
                    Console.WriteLine(".. Encontramos sua palavra ...");
                    resultado = string.Format("Posição: {0} \nGatinhos mortos: {1}", dicionarioDto.PosicaoPalavra, dicionarioDto.QtdGatinho);
                }
                else
                {
                    resultado = "Infelizmente, não encontramos sua palavra.";
                }
                
                Console.WriteLine();
                Console.WriteLine();
                Console.WriteLine(resultado);
                Console.WriteLine();
                Console.WriteLine();
                Console.Write("Deseja infomar outras palavras ? s ( Sim )   n (Não - Sair do Sistema) : ");

                continuar = string.Compare(Console.ReadLine(), "s");
            }
        }
    }
}
