// Title: Generate QR Code and prepare Docker container for isolated execution
// Description: Demonstrates creating a QR Code barcode image using Aspose.BarCode and writing a Dockerfile to run the generator in a container.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with QR symbology, configure error correction, and export the image. It also shows how to automate containerization by creating a Dockerfile that copies the compiled assembly and runs it with the .NET runtime. Developers looking to integrate barcode creation into CI/CD pipelines or isolated environments will find this pattern useful.
// Prompt: Generate QR Code barcode and use Docker container to run generation in isolated environment.
// Tags: qr code, barcode generation, docker, aspnet, aspose.barcode, image output

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and Dockerfile creation for containerized execution.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a QR Code image, writes a Dockerfile, and outputs usage instructions.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path for the generated QR code image
        string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");

        // Generate QR code using Aspose.BarCode
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the data to encode
            generator.CodeText = "https://example.com";

            // Configure a high error correction level for better resilience
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

            // Save the QR code image to the specified file
            generator.Save(outputFile);
        }

        Console.WriteLine($"QR code image saved to: {outputFile}");

        // Create a folder to hold Docker context files
        string dockerFolder = Path.Combine(Directory.GetCurrentDirectory(), "docker");
        Directory.CreateDirectory(dockerFolder);

        // Path for the Dockerfile within the Docker context folder
        string dockerfilePath = Path.Combine(dockerFolder, "Dockerfile");

        // Determine the name of the compiled assembly (DLL for framework‑dependent apps)
        string assemblyName = Path.GetFileName(Assembly.GetEntryAssembly()?.Location ?? "app.dll");
        if (!assemblyName.EndsWith(".dll", StringComparison.OrdinalIgnoreCase))
        {
            assemblyName = Path.ChangeExtension(assemblyName, ".dll");
        }

        // Build Dockerfile content that copies the application and runs it with the .NET runtime
        string dockerfileContent = $@"# Dockerfile to run the QR code generator in an isolated container
FROM mcr.microsoft.com/dotnet/runtime:8.0-alpine
WORKDIR /app
COPY . .
ENTRYPOINT [""dotnet"", ""{assemblyName}""]";

        // Write the Dockerfile to the Docker context folder
        File.WriteAllText(dockerfilePath, dockerfileContent);
        Console.WriteLine($"Dockerfile written to: {dockerfilePath}");

        // Output instructions for building and running the Docker container
        Console.WriteLine("To build and run the container:");
        Console.WriteLine($"  cd {dockerFolder}");
        Console.WriteLine("  docker build -t barcode-generator .");
        Console.WriteLine("  docker run --rm -v $(pwd)/output:/app/output barcode-generator");
    }
}