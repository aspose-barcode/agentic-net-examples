// Title: Rotate QR Code 45 Degrees and Save as JPEG
// Description: Demonstrates how to generate a QR code, apply a 45-degree rotation, and save it as a JPEG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes with specific visual transformations. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to produce a QR code, rotate it, and export the result. Developers often need to customize barcode orientation for design integration, printing layouts, or branding purposes.
// Prompt: Apply a 45‑degree RotationAngle to a QR code and save the result as a JPEG image.
// Tags: qr code, rotation, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code, rotates it by 45 degrees, and saves the image as a JPEG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the QR code, applies rotation, and writes the output file path to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG image.
        string outputPath = Path.Combine(Environment.CurrentDirectory, "qr_rotated_45.jpg");

        // Initialize the barcode generator with QR encoding and sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
        {
            // Set the rotation angle to 45 degrees.
            generator.Parameters.RotationAngle = 45f;

            // Save the rotated QR code as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}