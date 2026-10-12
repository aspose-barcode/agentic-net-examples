// Title: Generate MaxiCode barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode barcode (Mode 2) that includes a structured secondary message containing recipient name, street, and city, and saves the result as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to build and render barcodes with detailed secondary data. Developers working with shipping, logistics, or inventory systems often need to embed structured address information in MaxiCode barcodes for automated scanning and processing.
// Prompt: Generate a MaxiCode barcode with a structured secondary message containing recipient name, street, and city fields.
// Tags: maxicode, structured message, barcode generation, aspose.barcode, complexbarcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode barcode with a structured secondary message
/// and saves it to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare the output directory where the barcode image will be saved
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeStructured.png");

        // Create MaxiCode codetext for Mode 2 (postal code, country code, service category)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build the structured secondary message with recipient details
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("John Doe");        // recipient name
        structuredMessage.Add("123 Main St");    // street address
        structuredMessage.Add("Anytown");        // city
        structuredMessage.Year = 23;             // optional year field

        // Attach the secondary message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Generate the barcode using the complex barcode generator and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}