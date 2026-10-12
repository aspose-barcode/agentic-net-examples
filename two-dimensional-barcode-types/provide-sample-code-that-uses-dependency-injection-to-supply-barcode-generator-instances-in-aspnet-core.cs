// Title: Dependency Injection Example for Aspose.BarCode Generator in ASP.NET Core
// Description: Demonstrates how to register a barcode generator factory with ASP.NET Core's built‑in DI container and create a Code128 barcode image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of the BarcodeGenerator and related parameter classes together with Microsoft.Extensions.DependencyInjection. Developers often need to inject barcode creation services into ASP.NET Core applications for on‑the‑fly image generation, custom styling, and scalable architecture. The snippet shows typical registration, resolution, and usage patterns for such scenarios.
// Prompt: Provide sample code that uses dependency injection to supply barcode generator instances in ASP.NET Core.
// Tags: barcode, dependency injection, aspnet core, code128, image, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Microsoft.Extensions.DependencyInjection;

namespace BarcodeDiSample
{
    /// <summary>
    /// Factory interface for creating <see cref="BarcodeGenerator"/> instances.
    /// </summary>
    public interface IBarcodeGeneratorFactory
    {
        /// <summary>
        /// Creates a new <see cref="BarcodeGenerator"/> with the specified code text.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>A configured <see cref="BarcodeGenerator"/> instance.</returns>
        BarcodeGenerator Create(string codeText);
    }

    /// <summary>
    /// Concrete implementation of <see cref="IBarcodeGeneratorFactory"/> that injects the encode type.
    /// </summary>
    public class BarcodeGeneratorFactory : IBarcodeGeneratorFactory
    {
        private readonly BaseEncodeType _encodeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeGeneratorFactory"/> class.
        /// </summary>
        /// <param name="encodeType">The barcode symbology to use (e.g., Code128).</param>
        public BarcodeGeneratorFactory(BaseEncodeType encodeType)
        {
            _encodeType = encodeType ?? throw new ArgumentNullException(nameof(encodeType));
        }

        /// <inheritdoc/>
        public BarcodeGenerator Create(string codeText)
        {
            if (codeText == null) throw new ArgumentNullException(nameof(codeText));
            return new BarcodeGenerator(_encodeType, codeText);
        }
    }

    /// <summary>
    /// Sample program that shows how to use DI to obtain barcode generator instances.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point. Configures DI, resolves a factory, creates a barcode and saves it as PNG.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set up a simple DI container
            var services = new ServiceCollection();

            // Register the factory with a specific encode type (Code128)
            services.AddTransient<IBarcodeGeneratorFactory>(sp => new BarcodeGeneratorFactory(EncodeTypes.Code128));

            // Build the service provider to resolve services
            using (var serviceProvider = services.BuildServiceProvider())
            {
                // Resolve the factory from the container
                var factory = serviceProvider.GetRequiredService<IBarcodeGeneratorFactory>();

                // Create a barcode generator instance via DI
                using (var generator = factory.Create("DI-Example-123"))
                {
                    // Optional: customize appearance
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the barcode image to a temporary file
                    string outputPath = Path.Combine(Path.GetTempPath(), "di_barcode.png");
                    generator.Save(outputPath, BarCodeImageFormat.Png);

                    // Inform the user where the file was saved
                    Console.WriteLine($"Barcode saved to: {outputPath}");
                }
            }
        }
    }
}