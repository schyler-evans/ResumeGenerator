using Microsoft.AspNetCore.Http;
using System.Net;
using System.Text;
using Microsoft.AspNetCore.Mvc;
using ResumeGenerator.Utilities;
using ResumeGenerator.Clients;

namespace ResumeGenerator.Controllers
{
	[ApiController]
	[Route("[controller]")]
	public class ResumeController : ControllerBase
	{
		private readonly ILogger<ResumeController> _logger;
		private readonly IConfigProvider _configProvider;
		private readonly IGeminiClient _geminiClient;

		public ResumeController(ILogger<ResumeController> logger, IConfigProvider configProvider, IGeminiClient geminiClient)
		{
			_logger = logger;
			_configProvider = configProvider;
			_geminiClient = geminiClient;
		}
		
		//[HttpPost(Name = "GetResumeSummary")]
		//public String GetResumeSummary()
		//{
		//	throw new NotImplementedException();
		//}

		[HttpGet(Name = "GetKey")]
		public string GetKey()
		{
			var thing = _geminiClient.SmackGeminiApi("This is a test");

			return thing;
		}
	}
}
