using System;
using System.IO;
using Microsoft.Extensions.DependencyInjection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

namespace BarcodeDIExample
{
    // Service interface for barcode generation
    public interface IBarcodeService
    {
        void GenerateBarcode(BaseEncodeType type, string codeText, string outputPath);
    }

    /// <summary>
    /// Implementation of <see cref="IBarcodeService"/> using Aspose.BarCode.
    /// </summary>
    public class BarcodeService : IBarcodeService
    {
        /// <summary>
        /// Generates a barcode image of the specified type and saves it to the given path.
        /// </summary>
        /// <param name="type">The barcode encoding type (e.g., Code128).</param>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The full file path where the barcode image will be saved.</param>
        public void GenerateBarcode(BaseEncodeType type, string codeText, string outputPath)
        {
            // Ensure the output directory exists before attempting to save the file
            string directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a BarcodeGenerator, configure its parameters, and save the image
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Example: set a higher resolution for better image quality
                generator.Parameters.Resolution = 300f;

                // Save the barcode image as PNG to the specified path
                generator.Save(outputPath);
            }
        }
    }

    /// <summary>
    /// Entry point of the application demonstrating dependency injection with the barcode service.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Configures services, resolves the barcode service, and generates a sample barcode.
        /// </summary>
        static void Main()
        {
            // Set up the dependency injection container
            var services = new ServiceCollection();
            services.AddSingleton<IBarcodeService, BarcodeService>();
            var serviceProvider = services.BuildServiceProvider();

            // Resolve the barcode service from the DI container
            var barcodeService = serviceProvider.GetRequiredService<IBarcodeService>();

            // Define the output file path for the generated barcode image
            string outputFile = Path.Combine(Directory.GetCurrentDirectory(), "code128.png");

            // Generate a Code128 barcode with sample text and save it to the output file
            barcodeService.GenerateBarcode(EncodeTypes.Code128, "Sample12345", outputFile);

            // Inform the user where the barcode image has been saved
            Console.WriteLine($"Barcode generated and saved to: {outputFile}");
        }
    }
}