// Title: Set ECI Encoding to UTF‑8 for MaxiCode and Verify Embedded Identifier
// Description: Demonstrates how to generate a MaxiCode barcode with UTF‑8 ECI encoding and checks that the correct ECI identifier is embedded in the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on setting ECI (Extended Channel Interpretation) encodings for symbologies that support it. It showcases the use of BarcodeGenerator, its Parameters, and BarCodeReader to create and validate barcodes, a common task when handling international character sets. Developers often need to embed specific character set identifiers to ensure accurate decoding across different systems.
// Prompt: Set ECIEncoding to UTF‑8 for MaxiCode and verify the correct character set identifier is embedded.
// Tags: maxicode, eci encoding, png, barcodegenerator, barcodereader, barcoderesult

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a MaxiCode barcode with UTF‑8 ECI encoding, saves it to a temporary PNG file,
/// then reads the barcode back to verify that the UTF‑8 ECI identifier is present in the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, verification, and cleanup.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary file path for the generated barcode image
        // --------------------------------------------------------------------
        string outputPath = Path.Combine(Path.GetTempPath(), "maxicode_utf8.png");

        // Sample text containing Unicode characters (Japanese characters and English)
        string sampleText = "犬Right狗";

        // --------------------------------------------------------------------
        // Generate MaxiCode barcode with ECIEncoding set to UTF‑8
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, sampleText))
        {
            // Set the ECI encoding to UTF‑8 (will insert the UTF‑8 ECI identifier into the barcode)
            generator.Parameters.Barcode.MaxiCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the barcode image as PNG
            generator.Save(outputPath);
        }

        // --------------------------------------------------------------------
        // Verify that the generated barcode file exists and contains the correct UTF‑8 ECI identifier
        // --------------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        using (var reader = new BarCodeReader(outputPath, DecodeType.MaxiCode))
        {
            bool anyFound = false;

            // Iterate through all detected barcodes (should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // The UTF‑8 ECI identifier is "\000026"
                bool hasEciIdentifier = result.CodeText != null && result.CodeText.Contains("\\000026");
                Console.WriteLine($"UTF‑8 ECI identifier present: {hasEciIdentifier}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No MaxiCode barcode was detected in the image.");
            }
        }

        // --------------------------------------------------------------------
        // Clean up the temporary file (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(outputPath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}