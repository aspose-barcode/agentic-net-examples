// Title: Generate QR Code with Custom Green Foreground and Black Background
// Description: Demonstrates creating a QR Code barcode using Aspose.BarCode, applying a green bar color and black background, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes to produce QR Code symbology. It illustrates setting visual properties such as BarColor and BackColor, then saving the result in a common image format (PNG). Developers working on inventory, marketing, or authentication solutions often need to customize barcode appearance for branding or readability, making this pattern a frequent requirement.
// Prompt: Generate QR Code barcode and apply custom color palette with green foreground and black background.
// Tags: qr code, barcode generation, color customization, png, aspose.barcode, barcode generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR Code barcode with a custom color palette.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the QR Code and saves it to a file.
    /// </summary>
    static void Main()
    {
        // Output file path for the generated barcode image
        string outputPath = "qr_green.png";

        try
        {
            // Initialize the barcode generator for QR Code with the desired text
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello World"))
            {
                // Set the foreground (bars) color to green
                generator.Parameters.Barcode.BarColor = Color.Green;

                // Set the background color to black
                generator.Parameters.BackColor = Color.Black;

                // Save the generated barcode as a PNG image
                generator.Save(outputPath, BarCodeImageFormat.Png);
            }

            // Inform the user that the barcode was created successfully
            Console.WriteLine($"QR Code generated and saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            // Output any errors that occur during barcode generation
            Console.WriteLine($"Error generating QR Code: {ex.Message}");
        }
    }
}