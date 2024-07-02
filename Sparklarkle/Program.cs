using System.Text;

namespace Sparklarkle;

public class Program
{
    static void Main(string[] args)
    {
        Console.WriteLine("Want some sparkles?");
        string input;
        do
        {
            input = Console.ReadLine() ?? "";
            UnnecessaryClientClass client = new UnnecessaryClientClass(input);
            client.Run();
        } while (input != "exit");
    }
}

public class InputChecker(string input)
{
    public int Check()
    {
        if (int.TryParse(input, out int result)) return result;

        return 0;
    }
}

public interface IPrint
{
    void Print();
}

public class PrintSparkles(int num): IPrint
{
    public void Print()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string sparkleString = "\u2728 ";

        Enumerable.Range(0, num).ToList().ForEach(_ => Console.Write(sparkleString));
        Console.WriteLine();
    }
}

public class PrintText(string str): IPrint
{
    public void Print()
    {
        Console.OutputEncoding = Encoding.UTF8;
        string sparkleString = "\u2728 ";

        Console.WriteLine(str + sparkleString);
    }
}

public class UnnecessaryClientClass(string input)
{
    public void Run()
    {
        InputChecker checkInput = new InputChecker(input);
        int num = checkInput.Check();

        if (num == 0)
        {
            string text = input.ToString() ?? "Sparkle";
            PrintText printText = new PrintText(text); 
            printText.Print();
        }
        else if (num > 1000)
        {
            PrintText printText = new PrintText("Really? Why so many??"); 
            printText.Print();
        }
        else
        {
            PrintSparkles printSparkles = new PrintSparkles(num);
            printSparkles.Print();
        }   
    }
}