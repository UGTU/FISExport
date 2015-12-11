namespace AbitExportProject
{
    /// <summary>
    /// Определяет типы, возвращяющие параметры командной строки приложения
    /// </summary>
    internal interface ICommandLineParamResolver
    {
        /// <summary>
        /// Получает командную строку приложения
        /// </summary>
        string CommandLine { get; }
    }

    internal class StubCommandLineParamResolver : ICommandLineParamResolver
    {
        public string CommandLine { get; private set; }
        public StubCommandLineParamResolver(string commandLine)
        {
            CommandLine = commandLine;
        }
    }

    internal class CommandLineParamResolver : ICommandLineParamResolver
    {
        public string CommandLine
        {
            get { return System.Environment.CommandLine; }
        }

    }
}