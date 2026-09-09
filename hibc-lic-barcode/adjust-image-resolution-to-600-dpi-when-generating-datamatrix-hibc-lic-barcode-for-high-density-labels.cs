// Title: Generate a high‑resolution DataMatrix HIBC LIC barcode (600 DPI)
// Description: Demonstrates how to create a DataMatrix HIBC LIC barcode with a 600 DPI image resolution, suitable for high‑density label printing.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on setting image resolution for barcode images. It uses the BarcodeGenerator class together with EncodeTypes to specify the HIBC DataMatrix LIC symbology. Developers often need to adjust DPI for printing quality, especially for small or high‑density labels, and this snippet shows the typical steps: selecting the symbology via reflection, configuring resolution, and saving the image.
// Prompt: Adjust the image resolution to 600 DPI when generating a DataMatrix HIBC LIC barcode for high‑density labels.
// Tags: datamatrix, hibc, lic, resolution, dpi, barcode generation, aspnet, aspose.barcode, png

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix HIBC LIC barcode image at 600 DPI.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates the output directory, resolves the HIBC DataMatrix LIC symbology,
    /// generates the barcode with the required resolution, and saves it as a PNG file.
    /// </summary>
    static void Main()
    {
        // Define and create the output directory.
        string outputDir = Path.Combine(Path.GetTempPath(), "HIBCDataMatrixDemo");
        Directory.CreateDirectory(outputDir);
        string outputPath = Path.Combine(outputDir, "DataMatrixHIBC_LIC_600dpi.png");

        // Resolve the HIBC DataMatrix LIC symbology via reflection.
        const string symbologyName = "HIBCDataMatrixLIC";
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Symbology '{symbologyName}' not found.");
            return;
        }

        // Cast the reflected field to the appropriate encode type.
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
        string codeText = "A123B456C789";

        // Generate the barcode with a 600 DPI resolution.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            generator.Parameters.Resolution = 600f; // DPI
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}