using System;
using System.ComponentModel.DataAnnotations;

namespace Urgent.Domain.Models
{
    public class RectangleModel
    {
        [Range(0, 1000)]
        public uint PositionX { get; set; }
        [Range(0, 1000)]
        public uint PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
    }
}
