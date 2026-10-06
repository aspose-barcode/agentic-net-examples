// Title: Adjust XDimension for Barcode Generation and Recognition
// Description: Demonstrates how to set XDimension for a Code128 barcode and configure the reader's quality settings to use a minimal element width of 3 pixels.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating 1D barcodes, BarCodeReader for decoding them, and QualitySettings for fine‑tuning reading parameters such as XDimension. Developers commonly need to control element sizes to meet printing standards or improve scan reliability, making these APIs essential for barcode‑centric applications.
// Prompt: Adjust QualitySettings.XDimension to 3 pixels to match typical 1D barcode element widths.
// Tags: barcode, code128, xdimension, generation, recognition, qualitysettings, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates adjusting XDimension for barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode, saves it, reads it with custom XDimension settings, and outputs results.
    /// </summary>
    static void Main()
    {
        // Define a temporary file path for the generated barcode image
        string tempPath = Path.Combine(Path.GetTempPath(), "sample_barcode.png");

        // -------------------- Barcode Generation --------------------
        // Create a Code128 barcode with the text "ASPOSE"
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Optionally set the XDimension (module width) for generation; here 2 pixels is used as a baseline
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the barcode image as PNG to the temporary location
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // -------------------- Barcode Recognition --------------------
        // Initialize the reader to decode all supported barcode types from the saved image
        using (var reader = new BarCodeReader(tempPath, DecodeType.AllSupportedTypes))
        {
            // Configure quality settings to use a minimal XDimension mode
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Set the minimal element width to 3 pixels, matching typical 1D barcode specifications
            reader.QualitySettings.MinimalXDimension = 3f;

            // Perform the reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the results to the console
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // -------------------- Cleanup --------------------
        // Delete the temporary barcode image file if it still exists
        if (File.Exists(tempPath))
        {
            File.Delete(tempPath);
        }
    }
}