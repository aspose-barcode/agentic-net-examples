// Title: Read Mailmark barcode from an image stream using BarCodeReader
// Description: Demonstrates generating a Mailmark barcode, saving it to a memory stream, and decoding it with BarCodeReader using DecodeType.Mailmark.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator for creating Mailmark barcodes and BarCodeReader for decoding them. Developers working with postal services, logistics, or any application that requires Mailmark symbology can refer to this pattern for generating and reading Mailmark codes in .NET.
// Prompt: Read a Mailmark barcode from a JPEG stream using BarCodeReader with DecodeType.Mailmark.
// Tags: mailmark, barcode, generation, recognition, decode, c#, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Example program that creates a Mailmark barcode, stores it in a memory stream,
/// and reads it back using <see cref="BarCodeReader"/> with <see cref="DecodeType.Mailmark"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Mailmark barcode, writes it to a stream,
    /// then decodes the barcode from the same stream and prints the result.
    /// </summary>
    static void Main()
    {
        // Create a valid Mailmark codetext object with required fields.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                     // 4‑state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing space is required
        };

        // Generate the Mailmark barcode image into a memory stream.
        using (var barcodeStream = new MemoryStream())
        {
            // Use ComplexBarcodeGenerator to create the barcode image.
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as PNG into the stream.
                generator.Save(barcodeStream, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            barcodeStream.Position = 0;

            // Set the decode type to Mailmark for the reader.
            BaseDecodeType decodeType = DecodeType.Mailmark;

            // Initialize BarCodeReader with the stream and specified decode type.
            using (var reader = new BarCodeReader(barcodeStream, decodeType))
            {
                // Iterate through all detected barcodes (should be one in this case).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output the decoded Mailmark CodeText to the console.
                    Console.WriteLine($"Detected Mailmark CodeText: {result.CodeText}");
                }
            }
        }
    }
}