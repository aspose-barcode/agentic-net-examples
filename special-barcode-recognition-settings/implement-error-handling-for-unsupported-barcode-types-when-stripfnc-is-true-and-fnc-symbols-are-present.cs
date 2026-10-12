// Title: Demonstrate StripFNC handling with supported and unsupported barcode types
// Description: Shows how to generate a Code128 barcode, read it with StripFNC enabled, and handle errors when the decode type does not support StripFNC.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It illustrates the use of BarcodeGenerator for creating barcodes and BarCodeReader with BarcodeSettings to customize reading options such as StripFNC. Typical scenarios include processing linear barcodes while ignoring Function (FNC) symbols and gracefully handling cases where the selected DecodeType cannot process StripFNC. Developers often need to combine these APIs to build robust scanning solutions that adapt to varying symbology support.
// Prompt: Implement error handling for unsupported barcode types when StripFNC is true and FNC symbols are present.
// Tags: barcode symbology, stripfnc, error handling, generation, recognition, aspose.barcode, code128, qr, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program demonstrating barcode generation and reading with StripFNC enabled,
/// including error handling for unsupported decode types.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, reads it with StripFNC set,
    /// and gracefully handles errors for unsupported barcode types.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file path for the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode (contains no FNC symbols for simplicity)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set barcode module size (X-dimension) to 2 pixels
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine("Barcode generated at: " + barcodePath);
        Console.WriteLine();

        // ------------------------------------------------------------
        // Read the barcode using a supported decode type (Code128) with StripFNC enabled
        // ------------------------------------------------------------
        Console.WriteLine("Reading with supported DecodeType (Code128) and StripFNC = true:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable stripping of FNC symbols during recognition
            reader.BarcodeSettings.StripFNC = true;
            try
            {
                // Iterate through all detected barcodes
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                // Log any unexpected errors during reading
                Console.WriteLine("Error during reading: " + ex.Message);
            }
        }

        Console.WriteLine();

        // ------------------------------------------------------------
        // Attempt to read the same image with an unsupported decode type (QR) while StripFNC is true
        // ------------------------------------------------------------
        Console.WriteLine("Reading with unsupported DecodeType (QR) and StripFNC = true:");
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Enable stripping of FNC symbols (not supported for QR)
            reader.BarcodeSettings.StripFNC = true;
            try
            {
                // Attempt to read; expect an exception due to unsupported configuration
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                // Handle the expected error gracefully
                Console.WriteLine("Handled error: " + ex.Message);
            }
        }

        // ------------------------------------------------------------
        // Cleanup temporary files and directories
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
            // Ignored - cleanup failure should not affect program exit
        }
    }
}