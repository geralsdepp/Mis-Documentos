using System;
using System.Collections.Generic;
using System.Text;

namespace EntidadesPP
{
    public class DerivadaDos : Base
    {
        public override string VersionFull
        {
            get
            {
                return this.MostrarVersion();
            }
        }

        public DerivadaDos()
        {

        }

        protected override string MostrarVersion()
        {
            return base.MostrarVersion();
        }
    }
}
