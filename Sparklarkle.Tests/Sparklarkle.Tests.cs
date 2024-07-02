using Xunit;

namespace Sparklarkle.Tests;

public class UnnecessaryClientClassTests
{
    [Theory]
    [InlineData("3", "\u2728 \u2728 \u2728 \r\n")]
    [InlineData("cats", "cats\u2728 \r\n")]
    [InlineData("5000", "Really? Why so many??\u2728 \r\n")]
    public void Run_HandlesVariousInputs_CorrectlyPrintsOutput(string input, string expectedOutput)
    {
        // Arrange
        var client = new UnnecessaryClientClass(input);

        // Redirect console output
        using (var sw = new StringWriter())
        {
            Console.SetOut(sw);

            // Act
            client.Run();

            // Assert
            var result = sw.ToString();
            Assert.Equal(expectedOutput, result);
        }
    }
}