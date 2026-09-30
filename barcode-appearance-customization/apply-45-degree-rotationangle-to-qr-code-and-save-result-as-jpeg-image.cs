// Title: Rotate QR Code and Save as JPEG
// Description: Demonstrates applying a rotation angle to a QR code and saving the result as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode parameters such as rotation and output format. It uses the BarcodeGenerator class to create a QR code, sets the RotationAngle property, and saves the image with BarCodeImageFormat. Developers often need to rotate barcodes for layout requirements or visual styling, and this pattern illustrates the typical steps for QR code generation and image export.
// Prompt: Apply a 45‑degree RotationAngle to a QR code and save the result as a JPEG image.
// Tags: qr code, rotation, jpeg, aspose.barcode, barcode generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a QR code, applies a rotation, and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a QR code, sets a rotation angle, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the QR code.
        string codeText = "Hello, Aspose!";

        // Build the full output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_rotated.jpeg");

        // Ensure the directory for the output file exists.
        string outputDir = Path.GetDirectoryName(outputPath);
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Initialize the QR code generator with the desired symbology and text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, codeText))
        {
            // Set the rotation angle.
            // Aspose.BarCode supports rotation angles of 0, 90, 180, and 270 degrees.
            // The requested 45‑degree rotation is not supported, so the nearest supported value (90°) is used.
            generator.Parameters.RotationAngle = 90f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the file was saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}