using Autofac;
using LineEditor;
using LineEditor.Commands;
using LineEditor.Console;
using LineEditor.Logger;
using LineEditor.TextManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using IContainer = Autofac.IContainer;

namespace LineEditorTests
{
    [TestClass]
    public class AppRunTest
    {
        private static string testFileName = "LineEditorTestFile.txt";
        private static IContainer _container;

        [ClassInitialize]
        public static void MyTestInitialize(TestContext testContext)
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<TestableConsole>().As<IConsole>().SingleInstance();
            builder.RegisterType<UserInputHandler>().As<IUserInputHandler>();
            builder.RegisterType<LineEditorCommandsFactory>().As<ILineEditorCommandsFactory>();
            builder.RegisterType<LineEditorTextManager>().As<ITextManager>();
            builder.RegisterType<CommandTrack>().As<ICommandTrack>();
            builder.Register<ILogger>((c, p) => { return new LineEditorLogger(); }).SingleInstance();
            _container = builder.Build();

            File.WriteAllLines(testFileName, GenerateRows());
        }

        [ClassCleanup]
        public static void ClassCleanup()
        {
            if (File.Exists(testFileName))
            {
                File.Delete(testFileName);
            }
        }

        private static List<string> GenerateRows()
        {
            var lip = new NLipsum.Core.LipsumGenerator();
            var paraOptions = NLipsum.Core.Paragraph.Medium;
            paraOptions.MinimumSentences = 2;
            paraOptions.MaximumSentences = 5;
            return lip.GenerateParagraphs(5, paraOptions).ToList();
        }

        [TestInitialize]
        public void TestInitialize()
        {
            var console = _container.Resolve<IConsole>() as TestableConsole;
            console.LastWrittenLine.Clear();
            console.LineToRead = string.Empty;
        }

        [TestMethod]
        public void TestAppStart_EmptyArgument()
        {
            var console = _container.Resolve<IConsole>() as TestableConsole;
            var application = _container.Resolve<Application>();
            var args = new string[1] { "applicationName.exe" };
            application.Run(args);
            Assert.AreEqual("Wrong number of arguments", console.LastWrittenLine.First());
        }

        [TestMethod]
        public void TestAppStart_EmptyFilenameAsArgument()
        {
            var console = _container.Resolve<IConsole>() as TestableConsole;
            var application = _container.Resolve<Application>();
            var args = new string[2] { "applicationName.exe", "" };
            application.Run(args);
            Assert.AreEqual("Filename is requred", console.LastWrittenLine.First());
        }

        [TestMethod]
        public void TestAppStart_NonexistentFileAsArgument()
        {
            var console = _container.Resolve<IConsole>() as TestableConsole;
            var application = _container.Resolve<Application>();
            var args = new string[2] { "applicationName.exe", "NonexistentFile.txt" };
            application.Run(args);
            Assert.AreEqual("File not found", console.LastWrittenLine.First());
        }
    }
}
