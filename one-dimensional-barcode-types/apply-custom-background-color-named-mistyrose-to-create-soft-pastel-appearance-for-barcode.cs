// Title: Apply MistyRose Background to a Code128 Barcode
// Description: Demonstrates setting a custom MistyRose background color for a Code128 barcode and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to customize barcode appearance using the BarcodeGenerator class. Typical use cases include branding, UI integration, and print-ready barcode creation where visual styling such as background colors is required. Developers often need to adjust colors, fonts, and image formats to match design guidelines.
// Prompt: Apply a custom background color named “MistyRose” to create a soft pastel appearance for the barcode.
// Tags: barcode symbology, background color, png output, aspose.barcode, generation

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode with a MistyRose background and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for Code128 symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Define the text to be encoded in the barcode
            generator.CodeText = "Sample123";

            // Set the background color to MistyRose for a soft pastel look
            generator.Parameters.BackColor = Color.MistyRose;

            // Save the generated barcode image as a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been generated
        Console.WriteLine("Barcode generated with MistyRose background.");
    }
}