using System;
using System.Collections.Generic;
using Urgent.Domain.Constants;
using Urgent.Domain.Contracts;
using Urgent.Domain.Enums;

namespace Urgent.Application.Views
{
    public class UrgentApplication
    {
        private readonly IUrgentService _urgentService;

        public UrgentApplication(IUrgentService urgentService)
        {
            _urgentService = urgentService;
        }

        public void Run()
        {
            while (true)
            {
                InitialMessage();
                var answer = ReadInput();
                Console.WriteLine();
                var operationType = _urgentService.GetOperationType(answer);
                
                OptionToShow(operationType);
            }
        }

        private static void InitialMessage()
        {
            Console.WriteLine(TextVariables.InitialMessage);
            Console.WriteLine(TextVariables.CorrectOutput);
            Console.WriteLine(TextVariables.IncorrectOutput);
            Console.WriteLine(TextVariables.ShowErrorsFile);
            Console.WriteLine(TextVariables.Exit);
        }

        public string ReadInput()
        {
            return Console.ReadLine();
        }

        public void OptionToShow(OperationType operation)
        {
            switch (operation)
            {
                case OperationType.Correct:
                    CreateOutput();
                    Console.WriteLine();
                    break;

                case OperationType.Incorrect:
                    CreateOutput(true);
                    Console.WriteLine();
                    break;

                case OperationType.ErrorsLog:
                    var errors = ErrorsLog();
                    if (errors == null)
                        Console.WriteLine(TextVariables.NothingToShowHere);
                    else
                    {
                        foreach (var error in errors)
                        {
                            Console.WriteLine(error);
                        }
                    }

                    Console.WriteLine();
                    break;
                case OperationType.Exit:
                    Environment.Exit(1);
                    break;
                default:
                    ShowInputError();
                    Console.WriteLine();
                    break;
            }
        }

        private static void ShowInputError()
        {
            Console.WriteLine(TextVariables.InputNotRecognize);
        }

        public void CreateOutput(bool broken = false)
        {

            var listOutput = new List<string>();
            var bill = CreateBill(broken);

            if (bill == null)
                Console.WriteLine(TextVariables.Abort);
            else
            {
                listOutput.Add(TextVariables.Divider);
                listOutput.Add(TextVariables.Title);
                listOutput.Add(TextVariables.Divider);
                listOutput.Add(bill);
                listOutput.Add(TextVariables.Divider);

                foreach (var output in listOutput)
                {
                    Console.WriteLine(output);
                }
            }
        }

        public string CreateBill(bool broken)
        {
            return  _urgentService.CreateWidget(broken);
        }
        

        public List<string> ErrorsLog()
        {
            return _urgentService.ErrorsLog();
        }
    }
}
