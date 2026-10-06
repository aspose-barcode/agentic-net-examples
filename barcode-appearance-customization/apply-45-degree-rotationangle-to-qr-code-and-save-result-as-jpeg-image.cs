// Title: Rotate QR Code 45 Degrees and Save as JPEG
// Description: Demonstrates how to generate a QR code, apply a 45-degree rotation, and save it as a JPEG image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance with rotation. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create and export barcodes. Developers often need to rotate barcodes for branding, layout constraints, or visual effects, and this pattern illustrates the typical steps for such operations.
// Prompt: Apply a 45‑degree RotationAngle to a QR code and save the result as a JPEG image.
// Tags: qr code, rotation, jpeg, aspose.barcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a QR code, rotates it by 45 degrees, and saves the result as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the output path, configures the barcode generator,
    /// applies rotation, saves the image, and writes the result location to the console.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output JPEG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_rotated_45.jpg");

        // Initialize the barcode generator for a QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello QR"))
        {
            // Set the rotation angle to 45 degrees.
            generator.Parameters.RotationAngle = 45f;

            // Save the rotated QR code as a JPEG image to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR code image has been saved.
        Console.WriteLine($"QR code saved to: {outputPath}");
    }
}