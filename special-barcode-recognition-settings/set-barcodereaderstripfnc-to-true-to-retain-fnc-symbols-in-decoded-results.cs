using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode containing an FNC1 character,
/// then reading it with and without stripping FNC symbols.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, reads it under two different settings,
    /// and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary file path for the barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.GetTempPath();
        string barcodePath = Path.Combine(tempDir, "sample_code128.png");

        // --------------------------------------------------------------------
        // Build the Code128 text that includes an FNC1 character (ASCII 241).
        // --------------------------------------------------------------------
        string codeText = "ABC" + ((char)241).ToString() + "123";

        // --------------------------------------------------------------------
        // Generate the barcode image and save it as PNG.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode without stripping FNC symbols (default behavior).
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Without StripFNC (default):");
                Console.WriteLine($"  CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Read the barcode with StripFNC set to true to retain FNC symbols.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable retaining FNC symbols in the decoded result.
            reader.BarcodeSettings.StripFNC = true;

            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("With StripFNC = true:");
                Console.WriteLine($"  CodeText: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up the temporary image file.
        // --------------------------------------------------------------------
        if (File.Exists(barcodePath))
        {
            try
            {
                File.Delete(barcodePath);
            }
            catch
            {
                // Ignore any errors that occur during cleanup.
            }
        }
    }
}