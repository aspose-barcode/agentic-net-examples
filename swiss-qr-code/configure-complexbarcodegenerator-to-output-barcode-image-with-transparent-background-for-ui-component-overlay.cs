// Title: Generate MaxiCode barcode with transparent background
// Description: Demonstrates how to create a MaxiCode barcode using Aspose.BarCode and save it as a PNG with a transparent background, suitable for overlaying on UI components.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and related parameter settings to customize appearance, including background transparency. Developers working with advanced symbologies and needing image assets for UI integration can refer to this pattern for generating transparent barcode images.
// Prompt: Configure ComplexBarcodeGenerator to output a barcode image with transparent background for UI component overlay.
// Tags: maxicode, complex barcode, transparent background, png, aspose.barcode, barcode generation, ui overlay

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a MaxiCode barcode with a transparent background using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, configures transparency, and saves the image.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the temporary directory
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_transparent.png");

        // Build the MaxiCode codetext for Mode 3 with a standard second message
        var maxicode = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Create and assign the second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Second message"
        };
        maxicode.SecondMessage = secondMessage;

        // Initialize the complex barcode generator with the prepared codetext
        using (var generator = new ComplexBarcodeGenerator(maxicode))
        {
            // Set the background color to transparent so the image can be overlaid
            generator.Parameters.BackColor = Color.Transparent;

            // Optionally set the bar (foreground) color; default is black
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG file
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}