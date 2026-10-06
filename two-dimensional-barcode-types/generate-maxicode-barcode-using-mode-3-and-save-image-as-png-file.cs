// Title: Generate MaxiCode Mode 3 Barcode and Save as PNG
// Description: Demonstrates creating a MaxiCode barcode in mode 3 using Aspose.BarCode, configuring postal information and a secondary message, and saving the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode3, MaxiCodeStandardSecondMessage, and related parameter settings. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols for package tracking; this snippet illustrates the typical workflow and key API classes for such scenarios.
// Prompt: Generate a MaxiCode barcode using mode 3 and save the image as PNG file.
// Tags: maxicode, barcode, generation, png, aspose.barcode, complexbarcode, mode3

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode barcode in mode 3 and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the barcode, configures colors, and writes the image to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode3.png");

        // Create MaxiCode codetext for mode 3 and set required fields
        MaxiCodeCodetextMode3 codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Create an unstructured second message and assign it to the codetext
        MaxiCodeStandardSecondMessage secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Second message"
        };
        codetext.SecondMessage = secondMessage;

        // Generate the barcode using ComplexBarcodeGenerator
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            // Optional: set the foreground (barcode) color
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}