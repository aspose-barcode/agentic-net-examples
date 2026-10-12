// Title: Checksum Validation Default for Mixed Symbology Barcodes
// Description: Demonstrates generating Code11 and Code39 barcodes and reading them with ChecksumValidation.Default to enforce default checksum handling.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to work with mixed‑symbology images. It uses BarcodeGenerator for creating barcodes and BarCodeReader for decoding, focusing on checksum validation via the ChecksumValidation property. Developers often need to ensure correct checksum handling when processing diverse barcode types in a single workflow.
// Prompt: Apply ChecksumValidation.Default to enforce default checksum handling when reading mixed‑symbology images.
// Tags: barcode symbology, checksum validation, mixed symbology, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code11 and Code39 barcodes and reading them with default checksum validation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them with default checksum handling, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "MixedChecksumDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Generate a Code11 barcode (checksum is mandatory for this symbology)
        string code11Path = Path.Combine(tempDir, "Code11.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Save(code11Path, BarCodeImageFormat.Png);
        }

        // Generate a Code39 barcode (checksum is optional for this symbology)
        string code39Path = Path.Combine(tempDir, "Code39.png");
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code39, "ABC123"))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Save(code39Path, BarCodeImageFormat.Png);
        }

        // Read the generated barcodes using the default checksum validation mode
        ReadBarcode(code11Path, DecodeType.Code11);
        ReadBarcode(code39Path, DecodeType.Code39);

        // Attempt to delete the temporary files and directory; ignore any errors
        try
        {
            File.Delete(code11Path);
            File.Delete(code39Path);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Cleanup errors are non‑critical for the demo
        }
    }

    /// <summary>
    /// Reads a barcode image using the specified decode type and outputs details to the console.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    /// <param name="decodeType">The symbology type to use for decoding.</param>
    static void ReadBarcode(string imagePath, BaseDecodeType decodeType)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader with the image and the expected decode type
        using (BarCodeReader reader = new BarCodeReader(imagePath, decodeType))
        {
            // Enforce default checksum validation behavior
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // If extended 1D information is available, display its value and checksum
                if (result.Extended != null && result.Extended.OneD != null)
                {
                    Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
                }

                Console.WriteLine();
            }
        }
    }
}