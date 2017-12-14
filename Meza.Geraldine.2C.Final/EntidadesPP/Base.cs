using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Data;

namespace EntidadesPP
{
    public abstract class Base
    {
        private static int subversion;
        private static int version;

        public string Version
        {
            get { return Base.version.ToString(); }
            set
            {
                int result;
                if(Int32.TryParse(value, out result))
                {
                    Base.version = result;
                }
            }
        }

        public abstract string VersionFull
        {
            get;
        }

        static Base()
        {
            Base.version = 1;
            Base.subversion = 0;
        }

        public Base(int version, int subversion)
        {
            this.Version = version.ToString();
            Base.subversion = subversion;
        }

        public override int GetHashCode()
        {
            return Base.version + Base.subversion;
        }

        public static bool GuardarDatos<T>(T datos)
            where T:Base
        {
            SqlCommand comando = new SqlCommand();
        }

        public Queue<T> LeerDatos<T>()
        {

        }

        protected virtual string MostrarVersion()
        {
            string retorno = string.Format("{0}.{1}", Base.version, Base.subversion);
            return retorno;
        }

        public static string operator ~(Base b)
        {
            string retorno = b.MostrarVersion();
            retorno = string.Format(retorno.Replace(version.ToString(), subversion.ToString()));
            return retorno;
        }
    }
}
