// Title: Code128 barcode generation with checksum validation
// Description: Demonstrates generating a Code128 barcode, enabling checksum validation during recognition, and reporting any verification failures.
// Prompt: Enable checksum validation for Code128 barcodes and report any verification failures during processing.
// Tags: barcode symbology, checksum validation, code128, generation, recognition, console output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, validates its checksum during recognition,
/// and reports verification results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a temporary barcode image, reads it with checksum validation,
    /// and outputs detection details or failure messages.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the barcode image
        string imagePath = Path.Combine(Path.GetTempPath(), "code128.png");

        // -------------------------------------------------
        // Generate a Code128 barcode and save it to file
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode image to the temporary location
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // Verify the barcode with checksum validation enabled
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Turn on checksum validation for the reader
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;

            // Iterate through all detected barcodes (should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine("Barcode detected:");
                Console.WriteLine("  CodeText : " + result.CodeText);

                // For Code128, checksum information is available in the extended 1D data
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine("  Value    : " + result.Extended.OneD.Value);
                    Console.WriteLine("  CheckSum : " + result.Extended.OneD.CheckSum);
                }
                else
                {
                    Console.WriteLine("  No extended 1D data available.");
                }
            }

            // If no barcode was read, report checksum validation failure
            if (!anyResult)
            {
                Console.WriteLine("Checksum validation failed: no valid barcode detected.");
            }
        }

        // -------------------------------------------------
        // Clean up the temporary image file
        // -------------------------------------------------
        if (File.Exists(imagePath))
        {
            try
            {
                File.Delete(imagePath);
            }
            catch
            {
                // Ignore any cleanup errors
            }
        }
    }
}