// Title: Generate MaxiCode Mode 3 Barcode and Save as PNG
// Description: Demonstrates creating a MaxiCode barcode in mode 3 using Aspose.BarCode, setting postal, country, and service data, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to work with the MaxiCode symbology via the ComplexBarcodeGenerator and related codetext classes. Developers often need to encode shipping information for logistics, requiring mode‑specific fields such as postal code, country code, and service category. The snippet shows typical usage of MaxiCodeCodetextMode3, MaxiCodeStandardSecondMessage, and saving the barcode image.
// Prompt: Generate a MaxiCode barcode using mode 3 and save the image as PNG file.
// Tags: maxicode, barcode, generation, png, aspose.barcode, complexbarcode, mode3

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode (mode 3) and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures its fields, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode3.png");

        // Create codetext for MaxiCode mode 3 and set mandatory fields.
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Create and assign an optional second message.
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Second message"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Initialize the complex barcode generator with the prepared codetext.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Save the generated barcode as a PNG image.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Output the location of the saved barcode image.
        Console.WriteLine($"MaxiCode barcode saved to {outputPath}");
    }
}