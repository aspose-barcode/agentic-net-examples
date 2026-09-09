// Title: HIBC Code128 Barcode Generation with Checksum Validation
// Description: Demonstrates generating a HIBC Code128 LIC barcode, enabling checksum generation, and verifying the checksum by reading the barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext, configure checksum settings via BarcodeParameters, and validate the checksum using BarCodeReader. Developers working with healthcare industry barcodes (HIBC) often need to ensure checksum compliance for regulatory and scanning accuracy.
// Prompt: Validate that the generated barcode complies with HIBC specifications by checking its checksum after creation.
// Tags: hibc, checksum, barcode, generation, validation, aspose.barcode, complexbarcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a HIBC Code128 LIC barcode, forces checksum generation,
/// and then reads the barcode back to confirm the checksum is present and valid.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the barcode, validates its checksum, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hibc.png");

        // ------------------------------------------------------------
        // Build the primary data required for a HIBC LIC barcode
        // ------------------------------------------------------------
        var primaryData = new PrimaryData
        {
            ProductOrCatalogNumber = "12345",
            LabelerIdentificationCode = "A999",
            UnitOfMeasureID = 1
        };

        // Wrap the primary data in a codetext object specifying the barcode type
        var hibcCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = primaryData
        };

        // ------------------------------------------------------------
        // Generate the barcode image with checksum enabled
        // ------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(hibcCodetext))
        {
            // HIBC Code128 requires a checksum; enable it explicitly
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify that the barcode file was created successfully
        // ------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode and extract the checksum information
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.HIBCCode128LIC))
        {
            // Ensure checksum validation is active (default for obligatory checksum)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // The OneD extended data contains the checksum string
                string checksum = result.Extended.OneD.CheckSum;
                Console.WriteLine($"Extracted CheckSum: {checksum}");

                // Simple validation: checksum string should be non‑empty
                bool checksumValid = !string.IsNullOrEmpty(checksum);
                Console.WriteLine($"Checksum valid: {checksumValid}");
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and folder
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures are non‑critical for this example
        }
    }
}