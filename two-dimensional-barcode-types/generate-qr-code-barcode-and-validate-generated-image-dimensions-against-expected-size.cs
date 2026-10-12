// Title: Generate QR Code and Verify Image Dimensions
// Description: This example creates a QR Code barcode, saves it as a PNG file, and checks that the generated image matches the expected width and height.
// Category-Description: Demonstrates Aspose.BarCode barcode generation (BarcodeGenerator) with QR symbology, configuring image size, error correction level, and exporting to PNG. Typical for developers needing to produce QR codes for marketing, authentication, or data transfer and verify output dimensions using Aspose.Drawing imaging classes. Part of a collection of barcode creation and validation examples.
// Prompt: Generate a QR Code barcode and validate generated image dimensions against expected size.
// Tags: qr code, barcode generation, image validation, aspose.barcode, png, dimensions

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR Code barcode, saving it to a file, and validating the image dimensions.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates QR Code, saves it, and validates size.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory to store the generated QR code image.
        string tempDir = Path.Combine(Path.GetTempPath(), "QrDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "qr.png");

        // Expected image dimensions in pixels.
        const int expectedWidth = 200;
        const int expectedHeight = 200;

        // Initialize the barcode generator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Aspose QR Test"))
        {
            // Configure auto-sizing to use the nearest size that fits the specified dimensions.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Nearest;

            // Set the target image width and height.
            generator.Parameters.ImageWidth.Pixels = expectedWidth;
            generator.Parameters.ImageHeight.Pixels = expectedHeight;

            // Set module (dot) size and error correction level.
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the barcode image directly to a file in PNG format.
            generator.Save(imagePath, BarCodeImageFormat.Png);

            // Also save the barcode to a memory stream to validate dimensions without reloading from disk.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the image from the memory stream.
                using (var img = Image.FromStream(ms))
                {
                    int actualWidth = img.Width;
                    int actualHeight = img.Height;

                    // Output the actual dimensions and whether they match the expectations.
                    Console.WriteLine($"Generated image size: {actualWidth}x{actualHeight}");
                    bool sizeMatch = actualWidth == expectedWidth && actualHeight == expectedHeight;
                    Console.WriteLine($"Size matches expected: {sizeMatch}");
                }
            }
        }
    }
}