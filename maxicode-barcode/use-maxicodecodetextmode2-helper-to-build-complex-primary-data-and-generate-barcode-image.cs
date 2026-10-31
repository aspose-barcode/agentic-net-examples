// Title: Generate MaxiCode Mode 2 barcode with structured secondary message
// Description: Demonstrates how to use MaxiCodeCodetextMode2 to compose complex primary data and create a MaxiCode barcode image.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode2 and MaxiCodeStructuredSecondMessage to build detailed MaxiCode data. Developers creating shipping labels, parcel tracking, or logistics solutions often need to encode postal information and a multi‑line address in MaxiCode; this snippet illustrates the typical workflow and API calls.
// Prompt: Use the MaxiCodeCodetextMode2 helper to build complex primary data and generate the barcode image.
// Tags: maxicode, mode2, complex barcode, barcode generation, png, aspose.barcode, structured second message

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with a structured secondary message using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates output folder, builds MaxiCode codetext, generates the barcode, and saves it as PNG.
    /// </summary>
    static void Main()
    {
        // Determine the output directory relative to the current working directory
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not exist
            Directory.CreateDirectory(outputDir);
        }

        // Build MaxiCode codetext for Mode 2 with a structured second message
        MaxiCodeCodetextMode2 codetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // exactly 9 digits
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Populate the structured second message (address lines and year)
        MaxiCodeStructuredSecondMessage secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99;

        // Assign the second message to the codetext
        codetext.SecondMessage = secondMessage;

        // Define the full path for the output PNG file
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode2Structured.png");

        // Generate the barcode using ComplexBarcodeGenerator and save it as PNG
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}