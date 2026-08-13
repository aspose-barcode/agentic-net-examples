// Title: Reset barcode background color from gray to default white
// Description: Demonstrates how to change a barcode's background color to gray and then revert it back to white using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator and its Parameters property to customize visual aspects such as background color. Developers often need to adjust barcode appearance for branding or printing requirements, and this snippet shows the typical workflow for setting and resetting colors before saving images.
// Prompt: Reset the barcode background to default white after previously setting it to gray.
// Tags: barcode generation, background color, reset, code128, png, aspose.barcode, aspose.drawing

using System;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code128 barcode, first with a gray background,
/// then resets the background to the default white and saves both images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates two barcode images demonstrating background color reset.
    /// </summary>
    static void Main()
    {
        // Define output file names for the two barcode images.
        const string grayPath = "barcode_gray.png";
        const string whitePath = "barcode_white.png";

        // Initialize a BarcodeGenerator for Code128 with sample text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            // Set the background color to gray.
            generator.Parameters.BackColor = Color.Gray;

            // Save the barcode image with a gray background.
            generator.Save(grayPath);

            // Reset the background color to the default white.
            generator.Parameters.BackColor = Color.White;

            // Save the barcode image with a white background.
            generator.Save(whitePath);
        }

        // Inform the user about the generated files.
        Console.WriteLine("Barcodes generated: gray -> {0}, white -> {1}", grayPath, whitePath);
    }
}