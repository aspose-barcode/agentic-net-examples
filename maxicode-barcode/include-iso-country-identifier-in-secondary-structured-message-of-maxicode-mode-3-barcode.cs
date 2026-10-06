// Title: Generate MaxiCode Mode 3 barcode with ISO country identifier in secondary structured message
// Description: Demonstrates how to create a MaxiCode Mode 3 barcode, set its postal code, numeric country code, service category, and include an ISO country identifier in the secondary structured message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and MaxiCodeStructuredSecondMessage classes to build a barcode with detailed address information. Developers often need to embed structured data like postal addresses and ISO country codes in MaxiCode for shipping and logistics applications.
// Prompt: Include an ISO country identifier in the secondary structured message of a MaxiCode Mode 3 barcode.
// Tags: maxicode, mode3, structuredmessage, iso country identifier, barcode generation, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a MaxiCode Mode 3 barcode with a structured secondary message
/// containing an ISO country identifier.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates the output directory, builds the MaxiCode data,
    /// generates the barcode, and saves it to a PNG file.
    /// </summary>
    static void Main()
    {
        // Prepare the output folder and file path
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3StructuredSecondMessage.png");

        // Create MaxiCode Mode 3 codetext and set basic fields
        MaxiCodeCodetextMode3 codetext = new MaxiCodeCodetextMode3();
        codetext.PostalCode = "B1050";
        codetext.CountryCode = 840; // ISO numeric code for United States
        codetext.ServiceCategory = 999;

        // Build the structured secondary message and include the ISO country identifier
        MaxiCodeStructuredSecondMessage structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Add("US"); // ISO country identifier
        structuredMessage.Year = 99;

        // Assign the structured message to the codetext
        codetext.SecondMessage = structuredMessage;

        // Generate the barcode and save it to the specified file
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}