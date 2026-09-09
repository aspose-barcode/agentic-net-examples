// Title: Configure XDimension for Ultra‑Fine One‑Pixel Barcodes
// Description: Demonstrates generating a Code128 barcode with a 1‑pixel XDimension and configuring the reader to recognize such ultra‑fine barcodes accurately.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes, BarCodeReader to decode them, and QualitySettings to fine‑tune recognition parameters such as XDimension. Developers working with high‑density or low‑resolution barcodes often need to adjust these settings to ensure reliable scanning.
// Prompt: Configure QualitySettings.XDimension to 1 pixel for recognizing ultra‑fine one‑pixel wide barcodes accurately.
// Tags: barcode, symbology, generation, recognition, xdimension, code128, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a barcode with a 1‑pixel XDimension and
/// configure the reader to recognize ultra‑fine barcodes accurately.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it using minimal XDimension settings,
    /// and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a Code128 barcode with an XDimension of 1 pixel
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 1f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using minimal XDimension settings (1 pixel)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            BarCodeResult[] results = reader.ReadBarCodes();

            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup is not critical for the demo
        }
    }
}