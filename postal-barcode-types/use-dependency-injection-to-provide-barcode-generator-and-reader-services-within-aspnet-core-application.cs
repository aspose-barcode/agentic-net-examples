// Title: Demonstrate DI‑style barcode generation and reading in a console app
// Description: Shows how to generate a Code128 barcode image and then read it back using Aspose.BarCode, mimicking dependency injection in ASP.NET Core.
// Category-Description: This example belongs to the Aspose.BarCode .NET library collection that illustrates core barcode operations such as encoding and decoding. It highlights the use of BarcodeGenerator, BarCodeReader, and related parameter classes, which developers commonly employ when integrating barcode creation and scanning into web or service applications. The pattern demonstrates how these services can be registered and resolved via dependency injection for clean architecture.
// Prompt: Use dependency injection to provide barcode generator and reader services within an ASP.NET Core application.
// Tags: barcode, code128, generation, reading, aspnetcore, dependency-injection, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

namespace BarcodeDiDemo
{
    // Service contract for barcode generation
    public interface IBarcodeGeneratorService
    {
        void Generate(string codeText, string outputPath);
    }

    // Concrete implementation that uses Aspose.BarCode to create a PNG image
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void Generate(string codeText, string outputPath)
        {
            // Use Code128 symbology for encoding
            BaseEncodeType encodeType = EncodeTypes.Code128;
            using (var generator = new BarcodeGenerator(encodeType, ""))
            {
                // Set the text to encode with UTF‑8 encoding
                generator.SetCodeText(codeText, Encoding.UTF8);
                // Example of setting a barcode visual property
                generator.Parameters.Barcode.XDimension.Point = 2f;
                // Save the generated barcode as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    // Service contract for barcode reading
    public interface IBarcodeReaderService
    {
        void Read(string imagePath);
    }

    // Concrete implementation that uses Aspose.BarCode to decode barcodes from an image
    public class BarcodeReaderService : IBarcodeReaderService
    {
        public void Read(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Accept all supported barcode types for decoding
            BaseDecodeType decodeType = DecodeType.AllSupportedTypes;
            using (var reader = new BarCodeReader(imagePath, decodeType))
            {
                // Set a quality preset for faster processing
                reader.QualitySettings = QualitySettings.HighPerformance;

                var results = reader.ReadBarCodes();
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected CodeText: {result.CodeText}");
                        Console.WriteLine($"Detected Type   : {result.CodeTypeName}");
                    }
                }
            }
        }
    }

    /// <summary>
    /// Provides a simple console demonstration of barcode generation and reading using DI‑style services.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the demo. Manually resolves generator and reader services, creates a temporary barcode image, reads it, and cleans up.
        /// </summary>
        static void Main()
        {
            // Simulate ASP.NET Core DI by manually instantiating services

            // Create a temporary folder for the demo files
            string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDiDemo_" + Guid.NewGuid().ToString("N"));
            Directory.CreateDirectory(tempFolder);

            string barcodePath = Path.Combine(tempFolder, "sample.png");
            string sampleText = "ABC123456";

            // Resolve services (manual DI)
            IBarcodeGeneratorService generatorService = new BarcodeGeneratorService();
            IBarcodeReaderService readerService = new BarcodeReaderService();

            // Generate barcode image
            generatorService.Generate(sampleText, barcodePath);
            Console.WriteLine($"Barcode generated at: {barcodePath}");

            // Read and display barcode information
            readerService.Read(barcodePath);

            // Clean up temporary files
            try
            {
                if (File.Exists(barcodePath))
                {
                    File.Delete(barcodePath);
                }
                Directory.Delete(tempFolder);
            }
            catch
            {
                // Ignored - cleanup failure should not affect demo outcome
            }
        }
    }
}