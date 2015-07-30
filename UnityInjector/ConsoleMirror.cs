// --------------------------------------------------
// DebugPlugin - ConsoleMirror.cs
// --------------------------------------------------

#region Usings
using System;
using System.IO;
using System.Text;

#endregion

namespace UnityInjector
{

    internal class ConsoleMirror : IDisposable
    {
        #region Fields
        private readonly FileStream _fileStream;
        private readonly StreamWriter _fileWriter;
        private readonly MirrorWriter _tWriter;
        #endregion

        #region (De)Constructors
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
                Console.ForegroundColor = ConsoleColor.DarkGray;
                Console.WriteLine(e.Message);
                Console.ForegroundColor = ConsoleColor.Gray;
                return;
            }
            Console.SetOut(_tWriter);
        }
        #endregion

        #region Public Methods
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
        #endregion

        #region Class MirrorWriter
        private class MirrorWriter : TextWriter
        {
            #region Properties
            public TextWriter Console { get; }
            public override Encoding Encoding => File.Encoding;
            public TextWriter File { get; }
            #endregion

            #region (De)Constructors
            public MirrorWriter(TextWriter file, TextWriter console)
            {
                File = file;
                Console = console;
            }
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
        #endregion
    }

}