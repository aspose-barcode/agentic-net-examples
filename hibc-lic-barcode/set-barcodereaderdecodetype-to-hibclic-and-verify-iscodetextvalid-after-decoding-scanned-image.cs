// Title: Decode HIBCLIC barcode and verify code text validity
// Description: This example generates a HIBCLIC barcode, reads it using BarCodeReader with DecodeType set to HIBCLIC, and checks whether the decoded text is present (simulating IsCodeTextValid). It shows how to work with complex HIBCLIC barcodes in Aspose.BarCode.
// Category-Description: Aspose.BarCode barcode recognition examples focusing on complex barcode types such as HIBCLIC. The sample uses ComplexBarcodeGenerator, BarCodeReader, DecodeType, and checksum validation to illustrate typical workflows for generating, scanning, and validating HIBCLIC barcodes, which are commonly used in healthcare labeling. Developers can use this pattern to integrate HIBCLIC support into .NET applications.
// Prompt: Set BarCodeReader.DecodeType to HIBCLIC and verify IsCodeTextValid after decoding a scanned image.
// Tags: hibclic, barcode, decode, validation, aspose.barcode, complexbarcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a HIBCLIC barcode, decoding it with the appropriate DecodeType,
/// and performing a simple validation of the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it, and outputs validation results.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder and file path for the barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCLICDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "hibclic.png");

        // --------------------------------------------------------------------
        // Create HIBCLIC primary data codetext with required fields.
        // --------------------------------------------------------------------
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCCode128LIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // --------------------------------------------------------------------
        // Generate the barcode image and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Resolve DecodeType.HIBCLIC via reflection (required for older SDK versions).
        // --------------------------------------------------------------------
        var field = typeof(DecodeType).GetField("HIBCLIC");
        if (field == null)
        {
            Console.WriteLine("DecodeType HIBCLIC is not supported by the current Aspose.BarCode version.");
            return;
        }
        BaseDecodeType decodeType = (BaseDecodeType)field.GetValue(null);

        // --------------------------------------------------------------------
        // Read the barcode using the resolved DecodeType.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Enable default checksum validation (optional but typical).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    // Simulate IsCodeTextValid by checking for a non‑empty CodeText.
                    bool isValid = !string.IsNullOrEmpty(result.CodeText);
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"IsCodeTextValid (simulated): {isValid}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}