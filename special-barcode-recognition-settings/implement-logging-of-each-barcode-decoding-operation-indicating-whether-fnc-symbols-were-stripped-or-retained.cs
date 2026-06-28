using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a GS1 Code128 barcode with FNC characters,
/// then reading it back with different StripFNC settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it with StripFNC on/off,
    /// outputs the results, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary file path for the barcode image
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string barcodePath = Path.Combine(tempDir, "sample_barcode.png");

        // --------------------------------------------------------------------
        // Generate a GS1 Code128 barcode that contains FNC characters
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(
            EncodeTypes.GS1Code128,
            "(02)04006664241007(37)1(400)7019590754"))
        {
            // Save the barcode image to the file system
            generator.Save(barcodePath);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was created successfully
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{barcodePath}'.");
            return;
        }

        // --------------------------------------------------------------------
        // Perform two decoding runs: one with StripFNC disabled, one with it enabled
        // --------------------------------------------------------------------
        bool[] stripFncOptions = new bool[] { false, true };
        foreach (bool stripFnc in stripFncOptions)
        {
            using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
            {
                // Configure whether FNC symbols should be stripped from the decoded text
                reader.BarcodeSettings.StripFNC = stripFnc;

                // Read all barcodes from the image
                BarCodeResult[] results = reader.ReadBarCodes();

                // Log the outcome of each decoding operation
                foreach (var result in results)
                {
                    Console.WriteLine($"StripFNC: {stripFnc}, CodeText: {result.CodeText}");
                }

                // If no barcodes were found, indicate that as well
                if (results.Length == 0)
                {
                    Console.WriteLine($"StripFNC: {stripFnc}, no barcode detected.");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up the temporary image file
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
        }
        catch
        {
            // Ignored – file deletion failure is non‑critical for this demo
        }
    }
}