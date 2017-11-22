using System;
using System.Collections.Generic;
using System.IO;
using BajadorDeVideos.Common;
using BajadorDeVideos.PluginEjemplo;
using System.Web;
using HtmlAgilityPack;

namespace BajadorDeVideos.Consola
{
    class Program
    {
        static void Main(string[] args)
        {

        /* Agregar soporte para:
        - COMMONCRAFT
        - baseUrl: https://www.commoncraft.com/videolist
        - como listar:
            - bbuscar todos los links que estan dentro de un <span class="field-content">
        - como bajar video:
            - obtener la pagina asociada a cada video
            - Obtener la direccion del primer IFRAME que aparece en la pagina, donde se encuentra el reproductor
            - De esa URL del iframe, dentro del codigo de inicializacion del player de video, bajar alguna de las URLs indicadas como "url":"https://embed....."


        - GOGOANIME
            - baseUrl: direccion original de serie (ej, https://ww3.gogoanime.io/category/dragon-ball-kai)
            - como listar: 
                - Obtener del HTML que devuelve, el campo movie_id 
                - Obtener de la URL https://ww3.gogoanime.io/load-list-episode?ep_start=0&ep_end=10000&id=MOVIE_ID el listado de episodios (reemplazando MOVIE_ID por el numero obtenido)
            - como bajar video:
                - obtener la pagina asociada a cada video
                - Obtener la direccion del primer IFRAME que aparece en la pagina, donde se encuentra el reproductor
                - Obtener la segunda url del iframe
                - De esa URL del iframe, obtener la URL del video (tag source, dentro de video, el que diga type="video/mp4")
        */

            if (args.Length != 1)
            {
                Console.WriteLine("Se requiere un unico parametro 'baseUrl'");
                Console.WriteLine(args.Length);
                return;
            }
            
            string url = args[0];

            IPlugin plugin;

            if (url.Contains(url))
            {
                plugin = new DummyPlugin(url);
            }
            else
            {
                throw new NotSupportedException("URL no soportada");
            }
            

            plugin.ListarVideosDisponibles("https://www.commoncraft.com/videolist");
            byte[] b = plugin.Bajar();
            //BinaryWriter writer = new BinaryWriter(
            //writer.Write(b);
        }
    }
}
