using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of Mailmark barcodes for a list of order numbers
/// and outputs the Base64‑encoded PNG image for each.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode for each sample order number,
    /// encodes the PNG image to Base64, and writes it to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample collection of order numbers to process
        var orderNumbers = new List<int> { 1001, 1002, 1003, 1004, 1005 };

        // Iterate over each order number and generate its barcode
        foreach (int order in orderNumbers)
        {
            // Create Mailmark codetext with required fields
            var mailmark = new MailmarkCodetext
            {
                Format = 4,                     // 4‑state format
                VersionID = 1,
                Class = "0",                    // Test class identifier
                SupplychainID = 384224,
                ItemID = order,                 // Current order number
                DestinationPostCodePlusDPS = "EF61AH8T " // Valid postcode + DP
            };

            // Use a memory stream to hold the generated PNG image
            using (var ms = new MemoryStream())
            {
                // Generate the barcode and write it to the memory stream
                using (var generator = new ComplexBarcodeGenerator(mailmark))
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Convert the image bytes from the stream to a Base64 string
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Write the order number and its Base64 image to the console
                Console.WriteLine($"Order {order}: {base64}");
            }
        }
    }
}