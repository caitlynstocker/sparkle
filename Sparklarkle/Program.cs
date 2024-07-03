using System.Text;
using Autofac;

namespace Sparklarkle;

public class Program
{
    static void Main()
    {
        Console.OutputEncoding = Encoding.UTF8;

        // Once all your types are registered on your autofac container
        // you can request instances of them using container.Resolve, as below
        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<InputChecker>().As<IInputChecker>();
        builder.RegisterType<Printer>().As<IPrinter>();
        builder.RegisterType<NumberProvider>().As<INumberProvider>();
        builder.RegisterType<SparkleMachine>();
        IContainer container = builder.Build();
        
        Console.WriteLine("Want some sparkles?");
        string input;
        do
        {
            input = Console.ReadLine() ?? "";
            var client = container.Resolve<SparkleMachine>(new NamedParameter("input", input));
            client.Run();
        } while (input != "exit");
    }
}

public interface IInputChecker // not actually required in this case
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

public interface IPrinter // not actually required in this case
{
    void Print(int num, string? str = null);
}

public class Printer(): IPrinter
{
    public void Print(int num, string? str)
    {
        string sparkleString = "\u2728 ";

        Enumerable.Range(0, num).ToList().ForEach(_ => Console.Write(sparkleString));
        Console.WriteLine();
    }
}

public class SparkleMachine
{
    // Passes instances of service classes in constructor 
    public SparkleMachine(string input, IInputChecker checkInput, IPrinter printer, INumberProvider numberProvider)
    {
        _Input = input;
        _CheckInput = checkInput;           // Supplied by autofac
        _Printer = printer;                 // Supplied by autofac
        _NumberProvider = numberProvider;   // Supplied by autofac
    }

    string _Input;
    IInputChecker _CheckInput;
    IPrinter _Printer;
    INumberProvider _NumberProvider;
    
    public void Run()
    {
        int num = _CheckInput.Check(_Input);

        if (num == 0)
        {
            if (_Input == "random")
            {
                int randomNumber = _NumberProvider.GetRandomNumber();
                _Printer.Print(randomNumber);
            }
            else
            {
                Console.WriteLine("Why not? \u2728 ");
            }
        }
        else if (num > 10000)
        {
            Console.WriteLine("Why so many?? \u2728 ");
        }
        else
        {
            _Printer.Print(num);
        }   
    }
}