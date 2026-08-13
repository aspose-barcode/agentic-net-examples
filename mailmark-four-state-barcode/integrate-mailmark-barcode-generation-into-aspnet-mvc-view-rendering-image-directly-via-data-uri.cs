// Title: Generate Mailmark Barcode and Output as Data URI
// Description: Demonstrates creating a Mailmark barcode using Aspose.BarCode, encoding the PNG image as a Base64 data URI for direct embedding in an ASP.NET MVC view.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It shows how to use ComplexBarcodeGenerator and MailmarkCodetext classes to produce a barcode image, then convert it to a data URI for web rendering. Developers working with ASP.NET MVC or other web frameworks can embed the generated URI directly in HTML <img> tags, avoiding file I/O.
// Prompt: Integrate Mailmark barcode generation into an ASP.NET MVC view, rendering the image directly via data URI.
// Tags: mailmark, barcode, generation, data uri, aspnet mvc, png, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark barcode and prints a Base64 data URI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, converts it to a data URI, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: In a real ASP.NET MVC application the generated data URI would be embedded in a view.
        // The snippet runner is a console application, so we output the data URI to the console.

        // Create a MailmarkCodetext instance with valid sample data.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
        };

        // Generate the barcode image into a memory stream.
        using (var ms = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to render the Mailmark barcode.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Convert the image bytes to a Base64 data URI.
            string base64 = Convert.ToBase64String(ms.ToArray());
            string dataUri = "data:image/png;base64," + base64;

            // Output the data URI (can be used directly in an <img src="..."> tag).
            Console.WriteLine(dataUri);
        }
    }
}