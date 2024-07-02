using System.Text;
using Autofac;

namespace Sparklarkle;

public class Program
{
    static void Main(string[] args)
    {

        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<InputChecker>().As<IInputChecker>();
        builder.RegisterType<TextPrinter>().As<IPrint>();
        builder.RegisterType<SparklePrinter>().As<IPrint>();
        builder.RegisterType<UnnecessaryClientClass>();
        IContainer container = builder.Build();
        
        Console.WriteLine("Want some sparkles?");
        string input;
        do
        {
            input = Console.ReadLine() ?? "";
            var client = container.Resolve<UnnecessaryClientClass>();
            client.Run();
        } while (input != "exit");
    }
}

public interface IInputChecker
{
    int Check(string input);
}

public class InputChecker(): IInputChecker
{
    public int Check(string input)
    {
        if (int.TryParse(input, out int result)) return result;

        return 0;
    }
}

public interface IPrint
{
    void PrintString(string str);
    void PrintNumber(int num);
}

public class SparklePrinter(): IPrint
{
    public void PrintNumber(int num)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string sparkleString = "\u2728 ";

        Enumerable.Range(0, num).ToList().ForEach(_ => Console.Write(sparkleString));
        Console.WriteLine();
    }

    public void PrintString(string str)
    {
    }
}

public class TextPrinter(): IPrint
{
    public void PrintString(string str)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string sparkleString = "\u2728 ";

        Console.WriteLine(str + sparkleString);
    }
    
    public void PrintNumber(int num)
    {
    }
}

public class UnnecessaryClientClass
{
    public UnnecessaryClientClass(string input, IInputChecker checkInput, IPrint printText, IPrint printSparkles)
    {
        _Input = input;
        _CheckInput = checkInput;
        _PrintText = printText;
        _PrintSparkles = printSparkles;
    }

    string _Input;
    IInputChecker _CheckInput;
    IPrint _PrintText;
    IPrint _PrintSparkles;
    
    public void Run()
    {
        // InputChecker checkInput = new InputChecker(input);
        int num = _CheckInput.Check(_Input);

        if (num == 0)
        {
            string text = _Input.ToString() ?? "Sparkle";
            // PrintText printText = new PrintText(text); 
            _PrintText.PrintString(text);
        }
        else if (num > 10000)
        {
            // PrintText printText = new PrintText("Really? Why so many??"); 
            _PrintText.PrintString("Really? Why so many??");
        }
        else
        {
            // PrintSparkles printSparkles = new PrintSparkles(num);
            _PrintSparkles.PrintNumber(num);
        }   
    }
}