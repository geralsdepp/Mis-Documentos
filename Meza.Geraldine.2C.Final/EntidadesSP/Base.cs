using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.SqlClient;

namespace EntidadesSP
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
                if (Int32.TryParse(value, out result))
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
            where T : Base
        {
            SqlConnection connection = new SqlConnection("Data Source=GERMANA-NOTE\\SQLEXPRESS;Initial Catalog=Datos;Integrated Security=True");
            SqlCommand command = new SqlCommand();

            command.CommandType = System.Data.CommandType.Text;
            string consulta = "INSERT INTO Personas (version, subversion, revision, derivada) VALUES('" + Base.version + "'" + ",'" + Base.subversion + "')";
            command.CommandText = consulta;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            return true;
        }

        public static Queue<T> LeerDatos<T>()
            where T : Base
        {
            Queue<T> pila = new Queue<T>();
            SqlConnection connection = new SqlConnection("Data Source=GERMANA-NOTE\\SQLEXPRESS;Initial Catalog=Datos;Integrated Security=True");
            SqlCommand command = new SqlCommand();
            command.CommandType = System.Data.CommandType.Text;
            string consulta = "SELECT * FROM Datos";
            command.CommandText = consulta;
            connection.Open();
            command.ExecuteNonQuery();
            connection.Close();
            SqlDataReader reader = command.ExecuteReader();
            while (reader.Read())
            {
                Object[] valores = new Object[reader.FieldCount];

                int FieldCount = reader.GetValues(valores);
                foreach (var item in valores)
                {
                    pila.Enqueue((T)item);
                }


            }
            return pila;
           
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
