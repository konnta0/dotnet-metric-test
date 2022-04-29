using Microsoft.Extensions.Logging;
using Pulumi;
using Pulumi.Kubernetes.Yaml;

namespace Infrastructure.CI_CD.Tekton
{
    public class Tekton
    {
        private readonly ILogger<Tekton> _logger;
        private Config _config;

        public Tekton(ILogger<Tekton> logger, Config config)
        {
            _logger = logger;
            _config = config;
        }

        public void Apply()
        {
            var configFile = new ConfigFile("tekton-controller-release", new ConfigFileArgs
            {
                File = "https://storage.googleapis.com/tekton-releases/pipeline/previous/v0.35.0/release.yaml"
            });
            configFile.Ready();

            var dashboardConfigFile = new ConfigFile("tekton-dashboard-release", new ConfigFileArgs
            {
                File = "https://github.com/tektoncd/dashboard/releases/download/v0.25.0/tekton-dashboard-release.yaml"
            });
            dashboardConfigFile.Ready();
            
            var triggersConfigFile = new ConfigFile("tekton-triggers-release", new ConfigFileArgs
            {
                File = "https://storage.googleapis.com/tekton-releases/triggers/previous/v0.19.1/release.yaml"
            });
            triggersConfigFile.Ready();
        }
    }
}