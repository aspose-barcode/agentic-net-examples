// Title: Han Xin Barcode Generation with Capacity Exception Handling
// Description: Demonstrates generating a Han Xin barcode, handling exceptions when the data exceeds the selected symbol size, and falling back to automatic version selection.
// Category-Description: This example belongs to the Aspose.BarCode generation category, focusing on Han Xin symbology. It showcases the use of BarcodeGenerator, HanXinVersion, HanXinErrorLevel, and HanXinEncodeMode classes to create barcodes, handle capacity limits, and implement fallback strategies—common tasks for developers integrating high‑density 2D barcodes into applications.
// Prompt: Implement exception handling for data exceeding maximum capacity of selected Han Xin symbol size.
// Tags: hanxin, barcode, exception handling, capacity, aspose.barcode, generation, png, error handling

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Han Xin barcode, detects capacity overflow,
/// and retries with automatic version selection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode with a small fixed version,
    /// catches capacity exceptions, and retries using automatic version selection.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary output directory for generated images
        string outputDir = Path.Combine(Path.GetTempPath(), "HanXinDemo");
        Directory.CreateDirectory(outputDir);

        // Create a sample text that likely exceeds the capacity of the smallest Han Xin version
        string codeText = new string('A', 5000);

        // Path for the barcode image using a small fixed version
        string smallVersionPath = Path.Combine(outputDir, "HanXin_Version01.png");

        try
        {
            // Initialize the barcode generator with Han Xin symbology and the sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
            {
                // Configure a small fixed version to force a capacity check
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Version01;
                generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L1;
                generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

                // Generate the barcode image and save it as PNG
                using (Bitmap img = generator.GenerateBarCodeImage())
                {
                    img.Save(smallVersionPath, ImageFormat.Png);
                }
            }

            Console.WriteLine($"Barcode generated with Version01 and saved to: {smallVersionPath}");
        }
        catch (Exception ex)
        {
            // Handle capacity overflow or other generation errors
            Console.WriteLine("Failed to generate barcode with Version01:");
            Console.WriteLine(ex.Message);
            Console.WriteLine("Attempting generation with automatic version selection...");

            // Path for the barcode image using automatic version selection
            string autoVersionPath = Path.Combine(outputDir, "HanXin_Auto.png");

            try
            {
                // Reinitialize the generator for automatic version selection
                using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
                {
                    generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;
                    generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L4;
                    generator.Parameters.Barcode.HanXin.EncodeMode = HanXinEncodeMode.Auto;

                    // Generate the barcode image and save it as PNG
                    using (Bitmap img = generator.GenerateBarCodeImage())
                    {
                        img.Save(autoVersionPath, ImageFormat.Png);
                    }
                }

                Console.WriteLine($"Barcode generated with automatic version and saved to: {autoVersionPath}");
            }
            catch (Exception ex2)
            {
                // Report failure of the fallback attempt
                Console.WriteLine("Failed to generate barcode with automatic version as well:");
                Console.WriteLine(ex2.Message);
            }
        }
    }
}