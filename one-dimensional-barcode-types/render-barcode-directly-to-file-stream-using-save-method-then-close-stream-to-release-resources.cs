// Title: Render barcode to a file stream and save as PNG
// Description: Demonstrates how to generate a Code128 barcode, render it directly to a FileStream using the Save method, and store it as a PNG image file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to create barcodes programmatically. Typical use cases include generating barcodes for inventory, shipping labels, or product packaging where developers need to write barcode images directly to streams for further processing or storage.
// Prompt: Render barcode directly to a file stream using Save method, then close the stream to release resources.
// Tags: barcode, code128, generation, stream, png, aspose.barcode, save

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
    /// Entry point of the application. Generates the barcode and writes it to a PNG file via a stream.
    /// </summary>
    static void Main()
    {
        // Define the full path for the output PNG file in the temporary directory.
        string outputFile = Path.Combine(Path.GetTempPath(), "barcode_stream.png");

        // Create a FileStream for writing the barcode image; the using block ensures the stream is closed and disposed.
        using (FileStream stream = new FileStream(outputFile, FileMode.Create, FileAccess.Write))
        {
            // Initialize the barcode generator with Code128 symbology and the desired data.
            BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345678");

            // Render the barcode directly to the stream in PNG format.
            generator.Save(stream, BarCodeImageFormat.Png);
        }

        // Inform the user where the barcode image has been saved.
        Console.WriteLine($"Barcode saved to {outputFile}");
    }
}