// Title: Enable Checksum Validation for Code 39 Barcode Reading
// Description: Demonstrates how to generate a Code 39 barcode with checksum enabled and then read it with checksum validation turned on using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on optional symbologies like Code 39 where checksum validation can be toggled. Developers often need to ensure data integrity by enabling checksum validation during read operations.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for optional symbologies like Code 39 before reading.
// Tags: barcode symbology, generation, recognition, checksum, code39, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a Code 39 barcode with checksum enabled
/// and reads it back with checksum validation turned on.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, verifies its creation,
    /// and reads the barcode while validating its checksum.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Prepare a temporary folder and file path for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code39.png");

        // Generate a Code 39 barcode with checksum enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Read the barcode with checksum validation turned on
        Console.WriteLine("Reading barcode with ChecksumValidation.On:");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39))
        {
            // Enable checksum validation for the reader
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Iterate through all detected barcodes and display their details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }
        }
    }
}