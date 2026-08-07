// Title: Dependency Injection Example for Barcode Generation
// Description: Demonstrates how to inject a barcode generation service using a simple manual DI container and Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and related parameter classes. It illustrates typical scenarios where developers need to abstract barcode creation behind an interface for easier testing and maintainability, often employing dependency injection patterns in .NET applications.
// Prompt: Implement dependency injection to provide a barcode generation service throughout the application.
// Tags: barcode generation, dependency injection, code128, png, aspose.barcode, aspnet core

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDiExample
{
    // Service contract for barcode generation
    public interface IBarcodeService
    {
        /// <summary>
        /// Generates a barcode image from the provided text and saves it to the specified path.
        /// </summary>
        /// <param name="codeText">The text to encode in the barcode.</param>
        /// <param name="outputPath">The file system path where the barcode image will be saved.</param>
        void GenerateBarcode(string codeText, string outputPath);
    }

    // Concrete implementation using Aspose.BarCode
    public class BarcodeService : IBarcodeService
    {
        public void GenerateBarcode(string codeText, string outputPath)
        {
            // Ensure the output directory exists
            string directory = System.IO.Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !System.IO.Directory.Exists(directory))
            {
                System.IO.Directory.CreateDirectory(directory);
            }

            // Create and configure the barcode generator
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set basic colors (optional)
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image to the specified path
                generator.Save(outputPath);
            }
        }
    }

    /// <summary>
    /// Provides a simple example of using dependency injection to generate barcodes.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application. Resolves the IBarcodeService and generates a sample barcode.
        /// </summary>
        static void Main()
        {
            // Simple manual DI container: resolve the service implementation
            IBarcodeService barcodeService = new BarcodeService();

            // Sample barcode data and output file
            string sampleText = "ABC123";
            string outputFile = "barcode.png";

            // Generate the barcode using the injected service
            barcodeService.GenerateBarcode(sampleText, outputFile);

            Console.WriteLine($"Barcode generated and saved to '{outputFile}'.");
        }
    }
}