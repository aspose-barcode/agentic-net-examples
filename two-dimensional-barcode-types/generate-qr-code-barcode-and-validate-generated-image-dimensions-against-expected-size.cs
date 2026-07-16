// Title: Generate QR Code and Validate Image Dimensions
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, setting explicit image size, and verifying the generated image dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and image handling category. It showcases the use of BarcodeGenerator, EncodeTypes, and image size parameters (ImageWidth, ImageHeight, AutoSizeMode) to produce a QR Code of a specific dimension. Developers often need to generate barcodes with exact pixel sizes for UI layout or printing, and this pattern illustrates how to configure size, error correction, and validate the output.
// Prompt: Generate a QR Code barcode and validate generated image dimensions against expected size.
// Tags: qr code, barcode generation, image dimensions, aspose.barcode, bitmap, validation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode, forces a specific image size,
/// and validates that the resulting bitmap matches the expected dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR Code, sets size parameters,
    /// checks the bitmap dimensions, and saves the image to disk.
    /// </summary>
    static void Main()
    {
        // Expected dimensions in pixels for the generated QR Code image.
        const int expectedWidth = 200;
        const int expectedHeight = 200;

        // Initialize the QR Code generator with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
        {
            // Explicitly set the image width and height (in points, which map to pixels here).
            generator.Parameters.ImageWidth.Point = expectedWidth;
            generator.Parameters.ImageHeight.Point = expectedHeight;

            // Use interpolation mode so the size settings are respected during rendering.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Optional: set the QR Code error correction level to Medium.
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode image as a Bitmap.
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Validate that the generated bitmap dimensions match the expected size.
                if (bitmap.Width == expectedWidth && bitmap.Height == expectedHeight)
                {
                    Console.WriteLine("Dimensions match expected size.");
                }
                else
                {
                    Console.WriteLine($"Dimension mismatch: Expected {expectedWidth}x{expectedHeight}, " +
                                      $"but got {bitmap.Width}x{bitmap.Height}.");
                }

                // Save the image for visual verification (optional).
                bitmap.Save("qr_code.png", Aspose.Drawing.Imaging.ImageFormat.Png);
            }
        }
    }
}