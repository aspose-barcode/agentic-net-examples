// Title: Demonstrate checksum validation for Code 39 barcode reading
// Description: Shows how to generate a Code 39 barcode with checksum enabled, then read it with checksum validation turned on using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It illustrates the use of BarcodeGenerator, BarCodeReader, and related settings such as EnableChecksum and ChecksumValidation for optional symbologies like Code 39. Developers often need to enable checksum validation when reading barcodes to ensure data integrity, especially for symbologies that support optional checksums.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for optional symbologies like Code 39 before reading.
// Tags: barcode symbology, checksum validation, code39, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code 39 barcode with checksum enabled,
/// then reads it back with checksum validation turned on.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with checksum validation,
    /// displays the results, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "Code39Sample.png");

        // ------------------------------------------------------------
        // Generate a Code 39 barcode with checksum enabled
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            // Set barcode dimensions
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Enable checksum generation for the barcode
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the barcode image to the temporary file
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode with checksum validation turned on
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(tempPath, DecodeType.Code39))
        {
            // Enable checksum validation for optional symbologies like Code 39
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes (should be one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }

        // ------------------------------------------------------------
        // Clean up the temporary barcode image file
        // ------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}