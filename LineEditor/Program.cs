using Autofac;
using LineEditor.Commands;
using LineEditor.Console;
using LineEditor.Logger;
using LineEditor.TextManager;

namespace LineEditor
{
    class Program
    {
        private static IContainer CompositionRoot()
        {
            var builder = new ContainerBuilder();
            builder.RegisterType<Application>();
            builder.RegisterType<UserInputHandler>().As<IUserInputHandler>();
            builder.RegisterType<ProductionConsole>().As<IConsole>();
            builder.RegisterType<LineEditorCommandsFactory>().As<ILineEditorCommandsFactory>();
            builder.RegisterType<LineEditorTextManager>().As<ITextManager>();
            builder.RegisterType<CommandTrack>().As<ICommandTrack>();
            builder.Register<ILogger>((c, p) => { return new LineEditorLogger(); }).SingleInstance();
            return builder.Build();
        }

        static int Main()
        {
            var args = System.Environment.GetCommandLineArgs();
            return CompositionRoot().Resolve<Application>().Run(args);
        }
    }
}
