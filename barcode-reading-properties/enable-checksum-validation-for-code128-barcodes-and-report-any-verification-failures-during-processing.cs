// Title: Enable Checksum Validation for Code128 Barcodes
// Description: Demonstrates generating a Code128 barcode with checksum enabled, then reading it with checksum validation and reporting verification results.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It shows how to use BarcodeGenerator to create a barcode, configure checksum settings, and employ BarCodeReader with ChecksumValidation to verify the barcode during decoding. Developers working with barcode quality assurance, data integrity checks, or inventory systems often need to enable and validate checksums for one‑dimensional symbologies like Code128.
// Prompt: Enable checksum validation for Code128 barcodes and report any verification failures during processing.
// Tags: code128, checksum, barcode, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates enabling checksum validation for Code128 barcodes,
/// generating the barcode, reading it back, and reporting any verification failures.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode with checksum,
    /// reads it with checksum validation, and outputs the results.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare a temporary folder to store the generated barcode image
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "ChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodeFile = Path.Combine(tempFolder, "code128.png");

        // ------------------------------------------------------------
        // Define the barcode content
        // ------------------------------------------------------------
        string codeText = "1234567890";

        // ------------------------------------------------------------
        // Generate a Code128 barcode with checksum enabled
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Read the barcode and enable checksum validation during decoding
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (BarCodeReader reader = new BarCodeReader(barcodeFile, decodeType))
        {
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine("Barcode read successfully:");
                Console.WriteLine($"  Code Type : {result.CodeTypeName}");
                Console.WriteLine($"  Code Text : {result.CodeText}");
                Console.WriteLine($"  CheckSum  : {result.Extended.OneD.CheckSum}");
            }

            // If no barcode was read, the checksum verification failed
            if (!anyResult)
            {
                Console.WriteLine("Checksum verification failed: no valid barcode detected.");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and folder
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodeFile))
                File.Delete(barcodeFile);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}