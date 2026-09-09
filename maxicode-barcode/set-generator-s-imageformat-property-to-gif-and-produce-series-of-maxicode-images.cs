// Title: Generate MaxiCode barcodes and save as GIF images
// Description: Demonstrates how to create MaxiCode barcodes in different modes using Aspose.BarCode and save them as GIF files.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on MaxiCode symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to configure barcode parameters, select MaxiCode modes, and output images in GIF format. Developers working with shipping, logistics, or retail applications often need to generate MaxiCode barcodes for package tracking and inventory management.
// Prompt: Set the generator's ImageFormat property to GIF and produce a series of MaxiCode images.
// Tags: maxicode, barcode generation, gif, imageformat, aspose.barcode, encode types, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating MaxiCode barcodes in different modes and saving them as GIF images.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary output folder, defines sample data,
    /// generates MaxiCode barcodes, and saves them as GIF files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Control characters used in the structured MaxiCode format
        string gs = "\u001d"; // Group Separator
        string rs = "\u001e"; // Record Separator
        string eot = "\u0004"; // End Of Transmission

        // Define sample data: each tuple contains the MaxiCode mode, the text to encode, and the output file name
        var samples = new (MaxiCodeMode mode, string codeText, string fileName)[]
        {
            // Mode 2 with structured format (postal code, country code, service category, secondary message)
            (MaxiCodeMode.Mode2,
             $"[)>{rs}01{gs}B1050{gs}056{gs}001{gs}ADDITIONAL DATA{eot}",
             "MaxiCode_Mode2.gif"),

            // Mode 4 with arbitrary text
            (MaxiCodeMode.Mode4,
             "Sample MaxiCode Mode4",
             "MaxiCode_Mode4.gif")
        };

        // Iterate over each sample, generate the barcode, and save it as a GIF image
        foreach (var (mode, codeText, fileName) in samples)
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.MaxiCode, codeText))
            {
                // Set barcode visual properties
                generator.Parameters.Barcode.XDimension.Pixels = 15f;
                generator.Parameters.Barcode.MaxiCode.Mode = mode;

                // Build the full output path and save the image in GIF format
                string outputPath = Path.Combine(tempDir, fileName);
                generator.Save(outputPath, BarCodeImageFormat.Gif);

                Console.WriteLine($"Saved {outputPath}");
            }
        }

        Console.WriteLine("All MaxiCode images generated.");
    }
}