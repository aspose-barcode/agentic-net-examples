using System;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation of a MaxiCode Mode 3 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates a MaxiCode Mode 3 codetext,
    /// adds a structured secondary message, generates the barcode, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Create a MaxiCode Mode 3 codetext object and set its primary fields.
        var maxiCode = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",   // 6‑character alphanumeric postal code
            CountryCode = 840,      // Numeric ISO country code (e.g., 840 = USA)
            ServiceCategory = 999   // Service category identifier
        };

        // Build a structured secondary message consisting of address lines and country code.
        var secondaryMessage = new MaxiCodeStructuredSecondMessage();
        secondaryMessage.Add("634 ALPHA DRIVE"); // Street address
        secondaryMessage.Add("PITTSBURGH");      // City
        secondaryMessage.Add("PA");              // State abbreviation
        secondaryMessage.Add("US");              // ISO country identifier
        secondaryMessage.Year = 99;              // Two‑digit year

        // Attach the secondary message to the MaxiCode codetext.
        maxiCode.SecondMessage = secondaryMessage;

        // Generate the MaxiCode barcode and save it as a PNG image.
        using (var generator = new ComplexBarcodeGenerator(maxiCode))
        {
            generator.Save("maxicode_mode3.png"); // Save the barcode image
        }

        // Inform the user that the barcode has been generated.
        Console.WriteLine("MaxiCode Mode 3 barcode generated: maxicode_mode3.png");
    }
}