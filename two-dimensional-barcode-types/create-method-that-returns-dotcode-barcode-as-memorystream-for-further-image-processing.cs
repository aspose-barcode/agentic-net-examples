// Title: Generate DotCode barcode and return as MemoryStream
// Description: Demonstrates creating a DotCode barcode from a text string, saving it to a MemoryStream in PNG format, and persisting the image to a file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class with EncodeTypes.DotCode. Developers often need to generate barcodes for inventory, tracking, or authentication purposes and then process the image further (e.g., embed in PDFs, send over network). The code illustrates configuring barcode parameters, exporting to a MemoryStream, and handling the stream for downstream operations, a common pattern in automated CI pipelines.
// Prompt: Create method that returns DotCode barcode as MemoryStream for further image processing.
// Tags: dotcode, barcode, generation, memorystream, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Provides an entry point that generates a DotCode barcode, stores it in a <see cref="MemoryStream"/>,
/// and saves the resulting image to disk for demonstration purposes.
/// </summary>
class Program
{
    /// <summary>
    /// Main execution method. Generates a DotCode barcode, writes the image to a file,
    /// and outputs the file location to the console.
    /// </summary>
    static void Main()
    {
        // Sample text to encode in the DotCode barcode
        string sampleText = "Hello DotCode";

        // Generate the barcode image as a MemoryStream
        MemoryStream barcodeStream = GenerateDotCodeBarcode(sampleText);

        // Define the output file path (current directory)
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "dotcode.png");

        // Write the MemoryStream contents to a physical PNG file
        using (FileStream file = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            barcodeStream.CopyTo(file);
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine($"DotCode barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Generates a DotCode barcode image and returns it as a <see cref="MemoryStream"/>.
    /// </summary>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <returns>A <see cref="MemoryStream"/> containing the PNG image of the barcode.</returns>
    static MemoryStream GenerateDotCodeBarcode(string codeText)
    {
        // Initialize a memory stream to receive the barcode image
        MemoryStream ms = new MemoryStream();

        // Create and configure the barcode generator for DotCode symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            // Set the number of columns; rows are calculated automatically
            generator.Parameters.Barcode.DotCode.Columns = 20;

            // Enable UTF-8 ECI encoding to support Unicode characters
            generator.Parameters.Barcode.DotCode.ECIEncoding = ECIEncodings.UTF8;

            // Save the generated barcode to the memory stream in PNG format
            generator.Save(ms, BarCodeImageFormat.Png);
        }

        // Reset the stream position to the beginning for downstream reading
        ms.Position = 0;
        return ms;
    }
}