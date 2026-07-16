// Title: Generate QR Code and Run in Docker Container
// Description: This example creates a QR Code barcode image using Aspose.BarCode and demonstrates how to execute the generation inside a Docker container for isolation.
// Category-Description: This sample belongs to the Aspose.BarCode barcode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and QR-specific parameters to produce a PNG image. Typical use cases include creating QR codes for URLs, product information, or authentication tokens. Developers often need to generate barcodes in CI/CD pipelines or isolated environments, making Docker containerization a common practice.
// Prompt: Generate QR Code barcode and use Docker container to run generation in isolated environment.
// Tags: qr code, barcode generation, png, aspose.barcode, docker

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and provides Docker usage instructions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code image and outputs Docker guidance.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated QR Code image.
        const string outputFile = "qr.png";

        // Initialize a QR Code generator with the QR symbology.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            // Set the text (URL) to be encoded into the QR Code.
            generator.CodeText = "https://example.com";

            // Configure QR-specific parameters:
            // - Highest error correction level (Level H) for better resilience.
            // - Automatic encoding mode selection.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
            generator.Parameters.Barcode.QR.EncodeMode = QREncodeMode.Auto;

            // Define image rendering options:
            // - Use interpolation for auto-sizing.
            // - Set image dimensions to 300x300 points.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the generated QR Code as a PNG file.
            generator.Save(outputFile);
        }

        // Inform the user that the QR Code image has been created.
        Console.WriteLine($"QR Code image generated: {outputFile}");

        /*
         * Docker usage:
         * To run this barcode generation in an isolated Docker container, create a Dockerfile like the following:
         *
         *   FROM mcr.microsoft.com/dotnet/sdk:8.0 AS build
         *   WORKDIR /src
         *   COPY *.csproj ./
         *   RUN dotnet restore
         *   COPY . ./
         *   RUN dotnet publish -c Release -o /app
         *
         *   FROM mcr.microsoft.com/dotnet/runtime:8.0 AS runtime
         *   WORKDIR /app
         *   COPY --from=build /app ./
         *   # Ensure Aspose.BarCode and Aspose.Drawing DLLs are present in the output folder
         *   ENTRYPOINT ["dotnet", "YourAssemblyName.dll"]
         *
         * Build the image:
         *   docker build -t barcode-generator .
         *
         * Run the container (the generated QR image will be written to the container's filesystem):
         *   docker run --rm -v ${PWD}:/output barcode-generator
         *
         * Adjust the volume mount as needed to retrieve the generated file.
         */
    }
}