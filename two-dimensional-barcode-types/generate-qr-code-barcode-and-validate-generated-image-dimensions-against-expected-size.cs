// Title: Generate QR Code and Verify Image Dimensions
// Description: Creates a QR Code barcode, saves it as PNG, and checks that the generated image dimensions match the expected size for the specified QR version and X-dimension.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, demonstrating how to configure QR Code parameters such as X-dimension and version, generate the barcode image, and validate its pixel dimensions. It uses the BarcodeGenerator class together with EncodeTypes, QRVersion, and BarCodeImageFormat. Developers often need to ensure generated barcodes meet size requirements for downstream processing or printing.
// Prompt: Generate a QR Code barcode and validate generated image dimensions against expected size.
// Tags: qr code, barcode generation, image validation, aspose.barcode, png, dimensions, encode types, qr version

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates QR Code generation with Aspose.BarCode and validates the resulting image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR Code, saves it, and checks its width and height.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Define the output file path in the temporary directory.
        string outputPath = Path.Combine(Path.GetTempPath(), "qr.png");

        // Initialize the barcode generator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test QR"))
        {
            // Set the X-dimension (module size) to 4 pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Choose QR Code version 5 (Version05) which determines the matrix size.
            generator.Parameters.Barcode.QR.Version = QRVersion.Version05;

            // Save the generated QR Code as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);

            // Generate the barcode image in memory for dimension validation.
            using (var bitmap = generator.GenerateBarCodeImage())
            {
                int width = bitmap.Width;
                int height = bitmap.Height;
                int expectedSize = 180; // Expected size for Version05 with 4px XDimension

                Console.WriteLine($"Generated QR Code dimensions: {width}x{height}");

                // Compare actual dimensions with the expected size.
                if (width == expectedSize && height == expectedSize)
                {
                    Console.WriteLine($"Dimensions match expected size: {expectedSize}x{expectedSize}");
                }
                else
                {
                    Console.WriteLine($"Dimensions do NOT match expected size: {expectedSize}x{expectedSize}");
                }
            }
        }
    }
}