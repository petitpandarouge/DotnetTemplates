using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.TemplateEngine.Authoring.TemplateVerifier;

namespace Templates.Tests
{
    public class UnitTestsProject
    {
        [Theory]
        [InlineData("net8.0")]
        [InlineData("net9.0")]
        public async Task Given_an_allowed_framework_When_I_create_a_project_Should_succeed(string framework)
        {
            // Arrange
            ServiceProvider serviceProvider = new ServiceCollection()
                .AddLogging(builder => builder.AddDebug())
                .BuildServiceProvider();

            ILoggerFactory? factory = serviceProvider.GetService<ILoggerFactory>();

            ILogger<UnitTestsProject> logger = factory!.CreateLogger<UnitTestsProject>();

            TemplateVerifierOptions options = new(templateName: "p-unit")
            {
                TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Templates\UnitTestsProject\"),
                TemplateSpecificArgs = new[] { "-f", framework },
            };

            VerificationEngine engine = new(logger);

            // Act
            Func<Task> action = async () => await engine.Execute(options).ConfigureAwait(false);

            // Assert
            _ = await action.Should().NotThrowAsync();
        }
    }
}
