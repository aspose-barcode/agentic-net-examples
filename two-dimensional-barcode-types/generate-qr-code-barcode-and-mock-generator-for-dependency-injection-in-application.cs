using System;
using System.IO;
using System.Text;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDemo
{
    // Interface for barcode generation – useful for DI
    public interface IBarcodeGenerator
    {
        // Generates a barcode image for the given text and returns the image bytes
        byte[] Generate(string text);
    }

    // Real implementation using Aspose.BarCode to create a QR Code
    public class AsposeQrBarcodeGenerator : IBarcodeGenerator
    {
        public byte[] Generate(string text)
        {
            // Create the generator with QR symbology and the supplied text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, text))
            {
                // Example: set error correction level to Medium
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the barcode to a memory stream in PNG format
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    // Return the PNG image as a byte array
                    return ms.ToArray();
                }
            }
        }
    }

    // Mock implementation for unit testing / DI scenarios
    public class MockBarcodeGenerator : IBarcodeGenerator
    {
        public byte[] Generate(string text)
        {
            // Return a simple placeholder byte array (e.g., UTF‑8 encoded text)
            // In real tests this could be a pre‑generated image byte array.
            return Encoding.UTF8.GetBytes($"Mock barcode for: {text}");
        }
    }

    /// <summary>
    /// Demonstrates usage of real and mock barcode generators.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// Generates a QR code using the real generator, saves it to a file,
        /// and then demonstrates the mock generator output.
        /// </summary>
        static void Main()
        {
            // Sample text to encode
            const string sampleText = "Hello Aspose QR!";

            // Use the real generator
            IBarcodeGenerator realGenerator = new AsposeQrBarcodeGenerator();
            byte[] qrBytes = realGenerator.Generate(sampleText);

            // Save the generated QR code to a file
            const string qrFilePath = "qr.png";
            File.WriteAllBytes(qrFilePath, qrBytes);
            Console.WriteLine($"QR Code saved to {qrFilePath} (size: {qrBytes.Length} bytes).");

            // Demonstrate the mock generator
            IBarcodeGenerator mockGenerator = new MockBarcodeGenerator();
            byte[] mockBytes = mockGenerator.Generate(sampleText);
            Console.WriteLine($"Mock generator produced {mockBytes.Length} bytes.");
            Console.WriteLine($"Mock content (UTF‑8): {Encoding.UTF8.GetString(mockBytes)}");
        }
    }
}