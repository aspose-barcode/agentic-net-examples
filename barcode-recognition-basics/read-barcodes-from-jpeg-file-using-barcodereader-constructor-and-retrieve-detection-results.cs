// Title: Read barcodes from JPEG using BarCodeReader
// Description: Demonstrates generating a Code128 barcode, saving it as a JPEG, and then reading it back with BarCodeReader to obtain detection results.
// Category-Description: This example belongs to the Aspose.BarCode barcode reading and generation category. It shows how to use BarcodeGenerator to create an image and BarCodeReader to decode all supported symbologies from a file. Developers often need to process scanned images, extract barcode data, and retrieve positional information for further processing such as inventory management or document automation.
// Prompt: Read barcodes from a JPEG file using BarCodeReader constructor and retrieve detection results.
// Tags: barcode, code128, jpeg, read, generation, aspose.barcode, barcodereader, barcodegenerator, detection, region

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that creates a barcode image, saves it as JPEG, and reads it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a Code128 barcode, writes it to a temporary JPEG file, then reads and displays detection results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeReadDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the sample JPEG image
        string imagePath = Path.Combine(tempFolder, "sample.jpg");

        // Generate a Code128 barcode and save it as a JPEG image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456789"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Jpeg);
        }

        // Verify that the image file was created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Read all supported barcodes from the JPEG file
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            Console.WriteLine("ReadFromFile:");
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output detection results or indicate that none were found
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    // Display the type and text of each detected barcode
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");

                    // Show the region (position and size) of the barcode within the image
                    var bounds = result.Region.Rectangle;
                    Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}, Angle:{result.Region.Angle}");
                }
            }
        }

        // Optional cleanup of temporary files (comment out if inspection of files is needed)
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any errors that occur during cleanup
        }
    }
}