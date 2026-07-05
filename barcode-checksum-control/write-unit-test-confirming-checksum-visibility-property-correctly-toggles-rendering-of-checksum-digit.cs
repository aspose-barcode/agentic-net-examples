// Title: Checksum Visibility Toggle Test
// Description: Demonstrates generating a Code128 barcode with checksum enabled and verifying that the ChecksumAlwaysShow property controls whether the checksum digit appears in the decoded text.
// Prompt: Write a unit test confirming the checksum visibility property correctly toggles rendering of the checksum digit.
// Tags: barcode, checksum, code128, unit-test, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates two Code128 barcodes—one with the checksum digit displayed
/// and one without—and validates that the checksum visibility setting affects the decoded text length.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, reads them back, and checks the checksum visibility effect.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for barcode images
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeChecksumTest");
        Directory.CreateDirectory(outputDir);

        // Input data (without checksum)
        string codeText = "12345";

        // Paths for the two test images
        string pathShow = Path.Combine(outputDir, "code128_show.png");
        string pathHide = Path.Combine(outputDir, "code128_hide.png");

        // Generate barcode with checksum always shown
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;
            generator.Save(pathShow);
        }

        // Generate barcode with checksum not shown
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = false;
            generator.Save(pathHide);
        }

        // Read first barcode and get displayed text length (checksum shown)
        int lengthShow = -1;
        using (BarCodeReader reader = new BarCodeReader(pathShow, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                lengthShow = result.CodeText?.Length ?? 0;
                break; // only need first result
            }
        }

        // Read second barcode and get displayed text length (checksum hidden)
        int lengthHide = -1;
        using (BarCodeReader reader = new BarCodeReader(pathHide, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                lengthHide = result.CodeText?.Length ?? 0;
                break; // only need first result
            }
        }

        // Verify that the length with checksum shown is greater than without
        if (lengthShow > lengthHide && lengthHide > 0)
        {
            Console.WriteLine("PASSED: Checksum visibility toggles rendering correctly.");
            Console.WriteLine($"Length with checksum shown: {lengthShow}");
            Console.WriteLine($"Length without checksum shown: {lengthHide}");
        }
        else
        {
            Console.WriteLine("FAILED: Checksum visibility did not affect rendered text as expected.");
            Console.WriteLine($"Length with checksum shown: {lengthShow}");
            Console.WriteLine($"Length without checksum shown: {lengthHide}");
        }

        // Cleanup generated files (optional)
        try
        {
            File.Delete(pathShow);
            File.Delete(pathHide);
            Directory.Delete(outputDir);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}