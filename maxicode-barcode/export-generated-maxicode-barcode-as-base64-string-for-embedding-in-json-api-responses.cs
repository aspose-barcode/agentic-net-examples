// Title: Export MaxiCode barcode as Base64 string
// Description: Generates a MaxiCode barcode (Mode 2) and converts the PNG image to a Base64 string suitable for inclusion in JSON API responses.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, illustrating how to use ComplexBarcodeGenerator with MaxiCodeCodetextMode2, configure postal and secondary message data, and output the result in a web‑friendly format. Developers working with shipping, logistics, or inventory systems often need to embed barcode images directly in JSON payloads, and this snippet shows the typical workflow using Aspose.BarCode classes.
// Prompt: Export a generated MaxiCode barcode as a base64 string for embedding in JSON API responses.
// Tags: maxicode, barcode, base64, json, aspose.barcode, complexbarcode, generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and converting it to a Base64 string for JSON embedding.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a MaxiCode (Mode 2) barcode, saves it to a memory stream,
    /// converts the image to Base64, and writes the string to the console.
    /// </summary>
    static void Main()
    {
        // Prepare MaxiCode codetext for Mode 2 (postal information + data)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Country code (e.g., USA = 56)
            ServiceCategory = 999       // Example service category
        };

        // Add a simple second message
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample MaxiCode"
        };
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var ms = new MemoryStream())
            {
                // Save the barcode as PNG to the stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the Base64 string (can be embedded in JSON)
                Console.WriteLine(base64);
            }
        }
    }
}