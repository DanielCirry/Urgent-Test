using System;
using System.Collections.Generic;
using System.Text;
using Urgent.Domain.Contracts;
using Urgent.Domain.Enums;
using Urgent.Domain.Helpers;
using Urgent.Domain.Models;

namespace Urgent.Domain.Services
{
    public class UrgentService : IUrgentService
    {
        public ErrorLogsModel _errors = new ErrorLogsModel();

        public string CreateWidget(bool broken = false)
        {
            var defaultWidget = new DefaultWidget();

            var rectangle = defaultWidget.CreateRectangle();
            var square = defaultWidget.CreateSquare();
            var ellipse = defaultWidget.CreateEllipse();
            var circle = defaultWidget.CreateCircle();
            var textBox = defaultWidget.CreateTextBox();

            var widgetModel = new WidgetModel
            {
                Rectangles = new List<RectangleModel> {rectangle},
                Squares = new List<SquareModel> {square},
                Ellipses = new List<EllipseModel> {ellipse},
                Circles = new List<CircleModel> {circle},
                TextBoxes = new List<TextBoxModel> {textBox}
            };
            if (broken)
                widgetModel.Rectangles = null;

            return WidgetFormatting(widgetModel);
        }

        public string WidgetFormatting(WidgetModel widgetModel)
        {
            var builder = new StringBuilder();

            if (widgetModel == null)
                return null;
            try
            {
                foreach (var rectangle in widgetModel.Rectangles)
                {
                    builder.Append(
                            $"Rectangle ({rectangle.PositionX},{rectangle.PositionY}) width={rectangle.Width} height={rectangle.Height}")
                        .AppendLine();
                }

                foreach (var square in widgetModel.Squares)
                {
                    builder.Append(
                            $"Square ({square.PositionX},{square.PositionY}) size={square.Width}")
                        .AppendLine();
                }

                foreach (var ellipse in widgetModel.Ellipses)
                {
                    builder.Append(
                            $"Ellipse ({ellipse.PositionX},{ellipse.PositionY}) diameterH = {ellipse.DiameterH} diameterV = {ellipse.DiameterV}")
                        .AppendLine();
                }

                foreach (var circle in widgetModel.Circles)
                {
                    builder.Append(
                            $"Circle ({circle.PositionX},{circle.PositionY}) size={circle.Diameter}")
                        .AppendLine();
                }

                foreach (var textBox in widgetModel.TextBoxes)
                {
                    builder.Append(
                        $"Textbox ({textBox.PositionX},{textBox.PositionY}) width={textBox.Width} height={textBox.Height} text=\"{textBox.Text}\"");
                }
                return builder.ToString();
            }
            catch (Exception e)
            {
                ErrorsLog(e.Message);
            }

            return null;
        }

        public OperationType GetOperationType(string answer)
        {
            try
            {
                int.TryParse(answer, out int parsedAnswer);
                switch (parsedAnswer)
                {
                    case 1:
                        return OperationType.Correct;
                    case 2:
                        return OperationType.Incorrect;
                    case 3:
                        return OperationType.ErrorsLog;
                    case 4:
                        return OperationType.Exit;
                    default:
                        return OperationType.Default;
                }
            }
            catch (Exception e)
            {
                ErrorsLog(e.Message);
                return OperationType.Default;
            }
        }

        public void ErrorsLog(string error)
        {
            if (_errors.Errors == null)
                _errors.Errors = new List<string> {$"{DateTime.UtcNow}: {error}"};
            else
                _errors.Errors.Add( $"{DateTime.UtcNow}: {error}");
        }
        public List<string> ErrorsLog()
        {
            return _errors.Errors;
        }
    }
}
