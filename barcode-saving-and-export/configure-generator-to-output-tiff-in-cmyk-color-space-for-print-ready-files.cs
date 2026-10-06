// Title: Generate Code128 Barcode and Save as CMYK TIFF
// Description: Demonstrates creating a Code128 barcode and saving it as a TIFF image in CMYK color space, suitable for print‑ready output.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use the BarcodeGenerator class to encode data, configure image output, and save barcodes in various formats. Typical use cases include creating print‑ready barcodes for packaging, labels, and documents where CMYK color fidelity is required. Developers often need to select the appropriate BarCodeImageFormat and color space to meet publishing standards.
// Prompt: Configure the generator to output TIFF in CMYK color space for print‑ready files.
// Tags: code128, barcode, generation, tiff, cmyk, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it as a CMYK TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates an output folder, generates the barcode, and writes the image file.
    /// </summary>
    static void Main()
    {
        // Create a temporary output directory for the generated barcode image
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeOutput");
        Directory.CreateDirectory(outputDir);

        // Build the full file path for the TIFF image
        string outputPath = Path.Combine(outputDir, "barcode_cmyk.tif");

        // Initialize the barcode generator with Code128 symbology and the desired data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
        {
            // Save the barcode as a TIFF image using the CMYK color space (print‑ready)
            generator.Save(outputPath, BarCodeImageFormat.TiffInCmyk);
        }

        // Inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}