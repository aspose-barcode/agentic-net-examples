// Title: QR Code Generation with Real and Mock Implementations using Aspose.BarCode
// Description: Demonstrates how to generate a QR Code barcode with Aspose.BarCode and provides a mock generator for dependency injection testing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and QR error correction settings. It illustrates typical scenarios such as creating actual barcodes for production and supplying mock implementations for unit tests or DI containers. Developers often need to switch between real and mock generators without changing business logic.
// Prompt: Generate a QR Code barcode and mock generator for dependency injection in application.
// Tags: qr code, barcode generation, mock, dependency injection, aspose.barcode, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace AsposeBarcodeDemo
{
    // Interface for barcode generation
    public interface IBarcodeGenerator
    {
        void Generate(string text, string outputPath);
    }

    // Real implementation using Aspose.BarCode
    public class RealBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string text, string outputPath)
        {
            // Ensure the output directory exists
            string? directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create and configure the QR code generator
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set high error correction level
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;
                // Save as PNG
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }
        }
    }

    // Mock implementation for testing / DI scenarios
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string text, string outputPath)
        {
            // Ensure the output directory exists
            string? directory = Path.GetDirectoryName(outputPath);
            if (!string.IsNullOrEmpty(directory) && !Directory.Exists(directory))
            {
                Directory.CreateDirectory(directory);
            }

            // Create a simple placeholder image (white background)
            using (var bitmap = new Bitmap(200, 200))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                }

                // Save the placeholder image as PNG
                bitmap.Save(outputPath, ImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Demonstrates QR code generation using a real Aspose.BarCode generator and a mock generator for DI scenarios.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates an output folder, generates a real QR code and a mock placeholder image, and writes their paths to the console.
        /// </summary>
        static void Main()
        {
            // Define output folder in the system temporary directory
            string outputFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
            if (!Directory.Exists(outputFolder))
            {
                Directory.CreateDirectory(outputFolder);
            }

            // Real QR code generation
            IBarcodeGenerator realGenerator = new RealBarcodeGenerator();
            string realPath = Path.Combine(outputFolder, "qr_real.png");
            realGenerator.Generate("Hello Aspose!", realPath);
            Console.WriteLine($"Real QR code saved to: {realPath}");

            // Mock QR code generation
            IBarcodeGenerator mockGenerator = new MockBarcodeGenerator();
            string mockPath = Path.Combine(outputFolder, "qr_mock.png");
            mockGenerator.Generate("Mock QR", mockPath);
            Console.WriteLine($"Mock QR code saved to: {mockPath}");
        }
    }
}