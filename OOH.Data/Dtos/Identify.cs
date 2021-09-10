using System;
using System.Collections.Generic;
using System.Text;

namespace OOH.Data.Dtos
{
    public class Identify<T> where T : IConvertible
    { 
        public T Id { get; set; }
    }
}
