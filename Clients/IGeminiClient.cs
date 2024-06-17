
namespace ResumeGenerator.Clients
{
	public interface IGeminiClient
	{
		string SmackGeminiApi(string prompt);
	}
}