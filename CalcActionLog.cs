using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_Calc_Up
{
    internal class CalcActionLog
    {
        public CalculatorAction CalcAction { get; private set; }
        public double CalcArgument { get; private set; }

        public CalcActionLog ( CalculatorAction calculatorAction, double CalcArgument)
        {
            this.CalcAction = calculatorAction;
            this.CalcArgument = CalcArgument;
        }
    }
}
