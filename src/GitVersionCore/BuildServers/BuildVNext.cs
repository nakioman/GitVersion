namespace GitVersion
{
    using System;

    public class BuildVNext : BuildServerBase
    {
        public override bool CanApplyToCurrentContext()
        {
            return !string.IsNullOrEmpty(Environment.GetEnvironmentVariable("SYSTEM_TEAMFOUNDATIONSERVERURI"));
        }

        public override string GenerateSetVersionMessage(string versionToUseForBuildNumber)
        {
            var buildId = Environment.GetEnvironmentVariable("BUILD_BUILDID");
            Environment.SetEnvironmentVariable("BUILD_BUILDNUMBER", versionToUseForBuildNumber);

            return string.Format("Set TFS Build vNext build number to '{0} (Build Id {1})'.", versionToUseForBuildNumber, buildId);
        }

        public override string[] GenerateSetParameterMessage(string name, string value)
        {
            Environment.SetEnvironmentVariable(string.Format("GITVERSION_{0}", name), value);
            return new[]
            {
                string.Format("Adding Environment Variable. name='GITVERSION_{0}' value='{1}']", name, value)
            };
        }
    }
}
