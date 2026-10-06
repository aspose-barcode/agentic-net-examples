// Title: Decode MaxiCode barcodes while ignoring checksum errors in noisy images
// Description: Demonstrates configuring Aspose.BarCode's BarcodeReader to bypass checksum validation and tolerate incorrect barcodes when decoding MaxiCode symbols in a high‑noise environment.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on decoding settings for robust reading. It showcases the use of BarCodeReader, BarcodeSettings, and QualitySettings to adjust checksum validation and error tolerance, which developers commonly need when processing low‑quality or damaged barcodes in automated scanning systems.
// Prompt: Configure BarcodeReader to ignore checksum errors while decoding MaxiCode barcodes in a high‑noise environment.
// Tags: maxicode, checksumvalidation, barcode recognition, error tolerance, aspnet, aspose.barcode, decode, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads MaxiCode barcodes while ignoring checksum errors and allowing incorrect barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads a MaxiCode image, configures the reader, and outputs decoded results.
    /// </summary>
    static void Main()
    {
        // Build the full path to the sample image located in the current working directory.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "maxicode.png");

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Sample MaxiCode image not found at: " + imagePath);
            return;
        }

        // Initialize the barcode reader for the MaxiCode symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.MaxiCode))
        {
            // Disable checksum validation so that checksum errors are ignored.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // In noisy conditions allow the reader to return barcodes even if they are flagged as incorrect.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the decoding operation.
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the decoding results.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }
    }
}