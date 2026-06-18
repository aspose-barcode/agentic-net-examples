using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code128 barcodes with optional checksum visibility
/// and validates the effect on the rendered image size.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a Code128 barcode image.
    /// </summary>
    /// <param name="text">The data to encode in the barcode.</param>
    /// <param name="showChecksum">If true, the checksum digit is shown in the human‑readable text.</param>
    /// <param name="outputPath">File path where the barcode image will be saved.</param>
    static void GenerateBarcode(string text, bool showChecksum, string outputPath)
    {
        // Code128 always requires a checksum; enable it explicitly.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, text))
        {
            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            // Control whether the checksum digit appears in the displayed text.
            generator.Parameters.Barcode.ChecksumAlwaysShow = showChecksum;
            // Save the generated barcode image to the specified path.
            generator.Save(outputPath);
        }
    }

    /// <summary>
    /// Entry point of the program. Generates two barcode images (with and without checksum visibility),
    /// compares their file sizes, and reports the result.
    /// </summary>
    static void Main()
    {
        // Create a temporary directory for the generated images.
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeChecksumTest");
        Directory.CreateDirectory(tempDir);

        // Define file paths for the two test images.
        string pathWithout = Path.Combine(tempDir, "barcode_no_checksum.png");
        string pathWith = Path.Combine(tempDir, "barcode_with_checksum.png");

        // Sample data to encode; checksum will be calculated automatically.
        string codeText = "1234567";

        // Generate barcode without showing the checksum digit.
        GenerateBarcode(codeText, false, pathWithout);
        // Generate barcode with the checksum digit shown.
        GenerateBarcode(codeText, true, pathWith);

        // Verify that both barcode images were successfully created.
        if (!File.Exists(pathWithout) || !File.Exists(pathWith))
        {
            Console.WriteLine("FAILED: One or both barcode images were not created.");
            return;
        }

        // Use file size as a simple proxy to detect the extra checksum character.
        long sizeWithout = new FileInfo(pathWithout).Length;
        long sizeWith = new FileInfo(pathWith).Length;

        // Expect the image with visible checksum to be larger.
        if (sizeWith > sizeWithout)
        {
            Console.WriteLine("PASSED: Checksum visibility toggles rendering (size with checksum > size without).");
        }
        else
        {
            Console.WriteLine("FAILED: Checksum visibility does not affect rendering as expected.");
        }

        // Optional cleanup of temporary files and directory.
        try
        {
            File.Delete(pathWithout);
            File.Delete(pathWith);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignored – cleanup failure should not affect test result.
        }
    }
}