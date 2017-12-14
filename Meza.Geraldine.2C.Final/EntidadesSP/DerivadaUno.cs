using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesSP
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
            StringBuilder sb = new StringBuilder();
            sb.AppendFormat(" Revision : {0}", this.revision);
            return sb.ToString();
        }
    }
}
