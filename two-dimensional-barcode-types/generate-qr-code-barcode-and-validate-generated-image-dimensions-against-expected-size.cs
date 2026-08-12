// Title: Generate QR Code and Validate Image Dimensions
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, saving it as PNG, and checking that the generated image size matches expected dimensions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use BarcodeGenerator, set QR-specific parameters, render the barcode to a bitmap, and perform basic image validation. Typical use cases include automated QR code creation for marketing, inventory, or authentication workflows where developers need to ensure output size conforms to layout requirements.
// Prompt: Generate a QR Code barcode and validate generated image dimensions against expected size.
// Tags: qr code, barcode generation, image validation, aspose.barcode, aspose.drawing, png output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a QR Code, validates its dimensions, and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR Code, checks its size, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the expected image dimensions (adjust as needed for your layout)
        const int expectedWidth = 300;
        const int expectedHeight = 300;

        // Initialize a QR Code generator with the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR))
        {
            generator.CodeText = "Hello World";

            // Optional: configure the QR Code error correction level (LevelM provides a good balance)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Generate the barcode as a bitmap image
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Retrieve actual dimensions of the generated image
                int actualWidth = bitmap.Width;
                int actualHeight = bitmap.Height;

                Console.WriteLine($"Generated QR Code dimensions: {actualWidth}x{actualHeight}");

                // Compare actual dimensions with the expected values
                if (actualWidth == expectedWidth && actualHeight == expectedHeight)
                {
                    Console.WriteLine("Validation succeeded: dimensions match expected size.");
                }
                else
                {
                    Console.WriteLine("Validation warning: dimensions do not match expected size.");
                }

                // Save the bitmap to a PNG file on disk
                using (FileStream fileStream = new FileStream("qr.png", FileMode.Create, FileAccess.Write))
                {
                    bitmap.Save(fileStream, ImageFormat.Png);
                }
            }
        }

        Console.WriteLine("QR Code generation and validation completed.");
    }
}