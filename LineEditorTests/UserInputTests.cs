using Autofac;
using LineEditor;
using LineEditor.Commands;
using LineEditor.Console;
using LineEditor.ConsoleCommands;
using LineEditor.Logger;
using LineEditor.TextManager;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System.Collections.Generic;
using System.IO;
using System.Linq;

namespace LineEditorTests
{
    [TestClass]
    public class UserInputTests
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
        public void TestUserInput_EmptyCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            string userInput = string.Empty;
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.First().CommandType, ConsoleCommandType.DoNothing);
        }

        [TestMethod]
        public void TestUserInput_UnrecognizedCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            string userInput = "someUnrecognized command 123";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.ShowHelp);
        }

        [TestMethod]
        public void TestUserInput_ListCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            string userInput = "list";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.ListRows);
        }

        [TestMethod]
        public void TestUserInput_InsertLineCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            string userInput = "ins 2 \"it is a new inserted line\"";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.InsertRow);
        }

        [TestMethod]
        public void TestUserInput_UpdateLineCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            string userInput = "update 2 \"it is a new inserted line\"";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.UpdateRow);
        }

        [TestMethod]
        public void TestUserInput_DeleteLineCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            string userInput = "del 3";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.DeleteRow);
        }

        [TestMethod]
        public void TestUserInput_QuitCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            string userInput = "quit";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.Quit);
        }

        [TestMethod]
        public void TestUserInput_SaveCommand()
        {
            var consoleCommandHandler = _container.Resolve<IUserInputHandler>();
            var textManager = _container.Resolve<ITextManager>();
            textManager.LoadFile(testFileName);
            string userInput = "save";
            var consoleCommands = consoleCommandHandler.ParseInput(userInput);
            Assert.AreEqual(consoleCommands.Last().CommandType, ConsoleCommandType.SaveToFile);
        }


    }
}
