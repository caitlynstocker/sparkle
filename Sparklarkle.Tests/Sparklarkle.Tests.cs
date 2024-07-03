using Autofac;
using Autofac.Core;
using Xunit;

namespace Sparklarkle.Tests;

public class SparkeMachine_Run_Tests
{
    private IContainer container;
    public SparkeMachine_Run_Tests()
    {
        // Set up autofac container
        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<InputChecker>().As<IInputChecker>();
        builder.RegisterType<Printer>().As<IPrinter>();
        builder.RegisterType<MockNumberProvider>().As<INumberProvider>();
        builder.RegisterType<SparkleMachine>();
        container = builder.Build();
    }
    
    [Theory]
    [InlineData("3", "\u2728 \u2728 \u2728 \r\n")]
    [InlineData("cats", "Why not? \u2728 \r\n")]
    [InlineData("12000", "Why so many?? \u2728 \r\n")]
    public void Run_HandlesVariousInputs_CorrectlyPrintsOutput(string input, string expectedOutput)
    {
        // Request an instance of SparkleMachine
        var sparkleMachine = container.Resolve<SparkleMachine>(new NamedParameter("input", input));

        // Redirect console output
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Act
            sparkleMachine.Run();

            // Assert
            var result = sw.ToString();
            Assert.Equal(expectedOutput, result);
        }
    }
    
    [Fact]
    public void Run_HandlesRandomNumber_CorrectlyPrintsOutput()
    {
        // Request an instance of SparkleMachine
        var sparkleMachine = container.Resolve<SparkleMachine>(new NamedParameter("input", "random"));
    
        // Redirect console output
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);
    
            // Act
            sparkleMachine.Run();
    
            // Assert
            var result = sw.ToString();
            Assert.Equal("\u2728 \u2728 \u2728 \u2728 \r\n", result);
        }
    }
}
