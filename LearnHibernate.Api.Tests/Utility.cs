namespace LearnHibernate.Api.Tests
{
    using DryIoc;
    using DryIoc.Microsoft.DependencyInjection;
    using Microsoft.Extensions.Configuration;
    using Microsoft.Extensions.DependencyInjection;
    using System;
    using System.IO;
    using Xunit;

    internal static class Utility
    {
        public static string GetContentRootPath()
        {
            var testProjectPath = AppContext.BaseDirectory;
            var relativePathToHostProject = @"..\..\..\..\LearnHibernate.Api";
            return Path.Combine(testProjectPath, relativePathToHostProject);
        }
    }
}
