// Title: Dependency Injection for Aspose.BarCode Generator in ASP.NET Core
// Description: Demonstrates registering a barcode generator factory with Microsoft.Extensions.DependencyInjection, creating a Code128 barcode, and saving it as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator and related parameter classes together with .NET Core's built‑in DI container. Typical scenarios include web APIs or background services that need to produce barcodes on demand. Developers often register factories or services that encapsulate barcode configuration to keep controller code clean and testable.
// Prompt: Provide sample code that uses dependency injection to supply barcode generator instances in ASP.NET Core.
// Tags: barcode symbology, generation, dependency injection, aspnet core, aspose.barcode, png, code128

using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDiSample
{
    /// <summary>
    /// Factory interface for creating <see cref="BarcodeGenerator"/> instances with a predefined encode type.
    /// </summary>
    public interface IBarcodeGeneratorFactory
    {
        /// <summary>
        /// Creates a new <see cref="BarcodeGenerator"/> configured with the supplied code text.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>A configured <see cref="BarcodeGenerator"/> instance.</returns>
        BarcodeGenerator Create(string codeText);
    }

    /// <summary>
    /// Concrete implementation of <see cref="IBarcodeGeneratorFactory"/> that uses a fixed <see cref="BaseEncodeType"/>.
    /// </summary>
    public class BarcodeGeneratorFactory : IBarcodeGeneratorFactory
    {
        private readonly BaseEncodeType _encodeType;

        /// <summary>
        /// Initializes a new instance of the <see cref="BarcodeGeneratorFactory"/> class.
        /// </summary>
        /// <param name="encodeType">The barcode symbology to use for all generated barcodes.</param>
        public BarcodeGeneratorFactory(BaseEncodeType encodeType)
        {
            _encodeType = encodeType;
        }

        /// <inheritdoc/>
        public BarcodeGenerator Create(string codeText)
        {
            // Create a generator with the configured encode type and supplied text.
            var generator = new BarcodeGenerator(_encodeType, codeText);
            // Adjust the X-dimension (module width) for better visual quality.
            generator.Parameters.Barcode.XDimension.Point = 2f;
            return generator;
        }
    }

    /// <summary>
    /// Entry point of the sample application that demonstrates DI‑based barcode generation.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures the service container, resolves a barcode generator factory, creates a barcode, and saves it as PNG.
        /// </summary>
        static void Main()
        {
            // Set up a new service collection for dependency injection.
            var services = new ServiceCollection();

            // Register the encode type (Code128) as a singleton.
            services.AddSingleton<BaseEncodeType>(EncodeTypes.Code128);

            // Register the factory as a transient service.
            services.AddTransient<IBarcodeGeneratorFactory, BarcodeGeneratorFactory>();

            // Build the service provider to resolve services.
            var provider = services.BuildServiceProvider();

            // Resolve the factory from the DI container.
            var factory = provider.GetRequiredService<IBarcodeGeneratorFactory>();

            // Use the factory to create a barcode generator for the desired text.
            using (var generator = factory.Create("ABC-12345"))
            {
                // Determine a temporary file path for the output image.
                string outputPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

                // Save the generated barcode as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);

                // Inform the user where the file was saved.
                Console.WriteLine($"Barcode generated at: {outputPath}");
            }
        }
    }
}