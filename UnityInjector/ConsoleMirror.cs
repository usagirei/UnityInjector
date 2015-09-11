// --------------------------------------------------
// UnityInjector - ConsoleMirror.cs
// --------------------------------------------------

using System;
using System.IO;
using System.Text;

namespace UnityInjector
{
    internal class ConsoleMirror : IDisposable
    {
        private readonly FileStream _fileStream;
        private readonly StreamWriter _fileWriter;
        private readonly MirrorWriter _tWriter;

        public ConsoleMirror(string path)
        {
            try
            {
                _fileStream = File.Create(path);
                _fileWriter = new StreamWriter(_fileStream)
                {
                    AutoFlush = true
                };
                _tWriter = new MirrorWriter(_fileWriter, Console.Out);
            }
            catch (Exception e)
            {
                Console.WriteLine("Couldn't open file to write: {0}", Path.GetFileName(path));
#if COLOR
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
#else
                Console.WriteLine(e.Message);
#endif
                return;
            }
            Console.SetOut(_tWriter);
        }

        public void Dispose()
        {
            var cOld = _tWriter.Console;
            var fOld = _tWriter.File;
            Console.SetOut(cOld);
            if (fOld == null)
                return;
            fOld.Flush();
            fOld.Close();
        }

        private class MirrorWriter : TextWriter
        {
            #region (De)Constructors

            public MirrorWriter(TextWriter file, TextWriter console)
            {
                File = file;
                Console = console;
            }

            #endregion

            #region Properties

            public TextWriter Console { get; }
            public override Encoding Encoding => File.Encoding;
            public TextWriter File { get; }

            #endregion

            #region Public Methods

            public override void Flush()
            {
                File.Flush();
                Console.Flush();
            }

            public override void Write(char value)
            {
                File.Write(value);
                Console.Write(value);
            }

            #endregion
        }
    }
}
