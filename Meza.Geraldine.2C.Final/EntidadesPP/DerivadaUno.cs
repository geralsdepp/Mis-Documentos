using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesPP
{
    public sealed class DerivadaUno:Base
    {
        protected int revision;

        public override string VersionFull
        {
            get
            {
                return this.MostrarVersion();
            }
        }

        static DerivadaUno()
        {

        }

        public DerivadaUno(int version, int subversion, int revision)
            :base(version, subversion)
        {
            this.revision = revision;
        }

        protected override string MostrarVersion()
        {
            string retorno = string.Format("{0} Revision: {1}",base.MostrarVersion(), this.revision);
            
            return retorno;
        }
    }
}
