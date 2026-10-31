// Title: HIBC Barcode Generation and Checksum Validation Example
// Description: Demonstrates how to generate a HIBC Code 128 LIC barcode, save it as PNG, and verify its checksum using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows the use of ComplexBarcodeGenerator with HIBCLICPrimaryDataCodetext to create HIBC barcodes, and BarCodeReader with DecodeType.HIBCCode128LIC to validate the checksum. Developers working with healthcare product labeling often need to generate HIBC barcodes and ensure they meet specification requirements, making this pattern a common task.
// Prompt: Validate that the generated barcode complies with HIBC specifications by checking its checksum after creation.
// Tags: hibc,barcode,generation,validation,checksum,aspose.barcode,complexbarcode,code128lic

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation of a HIBC Code 128 LIC barcode and validation of its checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a HIBC barcode, saves it to a temporary file, reads it back, and checks the checksum.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBC_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hibc.png");

        // Create HIBC primary data codetext with required fields
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

        // Generate the barcode using ComplexBarcodeGenerator and save as PNG
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the generated barcode and verify checksum (default validation is enabled)
        bool isValid = false;
        using (var reader = new BarCodeReader(barcodePath, DecodeType.HIBCCode128LIC))
        {
            // If a result is returned, the checksum validation succeeded
            foreach (var result in reader.ReadBarCodes())
            {
                if (!string.IsNullOrEmpty(result.CodeText))
                {
                    isValid = true;
                    Console.WriteLine($"Decoded CodeText: {result.CodeText}");
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                }
            }
        }

        // Output the overall validation result
        Console.WriteLine($"HIBC barcode checksum validation result: {(isValid ? "Valid" : "Invalid")}");

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}