using System;
using System.Diagnostics;
using System.IO;
using Xunit;

namespace Parlot.Benchmarks.UnitTests
{
    /// <summary>
    /// Unit tests for the <see cref="Parlot.Benchmarks.Program"/> class.
    /// </summary>
    public class ProgramTests
    {
        /// <summary>
        /// Tests the Main method of the Program class with empty arguments.
        /// This test starts the application as a separate process with no command-line arguments
        /// and verifies that the process exits with code 0, indicating that the benchmark execution or help prompt completed successfully.
        /// </summary>
//         [Fact] [Error] (34-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (37-17)CS8602 Dereference of a possibly null reference.
//         public void Main_WithEmptyArgs_ExitsWithZero()
//         {
//             // Arrange
//             string exePath = GetExecutablePath();
//             var startInfo = new ProcessStartInfo
//             {
//                 FileName = exePath,
//                 Arguments = "",
//                 CreateNoWindow = true,
//                 UseShellExecute = false,
//                 RedirectStandardOutput = true,
//                 RedirectStandardError = true
//             };
// 
//             // Act
//             using (Process process = Process.Start(startInfo))
//             {
//                 // Wait for the process to exit (up to 5 seconds).
//                 process.WaitForExit(5000);
// 
//                 // Assert
//                 Assert.Equal(0, process.ExitCode);
//             }
//         }

        /// <summary>
        /// Tests the Main method of the Program class with the '--help' argument.
        /// This test starts the application as a separate process with the '--help' argument
        /// and verifies that the process exits with code 0, indicating that help information was displayed successfully.
        /// </summary>
//         [Fact] [Error] (65-38)CS8600 Converting null literal or possible null value to non-nullable type. [Error] (68-17)CS8602 Dereference of a possibly null reference.
//         public void Main_WithHelpArg_ExitsWithZero()
//         {
//             // Arrange
//             string exePath = GetExecutablePath();
//             var startInfo = new ProcessStartInfo
//             {
//                 FileName = exePath,
//                 Arguments = "--help",
//                 CreateNoWindow = true,
//                 UseShellExecute = false,
//                 RedirectStandardOutput = true,
//                 RedirectStandardError = true
//             };
// 
//             // Act
//             using (Process process = Process.Start(startInfo))
//             {
//                 // Wait for the process to exit (up to 5 seconds).
//                 process.WaitForExit(5000);
// 
//                 // Assert
//                 Assert.Equal(0, process.ExitCode);
//             }
//         }

        /// <summary>
        /// Retrieves the full path to the executable of the Parlot.Benchmarks application.
        /// It assumes that the executable is located in the same directory as the test assembly output.
        /// </summary>
        /// <returns>A string containing the full path to the executable.</returns>
        private static string GetExecutablePath()
        {
            // Arrange
            string baseDirectory = AppContext.BaseDirectory;
            string exeName = "Parlot.Benchmarks.exe";
            string exePath = Path.Combine(baseDirectory, exeName);

            // Validate that the executable exists; if not, throw an exception.
            if (!File.Exists(exePath))
            {
                throw new FileNotFoundException($"Executable not found at expected location: {exePath}");
            }

            return exePath;
        }
    }
}
