// Title: Generate MaxiCode Mode 5 barcode and save as TIFF
// Description: Demonstrates creating a MaxiCode Mode 5 barcode, customizing its image size, and exporting it to a TIFF file.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and MaxiCodeMode classes to produce high‑density 2‑D barcodes, a common requirement for shipping and logistics applications where compact data encoding and error correction are needed. Developers often need to adjust image dimensions and output formats, which this sample illustrates.
// Prompt: Produce a MaxiCode Mode 5 barcode, set custom image width and height, and save it as TIFF.
// Tags: maxicode, mode5, barcode generation, image size, tiff, aspose.barcode

using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Entry point for the MaxiCode Mode 5 barcode generation example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a MaxiCode Mode 5 barcode with custom dimensions and saves it as a TIFF image.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for the MaxiCode symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.MaxiCode))
        {
            // Assign the data to be encoded (standard test message for Mode 5)
            generator.CodeText = "Test message";

            // Configure the generator to use MaxiCode Mode 5 (data with long ECC correction)
            generator.Parameters.Barcode.MaxiCode.MaxiCodeMode = MaxiCodeMode.Mode5;

            // Set custom image dimensions (width and height in points)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode as a TIFF file
            generator.Save("maxicode_mode5.tiff");
        }
    }
}