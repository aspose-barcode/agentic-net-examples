// Title: Generate MaxiCode barcode with structured secondary message
// Description: Demonstrates creating a MaxiCode barcode (Mode 3) that includes a structured secondary message containing recipient name, street, and city, then saves the image as a PNG file.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MaxiCodeCodetextMode3, MaxiCodeStructuredSecondMessage, and ComplexBarcodeGenerator classes to produce MaxiCode symbols with detailed secondary data. Typical use cases include shipping labels and logistics where structured address information must be encoded. Developers often need to combine primary data fields with a secondary message for compliance with industry standards.
// Prompt: Generate a MaxiCode barcode with a structured secondary message containing recipient name, street, and city fields.
// Tags: maxicode, structured message, barcode generation, aspose.barcode, complexbarcode, png output, shipping label

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a MaxiCode barcode with a structured secondary message
/// and saves it to a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare the output directory where the barcode image will be saved
        // ------------------------------------------------------------
        string outputDir = Path.Combine(Environment.CurrentDirectory, "Output");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "MaxiCodeStructured.png");

        // ------------------------------------------------------------
        // Create MaxiCode codetext for Mode 3 (supports structured secondary message)
        // ------------------------------------------------------------
        MaxiCodeCodetextMode3 codetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",      // example postal code
            CountryCode = 56,          // example country code
            ServiceCategory = 999      // example service category
        };

        // ------------------------------------------------------------
        // Build the structured secondary message (recipient name, street, city)
        // ------------------------------------------------------------
        MaxiCodeStructuredSecondMessage secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("John Doe");          // recipient name
        secondMessage.Add("123 Main Street");  // street
        secondMessage.Add("Anytown");          // city
        secondMessage.Year = 23;                // optional year (e.g., 2023)

        // Attach the secondary message to the MaxiCode codetext
        codetext.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // Generate the MaxiCode barcode and save it as a PNG image
        // ------------------------------------------------------------
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(codetext))
        {
            generator.Save(outputPath);
        }

        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}