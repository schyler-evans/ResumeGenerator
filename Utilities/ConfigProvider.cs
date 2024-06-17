using System.Runtime.CompilerServices;
using Microsoft.AspNetCore.DataProtection.KeyManagement;

namespace ResumeGenerator.Utilities
{
	public class ConfigProvider : IConfigProvider
	{
		public GeminiConfig GeminiConfig { get; }

		public ConfigProvider(IConfiguration configuration)
		{
			GeminiConfig = new GeminiConfig { 
				Key = configuration.GetSection("GeminiConfig").GetValue<string>("Key"),
				BaseURL = configuration.GetSection("GeminiConfig").GetValue<string>("BaseURL")
			};
		}
	}

	public class GeminiConfig
	{
		public string Key;
		public string BaseURL;
	}
}
