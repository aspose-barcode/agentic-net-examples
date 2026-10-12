// Title: Generate Mailmark 4‑State barcodes and encode them as Base64 strings
// Description: Demonstrates creating Mailmark 4‑State barcodes for a list of order numbers using Aspose.BarCode and converting each PNG image to a Base64 string for easy transport or embedding.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode symbologies such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and barcode parameter customization. Developers often need to generate Mailmark barcodes for postal services and then serialize the images for web APIs or storage, making this pattern a common requirement.
// Prompt: Generate Mailmark barcodes for a set of order numbers and convert each image to a Base64 string.
// Tags: mailmark, barcode, generation, base64, png, aspose.barcode, complexbarcodegenerator

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that creates Mailmark 4‑State barcodes for a collection of order numbers
/// and outputs each barcode image as a Base64‑encoded PNG string.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode for each order number, saves it to a memory stream,
    /// converts the image to Base64, and writes the result to the console.
    /// </summary>
    static void Main()
    {
        // Define a sample list of order numbers to encode.
        List<int> orderNumbers = new List<int> { 1001, 1002, 1003 };

        // Iterate over each order number and generate its corresponding barcode.
        foreach (int orderNumber in orderNumbers)
        {
            // Build the Mailmark codetext with required fields.
            MailmarkCodetext mailmark = new MailmarkCodetext
            {
                Format = 4,
                VersionID = 1,
                Class = "0",
                SupplychainID = 384224,
                ItemID = orderNumber,
                DestinationPostCodePlusDPS = "EF61AH8T "
            };

            // Use a memory stream to hold the generated PNG image.
            using (MemoryStream ms = new MemoryStream())
            {
                // Create the complex barcode generator with the Mailmark codetext.
                using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
                {
                    // Set the X‑dimension (module width) to 4 pixels for better readability.
                    generator.Parameters.Barcode.XDimension.Pixels = 4;

                    // Save the barcode image to the memory stream in PNG format.
                    generator.Save(ms, BarCodeImageFormat.Png);
                }

                // Convert the image bytes from the memory stream to a Base64 string.
                string base64 = Convert.ToBase64String(ms.ToArray());

                // Output the order number and its corresponding Base64‑encoded barcode.
                Console.WriteLine($"Order {orderNumber}: {base64}");
            }
        }
    }
}