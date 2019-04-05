using System;

namespace Dicionario.WebServices
{
    public interface IDicionarioWebService : IDisposable
    {

        string RealizaPesquisaAtravesDaPosicao(int posicao);
    }
}
