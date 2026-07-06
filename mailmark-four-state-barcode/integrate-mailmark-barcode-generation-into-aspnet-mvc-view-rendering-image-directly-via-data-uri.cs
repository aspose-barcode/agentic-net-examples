// Title: Generate Mailmark Barcode and Output as Data URI
// Description: Demonstrates creating a Mailmark 4‑state barcode with Aspose.BarCode and converting it to a Base64 data URI for direct embedding in HTML.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of ComplexBarcodeGenerator and MailmarkCodetext to produce Mailmark barcodes, a common requirement for postal automation. Developers often need to render such barcodes in web applications, typically embedding the image via a data URI in MVC views or other HTML contexts.
// Prompt: Integrate Mailmark barcode generation into an ASP.NET MVC view, rendering the image directly via data URI.
// Tags: mailmark, barcode, generation, png, data uri, aspnet mvc, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates a Mailmark 4‑state barcode and prints a Base64 data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the console application. Generates the barcode, encodes it as a data URI,
    /// and writes the result to the console for use in an MVC view.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark 4‑state codetext with valid sample values.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                         // 4‑state format
            VersionID = 1,                      // version
            Class = "0",                         // service type / class
            SupplychainID = 384224,             // routing code
            ItemID = 16563762,                  // customer reference
            DestinationPostCodePlusDPS = "EF61AH8T " // postcode + DPS
        };

        // Generate the barcode using ComplexBarcodeGenerator.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Optional: set image resolution.
            generator.Parameters.Resolution = 300;

            // Save the barcode to a memory stream in PNG format.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);

                // Convert the image bytes to a Base64 data URI.
                string base64 = Convert.ToBase64String(ms.ToArray());
                string dataUri = $"data:image/png;base64,{base64}";

                // Output the data URI; it can be used directly in an <img> tag.
                Console.WriteLine(dataUri);
            }
        }
    }
}