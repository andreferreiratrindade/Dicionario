using System;
using System.Net.Http;

namespace Dicionario.WebServices
{
    public class DicionarioWebService : IDicionarioWebService
    {

        private HttpClient _httClient;
        public const string URL = "http://teste.xxx.com.br";
        public DicionarioWebService()
        {
            _httClient = new HttpClient();
            _httClient.BaseAddress = new Uri(URL);
        }


        public string RealizaPesquisaAtravesDaPosicao(int posicao)
        {
            return _httClient.GetAsync(string.Format("dic/api/words/{0}", posicao))
                      .Result.Content.ReadAsStringAsync().Result;
        }

        public void Dispose()
        {
            _httClient.Dispose();
        }
    }
}
