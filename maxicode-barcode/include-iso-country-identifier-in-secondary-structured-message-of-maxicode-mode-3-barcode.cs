// Title: Generate a MaxiCode Mode 3 barcode with ISO country identifier in secondary message
// Description: Demonstrates how to create a MaxiCode Mode 3 barcode using Aspose.BarCode, setting postal code, numeric ISO country code, service category, and adding a two‑letter ISO country identifier to the structured secondary message.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology and complex barcode creation. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode3, and MaxiCodeStructuredSecondMessage to build barcodes with detailed address information, a common requirement for shipping and logistics applications. Developers often need to embed structured messages and ISO identifiers for automated scanning systems.
// Prompt: Include an ISO country identifier in the secondary structured message of a MaxiCode Mode 3 barcode.
// Tags: maxicode, barcode, generation, secondary message, iso country, aspose.barcode, complexbarcode

using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a MaxiCode Mode 3 barcode with a structured secondary message
/// containing a two‑letter ISO country identifier.
/// </summary>
public static class Program
{
    /// <summary>
    /// Entry point of the example. Creates the barcode data, builds the secondary message,
    /// generates the image, and saves it to disk.
    /// </summary>
    public static void Main()
    {
        // Output file path for the generated barcode image
        string outputPath = "maxicode_mode3.png";

        // --------------------------------------------------------------------
        // Create MaxiCode Mode 3 codetext with required fields
        // --------------------------------------------------------------------
        var maxiCodeData = new MaxiCodeCodetextMode3
        {
            PostalCode = "B1050",   // 6‑character alphanumeric postal code
            CountryCode = 56,       // Numeric ISO country code (e.g., 56 = Belgium)
            ServiceCategory = 999   // Example service category
        };

        // --------------------------------------------------------------------
        // Build the structured secondary message (address lines, state, country)
        // --------------------------------------------------------------------
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE"); // Street address
        structuredMessage.Add("PITTSBURGH");      // City
        structuredMessage.Add("PA");              // State / province
        structuredMessage.Add("US");              // ISO country identifier (2‑letter code)
        structuredMessage.Year = 99;              // Two‑digit year

        // Assign the secondary message to the MaxiCode data object
        maxiCodeData.SecondMessage = structuredMessage;

        // --------------------------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator and save it
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            using (Bitmap image = generator.GenerateBarCodeImage())
            {
                image.Save(outputPath);
            }
        }
    }
}