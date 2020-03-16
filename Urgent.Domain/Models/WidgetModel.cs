using System.Collections.Generic;

namespace Urgent.Domain.Models
{
    public class WidgetModel
    {
        public List<RectangleModel> Rectangles { get; set; }
        public List<SquareModel> Squares { get; set; }
        public List<EllipseModel> Ellipses { get; set; }
        public List<CircleModel> Circles { get; set; }
        public List<TextBoxModel> TextBoxes { get; set; }
    }
}
