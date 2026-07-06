// Title: Validate Mailmark barcode checksum via Codetext inspection
// Description: Demonstrates generating a Mailmark barcode, automatically calculating its checksum, and verifying it by comparing the decoded Codetext with the expected value.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as Mailmark. It showcases the use of ComplexBarcodeGenerator for creating barcodes, BarCodeReader for decoding, and checksum validation settings. Developers often need to ensure data integrity for Mailmark barcodes in postal and logistics applications, making checksum verification a common requirement.
// Prompt: Validate generated Mailmark barcode includes automatically calculated checksum by inspecting Codetext property.
// Tags: mailmark, checksum, barcode generation, barcode recognition, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that generates a Mailmark barcode, automatically calculates its checksum,
/// and validates the checksum by decoding the barcode and comparing the Codetext.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, reads it back, and verifies the checksum.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext();
        mailmark.Format = 4; // 4‑state barcode
        mailmark.VersionID = 1;
        mailmark.Class = "0";
        mailmark.SupplychainID = 384224;
        mailmark.ItemID = 16563762;
        mailmark.DestinationPostCodePlusDPS = "EF61AH8T ";

        // Construct the full codetext (includes automatically calculated checksum)
        string expectedCodetext = mailmark.GetConstructedCodetext();

        // Generate the Mailmark barcode image into a memory stream
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            using (var ms = new MemoryStream())
            {
                // Save barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading

                // Recognize the barcode and enable checksum validation
                using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    // Force checksum validation during decoding
                    reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                    bool found = false;
                    // Iterate through all detected barcodes (should be only one)
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine("Decoded CodeText: " + result.CodeText);
                        if (result.CodeText == expectedCodetext)
                        {
                            Console.WriteLine("Checksum validation succeeded: decoded codetext matches expected.");
                        }
                        else
                        {
                            Console.WriteLine("Checksum validation failed: decoded codetext does not match expected.");
                        }
                        found = true;
                    }

                    // Inform the user if no barcode was detected
                    if (!found)
                    {
                        Console.WriteLine("No Mailmark barcode detected in the generated image.");
                    }
                }
            }
        }
    }
}