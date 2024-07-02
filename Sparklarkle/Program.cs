// See https://aka.ms/new-console-template for more information

using System.Text;

namespace Sparklarkle;

public class Program
{
    static void Main(string[] args)
    {
        StringInput input = new StringInput();
        int num = input.GetInput();
        
        Console.OutputEncoding = Encoding.UTF8;
        string sparkleString = "\u2728 ";

        Enumerable.Range(0, num).ToList().ForEach(_ => Console.Write(sparkleString));
    }
}

public interface IInput
{
    int GetInput();
}

public class StringInput: IInput
{
    public int GetInput()
    {
        if (int.TryParse(Console.ReadLine(), out int result)) return result;

        return 0;
    }
}