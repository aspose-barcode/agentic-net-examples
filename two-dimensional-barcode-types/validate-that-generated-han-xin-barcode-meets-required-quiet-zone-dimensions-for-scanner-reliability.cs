// Title: Han Xin Barcode Generation with Quiet Zone Validation
// Description: Demonstrates generating a Han Xin barcode, setting a quiet zone, and verifying that the quiet zone meets scanner requirements.
// Category-Description: Shows how to use Aspose.BarCode to create and read Han Xin barcodes, configure XDimension and padding, and validate quiet zone dimensions. This example belongs to the barcode generation and recognition category, illustrating typical use cases such as setting module size, quiet zone, and confirming readability with BarCodeReader. Developers working with 2D barcodes often need to ensure compliance with scanner specifications.
// Prompt: Validate that generated Han Xin barcode meets required quiet zone dimensions for scanner reliability.
// Tags: hanxin,barcode,generation,quietzone,validation,recognition,aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a Han Xin barcode, applies a quiet zone, and validates the quiet zone size.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves it, reads it back, and checks quiet‑zone compliance.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare temporary folder and file paths
        // ------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "HanXinQuietZone_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "hanxin.png");

        // ------------------------------------------------------------
        // Define barcode parameters
        // ------------------------------------------------------------
        const string codeText = "1234567890";
        const float xDimPoints = 2f;        // Module size (X‑dimension) in points
        const float paddingPoints = 10f;    // Desired quiet zone per side in points

        // ------------------------------------------------------------
        // Generate Han Xin barcode with explicit padding (quiet zone)
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            generator.Parameters.Barcode.XDimension.Point = xDimPoints;
            generator.Parameters.Barcode.Padding.Left.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Right.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Top.Point = paddingPoints;
            generator.Parameters.Barcode.Padding.Bottom.Point = paddingPoints;

            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // Verify the barcode can be read (implies quiet zone sufficient for scanner)
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.HanXin;
        bool readSuccess = false;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            var results = reader.ReadBarCodes();
            readSuccess = results != null && results.Length > 0;
        }

        // ------------------------------------------------------------
        // Validate quiet zone dimensions against typical requirement
        // ------------------------------------------------------------
        // Typical requirement: quiet zone >= 10 * XDimension
        float requiredQuietZone = 10f * xDimPoints;
        bool quietZoneValid = paddingPoints >= requiredQuietZone;

        // ------------------------------------------------------------
        // Output results
        // ------------------------------------------------------------
        Console.WriteLine($"Barcode image saved to: {barcodePath}");
        Console.WriteLine($"Read success: {readSuccess}");
        Console.WriteLine($"XDimension (points): {xDimPoints}");
        Console.WriteLine($"Padding set (points): {paddingPoints}");
        Console.WriteLine($"Required quiet zone (points): {requiredQuietZone}");
        Console.WriteLine($"Quiet zone validation: {(quietZoneValid ? "PASS" : "FAIL")}");

        // ------------------------------------------------------------
        // Clean up temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect validation result
        }
    }
}