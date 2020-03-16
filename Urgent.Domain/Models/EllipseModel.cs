using System;
using System.ComponentModel.DataAnnotations;

namespace Urgent.Domain.Models
{
    public class EllipseModel
    {
        [Range(0, 1000)]
        public uint PositionX { get; set; }
        [Range(0, 1000)]
        public uint PositionY { get; set; }
        public int DiameterH { get; set; }
        public int DiameterV { get; set; }
    }
}
