// Title: StripFNC handling with unsupported barcode types
// Description: Demonstrates generating a Code128 barcode that contains FNC symbols, reading it with StripFNC enabled, and handling errors when the barcode type is unsupported.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and the StripFNC setting to remove function characters during recognition. Developers often need to strip FNC symbols when processing linear barcodes and must gracefully handle cases where the chosen symbology does not support this operation.
// Prompt: Implement error handling for unsupported barcode types when StripFNC is true and FNC symbols are present.
// Tags: barcode symbology, stripfnc, error handling, code128, qr, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates barcode generation with FNC symbols, reading with StripFNC enabled,
/// and error handling for unsupported symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with StripFNC,
    /// and shows error handling when an unsupported decode type is used.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "Code128FNC.png");

        // --------------------------------------------------------------------
        // Define FNC characters (ASCII 241‑243 correspond to FNC1‑3).
        // --------------------------------------------------------------------
        string fnc1 = ((char)241).ToString();
        string fnc2 = ((char)242).ToString();
        string fnc3 = ((char)243).ToString();

        // --------------------------------------------------------------------
        // Generate a Code128 barcode that includes the FNC symbols.
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose" + fnc1 + fnc2 + fnc3))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Read the barcode using the correct symbology (Code128) with StripFNC enabled.
        // This should succeed and return the text without the FNC characters.
        // --------------------------------------------------------------------
        Console.WriteLine("Reading with correct symbology (Code128) and StripFNC = true:");
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = true;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
        Console.WriteLine();

        // --------------------------------------------------------------------
        // Attempt to read the same image with an unsupported symbology (QR) while
        // StripFNC is true. The operation is wrapped in a try/catch to demonstrate
        // error handling for this scenario.
        // --------------------------------------------------------------------
        Console.WriteLine("Attempting to read with unsupported symbology (QR) and StripFNC = true:");
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
            Console.WriteLine("Error while reading barcode with unsupported type and StripFNC = true:");
            Console.WriteLine(ex.Message);
        }

        // --------------------------------------------------------------------
        // Cleanup temporary files and directories.
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}