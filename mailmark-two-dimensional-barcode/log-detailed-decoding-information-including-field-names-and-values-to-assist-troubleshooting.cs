// Title: Generate and Decode a Code128 Barcode with Detailed Logging
// Description: This example creates a Code128 barcode image, saves it to a temporary folder, then reads and logs comprehensive decoding information for troubleshooting.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, covering BarcodeGenerator, BarCodeReader, and detailed result inspection via reflection. Useful for developers needing to create barcodes, decode them, and extract extended metadata for debugging or analytics. Typical use cases include inventory systems, shipping labels, and quality assurance of barcode scans.
// Prompt: Log detailed decoding information, including field names and values, to assist troubleshooting.
// Tags: code128, barcode generation, barcode decoding, detailed logging, aspose.barcode, reflection, c#

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, decoding, and detailed logging using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, decodes it, and logs all available information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string imagePath = Path.Combine(tempFolder, "sample.png");

        // Generate a Code128 barcode image and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;   // Set module width
            generator.Parameters.Barcode.BarHeight.Pixels = 50f; // Set bar height
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at {imagePath}");
            return;
        }

        // Initialize a reader to decode all supported barcode types from the image
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Optional: configure high‑performance quality settings
            reader.QualitySettings = QualitySettings.HighPerformance;

            // Iterate through all detected barcode results
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== Barcode Result ===");
                Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");

                // Log the region (position and size) of the detected barcode
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region: X={rect.X}, Y={rect.Y}, Width={rect.Width}, Height={rect.Height}");
                Console.WriteLine($"Angle: {result.Region.Angle}");

                // Use reflection to log any extended properties provided by the result
                var ext = result.Extended;
                if (ext != null)
                {
                    PropertyInfo[] extProps = ext.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    foreach (PropertyInfo prop in extProps)
                    {
                        try
                        {
                            object value = prop.GetValue(ext);
                            if (value != null)
                            {
                                Console.WriteLine($"Extended.{prop.Name}: {value}");
                            }
                        }
                        catch
                        {
                            // Silently ignore properties that cannot be read
                        }
                    }
                }
            }
        }

        // Attempt to clean up the temporary folder and its contents
        try
        {
            if (Directory.Exists(tempFolder))
            {
                Directory.Delete(tempFolder, true);
            }
        }
        catch
        {
            // Suppress any errors during cleanup to avoid breaking the example flow
        }
    }
}