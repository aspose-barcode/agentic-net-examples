// Title: Generate Mailmark barcode to MemoryStream
// Description: Demonstrates creating a Mailmark barcode, saving it as PNG into a MemoryStream, and returning the image bytes for use in a web API response.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation category, focusing on Mailmark symbology. It showcases the use of MailmarkCodetext to define barcode data and ComplexBarcodeGenerator to render the barcode. Developers working with logistics, mailing services, or any scenario requiring Mailmark barcodes often need to generate images in-memory for API delivery or further processing.
// Prompt: Generate a Mailmark barcode into a MemoryStream and return image bytes to a web API.
// Tags: barcode, mailmark, generation, memory stream, png, aspnet, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark barcode, writes it to a MemoryStream,
/// and outputs the image bytes as a Base64 string (simulating a web API response).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Builds the Mailmark codetext, generates the barcode,
    /// and writes the PNG image bytes to the console as Base64.
    /// </summary>
    static void Main()
    {
        // Initialize Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            // Set the barcode format to 4-state (standard Mailmark)
            Format = 4,
            // Version identifier for the barcode specification
            VersionID = 1,
            // Class identifier (e.g., "0" for standard class)
            Class = "0",
            // Supply chain identifier (numeric)
            SupplychainID = 384224,
            // Item identifier (numeric)
            ItemID = 16563762,
            // Destination postcode plus DPS (trailing space is significant)
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Create a ComplexBarcodeGenerator using the Mailmark codetext
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Prepare an in‑memory stream to hold the generated PNG image
            using (var memoryStream = new MemoryStream())
            {
                // Save the barcode image to the MemoryStream in PNG format
                generator.Save(memoryStream, BarCodeImageFormat.Png);

                // Retrieve the raw image bytes from the stream
                byte[] imageBytes = memoryStream.ToArray();

                // Output the image bytes as a Base64 string (simulating API response)
                Console.WriteLine(Convert.ToBase64String(imageBytes));
            }
        }
    }
}