// Title: Generate MaxiCode Mode 5 barcode and save as TIFF
// Description: Demonstrates creating a MaxiCode Mode 5 barcode with custom image dimensions using Aspose.BarCode and saving it as a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex barcode types such as MaxiCode. It shows how to use the ComplexBarcodeGenerator with MaxiCodeStandardCodetext, configure image size via generator parameters, and export the result in TIFF format. Developers working with shipping labels, logistics, or retail can use similar code to produce high‑density 2‑D barcodes for scanning systems.
// Prompt: Produce a MaxiCode Mode 5 barcode, set custom image width and height, and save it as TIFF.
// Tags: maxicode, barcode generation, tiff, complexbarcode, aspose.barcode, image size

using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a MaxiCode Mode 5 barcode,
/// customizes its image dimensions, and saves it as a TIFF file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates the barcode and writes it to disk.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated TIFF image.
        string outputPath = "maxicode_mode5.tiff";

        // Prepare the MaxiCode data: select Mode 5 and provide the message to encode.
        var maxiCodeData = new MaxiCodeStandardCodetext
        {
            Mode = MaxiCodeMode.Mode5,
            Message = "Sample MaxiCode Mode5"
        };

        // Create a ComplexBarcodeGenerator using the prepared MaxiCode data.
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            // Set custom image dimensions (in points). Adjust as needed for your layout.
            generator.Parameters.ImageWidth.Point = 400f;
            generator.Parameters.ImageHeight.Point = 300f;

            // Save the generated barcode image in TIFF format to the specified path.
            generator.Save(outputPath, BarCodeImageFormat.Tiff);
        }
    }
}