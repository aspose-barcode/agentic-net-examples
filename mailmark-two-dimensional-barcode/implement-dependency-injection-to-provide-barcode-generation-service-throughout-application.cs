// Title: Barcode Generation with Dependency Injection using Aspose.BarCode
// Description: Demonstrates registering a barcode generation service in a DI container and using it to create a Code128 PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to leverage Microsoft.Extensions.DependencyInjection to inject a barcode service. It highlights key API classes such as BarcodeGenerator, EncodeTypes, and the IBarcodeService contract. Developers often need to generate barcodes in various formats across different layers of an application; this pattern provides a clean, testable approach for such scenarios.
// Prompt: Implement dependency injection to provide a barcode generation service throughout the application.
// Tags: barcode symbology, generation, png, aspose.barcode, dependency injection, csharp

using System;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDIExample
{
    /// <summary>
    /// Service contract for barcode generation.
    /// </summary>
    public interface IBarcodeService
    {
        /// <summary>
        /// Generates a barcode image from the specified text and saves it to the given path.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The file path where the barcode image will be saved.</param>
        void Generate(string codeText, string outputPath);
    }

    /// <summary>
    /// Concrete implementation of <see cref="IBarcodeService"/> using Aspose.BarCode.
    /// </summary>
    public class BarcodeService : IBarcodeService, IDisposable
    {
        private bool _disposed = false;

        /// <inheritdoc/>
        public void Generate(string codeText, string outputPath)
        {
            // Use Code128 as an example symbology.
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                generator.CodeText = codeText;
                // Save the generated barcode as a PNG file.
                generator.Save(outputPath);
            }
        }

        /// <summary>
        /// Disposes the service. Currently no unmanaged resources are held, but the pattern is kept for future extensibility.
        /// </summary>
        public void Dispose()
        {
            if (!_disposed)
            {
                // No unmanaged resources to release.
                _disposed = true;
            }
        }
    }

    /// <summary>
    /// Application entry point demonstrating DI-based barcode generation.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the DI container, resolves the barcode service, and generates a sample barcode.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set up a simple DI container.
            var services = new ServiceCollection();

            // Register the barcode service as a transient dependency.
            services.AddTransient<IBarcodeService, BarcodeService>();

            // Build the service provider and resolve services within a using block to ensure disposal.
            using (var provider = services.BuildServiceProvider())
            {
                // Resolve the barcode service.
                var barcodeService = provider.GetRequiredService<IBarcodeService>();

                // Sample data and output file.
                string sampleText = "123ABC456";
                string outputFile = "sample_code128.png";

                // Generate the barcode image.
                barcodeService.Generate(sampleText, outputFile);

                Console.WriteLine($"Barcode generated and saved to '{outputFile}'.");
            }

            // Program exits automatically.
        }
    }
}