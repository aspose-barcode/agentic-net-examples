// Title: Export DataMatrix Barcode to JPEG MemoryStream
// Description: Demonstrates exporting a DataMatrix barcode as a JPEG image into a memory stream, suitable for HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes using the BarcodeGenerator class, encode data into DataMatrix symbology, and output the result in JPEG format. Developers often need to generate barcode images on-the-fly for web APIs, embed them in HTML responses, or store them in databases. The example highlights key API classes such as BarcodeGenerator, EncodeTypes, and BarCodeImageFormat, providing a template for similar barcode export scenarios.
// Prompt: Export a DataMatrix barcode to a memory stream in JPEG format for HTTP response.
// Tags: datamatrix, export, jpeg, memorystream, barcode generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides an example of generating a DataMatrix barcode and exporting it as a JPEG image
/// into a memory stream, which can be used directly in an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode, saves it as JPEG into a
    /// memory stream, and writes the resulting byte size to the console.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the barcode.
        const string codeText = "Hello World";

        // Create a memory stream that will hold the JPEG image data.
        using (var memoryStream = new MemoryStream())
        {
            // Initialize the barcode generator with DataMatrix symbology and the sample text.
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Save the generated barcode directly into the memory stream in JPEG format.
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);
            }

            // Output the size of the generated JPEG image for verification.
            Console.WriteLine($"Generated DataMatrix JPEG size: {memoryStream.Length} bytes");
        }
    }
}