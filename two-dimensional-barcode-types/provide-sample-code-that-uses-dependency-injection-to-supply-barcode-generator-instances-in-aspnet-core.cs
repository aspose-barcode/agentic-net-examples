using System;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDiSample
{
    // Interface for creating BarcodeGenerator instances
    public interface IBarcodeGeneratorFactory
    {
        /// <summary>
        /// Creates a new <see cref="BarcodeGenerator"/> with the specified encoding type and text.
        /// </summary>
        /// <param name="type">The barcode encoding type.</param>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <returns>A configured <see cref="BarcodeGenerator"/> instance.</returns>
        BarcodeGenerator Create(BaseEncodeType type, string codeText);
    }

    // Concrete factory that creates and configures BarcodeGenerator objects
    public class BarcodeGeneratorFactory : IBarcodeGeneratorFactory
    {
        /// <inheritdoc/>
        public BarcodeGenerator Create(BaseEncodeType type, string codeText)
        {
            // Instantiate a new BarcodeGenerator (implements IDisposable)
            var generator = new BarcodeGenerator(type, codeText);

            // Enable checksum for Code128 (required for this symbology)
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Set image resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Set barcode bar height (effective when AutoSizeMode is None)
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Configure padding around the barcode (left, top, right, bottom)
            generator.Parameters.Barcode.Padding.Left.Point = 5f;
            generator.Parameters.Barcode.Padding.Top.Point = 5f;
            generator.Parameters.Barcode.Padding.Right.Point = 5f;
            generator.Parameters.Barcode.Padding.Bottom.Point = 5f;

            // Return the fully configured generator
            return generator;
        }
    }

    /// <summary>
    /// Demonstrates dependency injection with Aspose.BarCode to generate a barcode image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Application entry point. Sets up DI, creates a barcode, and saves it to a file.
        /// </summary>
        /// <param name="args">Command‑line arguments (not used).</param>
        static void Main(string[] args)
        {
            // Set up a simple DI container
            var services = new ServiceCollection();

            // Register the factory as a singleton service
            services.AddSingleton<IBarcodeGeneratorFactory, BarcodeGeneratorFactory>();

            // Build the service provider
            var provider = services.BuildServiceProvider();

            // Resolve the factory from the container
            var factory = provider.GetRequiredService<IBarcodeGeneratorFactory>();

            // Create a barcode generator for Code128 with sample text
            using (var generator = factory.Create(EncodeTypes.Code128, "123ABC"))
            {
                // Save the generated barcode image to a PNG file
                generator.Save("code128.png");

                // Inform the user that the file has been saved
                Console.WriteLine("Barcode saved to code128.png");
            }
        }
    }
}