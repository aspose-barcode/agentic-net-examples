// Title: Specify Target Region for Barcode Recognition
// Description: Demonstrates how to limit barcode detection to a rectangular area of an image using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It shows how to use the BarCodeReader class with a specified Rectangle to focus detection on a particular region of an image. Typical use cases include processing large images where only a portion contains barcodes, improving performance and accuracy. Developers often generate barcodes with BarcodeGenerator and then recognize them with BarCodeReader, optionally constraining the search area.
// Prompt: Specify a rectangular target region before recognition to limit barcode detection to a defined area of the image.
// Tags: qr code, barcode recognition, target region, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a QR code and reads it only within a defined rectangular region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a QR code, defines a target region, and reads barcodes within that region.
    /// </summary>
    static void Main()
    {
        // Create a temporary working folder for the demo files
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeRegionDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(workFolder, "sample.png");

        // Generate a QR Code barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Load the image to obtain its dimensions and define a target region (top-left quarter)
        using (Bitmap bmp = new Bitmap(barcodePath))
        {
            int regionWidth = bmp.Width / 2;
            int regionHeight = bmp.Height / 2;
            Rectangle targetRegion = new Rectangle(0, 0, regionWidth, regionHeight);

            // Initialize the reader to scan only within the specified region for QR codes
            using (BarCodeReader reader = new BarCodeReader(bmp, targetRegion, DecodeType.QR))
            {
                Console.WriteLine("Reading barcodes within the defined region:");
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}