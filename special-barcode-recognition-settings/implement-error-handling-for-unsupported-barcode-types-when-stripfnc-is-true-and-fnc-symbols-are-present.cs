// Title: Code128 barcode generation with FNC symbols and StripFNC error handling
// Description: Demonstrates generating a Code128 barcode containing FNC symbols, reading it with StripFNC enabled or disabled, and handling errors when the operation is unsupported for a given symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the StripFNC setting to control the handling of Function (FNC) characters. Developers often need to generate barcodes with special control characters and later decide whether to retain or strip them during recognition; this snippet illustrates typical patterns and error handling for unsupported scenarios.
// Prompt: Implement error handling for unsupported barcode types when StripFNC is true and FNC symbols are present.
// Tags: barcode symbology, code128, fnc, stripfnc, error-handling, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with FNC symbols and reading with StripFNC, including error handling for unsupported types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory and define the output file path
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "Code128FNC.png");

        // Define FNC characters (using Unicode private use area as placeholders)
        const char FNC1 = '\u00F1';
        const char FNC2 = '\u00F2';
        const char FNC3 = '\u00F3';
        string codeText = "Aspose" + FNC1 + FNC2 + FNC3;

        // Generate a Code128 barcode that includes the FNC symbols
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine();

        // -----------------------------------------------------------------
        // Read the barcode with StripFNC = false (retain FNC symbols)
        // -----------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = false:");
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        Console.WriteLine();

        // -----------------------------------------------------------------
        // Read the barcode with StripFNC = true (strip FNC symbols) and
        // handle any exceptions that may arise (e.g., unsupported symbology)
        // -----------------------------------------------------------------
        Console.WriteLine("Read with StripFNC = true (error handling):");
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
            {
                reader.BarcodeSettings.StripFNC = true;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while reading with StripFNC = true: " + ex.Message);
        }

        Console.WriteLine();

        // -----------------------------------------------------------------
        // Attempt to read the same image as a QR code with StripFNC = true.
        // QR does not support FNC stripping, so this demonstrates error handling.
        // -----------------------------------------------------------------
        Console.WriteLine("Read as QR with StripFNC = true (unsupported type):");
        try
        {
            using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.QR))
            {
                reader.BarcodeSettings.StripFNC = true;
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
        }
        catch (Exception ex)
        {
            Console.WriteLine("Error while reading QR with StripFNC = true: " + ex.Message);
        }

        // -----------------------------------------------------------------
        // Cleanup temporary files and directories
        // -----------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}