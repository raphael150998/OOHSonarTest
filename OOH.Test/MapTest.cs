using GoogleMapGenerator.Provider;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OOH.Test
{
    public class MapTest
    {
        [Test]
        public async Task GetMapTest_Ok()
        {
            MapGenerator mapGenerator = new MapGenerator();

            //byte[] result = await mapGenerator.GenerateMap((float)13.7029, (float)-89.2433);
        }
    }
}
