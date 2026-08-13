// Title: Generate Continuous DataBar Omnidirectional Barcodes as JPEG Images
// Description: Demonstrates how to create multiple DataBar Omnidirectional barcodes with a fixed bar height and save them as JPEG files using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating the use of the BarcodeGenerator class with EncodeTypes.DatabarOmniDirectional. Developers commonly generate DataBar symbols for retail and inventory applications, adjusting parameters such as bar height, X‑dimension, and output image format. The snippet shows typical steps: setting up the generator, configuring barcode parameters, and saving the image.
// Prompt: Produce continuous DataBar Omnidirectional barcodes with bar height 50 pixels, output JPEG.
// Tags: databar, omnidirectional, barcode, generation, jpeg, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a series of DataBar Omnidirectional barcodes
/// and saves each as a JPEG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Creates an output folder, generates five
    /// barcodes with a fixed height, and writes them to JPEG files.
    /// </summary>
    static void Main()
    {
        // Define the output directory for generated barcode images
        string outputDir = "Barcodes";

        // Ensure the output directory exists
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Loop to generate 5 distinct DataBar Omnidirectional barcodes
        for (int i = 0; i < 5; i++)
        {
            // Sample GTIN code text for DataBar symbology; the last digit varies per iteration
            string codeText = $"(01)1234567890123{i}";

            // Initialize the barcode generator with the desired symbology and text
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, codeText))
            {
                // Set the bar height to 50 pixels (AutoSizeMode is None by default)
                generator.Parameters.Barcode.BarHeight.Pixels = 50f;

                // Optionally adjust the X-dimension for better visual scaling
                generator.Parameters.Barcode.XDimension.Pixels = 2f;

                // Build the full file path for the JPEG output
                string filePath = Path.Combine(outputDir, $"databar_omni_{i}.jpg");

                // Save the generated barcode image as a JPEG file
                generator.Save(filePath, BarCodeImageFormat.Jpeg);
            }
        }

        // Inform the user that the process completed successfully
        Console.WriteLine("Barcode images generated successfully.");
    }
}