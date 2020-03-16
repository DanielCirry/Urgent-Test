using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FakeItEasy;
using Urgent.Application;
using Urgent.Application.Views;
using Urgent.Domain.Contracts;
using Urgent.Domain.Enums;
using Urgent.Domain.Models;
using Xunit;

namespace Urgent.Tests
{
    public class UrgentApplicationUnitTests
    {
        [Fact]
        public void CreateWidget_NotBroken_ReturnsValidOutput()
        {
            //Given
            var urgentService = A.Fake<IUrgentService>();
            var widgetModel = CreateWidgetModel();
            var formattedWidget = WidgetFormatting(widgetModel);
            
            //When
            A.CallTo(() =>urgentService.CreateWidget(false)).Returns(
                $"Rectangle ({widgetModel.Rectangles[0].PositionX},{widgetModel.Rectangles[0].PositionY}) width={widgetModel.Rectangles[0].Width} height={widgetModel.Rectangles[0].Height}\r\n" +
                $"Square ({widgetModel.Squares[0].PositionX},{widgetModel.Squares[0].PositionY}) size={widgetModel.Squares[0].Width}\r\n" +
                $"Ellipse ({widgetModel.Ellipses[0].PositionX},{widgetModel.Ellipses[0].PositionY}) diameterH = {widgetModel.Ellipses[0].DiameterH} diameterV = {widgetModel.Ellipses[0].DiameterV}\r\n" +
                $"Circle ({widgetModel.Circles[0].PositionX},{widgetModel.Circles[0].PositionY}) size={widgetModel.Circles[0].Diameter}\r\n" +
                $"Textbox ({widgetModel.TextBoxes[0].PositionX},{widgetModel.TextBoxes[0].PositionY}) width={widgetModel.TextBoxes[0].Width} height={widgetModel.TextBoxes[0].Height} text=\"{widgetModel.TextBoxes[0].Text}\"");

            
            //Then
            Assert.Equal(formattedWidget, urgentService.CreateWidget(false));
        }

        [Fact]
        public void CreateWidget_Broken_ReturnsInvalidOutput()
        {
            //Given
            var urgentService = A.Fake<IUrgentService>();
            var widgetModel = CreateWidgetModel();
            var formattedWidget = WidgetFormatting(widgetModel).Remove(0,36);

            //When
            A.CallTo(() => urgentService.CreateWidget(true)).Returns(
                $"Square ({widgetModel.Squares[0].PositionX},{widgetModel.Squares[0].PositionY}) size={widgetModel.Squares[0].Width}\r\n" +
                $"Ellipse ({widgetModel.Ellipses[0].PositionX},{widgetModel.Ellipses[0].PositionY}) diameterH = {widgetModel.Ellipses[0].DiameterH} diameterV = {widgetModel.Ellipses[0].DiameterV}\r\n" + 
                $"Circle ({widgetModel.Circles[0].PositionX},{widgetModel.Circles[0].PositionY}) size={widgetModel.Circles[0].Diameter}\r\n" +
                $"Textbox ({widgetModel.TextBoxes[0].PositionX},{widgetModel.TextBoxes[0].PositionY}) width={widgetModel.TextBoxes[0].Width} height={widgetModel.TextBoxes[0].Height} text=\"{widgetModel.TextBoxes[0].Text}\"");

            //Then
            Assert.Equal(formattedWidget, urgentService.CreateWidget(true));
        }

        [Fact]
        public void ErrorsLog_NotBroken_ReturnsValidOutput()
        {
            //Given
            var urgentService = A.Fake<IUrgentService>();
            var error = new List<string>();
            error.Add($"{DateTime.UtcNow}: a custom error");

            //When
            A.CallTo(() => urgentService.ErrorsLog()).Returns(error);

            //Then
            Assert.Equal(error, urgentService.ErrorsLog());
        }


        [Fact]
        public void GetOperationType_NotBroken_ReturnsValidOutput()
        {
            //Given
            var urgentService = A.Fake<IUrgentService>();
            var operationType1 = "1";
            var operationType2 = "2";
            var operationType3 = "3";
            var operationType4 = "4";
            var operationType0 = "0";
            var operationTypeX = "X";

            //When
            A.CallTo(() => urgentService.GetOperationType(operationType1)).Returns(OperationType.Correct);
            A.CallTo(() => urgentService.GetOperationType(operationType2)).Returns(OperationType.Incorrect);
            A.CallTo(() => urgentService.GetOperationType(operationType3)).Returns(OperationType.ErrorsLog);
            A.CallTo(() => urgentService.GetOperationType(operationType4)).Returns(OperationType.Exit);
            A.CallTo(() => urgentService.GetOperationType(operationType0)).Returns(OperationType.Default);
            A.CallTo(() => urgentService.GetOperationType(operationTypeX)).Returns(OperationType.Default);

            //Then
            Assert.Equal(OperationType.Correct, urgentService.GetOperationType(operationType1));
            Assert.Equal(OperationType.Incorrect, urgentService.GetOperationType(operationType2));
            Assert.Equal(OperationType.ErrorsLog, urgentService.GetOperationType(operationType3));
            Assert.Equal(OperationType.Exit, urgentService.GetOperationType(operationType4));
            Assert.Equal(OperationType.Default, urgentService.GetOperationType(operationType0));
            Assert.Equal(OperationType.Default, urgentService.GetOperationType(operationTypeX));
        }

        public string WidgetFormatting(WidgetModel widgetModel)
        {
            var builder = new StringBuilder();

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


public WidgetModel CreateWidgetModel()
        {
            return new WidgetModel()
            {
                Rectangles = new List<RectangleModel> {CreateRectangleModel()},
                Squares = new List<SquareModel> {CreateSquareModel()},
                Ellipses = new List<EllipseModel>{CreateEllipseModel()},
                Circles = new List<CircleModel> {CreateCircleModel()},
                TextBoxes = new List<TextBoxModel> {CreateTextBoxModel()},
            };
        }

        public RectangleModel CreateRectangleModel()
        {
            return new RectangleModel()
            {
                PositionX = 0,
                PositionY = 10,
                Height = 51,
                Width = 5
            };
        }

        public SquareModel CreateSquareModel()
        {
            return new SquareModel
            {
                PositionX = 15,
                PositionY = 30,
                Width = 35
            };
        }

        public EllipseModel CreateEllipseModel()
        {
            return new EllipseModel
            {
                PositionX = 100,
                PositionY = 150,
                DiameterH = 300,
                DiameterV = 200
            };
        }

        public CircleModel CreateCircleModel()
        {
            return new CircleModel
            {
                PositionX = 1,
                PositionY = 1,
                Diameter = 300
            };
        }

        public TextBoxModel CreateTextBoxModel()
        {
            return new TextBoxModel
            {
                PositionX = 5,
                PositionY = 5,
                Width = 200,
                Height = 100,
                Text = "some text"
            };
        }

        public ErrorLogsModel CreateErrorLogsModel()
        {
            return new ErrorLogsModel()
            {
                Errors = new List<string>()
            };
        }
    }
}
