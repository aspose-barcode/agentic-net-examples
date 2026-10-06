// Title: Barcode generation with FNC symbols and decoding with optional stripping
// Description: Demonstrates creating a Code128 barcode that includes FNC1‑FNC3 characters, then decoding it twice—once retaining the FNC symbols and once stripping them.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and BarcodeSettings.StripFNC to control FNC symbol handling. Developers working with GS1‑compliant barcodes or needing to preserve/ignore function characters will find this pattern useful for testing and integration.
// Prompt: Implement logging of each barcode decoding operation, indicating whether FNC symbols were stripped or retained.
// Tags: barcode, code128, fnc, stripfnc, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a Code128 barcode containing FNC1‑FNC3 characters,
/// then reads the barcode twice to demonstrate the effect of the
/// <c>StripFNC</c> setting on the decoded text.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode creation, two decoding passes,
    /// and cleanup of temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary directory and file path for the barcode image.
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeFncDemo");
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "Code128FNC.png");

        // ---------------------------------------------------------------
        // Build the barcode text containing FNC1, FNC2, and FNC3 symbols.
        // ---------------------------------------------------------------
        string fnc1 = ((char)200).ToString(); // FNC1
        string fnc2 = ((char)201).ToString(); // FNC2
        string fnc3 = ((char)202).ToString(); // FNC3
        string codeText = "Aspose" + fnc1 + fnc2 + fnc3;

        // ---------------------------------------------------------------
        // Generate the barcode image using BarcodeGenerator.
        // ---------------------------------------------------------------
        using (BarcodeGenerator gen = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            gen.Parameters.Barcode.XDimension.Pixels = 2;
            gen.Save(imagePath, BarCodeImageFormat.Png);
        }

        // ---------------------------------------------------------------
        // Verify that the image was created successfully.
        // ---------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ---------------------------------------------------------------
        // Decode the barcode while retaining FNC symbols (StripFNC = false).
        // ---------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = false:");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"StripFNC: false");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                bool fncPresent = result.CodeText.Contains("<FNC1>") ||
                                  result.CodeText.Contains("<FNC2>") ||
                                  result.CodeText.Contains("<FNC3>");
                Console.WriteLine($"FNC symbols retained: {fncPresent}");
            }
        }

        // ---------------------------------------------------------------
        // Decode the barcode while stripping FNC symbols (StripFNC = true).
        // ---------------------------------------------------------------
        Console.WriteLine("\nRead with StripFNC = true:");
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"StripFNC: true");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                bool fncPresent = result.CodeText.Contains("<FNC1>") ||
                                  result.CodeText.Contains("<FNC2>") ||
                                  result.CodeText.Contains("<FNC3>");
                Console.WriteLine($"FNC symbols retained: {fncPresent}");
            }
        }

        // ---------------------------------------------------------------
        // Clean up temporary files and directory.
        // ---------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors.
        }
    }
}