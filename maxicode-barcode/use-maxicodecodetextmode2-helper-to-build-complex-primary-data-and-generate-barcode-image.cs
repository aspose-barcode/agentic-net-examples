// Title: Generate MaxiCode Mode 2 Barcode with Structured Second Message
// Description: Demonstrates building complex primary data using MaxiCodeCodetextMode2, adding a structured second message, and saving the resulting barcode as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode ComplexBarcode generation category. It showcases the use of ComplexBarcodeGenerator together with MaxiCodeCodetextMode2 and MaxiCodeStructuredSecondMessage to create shipping‑label style MaxiCode barcodes. Developers working with logistics, parcel tracking, or any application that requires encoding detailed address information in MaxiCode will find these APIs essential for constructing primary and secondary message data.
// Prompt: Use the MaxiCodeCodetextMode2 helper to build complex primary data and generate the barcode image.
// Tags: maxicode, mode2, complexbarcode, barcode, image, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode with a structured second message.
/// </summary>
class Program
{
    /// <summary>
    /// Builds the MaxiCode codetext, generates the barcode image, and saves it to disk.
    /// </summary>
    static void Main()
    {
        // Build complex primary data for MaxiCode Mode 2
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140", // 9‑digit postal code
            CountryCode = 56,         // Numeric ISO country code
            ServiceCategory = 999     // Service category identifier
        };

        // Create a structured second message containing address lines and year
        var structuredSecondMessage = new MaxiCodeStructuredSecondMessage();
        structuredSecondMessage.Add("634 ALPHA DRIVE"); // Street address
        structuredSecondMessage.Add("PITTSBURGH");      // City
        structuredSecondMessage.Add("PA");              // State abbreviation
        structuredSecondMessage.Year = 99;              // Two‑digit year

        // Assign the second message to the codetext
        maxiCodeCodetext.SecondMessage = structuredSecondMessage;

        // Define the output file path for the generated PNG image
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode2StructuredSecondMessage.png");

        // Generate the barcode using ComplexBarcodeGenerator and save it to the file system
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            complexGenerator.Save(outputPath);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}