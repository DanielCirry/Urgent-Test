using Urgent.Domain.Models;

namespace Urgent.Domain.Helpers
{
    public class DefaultWidget
    {
        public RectangleModel CreateRectangle()
        {
            return new RectangleModel
            {
                PositionX = 10,
                PositionY = 10,
                Width = 30,
                Height = 40
            };
        }

        public SquareModel CreateSquare()
        {
            return new SquareModel
            {
                PositionX = 15,
                PositionY = 30,
                Width = 35
            };
        }

        public EllipseModel CreateEllipse()
        {
            return new EllipseModel
            {
                PositionX = 100,
                PositionY = 150,
                DiameterH = 300,
                DiameterV = 200
            };
        }

        public CircleModel CreateCircle()
        {
            return new CircleModel
            {
                PositionX = 1,
                PositionY = 1,
                Diameter = 300
            };
        }

        public TextBoxModel CreateTextBox()
        {
            return new TextBoxModel
            {
                PositionX = 5,
                PositionY = 5,
                Width = 200,
                Height = 100,
                Text = "sample text"
            };
        }
    }
}
