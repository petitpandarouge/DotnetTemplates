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

			ILogger<MyTestClass> logger = factory.CreateLogger<MyTestClass>();

			TemplateVerifierOptions options = new(templateName: "ut")
			{
				TemplateSpecificArgs = new[] { "-tf", "net6.0" },
				VerifyCommandOutput = false,
			};

			VerificationEngine engine = new(logger);
			await engine.Execute(options).ConfigureAwait(false);
		}
	}
}