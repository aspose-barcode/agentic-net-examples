// Title: Generate MaxiCode Mode 3 barcode with ISO country identifier in secondary structured message
// Description: Demonstrates how to create a MaxiCode Mode 3 barcode and embed an ISO numeric country identifier within the secondary structured message.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It shows usage of MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator to build a barcode with postal, country, and service data, plus a custom secondary message. Developers working with shipping labels, logistics, or retail can use these APIs to encode detailed address information and ISO country codes.
// Prompt: Include an ISO country identifier in the secondary structured message of a MaxiCode Mode 3 barcode.
// Tags: maxicode, mode3, secondarystructuredmessage, iso country code, barcode generation, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 3 barcode with a secondary structured message that includes an ISO country identifier.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, saves it to a temporary folder, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output directory
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeMode3StructuredSecondMessage.png");

        // Initialize MaxiCode codetext for Mode 3 and set required fields
        MaxiCodeCodetextMode3 maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",
            CountryCode = 56, // ISO numeric country identifier
            ServiceCategory = 999
        };

        // Build the secondary structured message with address lines and ISO country info
        MaxiCodeStructuredSecondMessage structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Add("ISO Country: 56"); // Include ISO country identifier in the message
        structuredMessage.Year = 99;

        // Assign the structured second message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Generate the barcode and save it as a PNG file
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            generator.Save(outputPath);
        }

        // Output the location of the generated barcode
        Console.WriteLine("MaxiCode Mode 3 barcode saved to:");
        Console.WriteLine(outputPath);
    }
}