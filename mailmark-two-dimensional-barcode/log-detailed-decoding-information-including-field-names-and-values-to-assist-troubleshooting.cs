// Title: Log detailed decoding information for a Code128 barcode
// Description: Generates a Code128 barcode image, reads it, and logs comprehensive decoding details such as code text, type, region, and extended info for troubleshooting.
// Category-Description: This example demonstrates Aspose.BarCode generation and recognition workflows. It uses BarcodeGenerator to create a barcode image and BarCodeReader to decode it, showcasing typical use cases like validating barcode quality, extracting metadata, and troubleshooting decoding issues. Developers working with barcode creation, scanning, or diagnostics frequently rely on these core API classes to integrate barcode functionality into .NET applications.
// Prompt: Log detailed decoding information, including field names and values, to assist troubleshooting.
// Tags: barcode, code128, generation, recognition, decoding, logging, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates how to generate a Code128 barcode, read it, and output detailed decoding information for troubleshooting purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary barcode image, decodes it, and logs all relevant fields.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a sample Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeSample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 3f;
            generator.Parameters.Barcode.BarHeight.Pixels = 50f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize the barcode reader with all supported decode types
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            // Optional: configure quality settings for more detailed output
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 2f;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle case where no barcode was detected
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
                return;
            }

            // Iterate through each detected barcode and log detailed information
            int index = 1;
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"--- Barcode #{index} ---");
                Console.WriteLine($"CodeText          : {result.CodeText}");
                Console.WriteLine($"CodeTypeName      : {result.CodeTypeName}");
                Console.WriteLine($"ReadingQuality    : {result.ReadingQuality}");

                // Log region (bounding box) details
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region X          : {bounds.X}");
                Console.WriteLine($"Region Y          : {bounds.Y}");
                Console.WriteLine($"Region Width      : {bounds.Width}");
                Console.WriteLine($"Region Height     : {bounds.Height}");
                Console.WriteLine($"Region Angle      : {result.Region.Angle}");

                // Log any extended information if available
                if (result.Extended != null)
                {
                    Console.WriteLine($"Extended Info     : {result.Extended}");
                }
                else
                {
                    Console.WriteLine("Extended Info     : None");
                }

                index++;
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}