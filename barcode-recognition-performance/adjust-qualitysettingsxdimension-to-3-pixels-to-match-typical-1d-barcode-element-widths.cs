// Title: Adjust XDimension for 1D Barcode Generation and Recognition
// Description: Demonstrates setting XDimension to 3 pixels for a Code128 barcode and configuring the reader's quality settings to match, ensuring accurate detection of typical 1D barcode element widths.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers often need to control XDimension to align with printing or scanning requirements, and the QualitySettings API helps fine‑tune recognition parameters for reliable results.
// Prompt: Adjust QualitySettings.XDimension to 3 pixels to match typical 1D barcode element widths.
// Tags: barcode, code128, generation, recognition, xdimension, qualitysettings, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates adjusting XDimension for 1D barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a Code128 barcode with a 3‑pixel XDimension, reads it using matching quality settings, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "barcode.png");

        // Generate a simple Code128 barcode with XDimension set to 3 pixels
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ASPOSE"))
        {
            // Typical XDimension for 1D barcodes (3 pixels)
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode using QualitySettings that match the generated XDimension
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Configure the reader to use a minimal X dimension of 3 pixels
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 3f;

            // Perform barcode detection
            BarCodeResult[] results = reader.ReadBarCodes();
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}