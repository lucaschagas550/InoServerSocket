using System;
using System.Collections.Generic;
using System.Text;

namespace InoServerSocketTcp
{
    public class TagReceived
    {
        public string epc { get; set; }

        public TagReceived()
        {

        }

        public TagReceived(string epc)
        {
            this.epc = epc;
        }
    }
}
