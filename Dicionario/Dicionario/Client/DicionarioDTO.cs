using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dicionario.Client
{
    public class DicionarioDTO
    {
        public int QtdGatinho { get; set; }
        public string Palavra { get; set; }
        public int PosicaoPalavra { get; set; }
        public bool  Encontrou { get; set; }
    }
}
