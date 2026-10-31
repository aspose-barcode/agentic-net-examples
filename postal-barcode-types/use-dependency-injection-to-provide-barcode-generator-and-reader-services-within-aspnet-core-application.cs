// Title: Dependency Injection Demo for Aspose.BarCode in a Console App
// Description: Demonstrates how to inject barcode generator and reader services using interfaces, mimicking ASP.NET Core DI, and performs generation and reading of a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode .NET library collection showing service‑oriented patterns. It highlights key API classes such as BarcodeGenerator, BarCodeReader, and related parameter objects. Typical use cases include generating barcodes for labels and scanning them in back‑office systems, where developers often register these services in an ASP.NET Core DI container for clean architecture.
// Prompt: Use dependency injection to provide barcode generator and reader services within an ASP.NET Core application.
// Tags: barcode, code128, generation, reading, dependency injection, aspnet core, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDiConsole
{
    /// <summary>
    /// Service contract for generating barcodes.
    /// </summary>
    public interface IBarcodeGeneratorService
    {
        void Generate(string codeText, string outputPath);
    }

    /// <summary>
    /// Service contract for reading barcodes from images.
    /// </summary>
    public interface IBarcodeReaderService
    {
        string Read(string imagePath);
    }

    /// <summary>
    /// Implements <see cref="IBarcodeGeneratorService"/> using Aspose.BarCode's <see cref="BarcodeGenerator"/>.
    /// </summary>
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void Generate(string codeText, string outputPath)
        {
            // Create a generator for Code128 symbology with the supplied text.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Simple appearance settings.
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;
                generator.Parameters.RotationAngle = 0f;
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Implements <see cref="IBarcodeReaderService"/> using Aspose.BarCode's <see cref="BarCodeReader"/>.
    /// </summary>
    public class BarcodeReaderService : IBarcodeReaderService
    {
        public string Read(string imagePath)
        {
            // Verify that the image file exists before attempting to read.
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return string.Empty;
            }

            // Restrict decoding to Code128 for this demo.
            BaseDecodeType decodeType = DecodeType.Code128;
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                // Iterate through all detected barcodes.
                foreach (var result in reader.ReadBarCodes())
                {
                    // In evaluation mode non‑Code39 results have masked text,
                    // so we consider any non‑empty result as a successful read.
                    if (!string.IsNullOrEmpty(result.CodeText))
                    {
                        return result.CodeText;
                    }
                }
            }

            // Return empty string if no readable barcode was found.
            return string.Empty;
        }
    }

    /// <summary>
    /// Console demo of DI‑based barcode services.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that composes services, generates a barcode, reads it back, and cleans up temporary files.
        /// </summary>
        static void Main()
        {
            // In a real ASP.NET Core app these services would be registered in the DI container.
            // Here we manually compose them for a console demonstration.

            IBarcodeGeneratorService generatorService = new BarcodeGeneratorService();
            IBarcodeReaderService readerService = new BarcodeReaderService();

            // Prepare a temporary folder and file path for the barcode image.
            string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);
            string barcodePath = Path.Combine(tempFolder, "sample.png");

            string sampleText = "ABC123456";

            // Generate the barcode image.
            generatorService.Generate(sampleText, barcodePath);
            Console.WriteLine($"Barcode generated at: {barcodePath}");

            // Read the barcode back from the image.
            string decodedText = readerService.Read(barcodePath);
            if (!string.IsNullOrEmpty(decodedText))
            {
                Console.WriteLine($"Decoded text: {decodedText}");
            }
            else
            {
                Console.WriteLine("No barcode detected or decoding failed.");
            }

            // Clean up temporary files and directory.
            try
            {
                if (File.Exists(barcodePath))
                {
                    File.Delete(barcodePath);
                }
                if (Directory.Exists(tempFolder))
                {
                    Directory.Delete(tempFolder, true);
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Cleanup error: {ex.Message}");
            }
        }
    }
}