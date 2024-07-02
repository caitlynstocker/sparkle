﻿using System.Text;
using Autofac;

namespace Sparklarkle;

public class Program
{
    static void Main(string[] args)
    {

        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<InputChecker>().As<IInputChecker>();
        builder.RegisterType<Printer>().As<IPrint>();
        builder.RegisterType<UnnecessaryClientClass>();
        IContainer container = builder.Build();
        
        Console.WriteLine("Want some sparkles?");
        string input;
        do
        {
            input = Console.ReadLine() ?? "";
            var client = container.Resolve<UnnecessaryClientClass>(new NamedParameter("input", input));
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
    void Print(int num, string? str = null);
}

public class Printer(): IPrint
{
    public void Print(int num, string? str)
    {
        Console.OutputEncoding = Encoding.UTF8;
        string sparkleString = "\u2728 ";

        Enumerable.Range(0, num).ToList().ForEach(_ => Console.Write(sparkleString));
        Console.WriteLine();
    }
}

public class UnnecessaryClientClass
{
    public UnnecessaryClientClass(string input, IInputChecker checkInput, IPrint printer)
    {
        _Input = input;
        _CheckInput = checkInput;
        _Printer = printer;
    }

    string _Input;
    IInputChecker _CheckInput;
    IPrint _Printer;
    
    public void Run()
    {
        int num = _CheckInput.Check(_Input);

        if (num == 0)
        {
            Console.WriteLine("Why not? \u2728 ");
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