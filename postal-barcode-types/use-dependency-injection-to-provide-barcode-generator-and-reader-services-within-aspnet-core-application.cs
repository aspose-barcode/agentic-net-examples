// Title: Barcode generation and reading using DI in ASP.NET Core console demo
// Description: Demonstrates how to generate a Code128 barcode image and then read it back using Aspose.BarCode with dependency injection.
// Category-Description: This example belongs to the Aspose.BarCode ASP.NET Core integration category, showcasing the use of BarcodeGenerator, BarCodeReader, and related quality settings. It illustrates typical scenarios such as creating barcode images for labeling and decoding them in applications, helping developers learn how to register and resolve barcode services via Microsoft.Extensions.DependencyInjection.
// Prompt: Use dependency injection to provide barcode generator and reader services within an ASP.NET Core application.
// Tags: barcode generation, barcode reading, code128, aspnet core, dependency injection, aspose.barcode

using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace BarcodeDiDemo
{
    // Service interface for barcode generation
    public interface IBarcodeGeneratorService
    {
        void Generate(string codeText, string outputPath);
    }

    // Service interface for barcode reading
    public interface IBarcodeReaderService
    {
        void Read(string imagePath);
    }

    // Implementation of the generator service
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void Generate(string codeText, string outputPath)
        {
            // Create a generator for Code128 barcode
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: set image size
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 150f;

                // Save the barcode image
                generator.Save(outputPath);
                Console.WriteLine($"Barcode generated and saved to: {outputPath}");
            }
        }
    }

    // Implementation of the reader service
    public class BarcodeReaderService : IBarcodeReaderService
    {
        public void Read(string imagePath)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                return;
            }

            // Initialize reader for all supported types
            using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Use a high‑performance quality preset
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Iterate through detected barcodes
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text   : {result.CodeText}");
                    Console.WriteLine($"Confidence  : {result.Confidence}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region      : X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");
                    Console.WriteLine();
                }
            }
        }
    }

    /// <summary>
    /// Demonstrates barcode generation and reading using dependency injection.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that sets up DI, generates a barcode, and reads it back.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set up the DI container
            ServiceCollection services = new ServiceCollection();
            services.AddTransient<IBarcodeGeneratorService, BarcodeGeneratorService>();
            services.AddTransient<IBarcodeReaderService, BarcodeReaderService>();

            // Build the service provider
            using (ServiceProvider provider = services.BuildServiceProvider())
            {
                // Resolve services
                var generator = provider.GetRequiredService<IBarcodeGeneratorService>();
                var reader = provider.GetRequiredService<IBarcodeReaderService>();

                // Define file path for the barcode image
                string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "sample_barcode.png");

                // Generate a barcode
                generator.Generate("1234567890", outputPath);

                // Read the generated barcode
                reader.Read(outputPath);
            }
        }
    }
}