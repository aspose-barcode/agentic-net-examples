// Title: Set XDimension and YDimension for a Code128 barcode
// Description: Demonstrates how to configure the module size (XDimension) and bar height (YDimension) of a Code128 barcode using Aspose.BarCode and save it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class together with EncodeTypes and the Parameters property to customize barcode appearance. Typical use cases include creating labels with precise dimensions for packaging, inventory, or shipping. Developers often need to adjust XDimension and YDimension to meet label size specifications, ensuring readability and scanner compatibility.
// Prompt: Set barcode XDimension to 0.4 mm and YDimension to 25 mm to meet specific label dimensions.
// Tags: code128, xdimension, ydimension, barcode, generation, png, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Code128 barcode with custom XDimension and YDimension.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode with specified dimensions and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the Code128 symbology
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Configure the module size (XDimension) to 0.4 millimeters
            generator.Parameters.Barcode.XDimension.Millimeters = 0.4f;

            // Configure the bar height (YDimension) to 25 millimeters
            generator.Parameters.Barcode.BarHeight.Millimeters = 25f;

            // Define the text to encode in the barcode
            generator.CodeText = "123456";

            // Save the generated barcode as a PNG image file
            generator.Save("barcode.png");
        }

        // Inform the user that the barcode has been created
        Console.WriteLine("Barcode generated and saved as 'barcode.png'.");
    }
}