// Title: Dependency Injection Example for Barcode Generation with Aspose.BarCode
// Description: Demonstrates how to use a simple DI container to inject a barcode generation service and create a Code128 barcode image.
// Category-Description: Shows Aspose.BarCode generation operations, focusing on the BarcodeGenerator class and related parameters. This example belongs to the barcode creation category, illustrating typical use cases such as setting colors, resolution, and saving to PNG. Developers looking for DI patterns with Aspose.BarCode can reference this snippet for quick integration.
// Prompt: Implement dependency injection to provide a barcode generation service throughout the application.
// Tags: barcode, dependency injection, code128, png, aspose.barcode, generation, service, di

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDIExample
{
    // Service contract for barcode generation
    public interface IBarcodeGeneratorService
    {
        void GenerateBarcode(string codeText, string outputPath);
    }

    // Concrete implementation using Aspose.BarCode
    public class BarcodeGeneratorService : IBarcodeGeneratorService
    {
        public void GenerateBarcode(string codeText, string outputPath)
        {
            // Validate input
            if (string.IsNullOrWhiteSpace(codeText))
                throw new ArgumentException("Code text must not be empty.", nameof(codeText));

            // Ensure output directory exists
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            // Create and configure the barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set visual parameters
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.Resolution = 300f;

                // Save the barcode image as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    // Very simple service container for singleton registrations
    public class ServiceProvider
    {
        private readonly Dictionary<Type, object> _services = new Dictionary<Type, object>();

        // Register a singleton instance
        public void AddSingleton<TService>(TService implementation) where TService : class
        {
            _services[typeof(TService)] = implementation ?? throw new ArgumentNullException(nameof(implementation));
        }

        // Resolve a registered service
        public TService GetService<TService>() where TService : class
        {
            if (_services.TryGetValue(typeof(TService), out var service))
                return service as TService;

            throw new InvalidOperationException($"Service of type {typeof(TService).FullName} is not registered.");
        }
    }

    /// <summary>
    /// Entry point of the application demonstrating DI with a barcode generation service.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the DI container, resolves the barcode service, and generates a sample barcode image.
        /// </summary>
        static void Main()
        {
            // Set up simple DI container
            var serviceProvider = new ServiceProvider();
            serviceProvider.AddSingleton<IBarcodeGeneratorService>(new BarcodeGeneratorService());

            // Resolve the barcode generation service
            var barcodeService = serviceProvider.GetService<IBarcodeGeneratorService>();

            // Prepare temporary output folder
            string tempPath = Path.Combine(Path.GetTempPath(), "barcode_example");
            if (!Directory.Exists(tempPath))
                Directory.CreateDirectory(tempPath);

            // Define output file and sample text
            string outputFile = Path.Combine(tempPath, "sample_barcode.png");
            string sampleText = "Sample123";

            try
            {
                // Generate the barcode image
                barcodeService.GenerateBarcode(sampleText, outputFile);
                Console.WriteLine($"Barcode generated at: {outputFile}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation
                Console.WriteLine($"Error generating barcode: {ex.Message}");
            }
        }
    }
}