// Title: QR Code generation with Aspose.BarCode and mock implementation for DI
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and provides a mock generator to facilitate dependency injection in unit tests.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with QR symbology, configure parameters like XDimension and error correction level, and save the image as PNG. It also illustrates a simple mock implementation of a barcode generator interface for testing purposes, a common pattern for developers integrating barcode creation into applications via dependency injection.
// Prompt: Generate a QR Code barcode and mock generator for dependency injection in application.
// Tags: qr code, barcode generation, dependency injection, mock, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDemo
{
    /// <summary>
    /// Defines a contract for generating barcodes, enabling real and mock implementations.
    /// </summary>
    public interface IBarcodeGenerator
    {
        /// <summary>
        /// Generates a barcode from the specified text and saves it to the given path.
        /// </summary>
        /// <param name="text">The data to encode in the barcode.</param>
        /// <param name="outputPath">The file system path where the barcode image will be saved.</param>
        void Generate(string text, string outputPath);
    }

    /// <summary>
    /// Real implementation that uses Aspose.BarCode to generate QR Code images.
    /// </summary>
    public class AsposeBarcodeGenerator : IBarcodeGenerator
    {
        /// <inheritdoc/>
        public void Generate(string text, string outputPath)
        {
            // Create a BarcodeGenerator for QR symbology with the provided text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set the size of each QR module (pixel dimension).
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Configure the error correction level (Level M provides a good balance).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG file.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Mock implementation used for unit testing or DI scenarios where actual barcode generation is unnecessary.
    /// </summary>
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        /// <inheritdoc/>
        public void Generate(string text, string outputPath)
        {
            // Simulate barcode generation by writing a descriptive message to the console.
            Console.WriteLine($"Mock generate QR code with text '{text}' to '{outputPath}'");
        }
    }

    /// <summary>
    /// Entry point of the demo application that showcases real and mock barcode generation.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Executes the demo: creates a temporary folder, generates a real QR code, and runs the mock generator.
        /// </summary>
        static void Main()
        {
            // Prepare a temporary directory for output files.
            string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeQrDemo");
            Directory.CreateDirectory(tempFolder);

            // Generate a real QR code using the Aspose implementation.
            string realOutput = Path.Combine(tempFolder, "real_qr.png");
            IBarcodeGenerator realGenerator = new AsposeBarcodeGenerator();
            realGenerator.Generate("Hello Aspose QR", realOutput);
            Console.WriteLine($"Real QR code saved to: {realOutput}");

            // Run the mock generator to demonstrate DI-friendly testing.
            string mockOutput = Path.Combine(tempFolder, "mock_qr.png");
            IBarcodeGenerator mockGenerator = new MockBarcodeGenerator();
            mockGenerator.Generate("Mock QR Text", mockOutput);
        }
    }
}