// Title: Rotate QR Code 45 Degrees and Save as JPEG
// Description: Demonstrates how to apply a 45-degree rotation to a QR code using Aspose.BarCode and save the image as a JPEG file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance with rotation. It uses the BarcodeGenerator class together with EncodeTypes and BarCodeImageFormat to create, rotate, and export barcodes. Developers often need to adjust barcode orientation for branding, layout constraints, or visual design, and this snippet provides a clear pattern for such operations.
// Prompt: Apply a 45‑degree RotationAngle to a QR code and save the result as a JPEG image.
// Tags: qr code, rotation, jpeg, aspose.barcode, generation, image-output

using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeRotationExample
{
    /// <summary>
    /// Generates a QR code, applies a 45-degree rotation, and saves it as a JPEG image.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the example. Creates the output file path, configures the barcode generator,
        /// sets the rotation angle, saves the image, and writes the result location to the console.
        /// </summary>
        static void Main()
        {
            // Define the full path for the output JPEG file.
            string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_rotated.jpg");

            // Initialize the barcode generator with QR encoding and sample text.
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Code"))
            {
                // Apply a 45-degree rotation to the generated QR code.
                generator.Parameters.RotationAngle = 45f;

                // Save the rotated QR code as a JPEG image to the specified path.
                generator.Save(outputPath, BarCodeImageFormat.Jpeg);
            }

            // Inform the user where the QR code image has been saved.
            Console.WriteLine($"QR code saved to: {outputPath}");
        }
    }
}