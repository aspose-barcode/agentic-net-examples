using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode Mode 3 barcode with a structured secondary message
/// using Aspose.BarCode's ComplexBarcodeGenerator.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and saves it as a JPEG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "MaxiCodeMode3.jpg");

        // Create MaxiCode codetext for Mode 3, populating required fields.
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050", // 6‑character alphanumeric postal code
            CountryCode = 56,     // Example country code
            ServiceCategory = 999 // Example service category
        };

        // Build the structured secondary message (address lines, state, and year).
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE"); // Street address
        structuredMessage.Add("PITTSBURGH");      // City
        structuredMessage.Add("PA");              // State abbreviation
        structuredMessage.Year = 99;              // Example year (two‑digit)

        // Attach the structured message to the MaxiCode codetext.
        maxiCodeCodetext.SecondMessage = structuredMessage;

        // Initialize the complex barcode generator with the prepared codetext.
        using (var complexGenerator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Generate the barcode image in memory (required before saving).
            complexGenerator.GenerateBarCodeImage();

            // Persist the generated image as a JPEG file.
            complexGenerator.Save(outputPath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode Mode 3 barcode saved to: {outputPath}");
    }
}