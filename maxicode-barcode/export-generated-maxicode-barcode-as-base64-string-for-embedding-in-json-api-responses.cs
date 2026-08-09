// Title: Export MaxiCode barcode as Base64 PNG string
// Description: Generates a MaxiCode barcode (Mode 2) and converts the PNG image to a Base64 string for embedding in JSON responses.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It demonstrates using the ComplexBarcodeGenerator, MaxiCodeCodetextMode2, and related classes to create shipping‑label barcodes, then exporting the image in PNG format and encoding it as Base64 for API payloads. Developers working with logistics, inventory, or any system that needs to embed barcode images in JSON will find this pattern useful.
// Prompt: Export a generated MaxiCode barcode as a base64 string for embedding in JSON API responses.
// Tags: maxicode, barcode generation, base64, png, aspose.barcode, json, api response, complex barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates exporting a generated MaxiCode barcode as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a MaxiCode barcode, saves it to a memory stream, converts to Base64, and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare MaxiCode codetext (Mode 2) with a standard second message
        // ------------------------------------------------------------
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample message"
        };
        maxiCodeData.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // 2. Generate the barcode and export it as a Base64‑encoded PNG string
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Convert the PNG byte array to a Base64 string
                string base64 = Convert.ToBase64String(memoryStream.ToArray());

                // Output the Base64 string (can be embedded in JSON)
                Console.WriteLine(base64);
            }
        }
    }
}