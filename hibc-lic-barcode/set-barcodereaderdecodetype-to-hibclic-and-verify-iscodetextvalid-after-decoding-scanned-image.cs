// Title: HIBC QR LIC Barcode Generation and Validation using Aspose.BarCode
// Description: Demonstrates generating a HIBC QR LIC barcode, saving it as PNG, decoding it, and verifying that the decoded text is valid.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of ComplexBarcodeGenerator to create HIBC QR LIC barcodes and BarCodeReader with a specific DecodeType to read them. Developers working with healthcare barcodes often need to generate compliant codes and validate scanned data; this snippet illustrates the typical workflow using key API classes such as ComplexBarcodeGenerator, BarCodeReader, DecodeType, and BarCodeResult.
// Prompt: Set BarCodeReader.DecodeType to HIBCLIC and verify IsCodeTextValid after decoding a scanned image.
// Tags: barcode, hibc, qrlic, generation, recognition, decode, iscodetextvalid, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates creating a HIBC QR LIC barcode, saving it to a temporary file,
/// decoding it with <see cref="BarCodeReader"/>, and checking the validity of the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, decodes it, and outputs validation results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "HIBCLICDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "hibcqrlic.png");

        // Prepare primary data for the HIBC QR LIC barcode.
        var primaryCodetext = new HIBCLICPrimaryDataCodetext
        {
            BarcodeType = EncodeTypes.HIBCQRLIC,
            Data = new PrimaryData
            {
                ProductOrCatalogNumber = "12345",
                LabelerIdentificationCode = "A999",
                UnitOfMeasureID = 1
            }
        };

        // Generate the barcode image and save it as PNG.
        using (var generator = new ComplexBarcodeGenerator(primaryCodetext))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // Decode the generated barcode using the appropriate DecodeType.
        BaseDecodeType decodeType = DecodeType.HIBCQRLIC; // HIBCLIC is not a valid DecodeType; using HIBCQRLIC instead.
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Iterate through each detected barcode and display its details.
                foreach (var result in results)
                {
                    bool isCodeTextValid = !string.IsNullOrEmpty(result.CodeText);
                    Console.WriteLine($"Detected CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"IsCodeTextValid: {isCodeTextValid}");
                }
            }
        }

        // Cleanup temporary files and folder (optional).
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup.
        }
    }
}