// Title: Generate QR Code and Verify Image Dimensions
// Description: This example creates a QR Code barcode, configures its version and module size, and validates that the resulting image dimensions match the expected size.
// Category-Description: Demonstrates Aspose.BarCode barcode generation and basic image validation. It uses BarcodeGenerator, EncodeTypes, QRVersion, and QRErrorLevel to produce a QR Code, then employs Aspose.Drawing.Bitmap to inspect the image dimensions. Typical for developers needing to ensure generated barcodes fit layout constraints before printing or embedding in documents. Part of a collection of examples showing barcode creation, customization, and verification with Aspose.BarCode.
// Prompt: Generate a QR Code barcode and validate generated image dimensions against expected size.
// Tags: qr code, barcode generation, image validation, dimensions, aspose.barcode, aspose.drawing, c#

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a QR Code barcode with specific parameters
/// and verify that the generated image dimensions match the expected size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the QR Code, checks dimensions,
    /// and saves the image to a temporary location.
    /// </summary>
    static void Main()
    {
        // Define expected QR version and module size
        // QR Version 5 has 37 modules per side (including quiet zone)
        const int moduleCount = 37;
        const float xDimensionPixels = 4f;
        int expectedSize = (int)Math.Round(moduleCount * xDimensionPixels);

        // Initialize the barcode generator for QR Code with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Configure QR Code specific parameters
            generator.Parameters.Barcode.XDimension.Pixels = xDimensionPixels; // size of one module in pixels
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;      // set QR version to 5
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // medium error correction

            // Generate the barcode image as a bitmap
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve actual image dimensions
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                // Compare actual dimensions with expected dimensions
                if (actualWidth == expectedSize && actualHeight == expectedSize)
                {
                    Console.WriteLine($"Success: Image dimensions match expected {expectedSize}x{expectedSize}.");
                }
                else
                {
                    Console.WriteLine($"Failure: Image dimensions {actualWidth}x{actualHeight} do not match expected {expectedSize}x{expectedSize}.");
                }

                // Optionally save the image to verify manually
                string outputPath = Path.Combine(Path.GetTempPath(), "qr_sample.png");
                using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, Aspose.Drawing.Imaging.ImageFormat.Png);
                }
                Console.WriteLine($"Barcode image saved to: {outputPath}");
            }
        }
    }
}