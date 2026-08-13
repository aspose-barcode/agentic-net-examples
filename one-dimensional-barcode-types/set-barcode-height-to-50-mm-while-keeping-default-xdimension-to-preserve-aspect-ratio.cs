// Title: Set barcode height to 50 mm while preserving default XDimension
// Description: Demonstrates how to generate a Code128 barcode image with a specific height of 50 mm, keeping the default XDimension to maintain the correct aspect ratio.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and barcode parameter settings. Typical use cases include creating barcodes with custom dimensions for printing or embedding in documents. Developers often need to adjust size properties while preserving default scaling factors to ensure readability and scanner compatibility.
// Prompt: Set barcode height to 50 mm while keeping default XDimension to preserve aspect ratio.
// Tags: code128, set-height, png, barcodegenerator, parameters

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Generates a Code128 barcode image with a custom height while preserving the default XDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a barcode, configures its height, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize the barcode generator for Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Define the data to encode in the barcode
            generator.CodeText = "123456";

            // Set the barcode height to 50 millimeters; XDimension remains at its default value
            generator.Parameters.Barcode.BarHeight.Millimeters = 50f;

            // Save the generated barcode image to a PNG file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as barcode.png");
    }
}