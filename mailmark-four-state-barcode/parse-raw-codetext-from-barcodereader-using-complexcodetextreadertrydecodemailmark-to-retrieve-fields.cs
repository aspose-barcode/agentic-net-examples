using System;
using System.IO;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generation and reading of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode, reads it back, and decodes its fields.
    /// </summary>
    static void Main()
    {
        // Create a Mailmark codetext object with sample data.
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                 // 4-state Mailmark
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        // Generate a Mailmark barcode image and store it in a memory stream.
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        using (var ms = new MemoryStream())
        {
            // Save the generated barcode as PNG into the memory stream.
            generator.Save(ms, BarCodeImageFormat.Png);
            // Reset stream position to the beginning for reading.
            ms.Position = 0;

            // Read the barcode from the memory stream using a Mailmark decoder.
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Retrieve all decoded barcode results.
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    // Output the raw CodeText obtained from the barcode.
                    Console.WriteLine("Raw CodeText: " + result.CodeText);

                    // Attempt to decode the raw CodeText into a structured MailmarkCodetext object.
                    var decoded = ComplexCodetextReader.TryDecodeMailmark(result.CodeText);
                    if (decoded != null)
                    {
                        // Display each decoded field of the Mailmark.
                        Console.WriteLine("Decoded Mailmark Fields:");
                        Console.WriteLine($"  Format: {decoded.Format}");
                        Console.WriteLine($"  VersionID: {decoded.VersionID}");
                        Console.WriteLine($"  Class: {decoded.Class}");
                        Console.WriteLine($"  SupplychainID: {decoded.SupplychainID}");
                        Console.WriteLine($"  ItemID: {decoded.ItemID}");
                        Console.WriteLine($"  DestinationPostCodePlusDPS: {decoded.DestinationPostCodePlusDPS}");
                    }
                    else
                    {
                        // Inform the user if decoding failed.
                        Console.WriteLine("Failed to decode Mailmark codetext.");
                    }
                }
            }
        }
    }
}