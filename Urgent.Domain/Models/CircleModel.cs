using System;
using System.ComponentModel.DataAnnotations;

namespace Urgent.Domain.Models
{
    public class CircleModel
    {
        [Range(0, 1000)]
        public uint PositionX { get; set; }
        [Range(0, 1000)]
        public uint PositionY { get; set; }
        public int Diameter { get; set; }
    }
}
