// Title: Generate QR Code with automatic version selection and save as JPEG
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode with automatic version selection and exporting it to a JPEG image file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class with EncodeTypes.QR to produce QR Code symbols. It shows typical steps such as configuring barcode parameters, letting the library choose the optimal QR version automatically, and saving the result in a common image format. Developers working on QR Code creation for marketing, authentication, or data encoding scenarios can reference this pattern.
// Prompt: Generate a QR Code barcode with automatic version selection and export as JPEG.
// Tags: qr code, barcode generation, jpeg output, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode and saves it as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "qr_auto.jpg");

        // Create a BarcodeGenerator for QR Code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose QR"))
        {
            // Set the X-dimension (module size) in pixels; the QR version is chosen automatically.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            // Save the generated barcode as a JPEG image.
            generator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the QR Code image was saved.
        Console.WriteLine($"QR Code saved to: {outputPath}");
    }
}