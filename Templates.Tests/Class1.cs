using FluentAssertions;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using Microsoft.TemplateEngine.Authoring.TemplateVerifier;

namespace Templates.Tests
{
	public class MyTestClass
	{
		[Fact]
		public async Task MyTemplate_InstantiationTest()
		{
			ServiceProvider serviceProvider = new ServiceCollection()
				.AddLogging(builder => builder.AddDebug())
				.BuildServiceProvider();

			ILoggerFactory? factory = serviceProvider.GetService<ILoggerFactory>();

			ILogger<MyTestClass> logger = factory!.CreateLogger<MyTestClass>();

			TemplateVerifierOptions options = new(templateName: "p-unit")
			{
				TemplatePath = Path.Combine(Directory.GetCurrentDirectory(), @"..\..\..\..\Templates\UnitTests\"),
				TemplateSpecificArgs = new[] { "-f", "net6.0" },
			};

			VerificationEngine engine = new(logger);
			Func<Task> action = async () => await engine.Execute(options).ConfigureAwait(false);

			_ = await action.Should().NotThrowAsync();

		}
	}
}