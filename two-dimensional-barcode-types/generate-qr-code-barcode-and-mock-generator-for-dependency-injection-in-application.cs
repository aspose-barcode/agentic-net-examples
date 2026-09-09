// Title: Generate QR Code barcode with Aspose and provide a mock generator for DI
// Description: Demonstrates creating a QR Code using Aspose.BarCode and a mock implementation that returns a placeholder image, useful for dependency injection in unit tests.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class with QR symbology, configure parameters like X‑Dimension and error correction level, and save the result as PNG. It also shows how to create a lightweight mock generator implementing a common IBarcodeGenerator interface for testing scenarios where the real barcode generation is unnecessary. Developers often need such patterns to decouple barcode creation from business logic and enable easy unit testing.
// Prompt: Generate a QR Code barcode and mock generator for dependency injection in application.
// Tags: qr code, barcode generation, dependency injection, mock, aspose.barcode, png, unit testing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDemo
{
    /// <summary>
    /// Simple abstraction for barcode generation. Allows swapping real and mock implementations.
    /// </summary>
    public interface IBarcodeGenerator
    {
        /// <summary>
        /// Generates a barcode image for the specified text and returns the image bytes.
        /// </summary>
        /// <param name="text">The data to encode in the barcode.</param>
        /// <returns>Byte array containing the generated image.</returns>
        byte[] Generate(string text);
    }

    /// <summary>
    /// Real barcode generator that uses Aspose.BarCode to create QR Code images.
    /// </summary>
    public class AsposeBarcodeGenerator : IBarcodeGenerator
    {
        public byte[] Generate(string text)
        {
            // Create a QR Code generator with the supplied text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Set QR Code specific parameters.
                generator.Parameters.Barcode.XDimension.Point = 4f;               // Size of a single module.
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Medium error correction.

                // Save the generated barcode to a memory stream in PNG format.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Mock barcode generator that returns a 1x1 white PNG image.
    /// Useful for unit tests where actual barcode generation is unnecessary.
    /// </summary>
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        public byte[] Generate(string text)
        {
            // Create a minimal bitmap.
            using (var bitmap = new Bitmap(1, 1))
            {
                // Fill the bitmap with white color.
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.Clear(Color.White);
                }

                // Encode the bitmap to PNG and return the bytes.
                using (var ms = new MemoryStream())
                {
                    bitmap.Save(ms, ImageFormat.Png);
                    return ms.ToArray();
                }
            }
        }
    }

    /// <summary>
    /// Entry point of the demo application. Generates a real QR Code and a mock image, then writes them to disk.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Main method that orchestrates barcode generation and file output.
        /// </summary>
        static void Main()
        {
            // Sample text to encode.
            string sampleText = "Hello Aspose QR!";

            // Use the real generator to create a QR Code.
            IBarcodeGenerator realGenerator = new AsposeBarcodeGenerator();
            byte[] qrBytes = realGenerator.Generate(sampleText);
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr.png");
            File.WriteAllBytes(outputPath, qrBytes);
            Console.WriteLine($"QR code saved to {outputPath}");

            // Use the mock generator to create a placeholder image.
            IBarcodeGenerator mockGenerator = new MockBarcodeGenerator();
            byte[] mockBytes = mockGenerator.Generate(sampleText);
            string mockPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_mock.png");
            File.WriteAllBytes(mockPath, mockBytes);
            Console.WriteLine($"Mock QR code saved to {mockPath}");
        }
    }
}