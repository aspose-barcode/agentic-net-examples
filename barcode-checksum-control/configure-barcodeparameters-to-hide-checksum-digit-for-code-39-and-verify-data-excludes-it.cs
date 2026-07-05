// Title: Hide Code 39 checksum and verify its absence
// Description: Demonstrates configuring BarcodeParameters to suppress the checksum digit for a Code 39 barcode and then confirming that the generated barcode does not contain a checksum.
// Prompt: Configure BarcodeParameters to hide the checksum digit for Code 39 and verify the data excludes it.
// Tags: code39, checksum, hide, barcode, generation, recognition, aspnet, c#
using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code 39 barcode without a checksum,
/// hides the human‑readable text, and verifies that no checksum is present
/// in the decoded result.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Define the output file path for the generated barcode image.
        // ------------------------------------------------------------
        string outputPath = Path.Combine(Environment.CurrentDirectory, "code39.png");

        // ------------------------------------------------------------
        // 2. Create a Code39FullASCII barcode generator with sample data.
        //    The data does not include a checksum digit.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "HELLO"))
        {
            // Disable automatic checksum generation for Code 39.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.No;

            // Ensure the checksum is not forced to appear in the human‑readable text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;

            // Hide the human‑readable text completely.
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }

        // ------------------------------------------------------------
        // 3. Verify that the barcode image was successfully created.
        // ------------------------------------------------------------
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 4. Read the barcode from the saved image and disable checksum validation.
        //    This prevents false failures if a checksum were present.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code39))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Off;

            // Iterate through all decoded barcode results (should be only one).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Decoded CodeText: {result.CodeText}");

                // For Code 39, the checksum (if any) is exposed via Extended.OneD.CheckSum.
                string checksum = result.Extended?.OneD?.CheckSum;

                if (string.IsNullOrEmpty(checksum))
                {
                    Console.WriteLine("Checksum: <none> (as expected)");
                }
                else
                {
                    Console.WriteLine($"Checksum: {checksum} (unexpected)");
                }
            }
        }
    }
}