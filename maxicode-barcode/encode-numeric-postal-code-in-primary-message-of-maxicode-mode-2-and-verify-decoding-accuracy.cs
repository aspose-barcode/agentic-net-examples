// Title: Encode and Verify MaxiCode Mode 2 with Numeric Postal Code
// Description: This example encodes a 9‑digit numeric postal code into the primary message of a MaxiCode Mode 2 barcode, saves it as PNG, then decodes it to confirm the data matches.
// Category-Description: Shows how to work with complex barcodes using Aspose.BarCode, specifically generating and reading MaxiCode symbols. It covers the ComplexBarcodeGenerator, MaxiCodeCodetextMode2, BarCodeReader, and related classes—common tasks for developers needing to create or validate MaxiCode barcodes in logistics and shipping applications.
// Prompt: Encode a numeric postal code in the primary message of a MaxiCode Mode 2 and verify decoding accuracy.
// Tags: maxicode, mode2, encoding, decoding, barcode-generation, barcode-recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates encoding a numeric postal code in MaxiCode Mode 2 and verifying the decoded result.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a MaxiCode Mode 2 barcode with a numeric postal code, saves it as PNG,
    /// then reads it back to confirm the encoded data matches the original.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for the generated image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "maxicode_mode2.png");

        // --------------------------------------------------------------------
        // Define sample data for the MaxiCode
        // --------------------------------------------------------------------
        string postalCode = "123456789"; // 9‑digit numeric postal code
        int countryCode = 56;
        int serviceCategory = 999;

        // --------------------------------------------------------------------
        // Build the complex codetext for MaxiCode Mode 2
        // --------------------------------------------------------------------
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = postalCode,
            CountryCode = countryCode,
            ServiceCategory = serviceCategory
        };

        // --------------------------------------------------------------------
        // Generate the barcode image
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            generator.Parameters.Barcode.MaxiCode.Mode = MaxiCodeMode.Mode2;
            generator.Parameters.Barcode.XDimension.Pixels = 15;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Decode the saved image and verify the postal code
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the mode used during generation from the extended result
                var mode = result.Extended.MaxiCode.Mode;

                // Decode the complex codetext back into a strongly‑typed object
                MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                var decodedMode2 = decoded as MaxiCodeCodetextMode2;

                if (decodedMode2 != null)
                {
                    bool match = decodedMode2.PostalCode == postalCode;
                    Console.WriteLine($"Decoded PostalCode: {decodedMode2.PostalCode}");
                    Console.WriteLine($"Verification: {(match ? "Success" : "Failure")}");
                }
                else
                {
                    Console.WriteLine("Failed to decode MaxiCode as Mode 2.");
                }
            }
        }
    }
}