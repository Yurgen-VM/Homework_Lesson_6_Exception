using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_Calc_Up
{
    internal class CalculatorException : Exception
    {
        public CalculatorException() { }        
        public CalculatorException(string message, Exception e): base(message, e) { }
        public CalculatorException(string message, Stack<CalcActionLog> log): base(message) { ActionLogs = log; }

        public Stack<CalcActionLog> ActionLogs { get; private set; } = new();

        public override string ToString()
        {
            //return Message + ":)))))" + string.Join(" \n", ActionLogs.Select(x => $"{x.CalcAction} , {x.CalcArgument}"));
            return Message + " " + string.Join("\n", ActionLogs.Select(x => $"Операция вызвавшая исключение - {x.CalcAction}. Переданный аргумент - {x.CalcArgument}\n"));
        }

    }

    internal class CalculatorDivideByZeroException : CalculatorException
    {
        public CalculatorDivideByZeroException(string message, Stack<CalcActionLog> actionLogs) : base(message, actionLogs) { }
        public CalculatorDivideByZeroException(string message, Exception e) : base(message, e) { }
    }

    internal class CalculatorOperationCauseOverflowException : CalculatorException
    {
        public CalculatorOperationCauseOverflowException(string message, Stack<CalcActionLog> actionLogs) : base(message, actionLogs) { }
        public CalculatorOperationCauseOverflowException(string message, Exception e) : base(message, e) { }
    }    
}
