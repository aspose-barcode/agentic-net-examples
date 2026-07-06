// Title: Parse Mailmark CodeText with ComplexCodetextReader
// Description: Demonstrates generating a Mailmark barcode, reading its raw CodeText, and decoding it into individual fields using ComplexCodetextReader.TryDecodeMailmark.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode handling category, focusing on Mailmark symbology. It shows how to use ComplexBarcodeGenerator to create a Mailmark barcode, BarCodeReader to scan it, and ComplexCodetextReader to parse the raw CodeText into structured MailmarkCodetext fields. Developers working with postal or logistics applications often need to generate and decode Mailmark barcodes to exchange supply‑chain information.
// Prompt: Parse raw CodeText from BarCodeReader using ComplexCodetextReader.TryDecodeMailmark to retrieve fields.
// Tags: mailmark, barcode, decoding, complexcodetextreader, aspnet, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Mailmark barcode, reads it back, and decodes the raw CodeText
/// into its constituent fields using <see cref="ComplexCodetextReader.TryDecodeMailmark"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode image, reads the barcode,
    /// decodes the raw CodeText, and outputs the extracted fields to the console.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "mailmark.png");

        // Create and populate a MailmarkCodetext instance with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4, // unspecified/default format
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode image and save it to the temporary file
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the barcode from the saved image using the Mailmark decode type
        using (var reader = new BarCodeReader(imagePath, DecodeType.Mailmark))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the raw CodeText obtained from the barcode
                Console.WriteLine($"Raw CodeText: {result.CodeText}");

                // Decode the raw CodeText into a structured MailmarkCodetext object
                MailmarkCodetext decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Unable to decode Mailmark codetext.");
                    continue;
                }

                // Output each decoded field to the console
                Console.WriteLine("Decoded Mailmark fields:");
                Console.WriteLine($"  Format: {decoded.Format}");
                Console.WriteLine($"  VersionID: {decoded.VersionID}");
                Console.WriteLine($"  Class: {decoded.Class}");
                Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                Console.WriteLine($"  ItemID: {decoded.ItemID}");
                Console.WriteLine($"  DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
            }
        }

        // Clean up the temporary image file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}