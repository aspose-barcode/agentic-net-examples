// Title: Generate a Postnet barcode with custom bar height
// Description: Demonstrates how to set the BarHeight of a Postnet barcode to 40 points while allowing the library to calculate the optimal width automatically.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on symbology-specific parameter adjustments. It showcases the use of BarcodeGenerator, EncodeTypes, and the Parameters.Barcode properties to customize visual aspects of a barcode. Developers often need to modify dimensions such as bar height or module size for specific printing or scanning requirements, and this snippet illustrates the typical approach for Postnet barcodes.
// Prompt: Adjust the BarHeight of a Postnet barcode to 40 points while keeping automatic width calculation.
// Tags: postnet, barcode, barheight, dimension, aspnet, aspose.barcode, generation, png

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Postnet barcode with a custom bar height.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Postnet symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Postnet))
        {
            // Set the postal code to be encoded
            generator.CodeText = "12345";

            // Set the bar height to 40 points; width will be calculated automatically
            generator.Parameters.Barcode.BarHeight.Point = 40f;

            // Save the generated barcode image as PNG
            generator.Save("postnet.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Postnet barcode generated: postnet.png");
    }
}