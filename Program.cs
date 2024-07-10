using Task_1_Calc_Up;

namespace Task_1
{

    /*
     
    Доработайте программу калькулятор реализовав выбор действий
    и вывод результатов на экран в цикле так, чтобы калькулятор мог работать до тех пор
    пока пользователь не нажмет отмена или введёт пустую строку.

    Доработайте реализацию калькулятора разработав собственные типы ошибок:
    CalculatorDivideByZeroException
    CalculateOperationCauseOverflowException

    В прошлой лекции мы реализовали метод CancelLast позволяющий отменить любое сделанное действие.
    Реализуйте класс - список, описывающий последовательность действий приведших исключению.
    Очевидно что класс калькулятора должен быть доработан чтобы хранить не только информацию необходимую нам
    для отмены но и информацию о самом действии. Продумайте как лучше это реализовать.
                
    */


    internal class Program
    {

        static void Execute(Action<double> action, double value)
        {
            try
            {
                action.Invoke(value);
            }
            catch (CalculatorDivideByZeroException ex)
            {
                Console.WriteLine(ex);
            }
            catch (CalculatorOperationCauseOverflowException ex)
            {
                Console.WriteLine(ex);
            }

        }

        static void Calculator_GotResult(object sendler, EventArgs eventArgs)
        {
            Console.WriteLine($"Результат: {((Calculator)sendler).Result} \n");
        }

        static void Main(string[] args)
        {
            Console.WriteLine("Для расчетов используйте операторы: + - / *");
            Console.WriteLine("Для сброса на 0 нажмите  - Delete");
            Console.WriteLine("Для выхода нажмите Space или Enter");
            Console.WriteLine("_______________________________________\n");

            ICalculator calc = new Calculator();
            calc.GotResult += Calculator_GotResult;

            while (true)
            {
                calc.InsertFirstValue();

                while (true)
                {

                    Console.Write("Укажите оператор: ");
                    switch (calc.CheckOperator())
                    {
                        case '+':

                            double AddSecondValue = calc.InsertValue();
                            Execute(calc.Add, AddSecondValue);
                            //calc.Add(AddSecondValue);
                            break;

                        case '-':

                            double SubSecondValue = calc.InsertValue();
                            Execute(calc.Sub, SubSecondValue);
                            //calc.Sub(SubSecondValue);
                            break;

                        case '*':

                            double MulSecondValue = calc.InsertValue();
                            Execute(calc.Mul, MulSecondValue);
                            //calc.Mul(MulSecondValue);
                            break;

                        case '/':

                            double DivSecondValue = calc.InsertValue();
                            Execute(calc.Div, DivSecondValue);
                            //calc.Div(DivSecondValue);
                            break;

                        case 'D':

                            calc.Clear();
                            calc.InsertFirstValue();
                            break;
                    }
                }
            }
        }
    }
}

