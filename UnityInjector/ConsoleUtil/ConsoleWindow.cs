// --------------------------------------------------
// UnityInjector - ConsoleWindow.cs
// --------------------------------------------------

#region Usings

using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Text;

#endregion

namespace UnityInjector.ConsoleUtil
{
    internal class ConsoleWindow
    {
        private static bool _attached;
        private static IntPtr _cOut;
        private static IntPtr _oOut;

        public static void Attach()
        {
            if (_attached)
                return;
            if (_oOut == IntPtr.Zero)
                _oOut = GetStdHandle(-11);
            if (!AllocConsole())
                throw new Exception("AllocConsole() failed");
            _cOut = CreateFile("CONOUT$", 0x40000000, 2, IntPtr.Zero, 3, 0, IntPtr.Zero);
            if (!SetStdHandle(-11, _cOut))
                throw new Exception("SetStdHandle() failed");
            Init();
            _attached = true;
        }

        public static void Detach()
        {
            if (!_attached)
                return;
            if (!CloseHandle(_cOut))
                throw new Exception("CloseHandle() failed");
            _cOut = IntPtr.Zero;
            if (!FreeConsole())
                throw new Exception("FreeConsole() failed");
            if (!SetStdHandle(-11, _oOut))
                throw new Exception("SetStdHandle() failed");
            Init();
            _attached = false;
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool AllocConsole();

        [DllImport("kernel32.dll", ExactSpelling = true, SetLastError = true)]
        private static extern bool CloseHandle(IntPtr handle);

        [DllImport("kernel32.dll", CharSet = CharSet.Auto, SetLastError = true)]
        private static extern IntPtr CreateFile(
            string fileName,
            int desiredAccess,
            int shareMode,
            IntPtr securityAttributes,
            int creationDisposition,
            int flagsAndAttributes,
            IntPtr templateFile);

        [DllImport("kernel32.dll", SetLastError = false)]
        private static extern bool FreeConsole();

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        private static void Init()
        {
            var stdOut = Console.OpenStandardOutput();
            var stdWriter = new StreamWriter(stdOut, Encoding.Default)
            {
                AutoFlush = true
            };
            Console.SetOut(stdWriter);
            Console.SetError(stdWriter);
        }

        [DllImport("kernel32.dll", SetLastError = true)]
        private static extern bool SetStdHandle(int nStdHandle, IntPtr hConsoleOutput);
    }
}
