// Title: QR Code generation with Aspose.BarCode and mock generator for DI testing
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode and provides a mock implementation for dependency injection scenarios.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce QR Code images and how to abstract the generator behind an interface for unit testing. Typical use cases include embedding QR codes in applications and mocking barcode creation during automated tests. Developers often need to inject barcode services, configure parameters like X dimension and error correction, and verify behavior without external dependencies.
// Prompt: Generate a QR Code barcode and mock generator for dependency injection in application.
// Tags: qr code, barcode generation, dependency injection, mock, aspose.barcode, c#, unit testing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeDemo
{
    // Defines a contract for barcode generation services.
    public interface IBarcodeGenerator
    {
        void Generate(string text, string outputPath);
    }

    // Real implementation that uses Aspose.BarCode to generate QR codes.
    public class AsposeBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string text, string outputPath)
        {
            // Validate input parameters.
            if (string.IsNullOrEmpty(text))
                throw new ArgumentException("Code text must not be null or empty.", nameof(text));
            if (string.IsNullOrEmpty(outputPath))
                throw new ArgumentException("Output path must not be null or empty.", nameof(outputPath));

            // Create a generator for QR code with the supplied text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set a modest X dimension and error correction level.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated QR code as a PNG image.
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    // Mock implementation used for dependency injection testing.
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string text, string outputPath)
        {
            // Write a simple text file indicating a mock operation.
            string mockContent = $"[Mock Barcode] Text: {text}";
            File.WriteAllText(outputPath, mockContent);
        }
    }

    /// <summary>
    /// Entry point demonstrating real and mock barcode generation.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Executes the demo: creates output directory, generates a real QR code, and a mock barcode file.
        /// </summary>
        static void Main()
        {
            // Prepare output directory in the system temporary folder.
            string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
            Directory.CreateDirectory(outputDir);

            // Real QR code generation.
            string realOutputPath = Path.Combine(outputDir, "qr_real.png");
            IBarcodeGenerator realGenerator = new AsposeBarcodeGenerator();
            realGenerator.Generate("https://example.com", realOutputPath);
            Console.WriteLine($"Real QR code saved to: {realOutputPath}");

            // Mock generation for dependency injection testing.
            string mockOutputPath = Path.Combine(outputDir, "qr_mock.txt");
            IBarcodeGenerator mockGenerator = new MockBarcodeGenerator();
            mockGenerator.Generate("https://example.com", mockOutputPath);
            Console.WriteLine($"Mock barcode output saved to: {mockOutputPath}");
        }
    }
}