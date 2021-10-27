using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointProvider.Dtos
{
    /// <summary>
    /// DTO de entrada de un archivo
    /// </summary>
    public class FileInputDto
    {
        /// <summary>
        /// Direccion de donde se encuentra almacenado el archivo
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Determina si el archivo se encuentra en una direccion local
        /// </summary>
        public bool IsLocalPath { get; set; }

        /// <summary>
        /// MimeType
        /// </summary>
        public string MimeType { get; set; }

        /// <summary>
        /// Extension del archivo (es obnligatorio que tenga el punto ejm: .png .jpg .pptx)
        /// </summary>
        public string Extension { get; set; }

        /// <summary>
        /// Archivo
        /// </summary>
        public byte[] File { get; set; }

        /// <summary>
        /// Nombre del archivo
        /// </summary>
        public string FileName { get; set; }
    }
}
