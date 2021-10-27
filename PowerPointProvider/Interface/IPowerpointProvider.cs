using PowerPointProvider.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointProvider.Interface
{
    public interface IPowerpointProvider
    {
        /// <summary>
        /// Create a powerpoint presentation
        /// </summary>
        /// <param name="pptxTemplate">PowerPoint File to be used as template</param>
        /// <param name="presentationData">Data to be insert into presentatio</param>
        /// <returns><see cref="byte[]"/></returns>
        Task<byte[]> GetPowerpoint(FileInputDto pptxTemplate, PresentationInputDto presentationData);

        /// <summary>
        /// Download a file from given url
        /// </summary>
        /// <param name="url">URL Address</param>
        /// <returns><see cref="byte[]"/></returns>
        Task<byte[]> DownloadFileFromUrl(string url);
    }
}
