// Title: Encode and Verify MaxiCode Mode 2 Postal Code
// Description: Demonstrates encoding a numeric postal code into the primary message of a MaxiCode Mode 2 barcode, saving it as a PNG image, and confirming that decoding reproduces the original value.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and recognition category. It showcases the use of ComplexBarcodeGenerator with MaxiCodeCodetextMode2 for creating shipping‑label barcodes, and BarCodeReader for extracting data. Developers working with logistics, parcel tracking, or any scenario requiring MaxiCode symbology can learn how to set primary and secondary messages, adjust image parameters, and validate decoding accuracy.
// Prompt: Encode a numeric postal code in the primary message of a MaxiCode Mode 2 and verify decoding accuracy.
// Tags: maxicode, mode2, barcode, encoding, decoding, aspose.barcode, complexbarcode, png, postalcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a MaxiCode Mode 2 barcode containing a numeric postal code,
/// saves it to a temporary PNG file, and verifies that the barcode can be decoded correctly.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode, writes it to disk, and checks decoding.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare data for the primary message of the MaxiCode barcode
        // ------------------------------------------------------------
        string postalCode = "123456789"; // 9‑digit numeric postal code
        int countryCode = 56;            // ISO numeric country code
        int serviceCategory = 999;       // Service category identifier

        // ------------------------------------------------------------
        // Build the MaxiCode codetext for Mode 2 (primary message)
        // ------------------------------------------------------------
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = postalCode,
            CountryCode = countryCode,
            ServiceCategory = serviceCategory
        };

        // Optional secondary message (standard format)
        var secondMessage = new MaxiCodeStandardSecondMessage
        {
            Message = "Sample secondary"
        };
        maxiCodeData.SecondMessage = secondMessage;

        // ------------------------------------------------------------
        // Generate the barcode image and save it as PNG
        // ------------------------------------------------------------
        string tempPath = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N") + ".png");
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Increase module size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 10f;
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {tempPath}");

        // ------------------------------------------------------------
        // Verify that the saved barcode can be decoded and matches the original data
        // ------------------------------------------------------------
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Generated file not found.");
            return;
        }

        using (var reader = new BarCodeReader(tempPath, DecodeType.MaxiCode))
        {
            bool decoded = false;

            // Iterate through all detected barcodes (should be only one)
            foreach (var result in reader.ReadBarCodes())
            {
                // Attempt to parse the MaxiCode codetext based on its mode
                var decodedCodetext = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                if (decodedCodetext is MaxiCodeCodetextMode2 decodedMode2)
                {
                    Console.WriteLine($"Decoded PostalCode: {decodedMode2.PostalCode}");
                    Console.WriteLine($"Original PostalCode: {postalCode}");
                    Console.WriteLine(decodedMode2.PostalCode == postalCode
                        ? "Decoding successful: postal code matches."
                        : "Decoding error: postal code does not match.");
                    decoded = true;
                }
            }

            if (!decoded)
            {
                Console.WriteLine("No MaxiCode barcode detected or decoding failed.");
            }
        }
    }
}