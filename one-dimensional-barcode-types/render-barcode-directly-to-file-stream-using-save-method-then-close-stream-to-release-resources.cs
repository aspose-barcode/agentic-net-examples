// Title: Render barcode to a file stream using Aspose.BarCode Save method
// Description: Demonstrates how to generate a Code128 barcode and write it directly to a file stream, then close the stream to release resources.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator to create barcodes and the Save method to output images to streams. Typical use cases include server‑side barcode creation for web services, batch processing, or saving to custom storage. Developers often need to work with FileStream, MemoryStream, or other streams to integrate barcode images into file systems, databases, or HTTP responses.
// Prompt: Render barcode directly to a file stream using Save method, then close the stream to release resources.
// Tags: barcode generation, code128, save to stream, png, aspose.barcode, file stream

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates rendering a Code128 barcode directly to a file stream using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a temporary directory, generates a barcode, saves it to a file via a stream, and outputs the file path.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo
        string tempDir = Path.Combine(Path.GetTempPath(), "BarcodeStreamDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Build full file path for the PNG barcode image
        string filePath = Path.Combine(tempDir, "barcode.png");

        // Open a FileStream for writing the barcode image
        using (FileStream stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            // Initialize BarcodeGenerator with Code128 symbology and data
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678"))
            {
                // Save the generated barcode to the stream in PNG format
                generator.Save(stream, BarCodeImageFormat.Png);
            } // BarcodeGenerator disposed here, releasing internal resources
        } // FileStream disposed here, closing the stream and releasing the file handle

        // Output the location of the saved barcode file
        Console.WriteLine($"Barcode saved to stream and file: {filePath}");
    }
}