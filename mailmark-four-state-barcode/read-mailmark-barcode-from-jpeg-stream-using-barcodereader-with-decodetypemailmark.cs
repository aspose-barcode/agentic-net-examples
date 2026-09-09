// Title: Read Mailmark barcode from JPEG stream using BarCodeReader
// Description: Demonstrates generating a Mailmark 4‑state barcode, saving it as a JPEG in a memory stream, and decoding it with BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode suite covering complex barcode generation and recognition. It showcases the use of ComplexBarcodeGenerator for creating Mailmark symbols, BarCodeImageFormat for image output, and BarCodeReader with DecodeType.Mailmark for extraction. Developers working with postal automation, supply‑chain tracking, or any scenario requiring Mailmark decoding will find these APIs essential for creating and reading high‑density barcodes.
// Prompt: Read a Mailmark barcode from a JPEG stream using BarCodeReader with DecodeType.Mailmark.
// Tags: mailmark, barcode, read, jpeg, aspose.barcode, complexbarcode, decode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Mailmark barcode, stores it in a JPEG memory stream,
/// and then reads and decodes the barcode using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, saving, and reading of a Mailmark barcode.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a Mailmark 4‑state codetext instance with sample data.
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
        // 2. Generate the barcode image and write it to a memory stream as JPEG.
        // ------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Set the X‑dimension (module size) to 4 pixels for better readability.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;

                // Save the generated barcode into the memory stream in JPEG format.
                generator.Save(ms, BarCodeImageFormat.Jpeg);
            }

            // Reset stream position to the beginning before reading.
            ms.Position = 0;

            // ------------------------------------------------------------
            // 3. Read the barcode from the memory stream using BarCodeReader.
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Attempt to read all barcodes of the specified type.
                var results = reader.ReadBarCodes();

                // If no barcode was detected, inform the user and exit.
                if (results.Length == 0)
                {
                    Console.WriteLine("No Mailmark barcode detected.");
                    return;
                }

                // --------------------------------------------------------
                // 4. Decode the complex Mailmark codetext into its components.
                // --------------------------------------------------------
                var decoded = ComplexCodetextReader.TryDecodeMailmark(results[0].CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode Mailmark codetext.");
                    return;
                }

                // --------------------------------------------------------
                // 5. Output the decoded Mailmark fields to the console.
                // --------------------------------------------------------
                Console.WriteLine($"Format: {decoded.Format}");
                Console.WriteLine($"VersionID: {decoded.VersionID}");
                Console.WriteLine($"Class: {decoded.Class}");
                Console.WriteLine($"SupplychainID: {decoded.SupplychainID}");
                Console.WriteLine($"ItemID: {decoded.ItemID}");
                Console.WriteLine($"DestinationPostCodePlusDPS: '{decoded.DestinationPostCodePlusDPS}'");
            }
        }
    }
}