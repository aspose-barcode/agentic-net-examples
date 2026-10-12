// Title: Decode Mailmark barcode from JPEG image
// Description: Demonstrates generating a Mailmark barcode, saving it as a JPEG file, then decoding the barcode from the image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create a Mailmark barcode, BarCodeReader to read the barcode from an image, and ComplexCodetextReader to parse the Mailmark codetext. Developers working with postal or logistics solutions often need to generate and validate Mailmark symbols, making this pattern a common requirement.
// Prompt: Decode a Mailmark barcode from a JPEG image and output the result to the console.
// Tags: mailmark, barcode, decode, jpeg, console, aspose.barcode, complexbarcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Mailmark barcode, saving it as a JPEG image,
/// and decoding it using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode image, attempts to decode it,
    /// and prints the decoded fields to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the generated image
        string tempDir = Path.Combine(Path.GetTempPath(), "MailmarkDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "mailmark.jpg");

        // Prepare Mailmark 4‑state codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode image and save it as JPEG
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Attempt to read and decode the Mailmark barcode from the generated image
        bool decodedFromImage = false;
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (decoded != null)
                {
                    PrintMailmark(decoded);
                    decodedFromImage = true;
                }
            }
        }

        // If no barcode was decoded from the image, fall back to decoding the constructed codetext string
        if (!decodedFromImage)
        {
            string constructed = mailmark.GetConstructedCodetext();
            var decoded = ComplexCodetextReader.TryDecodeMailmark(constructed);
            if (decoded != null)
            {
                Console.WriteLine("Decoded from constructed codetext (image read yielded no results):");
                PrintMailmark(decoded);
            }
            else
            {
                Console.WriteLine("Unable to decode Mailmark barcode.");
            }
        }
    }

    /// <summary>
    /// Prints the individual fields of a decoded Mailmark codetext to the console.
    /// </summary>
    /// <param name="m">The decoded MailmarkCodetext instance.</param>
    static void PrintMailmark(MailmarkCodetext m)
    {
        Console.WriteLine($"Format: {m.Format}");
        Console.WriteLine($"VersionID: {m.VersionID}");
        Console.WriteLine($"Class: {m.Class}");
        Console.WriteLine($"SupplychainID: {m.SupplychainID}");
        Console.WriteLine($"ItemID: {m.ItemID}");
        Console.WriteLine($"DestinationPostCodePlusDPS: {m.DestinationPostCodePlusDPS}");
    }
}