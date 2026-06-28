using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation and recognition of a Mailmark complex barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a Mailmark barcode, saves it to a memory stream, and then reads it back.
    /// </summary>
    static void Main()
    {
        // Prepare Mailmark codetext with required fields
        var mailmark = new MailmarkCodetext
        {
            Format = 4,
            VersionID = 1,
            Class = "0",
            SupplychainID = 384224,
            ItemID = 16563762,
            DestinationPostCodePlusDPS = "EF61AH8T "
        };

        ComplexBarcodeGenerator generator = null; // Will hold the barcode generator instance
        BarCodeReader reader = null;               // Will hold the barcode reader instance

        try
        {
            // Generate barcode image into a memory stream
            using (var ms = new MemoryStream())
            {
                // Initialize generator with the prepared Mailmark codetext
                generator = new ComplexBarcodeGenerator(mailmark);

                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);

                // Reset stream position to the beginning for reading
                ms.Position = 0;

                // Initialize reader to decode all supported barcode types from the stream
                reader = new BarCodeReader(ms, DecodeType.AllSupportedTypes);

                // Read all barcodes found in the stream
                var results = reader.ReadBarCodes();

                // Output each detected barcode's codetext to the console
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                }
            }
        }
        finally
        {
            // Ensure proper disposal of resources even if an exception occurs
            if (reader != null)
                reader.Dispose();

            if (generator != null)
                generator.Dispose();
        }
    }
}