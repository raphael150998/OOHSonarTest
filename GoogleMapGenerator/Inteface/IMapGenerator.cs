using GoogleMapGenerator.Dtos;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoogleMapGenerator.Inteface
{
    public interface IMapGenerator
    {
        /// <summary>
        /// Generate a map base on <paramref name="coordinates"/>
        /// </summary>
        /// <param name="coordinates">Coordinates of map's location</param>
        /// <returns></returns>
        Task<byte[]> GenerateMap(CoordinatesInputDto coordinates);

        /// <summary>
        /// Generate a map with multiple markers base on <paramref name="coordinates"/>
        /// </summary>
        /// <param name="coordinates">Coordinates list of map's locations</param>
        /// <returns></returns>
        Task<byte[]> GenerateMap(List<CoordinatesInputDto> coordinates);
    }
}
