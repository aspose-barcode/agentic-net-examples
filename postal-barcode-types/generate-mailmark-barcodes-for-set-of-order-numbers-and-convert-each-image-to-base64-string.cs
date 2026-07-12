// Title: Generate Mailmark barcodes and convert to Base64 strings
// Description: Demonstrates creating Mailmark barcodes for a list of order numbers and encoding each barcode image as a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and image format handling to produce PNG images, a common requirement for embedding barcodes in web services or APIs. Developers often need to generate barcodes programmatically and transmit them as Base64 for JSON payloads or HTML img tags.
// Prompt: Generate Mailmark barcodes for a set of order numbers and convert each image to a Base64 string.
// Tags: mailmark, barcode, generation, base64, png, aspnet, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating Mailmark barcodes for a collection of order numbers
/// and converting each barcode image to a Base64 string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates through sample order numbers,
    /// creates a Mailmark barcode for each, and writes the Base64 representation to the console.
    /// </summary>
    static void Main(string[] args)
    {
        // Sample order numbers – in a real scenario these could come from a database or input source.
        int[] orderNumbers = new int[] { 1001, 1002, 1003, 1004, 1005 };

        foreach (int order in orderNumbers)
        {
            // Prepare Mailmark codetext with required fields.
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                         // 4‑state barcode format.
                VersionID = 1,                      // Version identifier.
                Class = "0",                        // Service type / class.
                SupplychainID = 384224,             // Example supply‑chain identifier.
                ItemID = order,                     // Use the order number as the item identifier.
                DestinationPostCodePlusDPS = "EF61AH8T " // Known‑valid postcode‑plus‑DPS.
            };

            // Generate the Mailmark barcode and obtain its image as a Base64 string.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                using (var ms = new MemoryStream())
                {
                    // Save the barcode image to the memory stream in PNG format.
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the image bytes to a Base64 string.
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    // Output the result.
                    Console.WriteLine($"Order: {order}, Base64: {base64}");
                }
            }
        }
    }
}