// Title: Validate MaxiCode barcode against ISO/IEC 16023 using Aspose.BarCode
// Description: Demonstrates generating a MaxiCode barcode (Mode 2) with structured data, saving it as PNG, and validating its compliance with ISO/IEC 16023 using the built‑in complex codetext validator.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on complex barcode types such as MaxiCode. It showcases the use of ComplexBarcodeGenerator, MaxiCodeCodetextMode2, ComplexCodetextReader, and BarCodeReader to create, decode, and verify barcodes. Developers working with shipping, logistics, or inventory systems often need to generate MaxiCode symbols and ensure they meet the ISO/IEC 16023 standard for interoperability.
// Prompt: Validate that the generated MaxiCode barcode complies with ISO/IEC 16023 standard using built‑in validator.
// Tags: maxicode, iso/iec 16023, barcode generation, barcode validation, complex barcode, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and ISO/IEC 16023 validation of a MaxiCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a MaxiCode barcode, saves it, validates it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "maxicode.png");

        // Build structured MaxiCode codetext (Mode 2)
        var maxiCodeCodetext = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",
            CountryCode = 56,
            ServiceCategory = 999
        };

        // Populate the second message with address lines and year
        var secondMessage = new MaxiCodeStructuredSecondMessage();
        secondMessage.Add("634 ALPHA DRIVE");
        secondMessage.Add("PITTSBURGH");
        secondMessage.Add("PA");
        secondMessage.Year = 99;
        maxiCodeCodetext.SecondMessage = secondMessage;

        // Generate the MaxiCode barcode and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(maxiCodeCodetext))
        {
            // Optional visual settings: set X-dimension in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 15f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the MaxiCode barcode.");
            return;
        }

        // Read and validate the generated barcode using the built‑in validator
        bool isValid = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Decode using the complex codetext reader for MaxiCode
                var complexCodetext = ComplexCodetextReader.TryDecodeMaxiCode(
                    result.Extended.MaxiCode.Mode,
                    result.CodeText);

                if (complexCodetext != null)
                {
                    // Successful decoding implies compliance with ISO/IEC 16023
                    isValid = true;
                    Console.WriteLine("Validation succeeded. Decoded data:");
                    Console.WriteLine($"PostalCode: {((MaxiCodeCodetextMode2)complexCodetext).PostalCode}");
                    Console.WriteLine($"CountryCode: {((MaxiCodeCodetextMode2)complexCodetext).CountryCode}");
                    Console.WriteLine($"ServiceCategory: {((MaxiCodeCodetextMode2)complexCodetext).ServiceCategory}");
                }
                else
                {
                    Console.WriteLine("Decoding failed for a barcode result.");
                }
            }
        }

        // Output overall validation result
        Console.WriteLine(isValid
            ? "The generated MaxiCode barcode complies with ISO/IEC 16023."
            : "The generated MaxiCode barcode does NOT comply with ISO/IEC 16023.");

        // Cleanup temporary files (best‑effort)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup is best‑effort
        }
    }
}