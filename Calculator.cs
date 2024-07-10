namespace Task_1_Calc_Up
{
    internal class Calculator : ICalculator
    {
        public event EventHandler<EventArgs> GotResult;        

        private Stack<double> stack = new Stack<double>();
        private Stack<CalcActionLog> stackError = new Stack<CalcActionLog>();
               
        public double Result = 0;
        public double insertValue { get; set;}

        private void RaiseEvent()
        {
            GotResult?.Invoke(this, EventArgs.Empty);
        }

        public double InsertValue()
        {
            double InsertValue;
            while (true)
            {
                Console.Write("Введите число: ");

                var key = Console.ReadKey();
                switch (key.Key)
                {
                    case ConsoleKey.Enter:
                        Exit();
                        return '\0';

                    case ConsoleKey.Spacebar:
                        Exit();
                        return '\0';

                    case ConsoleKey.Backspace:
                        CancelLast();
                        return '\0';

                    case ConsoleKey.Delete:
                        return 'D';

                }
                char key_2 = key.KeyChar;
                string valueStr = key_2 + Console.ReadLine();

                bool check = double.TryParse(valueStr, out InsertValue);
                
                if (check)
                {
                   return InsertValue;
                }
                else
                {
                    Console.WriteLine("Ошибка ввода!");
                }
            }
        }

        public double InsertFirstValue()
        {
            Result = InsertValue();
            return Result;
        }

        public void Exit()
        {
            int CursorPosition = Console.CursorTop;
            Console.SetCursorPosition(0, CursorPosition);
            Console.WriteLine("Программа завершена");
            Thread.Sleep(1500);
            Environment.Exit(0);
        }

        public char CheckOperator()
        {
            var key = Console.ReadKey();
            switch (key.Key)
            {
                case ConsoleKey.Enter:
                    Exit();
                    return '\0';

                case ConsoleKey.Spacebar:
                    Exit();
                    return '\0';

                case ConsoleKey.Add:
                    Console.WriteLine();
                    return '+';

                case ConsoleKey.Subtract:
                    Console.WriteLine();
                    return '-';

                case ConsoleKey.Divide:
                    Console.WriteLine();
                    return '/';

                case ConsoleKey.Multiply:
                    Console.WriteLine();
                    return '*';

                case ConsoleKey.Backspace:
                    CancelLast();
                    return '\0';

                case ConsoleKey.Delete:
                    return 'D';

            }
            Console.WriteLine("\nНеверный оператор");
            return '\0';

        }


        public void Div(double value)
        {

            if (value == 0)
            {
                stackError.Push(new CalcActionLog(CalculatorAction.Div, value));
                throw new CalculatorDivideByZeroException("Деление на 0 запрещено!", stackError);
            }

            stack.Push(Result);
            Result /= value;
            RaiseEvent();
        }

        public void Sub(double value)
        {

            double AddExc = Result - value;
            if (AddExc < double.MinValue)
            {
                stackError.Push(new CalcActionLog(CalculatorAction.Sub, value));                
                throw new CalculatorOperationCauseOverflowException("Арифметическое переполнение при вычитании!", stackError);
            }

            stack.Push(Result);
            Result -= value;
            RaiseEvent();
        }

        public void Add(double value)
        {
           double AddExc = Result + value;
            if (AddExc > double.MaxValue)
            {
                stackError.Push(new CalcActionLog(CalculatorAction.Add, value));
                throw new CalculatorOperationCauseOverflowException("Арифметическое переполнение при сложении!", stackError);
            }
            stack.Push(Result);
            Result += value;
            RaiseEvent();
        }

        public void Mul(double value)
        {
            double MulExc = Result * value;
            if (MulExc > double.MaxValue)
            {
                stackError.Push(new CalcActionLog(CalculatorAction.Mul, value));               
                throw new CalculatorOperationCauseOverflowException("Арифметическое переполнение при умножении!", stackError);
            }
            stack.Push(Result);
            Result *= value;
            RaiseEvent();
        }

        public void CancelLast()
        {
            Console.WriteLine("\nОтмена последнего действия");
            if (stack.Count() > 0)
            {
                Result = stack.Pop();
                RaiseEvent();
            }
        }

        public void Clear()
        {
            Console.WriteLine("\nОчистка буфера");
            stack.Clear();
            Result = 0;
            RaiseEvent();
        }


        public static implicit operator double (Calculator ints)
        {
            return ints.insertValue;
        }
    }
}
