using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation, saving, and validation of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode (Mode 2), saves it to a file, and validates the encoded data.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare the MaxiCode codetext for Mode 2 (postal information + data)
        // --------------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999,      // Example service category
            // Standard second message (free‑form text)
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = "Sample MaxiCode message" }
        };

        // --------------------------------------------------------------------
        // 2. Generate the MaxiCode barcode image and write it to a memory stream
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            using (var ms = new MemoryStream())
            {
                // Save the generated barcode as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for subsequent reads

                // ----------------------------------------------------------------
                // 3. Optionally write the image to disk for visual inspection
                // ----------------------------------------------------------------
                File.WriteAllBytes("maxicode.png", ms.ToArray());

                // ----------------------------------------------------------------
                // 4. Recognize the barcode from the memory stream and validate it
                // ----------------------------------------------------------------
                using (var reader = new BarCodeReader(ms, DecodeType.MaxiCode))
                {
                    // Read all detected barcodes (should be one)
                    var results = reader.ReadBarCodes();

                    if (results.Length == 0)
                    {
                        Console.WriteLine("No MaxiCode barcode detected.");
                        return;
                    }

                    // Iterate through each detected barcode (normally just one)
                    foreach (var result in results)
                    {
                        // Decode the complex codetext using the built‑in decoder
                        var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                            result.Extended.MaxiCode.MaxiCodeMode,
                            result.CodeText);

                        if (decoded != null)
                        {
                            // Successful validation – output decoded fields
                            Console.WriteLine("MaxiCode validation succeeded.");
                            Console.WriteLine($"Postal Code: {((MaxiCodeCodetextMode2)decoded).PostalCode}");
                            Console.WriteLine($"Country Code: {((MaxiCodeCodetextMode2)decoded).CountryCode}");
                            Console.WriteLine($"Service Category: {((MaxiCodeCodetextMode2)decoded).ServiceCategory}");
                        }
                        else
                        {
                            // Decoding failed – barcode does not meet ISO/IEC 16023 spec
                            Console.WriteLine("MaxiCode validation failed.");
                        }
                    }
                }
            }
        }
    }
}