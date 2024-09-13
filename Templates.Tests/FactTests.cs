using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.TemplateEngine.Authoring.TemplateVerifier;

namespace Templates.Tests
{
    public class FactTests
    {
        // Ne fonctionne pas...
        [Fact]
        public async Task Given_parameters_When_condition_Should_result_()
        {
            // Arrange
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddDebug())
                .BuildServiceProvider();

            ILoggerFactory? factory = serviceProvider.GetService<ILoggerFactory>();

            ILogger<FactTests> logger = factory!.CreateLogger<FactTests>();

            TemplateVerifierOptions options = new(templateName: "p-fact")
            {
                TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Templates\Fact\"),
                TemplateSpecificArgs = new[] { "-n", "FactTests" },
            };

            VerificationEngine engine = new(logger);

            // Act
            Func<Task> action = async () => await engine.Execute(options).ConfigureAwait(false);

            // Assert
            _ = await action.Should().NotThrowAsync();
        }
    }
}
