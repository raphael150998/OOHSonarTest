using GoogleMapGenerator.Dtos;
using GoogleMapGenerator.Inteface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapGenerator.Provider
{
    public class MapGenerator : IMapGenerator
    {
        public async Task<byte[]> GenerateMap(CoordinatesInputDto coordinates)
        {
            byte[] mapBit;
            var longitud = coordinates.Longitude;
            var latitud = coordinates.Latitude;


            string url = $"https://maps.googleapis.com/maps/api/staticmap?size=700x700&key=AIzaSyDfxa1QSOHyjweIC7akZa9qly7zjVgKR2k&center={latitud},{longitud}&zoom=13&maptype=roadmap&markers=color:red%7Clabel:%7C{latitud},{longitud}";

            using (WebClient client = new WebClient())
            {
                mapBit = await client.DownloadDataTaskAsync(new Uri(url));
            }

            return mapBit;
        }

        public async Task<byte[]> GenerateMap(List<CoordinatesInputDto> coordinates)
        {
            byte[] mapBit;

            string url = $"https://maps.googleapis.com/maps/api/staticmap?size=700x600&key=AIzaSyDfxa1QSOHyjweIC7akZa9qly7zjVgKR2k&maptype=roadmap";

            foreach (var coordinate in coordinates)
            {
                url += $"&markers=color:red%7Clabel:%7C{coordinate.Latitude},{coordinate.Longitude}";
            }

            using (WebClient client = new WebClient())
            {
                mapBit = await client.DownloadDataTaskAsync(new Uri(url));
            }

            return mapBit;
        }
    }
}
