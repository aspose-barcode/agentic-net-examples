using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDockerDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // If the program is executed inside Docker, generate the QR code.
            if (Array.Exists(args, a => a.Equals("--docker", StringComparison.OrdinalIgnoreCase)))
            {
                GenerateQrCode();
                return;
            }

            // Otherwise, launch a Docker container that runs this program with the "--docker" flag.
            try
            {
                string exeDirectory = AppDomain.CurrentDomain.BaseDirectory.TrimEnd(Path.DirectorySeparatorChar);
                string dockerImage = "mcr.microsoft.com/dotnet/sdk:6.0";

                // Build the Docker command:
                //   docker run --rm -v "<host_dir>:/app" -w /app <image> dotnet run -- --docker
                string arguments = $"run --rm -v \"{exeDirectory}:/app\" -w /app {dockerImage} dotnet run -- --docker";

                using (var process = new Process())
                {
                    process.StartInfo.FileName = "docker";
                    process.StartInfo.Arguments = arguments;
                    process.StartInfo.UseShellExecute = false;
                    process.StartInfo.RedirectStandardOutput = true;
                    process.StartInfo.RedirectStandardError = true;

                    process.Start();

                    // Forward Docker output to console.
                    string output = process.StandardOutput.ReadToEnd();
                    string error = process.StandardError.ReadToEnd();

                    process.WaitForExit();

                    Console.WriteLine(output);
                    if (!string.IsNullOrWhiteSpace(error))
                    {
                        Console.Error.WriteLine(error);
                    }

                    if (process.ExitCode != 0)
                    {
                        Console.Error.WriteLine($"Docker process exited with code {process.ExitCode}.");
                    }
                }
            }
            catch (Exception ex)
            {
                Console.Error.WriteLine($"Failed to run Docker container: {ex.Message}");
            }
        }

        private static void GenerateQrCode()
        {
            // Define output file path.
            string outputPath = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "qr.png");

            // Create QR code generator with QR symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR))
            {
                // Set the text to encode.
                generator.CodeText = "https://example.com";

                // Set high error correction level.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Optionally adjust image size.
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the barcode image.
                generator.Save(outputPath);
            }

            Console.WriteLine($"QR code generated at: {outputPath}");
        }
    }
}