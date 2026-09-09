// Title: Generate Mailmark Barcode and Return Image Bytes
// Description: Demonstrates creating a Mailmark 4-state barcode, saving it to a MemoryStream, and obtaining the PNG image bytes for use in a web API response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator and related parameter classes to configure barcode appearance, a common requirement for developers integrating barcode images into web services, mobile apps, or document workflows. Typical use cases include generating printable barcodes on the fly, returning image data via APIs, and customizing barcode dimensions.
// Prompt: Generate a Mailmark barcode into a MemoryStream and return image bytes to a web API.
// Tags: mailmark, barcode, generation, png, memorystream, aspnet, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark barcode, writes it to a memory stream,
/// and outputs the resulting PNG image bytes. Suitable for integration into a web API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds a MailmarkCodetext, generates the barcode,
    /// and writes the image bytes to the console (simulating API response handling).
    /// </summary>
    static void Main()
    {
        // Define the Mailmark 4‑state codetext with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Create a memory stream to hold the generated PNG image.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the complex barcode generator with the Mailmark codetext.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module width) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the barcode image into the memory stream in PNG format.
                generator.Save(memoryStream, BarCodeImageFormat.Png);
            }

            // Retrieve the image bytes from the memory stream.
            byte[] imageBytes = memoryStream.ToArray();

            // Output the size of the generated image and its Base64 representation.
            Console.WriteLine($"Generated Mailmark barcode image bytes: {imageBytes.Length}");
            Console.WriteLine(Convert.ToBase64String(imageBytes));
        }
    }
}