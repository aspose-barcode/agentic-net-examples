// Title: Generate Mailmark Barcodes and Convert to Base64 Strings
// Description: Demonstrates creating Mailmark 4‑state barcodes for a list of order numbers and encoding each PNG image as a Base64 string for easy transport or storage.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category. It showcases the use of MailmarkCodetext and ComplexBarcodeGenerator to produce Mailmark barcodes, a common requirement in logistics and postal services. Developers often need to generate such barcodes programmatically and embed the resulting images in web pages, APIs, or databases, frequently converting them to Base64 for seamless integration.
// Prompt: Generate Mailmark barcodes for a set of order numbers and convert each image to a Base64 string.
// Tags: mailmark, barcode, generation, base64, png, aspose.barcode, complexbarcode

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates Mailmark barcodes for a collection of order numbers
/// and outputs each barcode image as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Iterates over sample order numbers,
    /// generates a Mailmark 4‑state barcode for each, and writes the Base64 string to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample set of order numbers to encode.
        List<int> orderNumbers = new List<int> { 1001, 1002, 1003, 1004, 1005 };

        // Process each order number individually.
        foreach (int orderNumber in orderNumbers)
        {
            // Build the Mailmark codetext with required fields.
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = 4,                     // Use 4‑state format.
                VersionID = 1,                  // Version identifier.
                Class = "0",                     // Class value.
                SupplychainID = 384224,          // Supply chain identifier.
                ItemID = orderNumber,            // Unique item identifier (order number).
                DestinationPostCodePlusDPS = "EF61AH8T " // Destination postcode with DPS.
            };

            // Generate the barcode image using the complex barcode generator.
            using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module size) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode to a memory stream in PNG format.
                using (MemoryStream ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);

                    // Convert the PNG byte array to a Base64 string.
                    string base64 = Convert.ToBase64String(ms.ToArray());

                    // Output the order number and its corresponding Base64 barcode.
                    Console.WriteLine($"Order {orderNumber}: {base64}");
                }
            }
        }
    }
}