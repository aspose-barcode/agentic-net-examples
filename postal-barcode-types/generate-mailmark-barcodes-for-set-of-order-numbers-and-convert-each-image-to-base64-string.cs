// Title: Generate Mailmark barcodes and encode as Base64
// Description: Demonstrates creating Mailmark barcodes for a list of order numbers using Aspose.BarCode and converting each PNG image to a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as Mailmark. It shows how to configure MailmarkCodetext, use ComplexBarcodeGenerator to render the barcode, and obtain the image data in memory. Developers working with postal services, logistics, or any system that requires Mailmark encoding can use this pattern to produce barcodes and embed them in web pages, emails, or APIs.
// Prompt: Generate Mailmark barcodes for a set of order numbers and convert each image to a Base64 string.
// Tags: mailmark, barcode, generation, base64, png, aspose.barcode, complexbarcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates Mailmark barcodes for a collection of order numbers
/// and outputs each barcode image as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, converts them to Base64,
    /// and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample set of order numbers to encode.
        List<int> orderNumbers = new List<int> { 100001, 100002, 100003, 100004, 100005 };

        // Fixed Mailmark field values required for all barcodes.
        const int format = 4;                     // Mailmark 4‑state format
        const int versionId = 1;                  // Version identifier
        const string mailClass = "0";             // Mail class code
        const int supplyChainId = 384224;         // Supply chain identifier
        const string destinationPostCodePlusDps = "EF61AH8T "; // Destination postcode plus DPS (trailing space required)

        // Iterate over each order number and generate its corresponding barcode.
        foreach (int orderNumber in orderNumbers)
        {
            // Build the Mailmark codetext with both fixed and variable values.
            var mailmark = new MailmarkCodetext
            {
                Format = format,
                VersionID = versionId,
                Class = mailClass,
                SupplychainID = supplyChainId,
                ItemID = orderNumber,
                DestinationPostCodePlusDPS = destinationPostCodePlusDps
            };

            // Variable to hold the Base64 representation of the generated image.
            string base64;

            // Use ComplexBarcodeGenerator to create the barcode image in memory.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                using (var ms = new MemoryStream())
                {
                    // Save the barcode as a PNG image into the memory stream.
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the raw image bytes to a Base64 string.
                    base64 = Convert.ToBase64String(ms.ToArray());
                }
            }

            // Output the order number together with its Base64‑encoded barcode.
            Console.WriteLine($"Order {orderNumber}: {base64}");
        }
    }
}