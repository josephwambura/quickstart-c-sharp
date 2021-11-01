Console.Write("Enter Base Number: ");

var baseNumber = Convert.ToDouble(Console.ReadLine());

Console.Write("Enter Power Number: ");

var powerNumber = Convert.ToDouble(Console.ReadLine());

var result = 1.0;

if (powerNumber >= 0)
{
    for (long i = 0; i < powerNumber; i++)
    {
        result *= baseNumber;
    }
}
else
{
    for (long i = -1; i > powerNumber; i--)
    {
        result /= baseNumber;
    }
}

Console.Write($"{baseNumber} raised to power {powerNumber} is equal to : {result}");

Console.ReadLine();