using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating and reading barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, saves them to disk, and reads them back with default checksum validation.
    /// </summary>
    static void Main()
    {
        // Prepare output directory for generated barcode images
        string outputDir = "Barcodes";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Define file paths for the sample barcodes
        string code128Path = Path.Combine(outputDir, "code128.png");
        string code39Path = Path.Combine(outputDir, "code39.png");

        // Generate a Code128 barcode (checksum always required)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789012"))
        {
            generator.Save(code128Path);
        }

        // Generate a Code39 barcode (checksum optional)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39FullASCII, "ABC-123"))
        {
            generator.Save(code39Path);
        }

        // Read the generated images using default checksum validation
        ReadBarcodeWithDefaultChecksum(code128Path);
        ReadBarcodeWithDefaultChecksum(code39Path);
    }

    /// <summary>
    /// Reads a barcode image and displays its type, text, and checksum (if applicable) using default checksum validation.
    /// </summary>
    /// <param name="imagePath">The full path to the barcode image file.</param>
    static void ReadBarcodeWithDefaultChecksum(string imagePath)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a reader for the image, enabling detection of all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply default checksum handling (Aspose's default behavior)
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.Default;

            // Iterate through all recognized barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Image: {Path.GetFileName(imagePath)}");
                Console.WriteLine($"  Type: {result.CodeTypeName}");
                Console.WriteLine($"  CodeText: {result.CodeText}");

                // If the barcode is 1D, display the detected checksum from extended parameters
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"  Detected Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }
    }
}