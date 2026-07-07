// Title: Generate MaxiCode Mode 2 barcode with secondary message
// Description: Demonstrates creating a MaxiCode Mode 2 barcode, adding an unstructured secondary message, and saving it as a PNG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related secondary message classes to produce high‑density 2‑D barcodes for shipping and logistics applications. Developers often need to encode postal information and custom messages for automated sorting systems.
// Prompt: Generate a MaxiCode Mode 2 barcode with an unstructured secondary message and save it as PNG.
// Tags: maxicode, mode2, secondary-message, png, generation, complexbarcodegenerator, maxicodecodetextmode2, maxicodesstandardsecondmessage

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode Mode 2 barcode with an unstructured secondary message and saving it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates the codetext, configures the generator, and saves the barcode image.
    /// </summary>
    static void Main()
    {
        // Initialize MaxiCode Mode 2 codetext object
        var codetext = new MaxiCodeCodetextMode2();

        // Set required postal information
        codetext.PostalCode = "524032140"; // 9‑digit US postal code
        codetext.CountryCode = 56;         // Example country code
        codetext.ServiceCategory = 999;    // Example service category

        // Create an unstructured (standard) secondary message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample secondary message"
        };
        codetext.SecondMessage = secondMessage; // Attach secondary message to codetext

        // Generate the barcode using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(codetext))
        {
            // Optional: set image resolution (dots per inch)
            generator.Parameters.Resolution = 300;

            // Save the generated barcode as a PNG file
            generator.Save("maxicode_mode2.png");
        }
    }
}