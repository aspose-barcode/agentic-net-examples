// Title: QR Code Generation with Real and Mock Implementations for DI
// Description: Demonstrates generating a QR Code barcode using Aspose.BarCode and a mock generator for unit testing or dependency injection scenarios.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing the use of BarcodeGenerator, EncodeTypes, and image format classes. Typical use cases include creating QR codes for URLs and providing mock implementations to facilitate testing without external dependencies. Developers often need to abstract barcode creation behind interfaces for DI and replace the real generator with a mock in test environments.
// Prompt: Generate a QR Code barcode and mock generator for dependency injection in application.
// Tags: qr code, barcode generation, mock, dependency injection, aspose.barcode, image output

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDemo
{
    // Simple abstraction for barcode generation
    public interface IBarcodeGenerator
    {
        void Generate(string codeText, string filePath);
    }

    // Real implementation using Aspose.BarCode
    public class AsposeBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string codeText, string filePath)
        {
            // Create a QR code generator with the supplied text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
            {
                // Set high error correction level for better resilience
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelH;

                // Use interpolation mode and define image size (300x300 points)
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 300f;

                // Save the generated QR code as a PNG file
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
        }
    }

    // Mock implementation for dependency injection testing
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        public void Generate(string codeText, string filePath)
        {
            // Create a simple placeholder image (300x300) with a light gray background
            using (var bitmap = new Bitmap(300, 300))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.LightGray);

                    // Draw placeholder text indicating this is a mock QR code
                    using (var font = new Font("Arial", 12f))
                    {
                        var brush = new SolidBrush(Color.Black);
                        graphics.DrawString("Mock QR", font, brush, new PointF(80f, 140f));
                    }
                }

                // Save the placeholder image as a PNG file
                bitmap.Save(filePath, ImageFormat.Png);
            }
        }
    }

    /// <summary>
    /// Demonstrates QR code generation using Aspose.BarCode and a mock generator for DI/testing.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point that creates a real QR code image and a mock placeholder image.
        /// </summary>
        static void Main()
        {
            const string outputReal = "qr_real.png";
            const string outputMock = "qr_mock.png";
            const string sampleText = "https://example.com";

            // Real barcode generation using the Aspose implementation
            IBarcodeGenerator realGenerator = new AsposeBarcodeGenerator();
            realGenerator.Generate(sampleText, outputReal);
            Console.WriteLine($"Real QR code saved to {outputReal}");

            // Mock barcode generation (useful for unit tests or when Aspose is unavailable)
            IBarcodeGenerator mockGenerator = new MockBarcodeGenerator();
            mockGenerator.Generate(sampleText, outputMock);
            Console.WriteLine($"Mock QR code saved to {outputMock}");
        }
    }
}