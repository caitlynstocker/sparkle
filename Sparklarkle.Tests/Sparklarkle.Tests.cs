using Autofac;
using Autofac.Core;
using Xunit;

namespace Sparklarkle.Tests;

public class SparkleMachineTests
{
    [Theory]
    [InlineData("3", "\u2728 \u2728 \u2728 \r\n")]
    [InlineData("cats", "Why not? \u2728 \r\n")]
    [InlineData("12000", "Why so many?? \u2728 \r\n")]
    public void Run_HandlesVariousInputs_CorrectlyPrintsOutput(string input, string expectedOutput)
    {
        // Set up autofac container
        ContainerBuilder builder = new ContainerBuilder();
        builder.RegisterType<InputChecker>().As<IInputChecker>();
        builder.RegisterType<Printer>().As<IPrinter>();
        builder.RegisterType<SparkleMachine>();
        IContainer container = builder.Build();
        
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
}