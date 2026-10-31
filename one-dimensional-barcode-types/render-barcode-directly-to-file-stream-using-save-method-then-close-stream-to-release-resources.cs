// Title: Render barcode to file stream using Aspose.BarCode
// Description: Demonstrates generating a Code128 barcode and saving it directly to a file stream with the Save method, then releasing the stream.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcodes programmatically and write them to a stream. It uses BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes, common for developers who need to output barcodes to files, memory streams, or network responses without intermediate image objects. Typical use cases include generating shipping labels, tickets, or inventory tags in automated workflows.
// Prompt: Render barcode directly to a file stream using Save method, then close the stream to release resources.
// Tags: barcode generation, code128, save to stream, png, aspose.barcode, file output

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a Code128 barcode and saves it directly to a file stream.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode, writes it to a PNG file via a FileStream, and reports the saved location.
    /// </summary>
    static void Main()
    {
        // Determine the full path for the output PNG file in the current directory.
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Create a FileStream for writing the barcode image; the using block ensures the stream is closed properly.
        using (FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Initialize the barcode generator with Code128 symbology and the desired data.
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

            // Save the generated barcode directly to the stream in PNG format.
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to: {outputPath}");
    }
}