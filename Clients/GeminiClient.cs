using System.Text;
using ResumeGenerator.Controllers;
using ResumeGenerator.Utilities;
using Newtonsoft.Json;
using System.Net.Http;

namespace ResumeGenerator.Clients
{
	public class GeminiClient : IGeminiClient
	{
		private readonly ILogger<ResumeController> _logger;
		private readonly IConfigProvider _configProvider;

		public GeminiClient(ILogger<ResumeController> logger, IConfigProvider configProvider)
		{
			_logger = logger;
			_configProvider = configProvider;
		}

		public string SmackGeminiApi(string prompt)
		{
			var endpointUrl = string.Concat(
					_configProvider.GeminiConfig.BaseURL,
					_configProvider.GeminiConfig.Key);

			using (HttpClient client = new HttpClient())
			{
				var payload = GeneratePayload(prompt);

				var content = new StreamContent(new MemoryStream(Encoding.UTF8.GetBytes(payload)));
				content.Headers.ContentType = new System.Net.Http.Headers.MediaTypeHeaderValue("application/json");

				var response = client.PostAsync(endpointUrl, content)
					.Result
					.Content
					.ReadAsStringAsync()
					.Result;

				//var thing = JsonConvert.DeserializeObject(response);

				return response;
			}
		}

		private static string GenerateImagePayload(string text, string imageUrl)
		{
			var payload = new
			{
				contents = new
				{
					role = "USER",
					parts = new object[] {
					new {text = text},
					new {file_data = new {
							mime_type = "image/png",
							file_uri = imageUrl
						}
					}
				}
				},
				generation_config = new
				{
					temperature = 0.4,
					top_p = 1,
					top_k = 32,
					max_output_tokens = 2048
				}
			};
			return JsonConvert.SerializeObject(payload);
		}

		private static string GeneratePayload(string text)
		{
			var payload = new
			{
				contents = new
				{
					role = "USER",
					parts = new object[] {
					new {text = text},
					new {file_data = new {
							mime_type = "TODO",
						}
					}
				}
				},
				generation_config = new
				{
					temperature = 0.4,
					top_p = 1,
					top_k = 32,
					max_output_tokens = 2048
				}
			};
			return JsonConvert.SerializeObject(payload);
		}
	}

	public class ContentPart
	{
		public string Role { get; set; }
		public string Text { get; set; }
	}

	public class GeminiApiRequest
	{
		public List<ContentPart> Contents { get; set; }
	}
}
