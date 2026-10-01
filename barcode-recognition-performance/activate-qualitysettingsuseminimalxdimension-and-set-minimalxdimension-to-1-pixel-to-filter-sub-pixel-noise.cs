// Title: Filter Sub‑Pixel Noise Using Minimal X Dimension in Barcode Recognition
// Description: Demonstrates how to enable the minimal X‑dimension mode and set it to 1 pixel when reading a Code128 barcode, reducing sub‑pixel noise.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of QualitySettings to improve reading accuracy. It showcases key classes such as BarCodeReader, QualitySettings, and XDimensionMode, typical for scenarios where image quality varies and developers need to fine‑tune detection parameters. Useful for developers implementing robust barcode scanning in .NET applications.
// Prompt: Activate QualitySettings.UseMinimalXDimension and set MinimalXDimension to 1 pixel to filter sub‑pixel noise.
// Tags: barcode, code128, minimalxdimension, subpixel noise, qualitysettings, barcoderecognition, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, then reads it using
/// QualitySettings to filter sub‑pixel noise by activating minimal X dimension mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it with
    /// specific quality settings, outputs the decoded information, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a simple Code128 barcode and save it as a PNG file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize the barcode reader with the generated image and specify the expected symbology
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Apply a performance preset that balances speed and accuracy
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Activate minimal X dimension mode and set the minimal dimension to 1 pixel
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Decode all barcodes found in the image and output their details
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }

        // Attempt to delete the temporary files and folder; ignore any errors
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Cleanup errors are non‑critical for this demo
        }
    }
}