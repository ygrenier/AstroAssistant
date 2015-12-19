using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AstroAssistant.Services
{
    /// <summary>
    /// Informations sur un fichier
    /// </summary>
    public sealed class FileInformation : IDisposable
    {
        /// <summary>
        /// Création d'un nouveau FileInfo
        /// </summary>
        public FileInformation(String fileName, Stream stream)
        {
            if (String.IsNullOrWhiteSpace(fileName)) throw new ArgumentNullException("fileName");
            if (stream == null) throw new ArgumentNullException("stream");
            this.FileName = fileName;
            this.Stream = stream;
        }

        /// <summary>
        /// Libération des ressources
        /// </summary>
        public void Dispose()
        {
            Stream.Dispose();
        }

        /// <summary>
        /// Nom du fichier
        /// </summary>
        public String FileName { get; private set; }
        /// <summary>
        /// Flux du contenu
        /// </summary>
        public Stream Stream { get; private set; }
    }
}
