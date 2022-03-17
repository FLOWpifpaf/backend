string[] Operations = { "+", "-", "*", ":" };

Console.WriteLine("Выберете операцию: \n - - Вычитание,\n + - Сложение,\n : - Деление,\n * - Умножение.");
string? Symbol = Console.ReadLine();
if (!Operations.Contains(Symbol))
{
    Console.WriteLine("Ошибка: не существует такой операции");
    System.Environment.Exit(1);
};

Console.WriteLine("Введите первое число (тип double).");
string? Num1 = Console.ReadLine();
double Double1 = double.Parse(Num1);

Console.WriteLine("Введите второе число (тип double).");
string? Num2 = Console.ReadLine();
double Double2 = double.Parse(Num2);

double result = 0;
if (Symbol == "-")
{
    result = Double1 - Double2;
}
if (Symbol == "+")
{
    result = Double1 + Double2;
}
if (Symbol == ":")
{
    result = Double1 / Double2;
}
if (Symbol == "*")
{
    result = Double1 * Double2;
}

Console.WriteLine("Результат: " + Double1 + Symbol + Double2 + "=" + result);
