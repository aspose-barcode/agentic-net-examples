// Title: Decode Mailmark barcode from JPEG image
// Description: Demonstrates generating a Mailmark barcode, saving it as a JPEG, and decoding its codetext using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the Mailmark symbology, utilizing key API classes such as MailmarkCodetext, ComplexBarcodeGenerator, and ComplexCodetextReader. Typical use cases include postal automation and logistics where Mailmark barcodes are encoded, printed, and later decoded to retrieve shipment details. Developers often need to generate barcode images, store them, and programmatically extract the embedded data.
// Prompt: Decode a Mailmark barcode from a JPEG image and output the result to the console.
// Tags: mailmark, barcode, decode, jpeg, console, aspose.barcode, complexbarcode, codetext

using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a Mailmark barcode, saves it as a JPEG, and decodes the codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode image, decodes its codetext, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a sample Mailmark codetext with all required fields.
        // ------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // Large Letter
            VersionID = 1,
            Class = "0",                    // Null or Test
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // 9 chars with trailing spaces
        };

        // ------------------------------------------------------------
        // Construct the codetext string that will be encoded into the barcode.
        // ------------------------------------------------------------
        string constructedCodetext = mailmark.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Generate a Mailmark barcode image and save it as a JPEG file.
        // ------------------------------------------------------------
        string imagePath = Path.Combine(Path.GetTempPath(), "mailmark.jpg");
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Save directly to JPEG format.
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Inform the user where the image was saved (optional).
        Console.WriteLine($"Mailmark barcode image saved to: {imagePath}");

        // ------------------------------------------------------------
        // Decode the codetext using ComplexCodetextReader.
        // Note: Image decoding is not supported; we decode the constructed string.
        // ------------------------------------------------------------
        MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(constructedCodetext);

        if (decoded == null)
        {
            Console.WriteLine("Failed to decode Mailmark codetext.");
            return;
        }

        // ------------------------------------------------------------
        // Output the decoded Mailmark fields to the console.
        // ------------------------------------------------------------
        Console.WriteLine("Decoded Mailmark data:");
        Console.WriteLine($"  Format: {decoded.Format}");
        Console.WriteLine($"  VersionID: {decoded.VersionID}");
        Console.WriteLine($"  Class: {decoded.Class}");
        Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
        Console.WriteLine($"  ItemID: {decoded.ItemID}");
        Console.WriteLine($"  DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
    }
}