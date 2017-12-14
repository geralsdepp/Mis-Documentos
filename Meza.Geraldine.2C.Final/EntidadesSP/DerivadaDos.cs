using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesSP
{
    public class DerivadaDos : Base
    {
        public override string VersionFull
        {
            get
            {
                DerivadaDos d2 = new DerivadaDos();

                return d2.MostrarVersion();
            }
        }

        public DerivadaDos() : base(1, 0) { }
        

        protected override string MostrarVersion()
        {
            return this.MostrarVersion();
        }
    }
}
