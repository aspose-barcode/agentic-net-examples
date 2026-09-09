// Title: Batch decode Swiss Post Parcel barcodes with checksum validation
// Description: Demonstrates generating a set of Swiss Post Parcel International barcodes, then decoding them in batch while detecting checksum mismatches.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for image creation, BarCodeReader for decoding, and the ChecksumValidation setting to differentiate valid and invalid barcodes. Developers working with postal symbologies often need to process multiple images and handle checksum errors efficiently.
// Prompt: Perform batch decoding of Swiss Post Parcel international barcodes from a network share and handle checksum mismatches.
// Tags: swisspostparcel, barcode, batch-decoding, checksum-validation, generation, recognition, aspose.barcode

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates batch generation and decoding of Swiss Post Parcel barcodes,
/// including handling of checksum mismatches.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, decodes them with checksum validation
    /// enabled and disabled, and reports the results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the batch
        string batchFolder = Path.Combine(Path.GetTempPath(), "Batch_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(batchFolder);

        // Sample Swiss Post Parcel International barcodes
        var codeTexts = new[]
        {
            "RM999605013CH", // correct checksum
            "RM999605017CH", // erroneous checksum
            "RM99960501CH"   // without checksum (will be generated)
        };

        var generatedFiles = new List<string>();

        // Generate barcode images for each sample text
        foreach (var code in codeTexts)
        {
            string filePath = Path.Combine(batchFolder, $"{code}.png");
            using (var generator = new BarcodeGenerator(EncodeTypes.SwissPostParcel, code))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2f;
                generator.Parameters.Barcode.BarHeight.Pixels = 40f;
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
        }

        Console.WriteLine("Batch decoding results:");
        Console.WriteLine();

        // Process each generated file:
        // first with checksum validation ON (default), then OFF to detect mismatches
        foreach (var file in generatedFiles)
        {
            bool hasValidResult = false;
            string decodedTextOn = null;
            string decodedTextOff = null;

            // Decode with checksum validation enabled
            try
            {
                using (var readerOn = new BarCodeReader(file, DecodeType.SwissPostParcel))
                {
                    readerOn.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;
                    foreach (var result in readerOn.ReadBarCodes())
                    {
                        hasValidResult = true;
                        decodedTextOn = result.CodeText;
                        break; // only need first result
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"File '{Path.GetFileName(file)}' could not be loaded: {ex.Message}");
                continue;
            }

            // Decode with checksum validation disabled to obtain raw data even if checksum is wrong
            try
            {
                using (var readerOff = new BarCodeReader(file, DecodeType.SwissPostParcel))
                {
                    readerOff.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;
                    foreach (var result in readerOff.ReadBarCodes())
                    {
                        decodedTextOff = result.CodeText;
                        break;
                    }
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"File '{Path.GetFileName(file)}' could not be loaded (off mode): {ex.Message}");
                continue;
            }

            // Output the appropriate result based on checksum validation outcome
            if (hasValidResult)
            {
                Console.WriteLine($"File '{Path.GetFileName(file)}': Checksum OK, Data = {decodedTextOn}");
            }
            else if (decodedTextOff != null)
            {
                Console.WriteLine($"File '{Path.GetFileName(file)}': Checksum MISMATCH, Raw Data = {decodedTextOff}");
            }
            else
            {
                Console.WriteLine($"File '{Path.GetFileName(file)}': No barcode detected.");
            }
        }

        // Cleanup: optional removal of temporary folder
        // Directory.Delete(batchFolder, true);
    }
}