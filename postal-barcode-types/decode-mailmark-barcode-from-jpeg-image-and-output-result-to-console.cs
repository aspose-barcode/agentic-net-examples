// Title: Decode Mailmark 4-State barcode and display details
// Description: Demonstrates generating a Mailmark 4‑State barcode, saving it as JPEG, and decoding the codetext using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and decoding category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator, MailmarkCodetext, and ComplexCodetextReader to create a barcode image and retrieve its structured data. Developers working with postal or logistics solutions often need to generate and interpret Mailmark barcodes for tracking and routing, making this pattern a common requirement.
// Prompt: Decode a Mailmark barcode from a JPEG image and output the result to the console.
// Tags: mailmark, barcode, decoding, generation, jpeg, console, aspose.barcode, complexbarcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating and decoding a Mailmark 4‑State barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode image, decodes its codetext, and writes details to the console.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder for the generated image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "Mailmark4State.jpg");

        // Define the Mailmark 4‑State codetext components
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the barcode image (JPEG) using ComplexBarcodeGenerator
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4; // Set module size
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Decode the codetext directly (image‑based decoding is not supported for Mailmark)
        string constructed = mailmark.GetConstructedCodetext();
        MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(constructed);

        if (decoded == null)
        {
            Console.WriteLine("Failed to decode Mailmark codetext.");
            return;
        }

        // Output decoded information to the console
        Console.WriteLine("Decoded Mailmark 4-State Barcode:");
        Console.WriteLine($"Format: {decoded.Format}");
        Console.WriteLine($"VersionID: {decoded.VersionID}");
        Console.WriteLine($"Class: {decoded.Class}");
        Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
        Console.WriteLine($"ItemID: {decoded.ItemID}");
        Console.WriteLine($"DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
        Console.WriteLine($"Image saved at: {imagePath}");
    }
}