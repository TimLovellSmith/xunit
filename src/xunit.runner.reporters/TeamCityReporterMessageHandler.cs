using System;
using System.Collections.Concurrent;
using Xunit.Abstractions;

namespace Xunit.Runner.Reporters
{
    public class TeamCityReporterMessageHandler : TestMessageVisitor
    {
        readonly TeamCityDisplayNameFormatter displayNameFormatter;
        readonly ConcurrentDictionary<string, string> flowMappings = new ConcurrentDictionary<string, string>();
        readonly Func<string, string> flowIdMapper;
        readonly IRunnerLogger logger;

        public TeamCityReporterMessageHandler(IRunnerLogger logger,
                                              Func<string, string> flowIdMapper = null,
                                              TeamCityDisplayNameFormatter displayNameFormatter = null)
        {
            this.logger = logger;
            this.flowIdMapper = flowIdMapper ?? (_ => Guid.NewGuid().ToString("N"));
            this.displayNameFormatter = displayNameFormatter ?? new TeamCityDisplayNameFormatter();
        }

        protected override bool Visit(IErrorMessage error)
        {
            LogError("FATAL ERROR", error);

            return base.Visit(error);
        }

        protected override bool Visit(ITestAssemblyCleanupFailure cleanupFailure)
        {
            LogError(string.Format("Test Assembly Cleanup Failure ({0})", cleanupFailure.TestAssembly.Assembly.AssemblyPath), cleanupFailure);

            return base.Visit(cleanupFailure);
        }

        protected override bool Visit(ITestCaseCleanupFailure cleanupFailure)
        {
            LogError(string.Format("Test Case Cleanup Failure ({0})", cleanupFailure.TestCase.DisplayName), cleanupFailure);

            return base.Visit(cleanupFailure);
        }

        protected override bool Visit(ITestClassCleanupFailure cleanupFailure)
        {
            LogError(string.Format("Test Class Cleanup Failure ({0})", cleanupFailure.TestClass.Class.Name), cleanupFailure);

            return base.Visit(cleanupFailure);
        }

        protected override bool Visit(ITestCollectionCleanupFailure cleanupFailure)
        {
            LogError(string.Format("Test Collection Cleanup Failure ({0})", cleanupFailure.TestCollection.DisplayName), cleanupFailure);

            return base.Visit(cleanupFailure);
        }

        protected override bool Visit(ITestCollectionFinished testCollectionFinished)
        {
            logger.LogImportantMessage("##teamcity[testSuiteFinished name='{0}' flowId='{1}']",
                                       Escape(displayNameFormatter.DisplayName(testCollectionFinished.TestCollection)),
                                       ToFlowId(testCollectionFinished.TestCollection.DisplayName));

            return base.Visit(testCollectionFinished);
        }

        protected override bool Visit(ITestCollectionStarting testCollectionStarting)
        {
            logger.LogImportantMessage("##teamcity[testSuiteStarted name='{0}' flowId='{1}']",
                                       Escape(displayNameFormatter.DisplayName(testCollectionStarting.TestCollection)),
                                       ToFlowId(testCollectionStarting.TestCollection.DisplayName));

            return base.Visit(testCollectionStarting);
        }

        protected override bool Visit(ITestCleanupFailure cleanupFailure)
        {
            LogError(string.Format("Test Cleanup Failure ({0})", cleanupFailure.Test.DisplayName), cleanupFailure);

            return base.Visit(cleanupFailure);
        }

        protected override bool Visit(ITestFailed testFailed)
        {
            logger.LogImportantMessage("##teamcity[testFailed name='{0}' details='{1}|r|n{2}' flowId='{3}']",
                                       Escape(displayNameFormatter.DisplayName(testFailed.Test)),
                                       Escape(ExceptionUtility.CombineMessages(testFailed)),
                                       Escape(ExceptionUtility.CombineStackTraces(testFailed)),
                                       ToFlowId(testFailed.TestCollection.DisplayName));
            LogFinish(testFailed);

            return base.Visit(testFailed);
        }

        protected override bool Visit(ITestMethodCleanupFailure cleanupFailure)
        {
            LogError(string.Format("Test Method Cleanup Failure ({0})", cleanupFailure.TestMethod.Method.Name), cleanupFailure);

            return base.Visit(cleanupFailure);
        }

        protected override bool Visit(ITestPassed testPassed)
        {
            LogFinish(testPassed);

            return base.Visit(testPassed);
        }

        protected override bool Visit(ITestSkipped testSkipped)
        {
            logger.LogImportantMessage("##teamcity[testIgnored name='{0}' message='{1}' flowId='{2}']",
                                       Escape(displayNameFormatter.DisplayName(testSkipped.Test)),
                                       Escape(testSkipped.Reason),
                                       ToFlowId(testSkipped.TestCollection.DisplayName));
            LogFinish(testSkipped);

            return base.Visit(testSkipped);
        }

        protected override bool Visit(ITestStarting testStarting)
        {
            logger.LogImportantMessage("##teamcity[testStarted name='{0}' flowId='{1}']",
                                       Escape(displayNameFormatter.DisplayName(testStarting.Test)),
                                       ToFlowId(testStarting.TestCollection.DisplayName));

            return base.Visit(testStarting);
        }

        // Helpers

        void LogError(string messageType, IFailureInformation failureInfo)
        {
            var message = string.Format("[{0}] {1}: {2}", messageType, failureInfo.ExceptionTypes[0], ExceptionUtility.CombineMessages(failureInfo));
            var stack = ExceptionUtility.CombineStackTraces(failureInfo);

            logger.LogImportantMessage("##teamcity[message text='{0}' errorDetails='{1}' status='ERROR']", Escape(message), Escape(stack));
        }

        void LogFinish(ITestResultMessage testResult)
        {
            var formattedName = Escape(displayNameFormatter.DisplayName(testResult.Test));

            if (!string.IsNullOrWhiteSpace(testResult.Output))
                logger.LogImportantMessage("##teamcity[testStdOut name='{0}' out='{1}']", formattedName, Escape(testResult.Output));

            logger.LogImportantMessage("##teamcity[testFinished name='{0}' duration='{1}' flowId='{2}']",
                                       formattedName,
                                       (int)(testResult.ExecutionTime * 1000M),
                                       ToFlowId(testResult.TestCollection.DisplayName));
        }

        static string Escape(string value)
        {
            if (value == null)
                return string.Empty;

            return value.Replace("|", "||")
                        .Replace("'", "|'")
                        .Replace("\r", "|r")
                        .Replace("\n", "|n")
                        .Replace("]", "|]")
                        .Replace("[", "|[")
                        .Replace("\u0085", "|x")
                        .Replace("\u2028", "|l")
                        .Replace("\u2029", "|p");
        }

        string ToFlowId(string testCollectionName)
        {
            return flowMappings.GetOrAdd(testCollectionName, flowIdMapper);
        }
    }
}
