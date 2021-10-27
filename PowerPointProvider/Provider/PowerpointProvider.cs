using GoogleMapGenerator.Inteface;
using PowerPointProvider.Base;
using PowerPointProvider.Dtos;
using PowerPointProvider.Interface;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace PowerPointProvider.Provider
{
    public class PowerpointProvider : IPowerpointProvider
    {
        private readonly IMapGenerator _mapGenerator;

        public PowerpointProvider(IMapGenerator mapGenerator)
        {
            _mapGenerator = mapGenerator;
        }

        public async Task<byte[]> DownloadFileFromUrl(string url)
        {
            using (WebClient client = new WebClient())
            {
                return await client.DownloadDataTaskAsync(new Uri(url));
            }
        }

        public async Task<byte[]> GetPowerpoint(FileInputDto pptxTemplate, PresentationInputDto presentationData)
        {
            string urlSource = Path.Combine(pptxTemplate.Url, $"{pptxTemplate.FileName}{pptxTemplate.Extension}");
            string urlTarget = Path.Combine(pptxTemplate.Url, $"{pptxTemplate.FileName}_Copy{pptxTemplate.Extension}");

            File.Delete(urlTarget);
            File.Copy(urlSource, urlTarget);

            Powerpoint pptx = new Powerpoint(urlTarget, FileAccess.ReadWrite);

            PowerpointSlide welcomeSlide = pptx.GetSlide(0);

            welcomeSlide.ReplaceTag("{{client}}", presentationData.Client, PowerpointSlide.ReplacementType.Global);
            welcomeSlide.ReplaceTag("{{date}}", DateTime.Now.Date.ToString("dd/MM/yyyy"), PowerpointSlide.ReplacementType.Global);

            //pptx.Save();

            PowerpointSlide slideTemplate = pptx.GetSlide(2);
            PowerpointSlide MapSlideTemplate = pptx.GetSlide(3);
            PowerpointSlide CircuitSlideTemplate = pptx.GetSlide(pptx.GetSlides().Count() - 3);

            #region asyncDatTest
            //await Task.WhenAll(presentationData.Faces.Select(async face => await Task.Run(async () =>
            //{
            //    PowerpointSlide newSlide = slideTemplate.Clone();

            //    PowerpointSlide.InsertAfter(newSlide, MapSlideTemplate);

            //    //Reemplazando tags por la informacion en la slide generada
            //    newSlide.ReplaceTag("{{code}}", face.Code, PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{address}}", face.Address, PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{structure}}", face.StructureType, PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{measure}}", $"{face.Height}{face.Width}", PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{available}}", face.Available ? "Disponible" : "No disponible", PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{hiring}}", face.HiringPrice.ToString(), PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{print}}", face.PrintPrice.ToString(), PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{traffic}}", face.DailyTraffic.ToString(), PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{notes}}", face.Notes, PowerpointSlide.ReplacementType.Global);
            //    newSlide.ReplaceTag("{{direction}}", face.Direction, PowerpointSlide.ReplacementType.Global);

            //    byte[] addressImage = new byte[] { };

            //    if (!string.IsNullOrEmpty(face.ReferenceImage.Url))
            //    {
            //        addressImage = await DownloadFileFromUrl(face.ReferenceImage.Url);
            //    }

            //    newSlide.ReplacePicture("{{faceImage}}", addressImage, face.ReferenceImage.MimeType);

            //    //Slide de mapa
            //    PowerpointSlide newMapSlide = MapSlideTemplate.Clone();

            //    PowerpointSlide.InsertAfter(newMapSlide, newSlide);

            //    var file = await _mapGenerator.GenerateMap(face.Map);

            //    newMapSlide.ReplaceTag("{{code}}", face.Code, PowerpointSlide.ReplacementType.Global);

            //    newMapSlide.ReplacePicture("{{map}}", file, "image/png");

            //    pptx.Save();
            //})));
            #endregion


            foreach (SlideFaceInputDto face in presentationData.Faces)
            {
                PowerpointSlide newSlide = slideTemplate.Clone();

                PowerpointSlide.InsertAfter(newSlide, MapSlideTemplate);

                //Reemplazando tags por la informacion en la slide generada
                newSlide.ReplaceTag("{{code}}", face.Code, PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{address}}", face.Address, PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{structure}}", face.StructureType, PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{measure}}", $"{face.Height}{face.Width}", PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{available}}", face.Available ? "Disponible" : "No disponible", PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{hiring}}", face.HiringPrice.ToString(), PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{print}}", face.PrintPrice.ToString(), PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{traffic}}", face.DailyTraffic.ToString(), PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{notes}}", face.Notes, PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{direction}}", face.Direction, PowerpointSlide.ReplacementType.Global);
                newSlide.ReplaceTag("{{link}}", "hola man", PowerpointSlide.ReplacementType.Global);

                byte[] addressImage = new byte[] { };

                if (!string.IsNullOrEmpty(face.ReferenceImage.Url))
                {
                    addressImage = await DownloadFileFromUrl(face.ReferenceImage.Url);
                }

                newSlide.ReplacePicture("{{faceImage}}", addressImage, face.ReferenceImage.MimeType);

                //Slide de mapa
                PowerpointSlide newMapSlide = MapSlideTemplate.Clone();

                PowerpointSlide.InsertAfter(newMapSlide, newSlide);

                var file = await _mapGenerator.GenerateMap(face.Map);

                NumberFormatInfo nfi = new();
                nfi.NumberDecimalSeparator = ".";

                string urlMap = $"https://www.google.com/maps/search/{face.Map.Latitude.ToString(nfi)},{face.Map.Longitude.ToString(nfi)}?hl=es";

                newMapSlide.ReplaceTag("{{code}}", face.Code, PowerpointSlide.ReplacementType.Global);

                newMapSlide.ReplacePicture("{{map}}", file, "image/png", urlMap);

                //pptx.Save();

            }

            slideTemplate.Remove();
            MapSlideTemplate.Remove();

            //imagen de mapa de circuito
            if (presentationData.HasCircuit)
            {
                byte[] circuitMap = await _mapGenerator.GenerateMap(presentationData.Faces.Select(x => x.Map).ToList());

                CircuitSlideTemplate.ReplacePicture("{{circuite}}", circuitMap, "image/png");
            }
            else CircuitSlideTemplate.Remove();

            pptx.Save();

            pptx.Close();

            return File.ReadAllBytes(urlTarget);
        }
    }
}
