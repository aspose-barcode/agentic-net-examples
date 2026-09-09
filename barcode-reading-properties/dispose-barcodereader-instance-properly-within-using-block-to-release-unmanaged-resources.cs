// Title: Demonstrate proper disposal of BarCodeReader with using block
// Description: This example generates a Code128 barcode image, reads it using BarCodeReader, and ensures unmanaged resources are released by disposing the reader within a using statement.
// Category-Description: Shows how to work with Aspose.BarCode for barcode generation and recognition, focusing on resource management. The example uses BarcodeGenerator, BarCodeReader, and related classes to create, read, and clean up barcode images—common tasks for developers integrating barcode scanning into .NET applications.
// Prompt: Dispose BarCodeReader instance properly within a using block to release unmanaged resources.
// Tags: barcode symbology, generation, recognition, resource management, using, aspose.barcode, code128, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Code128 barcode, reads it, and cleans up resources.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, reads it using BarCodeReader inside a using block, and deletes temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        // Define the full path for the barcode image file
        string imagePath = Path.Combine(tempDir, "code128.png");

        // Generate a Code128 barcode image and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode from the image using BarCodeReader inside a using block to ensure proper disposal
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            var results = reader.ReadBarCodes();
            foreach (var result in results)
            {
                Console.WriteLine($"Detected {result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Attempt to delete the temporary image file and directory; ignore any errors
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored
        }
    }
}