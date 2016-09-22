using System.Collections.Generic;
using Xunit.Abstractions;

namespace Xunit
{
    /// <summary>
    /// Class that maps test framework execution messages to events.
    /// </summary>
    public class ExecutionEventSink : LongLivedMarshalByRefObject, IMessageSinkWithTypes
    {
        /// <summary>
        /// Occurs when a <see cref="IAfterTestFinished"/> message is received.
        /// </summary>
        public event MessageHandler<IAfterTestFinished> AfterTestFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="IAfterTestStarting"/> message is received.
        /// </summary>
        public event MessageHandler<IAfterTestStarting> AfterTestStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="IBeforeTestFinished"/> message is received.
        /// </summary>
        public event MessageHandler<IBeforeTestFinished> BeforeTestFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="IBeforeTestStarting"/> message is received.
        /// </summary>
        public event MessageHandler<IBeforeTestStarting> BeforeTestStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestAssemblyCleanupFailure"/> message is received.
        /// </summary>
        public event MessageHandler<ITestAssemblyCleanupFailure> TestAssemblyCleanupFailureEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestAssemblyFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestAssemblyFinished> TestAssemblyFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestAssemblyStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestAssemblyStarting> TestAssemblyStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCaseCleanupFailure"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCaseCleanupFailure> TestCaseCleanupFailureEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCaseFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCaseFinished> TestCaseFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCaseStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCaseStarting> TestCaseStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassCleanupFailure"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassCleanupFailure> TestClassCleanupFailureEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassConstructionFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassConstructionFinished> TestClassConstructionFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassConstructionStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassConstructionStarting> TestClassConstructionStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassDisposeFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassDisposeFinished> TestClassDisposeFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassDisposeStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassDisposeStarting> TestClassDisposeStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassFinished> TestClassFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestClassStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestClassStarting> TestClassStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCleanupFailure"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCleanupFailure> TestCleanupFailureEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCollectionCleanupFailure"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCollectionCleanupFailure> TestCollectionCleanupFailureEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCollectionFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCollectionFinished> TestCollectionFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestCollectionStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestCollectionStarting> TestCollectionStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestFailed"/> message is received.
        /// </summary>
        public event MessageHandler<ITestFailed> TestFailedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestFinished> TestFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestMethodCleanupFailure"/> message is received.
        /// </summary>
        public event MessageHandler<ITestMethodCleanupFailure> TestMethodCleanupFailureEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestMethodFinished"/> message is received.
        /// </summary>
        public event MessageHandler<ITestMethodFinished> TestMethodFinishedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestMethodStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestMethodStarting> TestMethodStartingEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestOutput"/> message is received.
        /// </summary>
        public event MessageHandler<ITestOutput> TestOutputEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestPassed"/> message is received.
        /// </summary>
        public event MessageHandler<ITestPassed> TestPassedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestSkipped"/> message is received.
        /// </summary>
        public event MessageHandler<ITestSkipped> TestSkippedEvent;

        /// <summary>
        /// Occurs when a <see cref="ITestStarting"/> message is received.
        /// </summary>
        public event MessageHandler<ITestStarting> TestStartingEvent;

        /// <inheritdoc/>
        public void Dispose() { }

        /// <inheritdoc/>
        public bool OnMessageWithTypes(IMessageSinkMessage message, HashSet<string> messageTypes)
            => (AfterTestFinishedEvent == null || message.Dispatch(messageTypes, AfterTestFinishedEvent))
                && (AfterTestStartingEvent == null || message.Dispatch(messageTypes, AfterTestStartingEvent))
                && (BeforeTestFinishedEvent == null || message.Dispatch(messageTypes, BeforeTestFinishedEvent))
                && (BeforeTestStartingEvent == null || message.Dispatch(messageTypes, BeforeTestStartingEvent))
                && (TestAssemblyCleanupFailureEvent == null || message.Dispatch(messageTypes, TestAssemblyCleanupFailureEvent))
                && (TestAssemblyFinishedEvent == null || message.Dispatch(messageTypes, TestAssemblyFinishedEvent))
                && (TestAssemblyStartingEvent == null || message.Dispatch(messageTypes, TestAssemblyStartingEvent))
                && (TestCaseCleanupFailureEvent == null || message.Dispatch(messageTypes, TestCaseCleanupFailureEvent))
                && (TestCaseFinishedEvent == null || message.Dispatch(messageTypes, TestCaseFinishedEvent))
                && (TestCaseStartingEvent == null || message.Dispatch(messageTypes, TestCaseStartingEvent))
                && (TestClassCleanupFailureEvent == null || message.Dispatch(messageTypes, TestClassCleanupFailureEvent))
                && (TestClassConstructionFinishedEvent == null || message.Dispatch(messageTypes, TestClassConstructionFinishedEvent))
                && (TestClassConstructionStartingEvent == null || message.Dispatch(messageTypes, TestClassConstructionStartingEvent))
                && (TestClassDisposeFinishedEvent == null || message.Dispatch(messageTypes, TestClassDisposeFinishedEvent))
                && (TestClassDisposeStartingEvent == null || message.Dispatch(messageTypes, TestClassDisposeStartingEvent))
                && (TestClassFinishedEvent == null || message.Dispatch(messageTypes, TestClassFinishedEvent))
                && (TestClassStartingEvent == null || message.Dispatch(messageTypes, TestClassStartingEvent))
                && (TestCleanupFailureEvent == null || message.Dispatch(messageTypes, TestCleanupFailureEvent))
                && (TestCollectionCleanupFailureEvent == null || message.Dispatch(messageTypes, TestCollectionCleanupFailureEvent))
                && (TestCollectionFinishedEvent == null || message.Dispatch(messageTypes, TestCollectionFinishedEvent))
                && (TestCollectionStartingEvent == null || message.Dispatch(messageTypes, TestCollectionStartingEvent))
                && (TestFailedEvent == null || message.Dispatch(messageTypes, TestFailedEvent))
                && (TestFinishedEvent == null || message.Dispatch(messageTypes, TestFinishedEvent))
                && (TestFinishedEvent == null || message.Dispatch(messageTypes, TestFinishedEvent))
                && (TestMethodCleanupFailureEvent == null || message.Dispatch(messageTypes, TestMethodCleanupFailureEvent))
                && (TestMethodFinishedEvent == null || message.Dispatch(messageTypes, TestMethodFinishedEvent))
                && (TestMethodStartingEvent == null || message.Dispatch(messageTypes, TestMethodStartingEvent))
                && (TestOutputEvent == null || message.Dispatch(messageTypes, TestOutputEvent))
                && (TestPassedEvent == null || message.Dispatch(messageTypes, TestPassedEvent))
                && (TestSkippedEvent == null || message.Dispatch(messageTypes, TestSkippedEvent))
                && (TestStartingEvent == null || message.Dispatch(messageTypes, TestStartingEvent));
    }
}
