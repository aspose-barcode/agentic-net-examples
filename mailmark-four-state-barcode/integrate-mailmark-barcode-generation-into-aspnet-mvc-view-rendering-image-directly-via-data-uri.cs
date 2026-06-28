using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a Mailmark barcode, conversion to a Base64 data URI,
/// and output to the console. Intended for use in ASP.NET MVC views where the
/// data URI can be embedded directly in an <img> tag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, encodes it as a
    /// Base64 data URI, and writes the URI to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: Full ASP.NET MVC integration (controller, view, routing) cannot be demonstrated
        // in this console snippet. The core logic below generates a Mailmark barcode,
        // converts it to a Base64 data URI, and writes the URI to the console.
        // In an MVC view you would embed the string directly in an <img src="..."/> tag.

        // Prepare Mailmark data (valid 4‑state example)
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state format identifier
            VersionID = 1,                  // Version of the Mailmark specification
            Class = "0",                    // Class as a string (required by API)
            SupplychainID = 384224,         // Supply chain identifier
            ItemID = 16563762,              // Item identifier
            DestinationPostCodePlusDPS = "EF61AH8T " // Destination postcode plus DPS; trailing space is significant
        };

        // Generate the barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Convert the PNG bytes to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Build a data URI that can be used directly in HTML <img> tags
                string dataUri = "data:image/png;base64," + base64;

                // Output the data URI (can be used directly in an <img> tag)
                Console.WriteLine(dataUri);
            }
        }
    }
}