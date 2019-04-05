using Dicionario.WebServices;
using System.Globalization;

namespace Dicionario.Client
{
    public class DicionarioClient
    {
        public const int LIMITE_GATINHOS = 50;
        private int _almentaQtdMax;
        private int _valorMinimo;
        private int _valorMaximo = 10000;
        private int _estaPerto;

        private readonly IDicionarioWebService _dicionarioWS;

        public DicionarioClient(IDicionarioWebService dicionarioWS)
        {
            _dicionarioWS = dicionarioWS;
        }


        public DicionarioDTO ConsultaPalavra(string palavra)
        {

            DicionarioDTO dicionarioDto = new DicionarioDTO();

            while (dicionarioDto.QtdGatinho <= LIMITE_GATINHOS)
            {
                // Almenta o valor _valorMaximoimo dinamicamente
                ProcessaValorMaximo();

                int posicao = (_valorMinimo + _valorMaximo) / 2;

                // Mata um gatinho
                dicionarioDto.QtdGatinho++;

                var resposta = _dicionarioWS.RealizaPesquisaAtravesDaPosicao(posicao);

                // Compara todos os caracteres da resposta com a palavra informada
                bool achou = ProcessaComparacaoDePalavras(palavra, resposta, posicao);

                if (achou)
                {
                    dicionarioDto.Encontrou = true;
                    dicionarioDto.PosicaoPalavra = posicao;
                    dicionarioDto.Palavra = resposta;
                    break;
                }

            }
       
            _dicionarioWS.Dispose();

            return dicionarioDto;
        }

        private bool ProcessaComparacaoDePalavras(string palavra, string resposta, int posicao)
        {
            palavra = palavra.ToUpper();
            resposta = resposta.ToUpper();

            int comparacao = 0;

            if (string.Compare(palavra, resposta) == 0)
            {
                return true;
            }

            for (int i = 0; i < palavra.Length; i++)
            {

                if (string.IsNullOrEmpty(resposta))
                {
                    comparacao = -1;
                }

                // Verifica o tamanho da resposta
                if (resposta.Length > i)
                {
                    comparacao = string.Compare(palavra[i].ToString(), resposta[i].ToString(),
                        CultureInfo.CurrentCulture, CompareOptions.IgnoreNonSpace);
                }
                else if (resposta.Length > 1)
                {
                    comparacao = 1;
                }

                if (comparacao == 0)
                {
                    _estaPerto = i;
                    _almentaQtdMax = 0;
                }
                else if (comparacao == -1)
                {
                    _valorMaximo = posicao - 1;
                    _almentaQtdMax = 0;
                    break;
                }
                else if (comparacao == 1)
                {
                    _valorMinimo = posicao + 1;
                    _almentaQtdMax++;
                    break;
                }
            }

            // Caso a resposta seja maior do que a palavra
            if (palavra.Length < resposta.Length && _estaPerto == (palavra.Length - 1))
            {
                _valorMaximo = posicao - 1;
                _almentaQtdMax = 0;
            }

            //Caso a palavra esteja com acentos 
            if (palavra.Length == resposta.Length && _estaPerto == (palavra.Length - 1))
            {
                for (int i = 0; i < palavra.Length; i++)
                {
                    comparacao = string.Compare(palavra[i].ToString(), resposta[i].ToString());

                    if (comparacao == 0)
                    {
                        _estaPerto = i;
                        _almentaQtdMax = 0;
                    }
                    else if (comparacao == -1)
                    {
                        _valorMaximo = posicao - 1;
                        _almentaQtdMax = 0;

                        break;
                    }
                    else if (comparacao == 1)
                    {
                        _valorMinimo = posicao + 1;

                        break;
                    }
                }
            }

            return false;
        }

        private int ProcessaValorMaximo()
        {
            if (_almentaQtdMax == 3)
            {
                _valorMaximo += Proximidade(_estaPerto);
                _almentaQtdMax = 0;
            }
            else if (_valorMinimo > _valorMaximo)
            {
                _valorMaximo += Proximidade(_estaPerto);
                _almentaQtdMax = 0;
            }

            return _valorMaximo;
        }

        private int Proximidade(int estaProximo)
        {
            switch (estaProximo)
            {
                case 0: return 10000;

                case 1: return 1000;

                case 2: return 500;

                case 3: return 200;

                case 4: return 100;

                case 5: return 50;

                default: return 10;
            }
        }
    }
}



