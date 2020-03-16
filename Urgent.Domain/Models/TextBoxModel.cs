using System;
using System.ComponentModel.DataAnnotations;

namespace Urgent.Domain.Models
{
    public class TextBoxModel
    {
        [Range(0, 1000)]
        public uint PositionX { get; set; }
        [Range(0, 1000)]
        public uint PositionY { get; set; }
        public int Width { get; set; }
        public int Height { get; set; }
        #nullable enable
        public string? Text { get; set; }
    }
}
