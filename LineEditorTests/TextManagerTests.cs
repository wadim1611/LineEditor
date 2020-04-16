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

namespace LineEditorTests
{
    [TestClass]
    public class TextManagerTests
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

        [TestMethod]
        public void TestTextmanager_LoadText()
        {
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            Assert.IsTrue(textManager.Rows.Count > 0);
        }

        [TestMethod]
        public void TestTextmanager_SaveText()
        {
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            textManager.Rows.Add("AdditionalRow text");
            textManager.Save();
            var savedRows = File.ReadAllLines(testFileName).ToList();
            Assert.IsTrue(savedRows.Count == textManager.Rows.Count);
        }
    }
}
