// Title: Validate MaxiCode barcode against ISO/IEC 16023 using Aspose.BarCode
// Description: Generates a MaxiCode (Mode 2) barcode, saves it as a PNG, then reads it back to verify that the decoded data matches the original, demonstrating compliance with ISO/IEC 16023.
// Category-Description: This example belongs to the Aspose.BarCode complex barcode generation and validation category. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, BarCodeReader, and related classes to create, render, and recognize MaxiCode symbols. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode barcodes and ensure they conform to the ISO/IEC 16023 standard; this snippet provides a concise reference for those tasks.
// Prompt: Validate that the generated MaxiCode barcode complies with ISO/IEC 16023 standard using built‑in validator.
// Tags: maxicode, barcode, validation, iso/iec 16023, aspose.barcode, complexbarcode, generation, recognition, png

using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation of a MaxiCode (Mode 2) barcode, saving it to a temporary file,
/// and round‑trip validation using Aspose.BarCode's built‑in decoder to simulate ISO/IEC 16023 compliance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates a MaxiCode barcode, validates it by decoding, and cleans up the temporary image.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary file path for the generated PNG image.
        // --------------------------------------------------------------------
        string tempPath = Path.Combine(Path.GetTempPath(), "MaxiCode_" + Guid.NewGuid().ToString("N") + ".png");

        // --------------------------------------------------------------------
        // Build the structured second message (Mode 2) required by MaxiCode.
        // --------------------------------------------------------------------
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99; // Two‑digit year

        // --------------------------------------------------------------------
        // Assemble the full MaxiCode codetext with postal, country, and service data.
        // --------------------------------------------------------------------
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999,
            SecondMessage = structuredMessage
        };

        // --------------------------------------------------------------------
        // Generate the barcode image using ComplexBarcodeGenerator.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Optional visual customisation.
            generator.Parameters.Barcode.XDimension.Pixels = 5f;
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;

            // Save the image as PNG.
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the image file was successfully created.
        // --------------------------------------------------------------------
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Decode the generated barcode and compare the decoded data with the original.
        // --------------------------------------------------------------------
        bool validationPassed = false;
        using (var reader = new BarCodeReader(tempPath, DecodeType.MaxiCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Decode complex MaxiCode codetext based on the mode reported by the scanner.
                MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(result.Extended.MaxiCode.Mode, result.CodeText);
                if (decoded is MaxiCodeCodetextMode2 decodedMode2)
                {
                    // Compare basic fields (postal code, country code, service category).
                    bool basicMatch = decodedMode2.PostalCode == maxiCodeCodetext.PostalCode &&
                                      decodedMode2.CountryCode == maxiCodeCodetext.CountryCode &&
                                      decodedMode2.ServiceCategory == maxiCodeCodetext.ServiceCategory;

                    // Compare the structured second message if present.
                    if (decodedMode2.SecondMessage is MaxiCodeStructuredSecondMessage decodedSecond)
                    {
                        bool messageMatch = decodedSecond.Year == structuredMessage.Year &&
                                            decodedSecond.Identifiers.SequenceEqual(structuredMessage.Identifiers);
                        validationPassed = basicMatch && messageMatch;
                    }
                }
            }
        }

        // --------------------------------------------------------------------
        // Output the validation result.
        // --------------------------------------------------------------------
        Console.WriteLine(validationPassed
            ? "MaxiCode barcode round‑trip validation succeeded (ISO/IEC 16023 compliance simulated)."
            : "MaxiCode barcode validation failed.");

        // --------------------------------------------------------------------
        // Clean up the temporary image file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Suppress any errors during cleanup.
        }
    }
}