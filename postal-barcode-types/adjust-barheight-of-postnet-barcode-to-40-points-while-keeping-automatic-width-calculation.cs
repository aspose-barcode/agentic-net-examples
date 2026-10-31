// Title: Adjust Postnet Barcode BarHeight to 40 Points
// Description: Demonstrates how to set the bar height of a Postnet barcode to 40 points while allowing the library to calculate the optimal width automatically.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing the use of BarcodeGenerator with EncodeTypes.Postnet. It illustrates common tasks such as customizing barcode dimensions, saving to image formats, and handling temporary file paths—typical operations developers perform when integrating postal barcode creation into .NET applications.
// Prompt: Adjust the BarHeight of a Postnet barcode to 40 points while keeping automatic width calculation.
// Tags: postnet, barcode, barheight, aspose.barcode, image generation, png, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an entry point that creates a Postnet barcode, sets its bar height,
/// and saves the result as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Postnet barcode with a 40‑point bar height and writes the image to a temporary file.
    /// </summary>
    static void Main()
    {
        // Determine a temporary file path for the generated PNG image.
        string outputPath = Path.Combine(Path.GetTempPath(), "PostnetBarcode.png");

        // The numeric data to encode in the Postnet barcode.
        string codeText = "123456";

        // Create a barcode generator for the Postnet symbology with the specified text.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, codeText))
        {
            // Set the bar height to 40 points; the width is calculated automatically by the library.
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode as a PNG file at the previously defined location.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Postnet barcode saved to: {outputPath}");
    }
}