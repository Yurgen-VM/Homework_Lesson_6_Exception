using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Task_1_Calc_Up
{
    internal interface ICalculator
    {
        event EventHandler<EventArgs> GotResult;
        void Add(double value);
        void Sub(double value);
        void Div(double value);
        void Mul(double value);
        void CancelLast();
        double InsertFirstValue();
        double InsertValue ();
        void Exit();
        char CheckOperator();
        void Clear();
        
    }
}

