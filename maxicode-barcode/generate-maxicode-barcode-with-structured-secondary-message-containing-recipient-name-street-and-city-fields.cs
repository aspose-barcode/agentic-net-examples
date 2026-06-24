using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode with a structured secondary message using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a MaxiCode barcode and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image.
        string outputPath = "maxicode.png";

        // Create a structured secondary message containing recipient details.
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("John Doe");          // Recipient name
        structuredMessage.Add("123 Main Street");  // Street address
        structuredMessage.Add("Anytown");           // City
        structuredMessage.Year = 23;                // Optional year field (e.g., 2023)

        // Configure the MaxiCode codetext for Mode 3 (suitable for worldwide postal codes).
        var maxiCodeCodetext = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",   // Example alphanumeric postal code
            CountryCode = 056,      // Example 3‑digit country code
            ServiceCategory = 999,  // Example service category
            SecondMessage = structuredMessage // Attach the structured secondary message
        };

        // Generate the MaxiCode barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Set the image resolution (dots per inch) – optional but improves quality.
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image as a PNG file.
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"MaxiCode barcode saved to: {outputPath}");
    }
}