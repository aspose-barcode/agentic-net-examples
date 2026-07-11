// Title: Adjust Postnet barcode bar height while preserving automatic width
// Description: Demonstrates setting the BarHeight of a Postnet barcode to 40 points, letting the library compute the optimal width automatically.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on barcode parameter customization. It showcases the use of BarcodeGenerator, EncodeTypes, and the BarHeight property to modify visual dimensions. Developers often need to adjust size attributes while relying on automatic layout calculations for consistent rendering across formats.
// Prompt: Adjust the BarHeight of a Postnet barcode to 40 points while keeping automatic width calculation.
// Tags: postnet, barcode, barheight, size, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Postnet barcode with a custom bar height while allowing automatic width calculation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a Postnet barcode, sets its bar height, and saves it as a PNG image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Postnet symbology with a sample ZIP code.
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet, "12345"))
        {
            // Set the bar height to 40 points; width remains automatically calculated.
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode image to a PNG file.
            generator.Save("postnet.png");
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("Postnet barcode generated: postnet.png");
    }
}