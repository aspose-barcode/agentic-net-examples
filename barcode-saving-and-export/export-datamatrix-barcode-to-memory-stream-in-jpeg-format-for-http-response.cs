// Title: Export DataMatrix barcode to JPEG memory stream
// Description: Demonstrates generating a DataMatrix barcode and saving it as a JPEG image into a memory stream, suitable for sending in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to create barcodes using the BarcodeGenerator class, encode data with EncodeTypes, and output images in various formats such as JPEG. Developers often need to generate barcodes on-the-fly for web APIs, embed them in HTML, or return them as binary streams in HTTP responses. The snippet shows the typical workflow of initializing the generator, saving to a MemoryStream, and retrieving the byte array.
// Prompt: Export a DataMatrix barcode to a memory stream in JPEG format for HTTP response.
// Tags: datamatrix, barcode generation, jpeg, memory stream, http response, aspose.barcode, encode types

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates exporting a DataMatrix barcode to a JPEG memory stream.
/// </summary>
class Program
{
    /// <summary>
    /// Generates the barcode and writes its size and Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // Text to encode in the DataMatrix barcode
        string codeText = "DataMatrixSample";

        // Initialize the barcode generator with DataMatrix symbology and the desired text
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Create a memory stream to hold the generated JPEG image
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the barcode image to the memory stream in JPEG format
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);

                // Reset the stream position to the beginning for subsequent reads
                memoryStream.Position = 0;

                // Output the size of the generated JPEG image
                Console.WriteLine($"Generated JPEG size: {memoryStream.Length} bytes");

                // Convert the image bytes to a Base64 string (useful for embedding in JSON or HTML)
                string base64 = Convert.ToBase64String(memoryStream.ToArray());
                Console.WriteLine($"Base64 JPEG: {base64}");
            }
        }
    }
}