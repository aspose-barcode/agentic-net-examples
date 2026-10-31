// Title: Generate Mailmark barcodes and output Base64 strings
// Description: Demonstrates creating Mailmark 4‑state barcodes for a list of order numbers and converting each PNG image to a Base64 string.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It shows how to use MailmarkCodetext and ComplexBarcodeGenerator to produce Mailmark symbols, adjust rendering parameters, and export the result as an image. Developers working with postal or logistics solutions often need to generate Mailmark barcodes programmatically and embed them in data streams or web services, making Base64 encoding a common step.
// Prompt: Generate Mailmark barcodes for a set of order numbers and convert each image to a Base64 string.
// Tags: mailmark, barcode, generation, base64, png, aspose.barcode, complexbarcode, csharp

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates Mailmark barcodes for a collection of order numbers
/// and writes each barcode image as a Base64‑encoded PNG string to the console.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Iterates through sample order numbers, builds a MailmarkCodetext,
    /// generates a PNG barcode, encodes it to Base64 and prints the result.
    /// </summary>
    static void Main()
    {
        // Sample order numbers to encode
        List<int> orderNumbers = new List<int> { 1001, 1002, 1003, 1004, 1005 };

        // Process each order number individually
        foreach (int order in orderNumbers)
        {
            // Build the Mailmark 4‑state codetext with required fields
            MailmarkCodetext mailmark = new MailmarkCodetext();
            mailmark.Format = 4;
            mailmark.VersionID = 1;
            mailmark.Class = "0";
            mailmark.SupplychainID = 384224;
            mailmark.ItemID = order;
            mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";

            // Create a generator for the complex Mailmark barcode
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module size) to 4 pixels for better readability
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the barcode to a memory stream in PNG format
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the PNG byte array to a Base64 string
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    // Output the order identifier and its Base64 representation
                    Console.WriteLine($"Order {order}: {base64}");
                }
            }
        }
    }
}