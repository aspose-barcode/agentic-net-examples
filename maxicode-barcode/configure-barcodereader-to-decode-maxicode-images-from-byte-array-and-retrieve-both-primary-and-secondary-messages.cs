using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates creation, encoding, and decoding of a MaxiCode (Mode 2) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode barcode, saves it to a memory stream, and then reads it back.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Build the MaxiCode data structure (primary and secondary messages)
        // ------------------------------------------------------------
        var maxiCode = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit postal code
            CountryCode = 56,           // Numeric country identifier
            ServiceCategory = 999       // Service category value
        };

        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Hello World"     // Simple textual secondary message
        };
        maxiCode.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // 2. Generate the barcode image and store it in a memory stream
        // ------------------------------------------------------------
        using (var imageStream = new MemoryStream())
        {
            // Create a generator for the complex barcode (MaxiCode)
            using (var generator = new ComplexBarcodeGenerator(maxiCode))
            {
                // Save the generated barcode as PNG (evaluation mode supports PNG)
                generator.Save(imageStream, BarCodeImageFormat.Png);
            }

            // Reset stream position to the beginning for reading
            imageStream.Position = 0;

            // ------------------------------------------------------------
            // 3. Decode the barcode from the memory stream
            // ------------------------------------------------------------
            using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
            {
                // Iterate through all detected barcodes (should be only one)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Attempt to decode the complex MaxiCode codetext into a strongly‑typed object
                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.MaxiCodeMode,
                        result.CodeText);

                    // Verify that the decoded object is of the expected Mode 2 type
                    if (decoded is MaxiCodeCodetextMode2 mode2)
                    {
                        // Output primary message fields
                        Console.WriteLine("=== Primary Message ===");
                        Console.WriteLine($"Postal Code      : {mode2.PostalCode}");
                        Console.WriteLine($"Country Code     : {mode2.CountryCode}");
                        Console.WriteLine($"Service Category : {mode2.ServiceCategory}");

                        // Output secondary message based on its concrete type
                        Console.WriteLine("=== Secondary Message ===");
                        if (mode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                        {
                            Console.WriteLine($"Message: {stdMsg.Message}");
                        }
                        else if (mode2.SecondMessage is MaxiCodeStructuredSecondMessage structMsg)
                        {
                            Console.WriteLine("Identifiers:");
                            foreach (var id in structMsg.Identifiers)
                            {
                                Console.WriteLine($" - {id}");
                            }
                        }
                        else
                        {
                            Console.WriteLine("No secondary message found.");
                        }
                    }
                    else
                    {
                        // Decoding failed or returned an unexpected type
                        Console.WriteLine("Failed to decode MaxiCode codetext.");
                    }
                }
            }
        }
    }
}