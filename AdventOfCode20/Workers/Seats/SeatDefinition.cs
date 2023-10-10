using System;
using System.Collections.Generic;
using System.Text;

namespace AdventOfCode20.Workers.Seats
{
    public class SeatDefinition
    {
        public int Row { get; set; }    
        public int Column { get; set; }
        public int Id => Row * 8 + Column;
    }
}
