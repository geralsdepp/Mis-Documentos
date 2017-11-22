using System;
using System.Collections.Generic;

namespace BajadorDeVideos.Common
{
    public interface IPlugin
    {
        void ListarVideosDisponibles(string urlListado);
        byte[] Bajar();
    }
}   