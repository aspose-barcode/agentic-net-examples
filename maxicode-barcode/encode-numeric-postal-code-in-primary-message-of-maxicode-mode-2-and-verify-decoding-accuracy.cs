using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generation and decoding of a MaxiCode Mode 2 barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a MaxiCode Mode 2 barcode, decodes it, and prints the extracted fields.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Define sample data for MaxiCode Mode 2
        // ------------------------------------------------------------
        string postalCode = "524032140"; // 9‑digit US postal code
        int countryCode = 56;            // Example 3‑digit country code
        int serviceCategory = 999;       // Example service category
        string secondMessageText = "Test message";

        // ------------------------------------------------------------
        // 2. Build the codetext object that represents the MaxiCode data
        // ------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = postalCode,
            CountryCode = countryCode,
            ServiceCategory = serviceCategory,
            SecondMessage = new MaxiCodeStandardSecondMessage { Message = secondMessageText }
        };

        // ------------------------------------------------------------
        // 3. Generate the barcode image and store it in a memory stream
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        using (var imageStream = new MemoryStream())
        {
            // Save the generated barcode as PNG into the stream
            generator.Save(imageStream, BarCodeImageFormat.Png);
            // Reset stream position to the beginning for reading
            imageStream.Position = 0;

            // --------------------------------------------------------
            // 4. Decode the barcode from the memory stream
            // --------------------------------------------------------
            using (var reader = new BarCodeReader(imageStream, DecodeType.MaxiCode))
            {
                // Iterate over all detected barcodes (should be only one)
                foreach (var result in reader.ReadBarCodes())
                {
                    // Decode the complex codetext from the raw code text
                    var decoded = ComplexCodetextReader.TryDecodeMaxiCode(
                        result.Extended.MaxiCode.MaxiCodeMode,
                        result.CodeText);

                    // ----------------------------------------------------
                    // 5. If decoding succeeded as Mode 2, output the fields
                    // ----------------------------------------------------
                    if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                    {
                        Console.WriteLine($"Decoded PostalCode: {decodedMode2.PostalCode}");
                        Console.WriteLine($"Decoded CountryCode: {decodedMode2.CountryCode}");
                        Console.WriteLine($"Decoded ServiceCategory: {decodedMode2.ServiceCategory}");

                        // Output the optional second message if present
                        if (decodedMode2.SecondMessage is MaxiCodeStandardSecondMessage stdMsg)
                        {
                            Console.WriteLine($"Decoded Second Message: {stdMsg.Message}");
                        }

                        // Verify that the decoded postal code matches the original input
                        bool match = decodedMode2.PostalCode == postalCode;
                        Console.WriteLine($"Postal code match: {match}");
                    }
                    else
                    {
                        // Decoding did not produce a Mode 2 object
                        Console.WriteLine("Failed to decode MaxiCode as Mode 2.");
                    }
                }
            }
        }
    }
}