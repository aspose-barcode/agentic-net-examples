// Title: Demonstrate checksum validation for obligatory and optional checksum symbologies in a single read
// Description: This example generates Code11 and Code39 barcodes, enables checksum generation where applicable, and reads them with ChecksumValidation set to On.
// Category-Description: Shows how to configure Aspose.BarCode's BarcodeSettings.ChecksumValidation for both mandatory and optional checksum symbologies during a single read operation. It uses BarcodeGenerator to create barcodes, BarCodeReader to decode them, and highlights typical use cases such as validating data integrity in scanning applications. Developers working with 1D barcodes often need to enable checksum validation to ensure accurate reads.
// Prompt: Configure BarcodeSettings.ChecksumValidation to On for both obligatory and optional checksum symbologies in a single read operation.
// Tags: barcode symbology, checksum validation, read operation, png output, aspose barcode, barcode generator, barcode reader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates barcodes with and without mandatory checksums,
/// then reads them while enforcing checksum validation for both types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates temporary barcode images, reads them with checksum validation enabled,
    /// and outputs the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated images
        string code11Path = Path.Combine(tempFolder, "Code11.png");
        string code39Path = Path.Combine(tempFolder, "Code39.png");

        // Generate a Code11 barcode (obligatory checksum)
        using (BarcodeGenerator genObligatory = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            genObligatory.Parameters.Barcode.XDimension.Pixels = 2f;
            genObligatory.Save(code11Path, BarCodeImageFormat.Png);
        }

        // Generate a Code39 barcode (optional checksum) and enable checksum generation
        using (BarcodeGenerator genOptional = new BarcodeGenerator(EncodeTypes.Code39, "123456"))
        {
            genOptional.Parameters.Barcode.XDimension.Pixels = 2f;
            genOptional.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes; // optional checksum enabled for demo
            genOptional.Save(code39Path, BarCodeImageFormat.Png);
        }

        // Prepare list of files to read
        string[] files = new[] { code11Path, code39Path };

        // Iterate over each barcode image and read it with checksum validation turned on
        foreach (string file in files)
        {
            if (!File.Exists(file))
            {
                Console.WriteLine($"File not found: {file}");
                continue;
            }

            // Choose the appropriate decode type based on the file name
            BaseDecodeType decodeType = file.EndsWith("Code11.png") ? DecodeType.Code11 : DecodeType.Code39;

            using (BarCodeReader reader = new BarCodeReader(file, decodeType))
            {
                // Enable checksum validation for both obligatory and optional checksum symbologies
                reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

                // Read and display all detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(file)}");
                    Console.WriteLine($"Code Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");

                    // For 1D barcodes, the checksum value is available in Extended.OneD.CheckSum
                    if (result.Extended?.OneD != null)
                    {
                        Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                    }

                    Console.WriteLine();
                }
            }
        }

        // Cleanup temporary files (optional)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}