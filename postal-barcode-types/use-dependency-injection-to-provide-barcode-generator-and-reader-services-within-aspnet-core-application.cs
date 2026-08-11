// Title: ASP.NET Core Dependency Injection for Aspose.BarCode Generation and Reading
// Description: Demonstrates how to register and use barcode generator and reader services with ASP.NET Core's built‑in DI container.
// Category-Description: This example belongs to the Aspose.BarCode operations collection focusing on barcode generation and recognition. It showcases the use of key API classes such as BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include creating product labels, tickets, or QR codes in web applications where services are injected via ASP.NET Core's dependency injection framework. Developers often need reusable services that encapsulate barcode logic, and this pattern provides a clean, testable approach.
// Prompt: Use dependency injection to provide barcode generator and reader services within an ASP.NET Core application.
// Tags: barcode generation, barcode reading, aspnet core, dependency injection, code128, png, aspose.barcode

using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

namespace AsposeBarcodeDIExample
{
    // Service interface for barcode generation
    public interface IBarcodeGeneratorService
    {
        /// <summary>
        /// Generates a barcode image from the specified text and saves it to the given file path.
        /// </summary>
        /// <param name="text">The data to encode in the barcode.</param>
        /// <param name="filePath">The full path where the barcode image will be saved.</param>
        void Generate(string text, string filePath);
    }

    // Service interface for barcode reading
    public interface IBarcodeReaderService
    {
        /// <summary>
        /// Reads barcodes from the specified image file and writes detection results to the console.
        /// </summary>
        /// <param name="filePath">The full path of the image containing barcodes.</param>
        void Read(string filePath);
    }

    // Implementation of the generator service using Aspose.BarCode
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void Generate(string text, string filePath)
        {
            // Ensure the output directory exists
            string directory = Path.GetDirectoryName(filePath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create and save the barcode image
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
            {
                generator.Save(filePath);
                Console.WriteLine($"Barcode generated and saved to: {filePath}");
            }
        }
    }

    // Implementation of the reader service using Aspose.BarCode
    public class BarcodeReaderService : IBarcodeReaderService
    {
        public void Read(string filePath)
        {
            // Verify that the file exists before attempting to read
            if (!File.Exists(filePath))
            {
                Console.WriteLine($"File not found: {filePath}");
                return;
            }

            // Initialize the reader for all supported symbologies
            using (var reader = new BarCodeReader(filePath, DecodeType.AllSupportedTypes))
            {
                var results = reader.ReadBarCodes();

                // Handle case where no barcodes are detected
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                    return;
                }

                // Output each detected barcode's type and text
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }
    }

    /// <summary>
    /// Provides entry point for the Aspose.BarCode DI example.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures DI, generates a barcode, and reads it back.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set up the dependency injection container
            var services = new ServiceCollection();

            // Register the generator and reader services with transient lifetimes
            services.AddTransient<IBarcodeGeneratorService, BarcodeGeneratorService>();
            services.AddTransient<IBarcodeReaderService, BarcodeReaderService>();

            // Build the service provider to resolve services
            var provider = services.BuildServiceProvider();

            // Resolve services from the container
            var generatorService = provider.GetService<IBarcodeGeneratorService>();
            var readerService = provider.GetService<IBarcodeReaderService>();

            // Sample barcode data and output path
            string barcodeText = "HelloWorld";
            string outputPath = "barcode.png";

            // Generate the barcode image and then read it back
            generatorService?.Generate(barcodeText, outputPath);
            readerService?.Read(outputPath);
        }
    }
}