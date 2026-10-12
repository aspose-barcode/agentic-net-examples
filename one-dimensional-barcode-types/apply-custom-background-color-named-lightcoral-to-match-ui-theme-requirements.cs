// Title: Generate QR Code with LightCoral Background
// Description: Demonstrates how to create a QR barcode image with a custom LightCoral background color using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and drawing parameters to customize barcode appearance. Developers often need to match UI themes by setting background and foreground colors before saving the barcode in common image formats like PNG.
// Prompt: Apply a custom background color named “LightCoral” to match UI theme requirements.
// Tags: qr, background-color, png, generation, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR barcode with a LightCoral background color.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a BarcodeGenerator for QR code with the desired text.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Sample Text"))
        {
            // Set the background color to LightCoral to match UI theme requirements.
            generator.Parameters.BackColor = Aspose.Drawing.Color.LightCoral;

            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode generated with LightCoral background at: {outputPath}");
    }
}