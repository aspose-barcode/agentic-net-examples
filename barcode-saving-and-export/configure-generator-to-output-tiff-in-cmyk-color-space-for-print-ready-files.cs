// Title: Generate QR Code barcode saved as CMYK TIFF for print‑ready output
// Description: Demonstrates how to configure Aspose.BarCode to generate a QR code and save it as a TIFF image in CMYK color space, suitable for high‑quality printing.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes. Typical use cases include creating print‑ready barcodes for packaging, labels, and marketing materials where CMYK color fidelity is required. Developers often need to select specific image formats and color spaces to meet print production standards.
// Prompt: Configure the generator to output TIFF in CMYK color space for print‑ready files.
// Tags: qr code, barcode generation, tiff, cmyk, print ready, aspnet, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a QR code and saves it as a CMYK TIFF image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates the barcode and writes the output path to the console.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output TIFF file in the current working directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode_cmyk.tif");

        // Initialize the barcode generator with QR code symbology and sample data.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.QR, "PrintReady"))
        {
            // Optional: configure additional generation parameters here.
            // Example: generator.Parameters.Barcode.XDimension.Point = 2.0f;

            // Save the generated barcode as a TIFF image using the CMYK color space (print‑ready).
            generator.Save(outputPath, BarCodeImageFormat.TiffInCmyk);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}