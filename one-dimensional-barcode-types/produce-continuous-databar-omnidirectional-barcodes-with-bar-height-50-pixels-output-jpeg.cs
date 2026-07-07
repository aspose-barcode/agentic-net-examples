// Title: Generate DataBar Omnidirectional barcodes with custom height and JPEG output
// Description: Demonstrates creating continuous DataBar Omnidirectional barcodes using Aspose.BarCode, setting a fixed bar height of 50 pixels, and saving each barcode as a JPEG image.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to work with the BarcodeGenerator class, configure encoding parameters such as AutoSizeMode and BarHeight, and export images in common formats. Developers creating retail or logistics solutions often need to generate DataBar OmniDirectional symbols for GTIN encoding, and this snippet shows the typical steps required.
// Prompt: Produce continuous DataBar Omnidirectional barcodes with bar height 50 pixels, output JPEG.
// Tags: databar omnidirectional barcode generation jpeg aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation of DataBar Omnidirectional barcodes with a fixed bar height and JPEG output.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a set of barcodes from sample GTIN values and saves them as JPEG files.
    /// </summary>
    static void Main()
    {
        // Ensure the output directory exists
        string outputDir = "Output";
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Sample GTIN values for DataBar OmniDirectional encoding
        string[] gtinValues = new[]
        {
            "(01)12345678901231",
            "(01)12345678901232",
            "(01)12345678901233",
            "(01)12345678901234",
            "(01)12345678901235"
        };

        // Iterate over each GTIN value and generate a barcode
        for (int i = 0; i < gtinValues.Length; i++)
        {
            // Initialize the generator with the OmniDirectional symbology and current GTIN
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, gtinValues[i]))
            {
                // Disable automatic sizing to use explicit dimensions
                generator.Parameters.AutoSizeMode = AutoSizeMode.None;

                // Set the bar height to 50 pixels
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Define the output file path
                string filePath = Path.Combine(outputDir, $"databar_omni_{i + 1}.jpg");

                // Save the barcode as a JPEG image
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }
        }
    }
}