namespace Sparklarkle;

// Adding this to experiment with mocking

public interface INumberProvider
{
    int GetRandomNumber();
}
public class NumberProvider: INumberProvider
{
    readonly Random random = new();

    public int GetRandomNumber() => random.Next(0, 153);
}

public class MockNumberProvider : INumberProvider
{
    public int GetRandomNumber() => 4;
}