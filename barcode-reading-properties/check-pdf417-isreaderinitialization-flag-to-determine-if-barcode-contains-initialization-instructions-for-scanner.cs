// Title: PDF417 Barcode Reader Initialization Flag Demo
// Description: Demonstrates how to generate a PDF417 barcode with the IsReaderInitialization flag set and how to read that flag back using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, focusing on PDF417 symbology. It shows usage of BarcodeGenerator, BarCodeReader, and related parameter classes to embed and detect scanner initialization instructions, a common requirement for automated scanning systems. Developers looking for guidance on PDF417 configuration and flag inspection will find this pattern useful.
// Prompt: Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization instructions for the scanner.
// Tags: pdf417, barcode, initialization flag, generation, recognition, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a PDF417 barcode with the IsReaderInitialization flag and reading it back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, displays the flag, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417InitDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "pdf417.png");

        // Generate a PDF417 barcode with the IsReaderInitialization flag enabled
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Aspose"))
        {
            // Set barcode dimensions (pixel size of X-dimension)
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Enable the initialization flag so the barcode contains scanner instructions
            generator.Parameters.Barcode.Pdf417.IsReaderInitialization = true;
            // Save the barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and inspect the IsReaderInitialization flag
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsReaderInitialization: {result.Extended.Pdf417.IsReaderInitialization}");
            }
        }

        // Cleanup temporary files and folder (optional)
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors that occur during cleanup
        }
    }
}