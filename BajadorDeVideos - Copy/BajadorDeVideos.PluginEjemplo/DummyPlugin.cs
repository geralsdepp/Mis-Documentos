using System;
using System.Collections.Generic;
using BajadorDeVideos.Common;
using HtmlAgilityPack;
using System.Web;
using System.Net;
using System.IO;
using System.Net.Http;
using System.ComponentModel;
using Newtonsoft.Json;
using System.Collections;
using Newtonsoft.Json.Linq;

namespace BajadorDeVideos.PluginEjemplo
{
    public class DummyPlugin : Video,IPlugin
    {
        public List<Video> misVideos;
        string urlListado;
        string urlIframe;
        public DummyPlugin(string urlListado):base("","")
        {
            misVideos = new List<Video>();
            this.urlListado = urlListado;
        }

        public byte[] Bajar()
        {

            int indice = 0;

            Console.WriteLine("Bajar Videos");
            byte[] b = new byte[0];
            foreach (Video item in this.misVideos)
            {

                if(indice>3)
                    break;

                string htmlVideo = Obtenerhtml("https://www.commoncraft.com"+item.Link);
                HtmlDocument docVideo = new HtmlDocument();
                docVideo.LoadHtml(htmlVideo);

                foreach (HtmlNode node in docVideo.DocumentNode.SelectNodes("//iframe"))
                {
                    urlIframe = node.GetAttributeValue("src","");
                }

                HtmlDocument docIframe = new HtmlDocument();
                docIframe.LoadHtml(Obtenerhtml(urlIframe));
            
                string script =  docIframe.DocumentNode.SelectSingleNode("//script[2]").InnerText;
                //string script = node.InnerText;
                //Console.WriteLine(script);
                var inicioCadena = script.IndexOf("https://embed");
                var finCadena = script.IndexOf(".bin",inicioCadena);
                //Genero una nueva url con el link de descarga
                var direccionVideo = script.Substring(inicioCadena,finCadena-inicioCadena+4);
                WebClient client = new WebClient();

                indice++;
                //Descargo videos
                client.DownloadFile(direccionVideo,"C:\\Users\\geraldinem_lu\\Documents\\misVideos\\CommonCraft"+indice+".mp4");
                b = client.DownloadData(direccionVideo); 
            }
            return b;
        }
        private string Obtenerhtml(string url)
        {
            WebClient client = new WebClient();
            if (!url.Contains("https://"))
            {
                url = "https:"+ url;
            } 
            if(!url.Contains("fast.wistia.net"))
            {
                base.Link = url;
            }
            string html = client.DownloadString(url);
            return html;  
        }

        public void ListarVideosDisponibles(string urlListado)
        {
            //Llamo a la pagina y obtengo el html generado

            using (WebClient client = new WebClient())
            {
                //Descargo mi html en un string
                string html = client.DownloadString(urlListado);
                //Instancio mi HtmlDocument(docuemento html)
                HtmlDocument doc = new HtmlDocument();
                //Le paso mi string html a cargar
                doc.LoadHtml(html);
                
                //ya puedo trabajar con el y utilizo la clase HtmlNode para acceder a mis etiquetas.
                //Recorro mi span
                foreach (HtmlNode node in doc.DocumentNode.SelectNodes("//span[@class='field-content']"))
                {
                    //Recorro mi a ref(dentro esta mi link y mi titulo)
                    foreach (HtmlNode node2 in node.SelectNodes(".//a[@href]"))
                    { 
                        //1. Extraigo mediante node2.InnerText el titulo del video.
                        //2. Extraigo mediante node2.GetAttributeValue el link pedido.
                        //3. Creo mi nuevo video y le paso los parametros correspondientes.
                        //4. Añado el video a mi lista "misVideos".   
                        if(node2.GetAttributeValue("href","") != "/join" && node2.InnerText != "Select")
                        {
                            //if(this.misVideos.Contains(b)  node2.InnerText)
                            this.misVideos.Add(new Video(node2.InnerText,node2.GetAttributeValue("href","")));
                            
                            //this.misVideos.Add(new Video(node2.InnerText,base.Link)); 
                        }         
                    }       
                }
                foreach (Video item in this.misVideos)
                {
                    Console.WriteLine("Titulo: {0}",item.Titulo);
                    Console.WriteLine("Link: {0}",item.Link);
                }
            }
        } 

       /* public static DummyPlugin operator +(DummyPlugin plugin, Video v)
        {
            if(!plugin.misVideos.Contains(v))
            {
                plugin.misVideos.Add(v);
            }
            return plugin;
        }   */  
    }
}
