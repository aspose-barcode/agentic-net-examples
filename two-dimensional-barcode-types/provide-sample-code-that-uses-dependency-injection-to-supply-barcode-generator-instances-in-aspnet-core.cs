// Title: Dependency Injection Example for Aspose.BarCode Generator in ASP.NET Core
// Description: Demonstrates how to register and resolve a barcode generator service using ASP.NET Core's built‑in DI container, then generate a Code128 barcode and save it as PNG.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating typical use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat within a DI pattern. Developers often need to inject barcode services into controllers or background jobs to produce barcodes on demand, and this snippet shows the essential setup and usage.
// Prompt: Provide sample code that uses dependency injection to supply barcode generator instances in ASP.NET Core.
// Tags: barcode, code128, dependency injection, aspnet core, aspnetcore, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Microsoft.Extensions.DependencyInjection;

namespace BarcodeDiSample
{
    /// <summary>
    /// Service interface for barcode generation.
    /// </summary>
    public interface IBarcodeService
    {
        void GenerateBarcode(string codeText, string filePath);
    }

    /// <summary>
    /// Service for generating barcodes using Aspose.BarCode.
    /// </summary>
    public class BarcodeService : IBarcodeService
    {
        public void GenerateBarcode(string codeText, string filePath)
        {
            // Create a generator for Code128 symbology with the supplied text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Optional: customize appearance
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 30f;

                // Ensure the output directory exists
                string directory = Path.GetDirectoryName(filePath);
                if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                {
                    Directory.CreateDirectory(directory);
                }

                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
                Console.WriteLine($"Barcode saved to: {filePath}");
            }
        }
    }

    /// <summary>
    /// Entry point demonstrating DI registration and barcode generation.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the DI container, resolves the barcode service, and creates a sample barcode image.
        /// </summary>
        static void Main(string[] args)
        {
            // Set up the DI container
            var services = new ServiceCollection();
            services.AddTransient<IBarcodeService, BarcodeService>();
            IServiceProvider provider = services.BuildServiceProvider();

            // Resolve the barcode service from the container
            var barcodeService = provider.GetRequiredService<IBarcodeService>();

            // Define sample barcode data and output location
            string sampleText = "1234567890";
            string outputPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

            // Generate and save the barcode
            barcodeService.GenerateBarcode(sampleText, outputPath);
        }
    }
}