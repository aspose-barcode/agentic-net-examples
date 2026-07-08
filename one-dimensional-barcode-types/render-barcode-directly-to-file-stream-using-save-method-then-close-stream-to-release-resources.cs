// Title: Render barcode to file stream using Save method
// Description: Demonstrates generating a Code128 barcode and saving it directly to a file stream in PNG format.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to use BarcodeGenerator with EncodeTypes and BarCodeImageFormat to create barcodes. Typical use cases include creating barcode images for inventory, shipping labels, or product packaging, where developers need to write the output directly to a stream for further processing or storage.
// Prompt: Render barcode directly to a file stream using Save method, then close the stream to release resources.
// Tags: code128, barcode generation, save to stream, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates rendering a barcode directly to a file stream and saving it as a PNG image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, writes it to a file stream, and outputs the saved file path.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the barcode image
        string outputPath = "barcode.png";

        // Create a FileStream for writing the barcode image to disk
        using (FileStream stream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            // Initialize the barcode generator with Code128 symbology
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128))
            {
                // Set the text that will be encoded into the barcode
                generator.CodeText = "123456";

                // Save the generated barcode directly to the stream in PNG format
                generator.Save(stream, BarCodeImageFormat.Png);
            } // BarcodeGenerator disposed here

            // The FileStream will be closed automatically when exiting the using block
        } // FileStream disposed here

        // Inform the user where the barcode image has been saved
        Console.WriteLine($"Barcode saved to {Path.GetFullPath(outputPath)}");
    }
}