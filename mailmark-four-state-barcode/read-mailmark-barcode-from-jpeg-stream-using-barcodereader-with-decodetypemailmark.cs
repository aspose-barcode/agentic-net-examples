// Title: Read Mailmark barcode from JPEG stream using BarCodeReader
// Description: Demonstrates generating a Mailmark barcode, saving it to a JPEG memory stream, and reading it back with BarCodeReader configured for DecodeType.Mailmark.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, showcasing the ComplexBarcodeGenerator for creating Mailmark symbols and the BarCodeReader for decoding them. Developers working with postal automation, logistics, or supply‑chain tracking often need to embed and extract Mailmark data; the key API classes include ComplexBarcodeGenerator, MailmarkCodetext, BarCodeReader, DecodeType, and ComplexCodetextReader. The snippet serves as a reference for end‑to‑end Mailmark handling in .NET applications.
// Prompt: Read a Mailmark barcode from a JPEG stream using BarCodeReader with DecodeType.Mailmark.
// Tags: mailmark, barcode, generation, recognition, jpeg, stream, aspose.barcode, complexbarcodegenerator, barcodereader

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Mailmark barcode, writes it to a JPEG stream,
/// and then reads and decodes the barcode using <see cref="BarCodeReader"/> with <see cref="DecodeType.Mailmark"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, saves it to a memory stream as JPEG,
    /// and reads the barcode back, outputting both raw and decoded Mailmark fields.
    /// </summary>
    static void Main(string[] args)
    {
        // ------------------------------------------------------------
        // 1. Prepare Mailmark codetext with required fields.
        // ------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // ------------------------------------------------------------
        // 2. Generate the Mailmark barcode image and store it in a JPEG stream.
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X-dimension (module size) in pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 4f;

            using (var imageStream = new MemoryStream())
            {
                // Save the generated barcode as JPEG into the memory stream.
                generator.Save(imageStream, BarCodeImageFormat.Jpeg);
                // Reset stream position to the beginning for reading.
                imageStream.Position = 0;

                // ------------------------------------------------------------
                // 3. Read the barcode from the JPEG stream using BarCodeReader.
                // ------------------------------------------------------------
                BaseDecodeType decodeType = DecodeType.Mailmark;
                using (var reader = new BarCodeReader(imageStream, decodeType))
                {
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No barcodes were detected in the image.");
                    }
                    else
                    {
                        // Iterate through all detected barcodes (should be one in this case).
                        foreach (var result in results)
                        {
                            Console.WriteLine($"Detected CodeText: {result.CodeText}");
                            Console.WriteLine($"Detected CodeTypeName: {result.CodeTypeName}");

                            // ------------------------------------------------------------
                            // 4. Decode the Mailmark codetext into its individual fields.
                            // ------------------------------------------------------------
                            var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                            if (decoded != null)
                            {
                                Console.WriteLine("Decoded Mailmark fields:");
                                Console.WriteLine($"  Format: {decoded.Format}");
                                Console.WriteLine($"  VersionID: {decoded.VersionID}");
                                Console.WriteLine($"  Class: {decoded.Class}");
                                Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                                Console.WriteLine($"  ItemID: {decoded.ItemID}");
                                Console.WriteLine($"  DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
                            }
                            else
                            {
                                Console.WriteLine("Failed to decode Mailmark codetext.");
                            }
                        }
                    }
                }
            }
        }
    }
}