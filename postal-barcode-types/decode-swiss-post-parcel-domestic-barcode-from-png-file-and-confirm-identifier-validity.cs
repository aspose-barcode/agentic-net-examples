// Title: Decode Swiss Post Parcel Domestic Barcode and Validate Identifier
// Description: Generates a Swiss Post Parcel domestic barcode, saves it as a PNG image, reads the image back, decodes the barcode data, and verifies that the identifier conforms to the expected format.
// Category-Description: This example demonstrates the combined use of Aspose.BarCode generation and recognition APIs to work with Swiss Post Parcel barcodes. It shows how to create a barcode with BarcodeGenerator, persist it as an image, and then read it using BarCodeReader. Developers building shipping, logistics, or postal applications often need to generate barcodes for parcels and later validate the encoded identifiers during processing.
// Prompt: Decode a Swiss Post Parcel domestic barcode from a PNG file and confirm identifier validity.
// Tags: swisspost, parcel, barcode, generation, recognition, decode, validation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating, saving, reading, and validating a Swiss Post Parcel domestic barcode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, saves it as PNG, decodes it, and validates the identifier.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "SwissPostDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "SwissPostDomestic.png");

        // --------------------------------------------------------------------
        // Generate a Swiss Post Parcel Domestic barcode using the original
        // identifier format (dotted notation) and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, "98.34.123456.12345678"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.BarHeight.Pixels = 40f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read and decode the barcode from the PNG file using the appropriate
        // decode type for Swiss Post Parcel barcodes.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.SwissPostParcel))
        {
            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"Barcode type: {result.CodeTypeName}, Data: {result.CodeText}");

                // Validate the decoded identifier against the expected formats.
                bool isValid = ValidateSwissPostDomesticIdentifier(result.CodeText);
                Console.WriteLine($"Identifier valid: {isValid}");
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcode detected in the image.");
            }
        }
    }

    // ------------------------------------------------------------------------
    // Validates Swiss Post Parcel Domestic identifier.
    // Accepts either the original dotted format (dd.dd.dddddd.dddddddd)
    // or the 18‑digit plain format starting with 98 or 99.
    // ------------------------------------------------------------------------
    static bool ValidateSwissPostDomesticIdentifier(string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            return false;

        // Remove any surrounding whitespace.
        codeText = codeText.Trim();

        // Dotted format validation (e.g., "98.34.123456.12345678").
        if (System.Text.RegularExpressions.Regex.IsMatch(codeText, @"^\d{2}\.\d{2}\.\d{6}\.\d{8}$"))
            return true;

        // Plain 18‑digit format validation (e.g., "983412345612345678").
        if (System.Text.RegularExpressions.Regex.IsMatch(codeText, @"^(98|99)\d{16}$"))
            return true;

        return false;
    }
}