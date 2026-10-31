// Title: Read Mailmark barcode from JPEG stream using BarCodeReader
// Description: Demonstrates generating a Mailmark 4‑state barcode, saving it to a JPEG memory stream, and reading it back with BarCodeReader configured for DecodeType.Mailmark.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create Mailmark barcodes, BarCodeReader with DecodeType.Mailmark to detect them, and ComplexCodetextReader to parse the decoded data. Typical scenarios include postal automation, mail sorting, and logistics where Mailmark symbols are used. Developers often need to generate, embed, and later read Mailmark codes programmatically, making these APIs essential for end‑to‑end barcode workflows.
// Prompt: Read a Mailmark barcode from a JPEG stream using BarCodeReader with DecodeType.Mailmark.
// Tags: mailmark, barcode, read, jpeg, memorystream, aspose.barcode, complexbarcode, decode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Mailmark barcode, saves it to a JPEG stream,
/// and reads it back using Aspose.BarCode's recognition API.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark 4‑state barcode,
    /// writes it to a JPEG memory stream, and then reads and decodes it.
    /// </summary>
    static void Main(string[] args)
    {
        // Create Mailmark 4‑state codetext with required fields
        var mailmark = new Aspose.BarCode.ComplexBarcode.MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate the Mailmark barcode and write it to a JPEG memory stream
        using (var generator = new Aspose.BarCode.ComplexBarcode.ComplexBarcodeGenerator(mailmark))
        {
            // Adjust the X‑dimension (module size) for better image quality
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as JPEG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Jpeg);
                ms.Position = 0; // Reset stream position for reading

                // Read the barcode from the JPEG stream using Mailmark decode type
                using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
                {
                    var results = reader.ReadBarCodes();

                    if (results != null && results.Length > 0)
                    {
                        // Iterate through all detected barcodes (should be one in this case)
                        foreach (var result in results)
                        {
                            Console.WriteLine($"CodeText: {result.CodeText}");

                            // Attempt to parse the Mailmark codetext into its component fields
                            var decoded = Aspose.BarCode.ComplexBarcode.ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                            if (decoded != null)
                            {
                                Console.WriteLine($"Decoded ItemID: {decoded.ItemID}");
                            }
                        }
                    }
                    else
                    {
                        Console.WriteLine("No Mailmark barcode detected.");
                    }
                }
            }
        }
    }
}