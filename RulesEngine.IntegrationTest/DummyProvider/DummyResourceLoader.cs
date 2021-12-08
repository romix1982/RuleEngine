using RulesEngine.Infrastructure.Provider;
using System.Reflection;

namespace RulesEngine.IntegrationTest.DummyProvider
{
    public class DummyResourceLoader : IResourceLoader
    {
        public byte[] GetResourceBytes(string resourceName)
        {
            var assembly = Assembly.GetExecutingAssembly();

            using var stream = assembly.GetManifestResourceStream(resourceName);
            var bytes = new byte[stream.Length];
            stream.Read(bytes, 0, (int)stream.Length);

            return bytes;
        }
    }
}
