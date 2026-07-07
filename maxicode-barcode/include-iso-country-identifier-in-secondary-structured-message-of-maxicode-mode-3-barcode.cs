// Title: Generate MaxiCode Mode 3 barcode with ISO country identifier in secondary message
// Description: Demonstrates how to create a MaxiCode Mode 3 barcode and embed an ISO country identifier in its structured secondary message.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on MaxiCode symbology. It showcases the use of MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to build and render a MaxiCode with custom postal, country, and service data—common tasks for shipping and logistics applications. Developers often need to encode address information and ISO country codes for automated parcel sorting systems.
// Prompt: Include an ISO country identifier in the secondary structured message of a MaxiCode Mode 3 barcode.
// Tags: maxicode, mode3, secondarymessage, iso country code, barcode generation, aspnet, aspose.barcode, png output

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Entry point for the example that generates a MaxiCode Mode 3 barcode with a structured secondary message containing an ISO country identifier.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode, saves it as PNG, and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Define the output file name for the generated barcode image
        const string outputFile = "maxicode_mode3.png";

        // Create MaxiCode codetext for Mode 3 with required fields
        var maxiCode = new MaxiCodeCodetextMode3
        {
            // 6‑character alphanumeric postal code (example)
            PostalCode = "B1050",
            // 3‑digit numeric ISO country code (example: 056 = Belgium)
            CountryCode = 056,
            // Service category (example)
            ServiceCategory = 999
        };

        // Build the structured secondary message
        var secondaryMessage = new MaxiCodeStructuredSecondMessage();

        // Include ISO country identifier as the first line (e.g., "US")
        secondaryMessage.Add("US");
        // Additional address lines
        secondaryMessage.Add("634 ALPHA DRIVE");
        secondaryMessage.Add("PITTSBURGH");
        secondaryMessage.Add("PA");
        // Set the year (last two digits)
        secondaryMessage.Year = 99;

        // Assign the structured second message to the codetext
        maxiCode.SecondMessage = secondaryMessage;

        // Generate the MaxiCode barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            // Generate the barcode image
            generator.GenerateBarCodeImage();

            // Save the image to a PNG file
            generator.Save(outputFile, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"MaxiCode Mode 3 barcode saved to: {Path.GetFullPath(outputFile)}");
    }
}