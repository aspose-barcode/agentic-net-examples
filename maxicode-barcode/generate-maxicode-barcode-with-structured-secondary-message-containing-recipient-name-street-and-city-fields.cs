// Title: Generate MaxiCode barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode barcode (Mode 2) that includes a structured secondary message containing recipient name, street, and city, then saves the image as PNG.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode2, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to produce MaxiCode symbols. Typical use cases include shipping labels and logistics where MaxiCode encodes address information. Developers often need to construct secondary messages and configure postal, country, and service fields before rendering the barcode.
// Prompt: Generate a MaxiCode barcode with a structured secondary message containing recipient name, street, and city fields.
// Tags: maxicode, barcode, complexbarcode, secondarymessage, png, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a MaxiCode barcode with a structured secondary message.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode and saves it to a temporary folder.
    /// </summary>
    static void Main()
    {
        // Prepare output directory in the system temporary folder
        string outputDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "maxicode.png");

        // Create MaxiCode codetext for Mode 2 and set primary fields
        MaxiCodeCodetextMode2 maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Build structured secondary message: recipient name, street, city, and year
        MaxiCodeStructuredSecondMessage secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("John Doe");      // Recipient name
        secondMessage.Add("123 Main St");   // Street address
        secondMessage.Add("Anytown");       // City
        secondMessage.Year = 23;            // Two‑digit year

        // Attach the secondary message to the MaxiCode codetext
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the barcode and save it as a PNG file
        try
        {
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
            {
                generator.Save(outputPath);
            }

            Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
        }
        catch (Exception ex)
        {
            Console.WriteLine($"Error generating MaxiCode: {ex.Message}");
        }
    }
}