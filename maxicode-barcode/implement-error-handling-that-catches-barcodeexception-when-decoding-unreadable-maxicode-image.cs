using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a MaxiCode barcode, intentional corruption of the image,
/// and handling of decoding attempts with proper exception handling.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode, corrupts its image data, and attempts to read it back.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Create a simple MaxiCode codetext (Mode 2) with required fields.
        // --------------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Test" }
        };

        // ---------------------------------------------------------------
        // 2. Generate a valid MaxiCode image and store it in a memory stream.
        // ---------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        using (var ms = new MemoryStream())
        {
            // Save the barcode as PNG into the memory stream.
            generator.Save(ms, BarCodeImageFormat.Png);
            ms.Position = 0; // Reset stream position for further operations.

            // ---------------------------------------------------------------
            // 3. Corrupt the image data to simulate an unreadable barcode.
            //    Overwrite the first few bytes with arbitrary values.
            // ---------------------------------------------------------------
            byte[] corruptBytes = { 0xFF, 0x00, 0xFF, 0x00 };
            ms.Write(corruptBytes, 0, corruptBytes.Length);
            ms.Position = 0; // Reset again before reading.

            // ---------------------------------------------------------------
            // 4. Attempt to decode the corrupted MaxiCode image.
            //    Handle both barcode-specific and generic exceptions.
            // ---------------------------------------------------------------
            try
            {
                using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                {
                    // Read all barcodes found in the stream.
                    var results = reader.ReadBarCodes();

                    // Output each decoded result to the console.
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    }
                }
            }
            catch (BarCodeException ex)
            {
                // Specific exception thrown by Aspose.BarCode when decoding fails.
                Console.WriteLine($"BarcodeException caught: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Fallback for any other unexpected errors.
                Console.WriteLine($"Unexpected exception: {ex.Message}");
            }
        }
    }
}