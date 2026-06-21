using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, reading it with default settings,
/// then reading it again with <c>AllowIncorrectBarcodes</c> enabled to compare confidence values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary file path for the barcode image.
        // --------------------------------------------------------------------
        string tempPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // --------------------------------------------------------------------
        // 2. Generate a correct Code128 barcode and save it to the temporary file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // 3. Verify the file was created successfully.
        // --------------------------------------------------------------------
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // 4. Read the barcode with default settings (AllowIncorrectBarcodes = false).
        // --------------------------------------------------------------------
        BarCodeResult resultDefault;
        using (var readerDefault = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            // No change to AllowIncorrectBarcodes; default is false.
            resultDefault = GetFirstResult(readerDefault);
        }

        // --------------------------------------------------------------------
        // 5. Read the same barcode with AllowIncorrectBarcodes set to true.
        // --------------------------------------------------------------------
        BarCodeResult resultAllowIncorrect;
        using (var readerAllow = new BarCodeReader(tempPath, DecodeType.Code128))
        {
            // Enable reading of potentially incorrect barcodes.
            readerAllow.QualitySettings.AllowIncorrectBarcodes = true;
            resultAllowIncorrect = GetFirstResult(readerAllow);
        }

        // --------------------------------------------------------------------
        // 6. Output confidence values for comparison.
        // --------------------------------------------------------------------
        if (resultDefault != null && resultAllowIncorrect != null)
        {
            Console.WriteLine($"Confidence (default settings): {resultDefault.Confidence}");
            Console.WriteLine($"Confidence (AllowIncorrectBarcodes = true): {resultAllowIncorrect.Confidence}");

            if (resultDefault.Confidence == resultAllowIncorrect.Confidence)
            {
                Console.WriteLine("Confidence is unchanged when AllowIncorrectBarcodes is enabled.");
            }
            else
            {
                Console.WriteLine("Confidence differs; check engine behavior.");
            }
        }
        else
        {
            Console.WriteLine("Barcode could not be read in one of the attempts.");
        }

        // --------------------------------------------------------------------
        // 7. Clean up the temporary file.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }

    /// <summary>
    /// Reads the first barcode result from the provided <see cref="BarCodeReader"/>.
    /// </summary>
    /// <param name="reader">The barcode reader instance.</param>
    /// <returns>The first <see cref="BarCodeResult"/> found, or <c>null</c> if none.</returns>
    private static BarCodeResult GetFirstResult(BarCodeReader reader)
    {
        foreach (var result in reader.ReadBarCodes())
        {
            return result; // Return the first detected barcode.
        }

        return null; // No barcode detected.
    }
}