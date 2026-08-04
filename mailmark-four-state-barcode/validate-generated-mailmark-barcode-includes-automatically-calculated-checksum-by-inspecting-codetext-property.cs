// Title: Validate Mailmark barcode checksum via Codetext property
// Description: Demonstrates generating a Mailmark barcode, automatically calculating its checksum, and verifying the checksum by comparing the decoded codetext with the expected constructed codetext.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode symbologies such as Mailmark. It showcases the use of ComplexBarcodeGenerator to create a Mailmark barcode, BarCodeReader for decoding, and the MailmarkCodetext class for constructing codetext with an automatically computed checksum. Developers working with postal and logistics applications often need to generate and validate Mailmark barcodes to ensure data integrity and compliance with postal standards.
// Prompt: Validate generated Mailmark barcode includes automatically calculated checksum by inspecting Codetext property.
// Tags: mailmark, barcode, checksum, generation, recognition, complexbarcode, aspnet, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark barcode, decodes it, and validates the automatically calculated checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Mailmark barcode, reads it back, and checks that the decoded CodeText matches the expected value including checksum.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with known valid data.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space required
        };

        // Generate the barcode image into a memory stream.
        using (var imageStream = new MemoryStream())
        {
            // Create a ComplexBarcodeGenerator for the Mailmark codetext.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as PNG into the stream.
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position for reading.
            imageStream.Position = 0;

            // Decode the barcode and obtain the codetext.
            using (var reader = new BarCodeReader(imageStream, DecodeType.Mailmark))
            {
                var results = reader.ReadBarCodes();

                // Ensure at least one barcode was detected.
                if (results.Length == 0)
                {
                    Console.WriteLine("No Mailmark barcode detected.");
                    return;
                }

                // Extract the decoded CodeText.
                var decoded = results[0].CodeText;

                // Get the expected codetext (includes automatically calculated checksum).
                var expected = mailmark.GetConstructedCodetext();

                // Validate that the decoded codetext matches the expected codetext.
                bool checksumValid = string.Equals(decoded, expected, StringComparison.Ordinal);

                // Output the results.
                Console.WriteLine($"Decoded CodeText : {decoded}");
                Console.WriteLine($"Expected CodeText: {expected}");
                Console.WriteLine($"Checksum validated: {checksumValid}");
            }
        }
    }
}