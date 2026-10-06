// Title: Generate and Read a Code128 Barcode with Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode image, saving it to a temporary PNG file, and then reading the barcode to display its type and value.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing the use of BarcodeGenerator for barcode creation and BarCodeReader for barcode detection. Developers commonly use these APIs to produce barcodes for labeling, inventory, and to validate scanned data in applications.
// Prompt: Dispose BarCodeReader instance properly within a using block to release unmanaged resources.
// Tags: barcode symbology, generation, recognition, code128, png, aspose.barcode, barcodereader, barcodelibrary

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and recognition using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, saves it, reads it, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Full path for the generated PNG file
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file exists before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using BarCodeReader inside a using block to ensure proper disposal
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Output each detected barcode's type and text
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                }
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program outcome
        }
    }
}