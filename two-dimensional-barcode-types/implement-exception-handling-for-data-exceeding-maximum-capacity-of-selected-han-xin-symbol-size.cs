// Title: Han Xin Barcode Generation with Exception Handling for Data Capacity
// Description: Demonstrates generating a Han Xin barcode, handling the exception when the data exceeds the selected symbol size, and falling back to automatic version selection.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on Han Xin symbology. It showcases the use of BarcodeGenerator, EncodeTypes, and HanXin parameters to control version and error level. Developers often need to manage data size limits and implement fallback strategies when a specific symbol version cannot accommodate the input.
// Prompt: Implement exception handling for data exceeding maximum capacity of selected Han Xin symbol size.
// Tags: hanxin, barcode, exception-handling, generation, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Han Xin barcode, handling cases where the
/// input data exceeds the capacity of a specific symbol version and falling back
/// to automatic version selection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode with a forced small
    /// Han Xin version, catches capacity exceptions, and retries with auto version.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a sample string that is intentionally too long for the smallest Han Xin version
        string codeText = new string('A', 500);

        // Define temporary file paths for the two output images
        string outputPathSmall = Path.Combine(Path.GetTempPath(), "HanXin_Version01.png");
        string outputPathAuto = Path.Combine(Path.GetTempPath(), "HanXin_Auto.png");

        // Initialize the barcode generator for Han Xin symbology with the sample data
        using (var generator = new BarcodeGenerator(EncodeTypes.HanXin, codeText))
        {
            // Explicitly request the smallest Han Xin version and set an error correction level
            generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Version01;
            generator.Parameters.Barcode.HanXin.ErrorLevel = HanXinErrorLevel.L4;

            try
            {
                // Attempt to generate the barcode image with the forced version
                using (var bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the generated image as PNG
                    bitmap.Save(outputPathSmall, ImageFormat.Png);
                    Console.WriteLine($"Barcode saved with Version01: {outputPathSmall}");
                }
            }
            catch (Exception ex)
            {
                // Log the failure – most likely due to data exceeding symbol capacity
                Console.WriteLine($"Failed to generate with Version01: {ex.Message}");

                // Switch to automatic version selection as a fallback
                generator.Parameters.Barcode.HanXin.Version = HanXinVersion.Auto;

                try
                {
                    // Generate the barcode image using the automatically selected version
                    using (var bitmap = generator.GenerateBarCodeImage())
                    {
                        // Save the fallback image as PNG
                        bitmap.Save(outputPathAuto, ImageFormat.Png);
                        Console.WriteLine($"Barcode generated with auto version: {outputPathAuto}");
                    }
                }
                catch (Exception ex2)
                {
                    // Log any failure that occurs during the fallback attempt
                    Console.WriteLine($"Failed to generate barcode with auto version: {ex2.Message}");
                }
            }
        }
    }
}