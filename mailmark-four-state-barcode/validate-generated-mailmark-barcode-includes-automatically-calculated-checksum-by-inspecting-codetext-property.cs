using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation and validation of a Mailmark barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Mailmark barcode, reads it back,
    /// and verifies that the generated codetext matches the read codetext.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare Mailmark data (valid sample)
        // ------------------------------------------------------------
        var mailmark = new MailmarkCodetext
        {
            Format = 4,                 // 4-state Mailmark
            VersionID = 1,
            Class = "0",                // Null/Test class
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T " // trailing spaces are required
        };

        // ------------------------------------------------------------
        // Construct the expected codetext (includes automatically calculated checksum)
        // ------------------------------------------------------------
        string expectedCodetext = mailmark.GetConstructedCodetext();

        // ------------------------------------------------------------
        // Generate the barcode image into a memory stream
        // ------------------------------------------------------------
        using (var ms = new MemoryStream())
        {
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            ms.Position = 0;

            // ------------------------------------------------------------
            // Read the barcode back from the memory stream
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(ms, DecodeType.Mailmark))
            {
                // Ensure checksum validation is performed (default is appropriate, but set explicitly)
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read all detected barcodes
                var results = reader.ReadBarCodes();

                // If no barcodes were detected, inform the user and exit
                if (results.Length == 0)
                {
                    Console.WriteLine("No Mailmark barcode detected.");
                    return;
                }

                // ------------------------------------------------------------
                // Compare each read codetext with the expected codetext
                // ------------------------------------------------------------
                foreach (var result in results)
                {
                    string readCodetext = result.CodeText;
                    Console.WriteLine($"Read Codetext: {readCodetext}");
                    Console.WriteLine($"Expected Codetext: {expectedCodetext}");

                    // Verify that the read codetext matches the generated one exactly
                    if (string.Equals(readCodetext, expectedCodetext, StringComparison.Ordinal))
                    {
                        Console.WriteLine("Checksum validation succeeded: generated codetext matches read codetext.");
                    }
                    else
                    {
                        Console.WriteLine("Checksum validation failed: mismatch between generated and read codetext.");
                    }
                }
            }
        }
    }
}