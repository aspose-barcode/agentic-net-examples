// Title: PDF417 Barcode Generation and Linked Segment Detection
// Description: Demonstrates generating a PDF417 barcode with the IsLinked flag set and reading the barcode to verify the extended IsLinked parameter.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, focusing on PDF417 symbology. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding, and accessing extended PDF417 parameters such as IsLinked. Developers working with PDF417 often need to control and inspect segment linking for multi-part data encoding.
// Prompt: Access PDF417 extended parameters to check if the barcode is linked to another segment.
// Tags: pdf417, barcode, generation, recognition, extended-parameters, islinked, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to generate a PDF417 barcode with the IsLinked flag enabled,
/// then read the barcode and inspect the extended IsLinked parameter.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it, and outputs the IsLinked status.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417LinkedDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "barcode.png");

        // Generate a PDF417 barcode with the IsLinked property set to true.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "SampleText"))
        {
            // Set the X-dimension (module width) in pixels.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Enable linking of this barcode segment to the next one.
            generator.Parameters.Barcode.Pdf417.IsLinked = true;
            // Save the barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image and display the extended IsLinked parameter.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Pdf417))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"IsLinked: {result.Extended.Pdf417.IsLinked}");
            }
        }

        // Clean up temporary files (optional). Errors during cleanup are ignored.
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored
        }
    }
}