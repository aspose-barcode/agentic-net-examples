// Title: Export DataMatrix Barcode to JPEG Memory Stream
// Description: Generates a DataMatrix barcode, saves it as a JPEG into a memory stream, and outputs the image as a Base64 string for HTTP response demonstration.
// Prompt: Export a DataMatrix barcode to a memory stream in JPEG format for HTTP response.
// Tags: datamatrix, barcode-generation, export, jpeg, memorystream, aspose.barcode, aspose.barcode.generation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates how to generate a DataMatrix barcode and export it as a JPEG image
/// stored in a <see cref="MemoryStream"/>. The resulting image is shown as a Base64
/// string, which can be sent in an HTTP response.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a DataMatrix barcode, saves it to a memory
    /// stream in JPEG format, and writes the Base64 representation to the console.
    /// </summary>
    static void Main()
    {
        // Initialize a DataMatrix barcode generator with sample text.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Sample DataMatrix"))
        {
            // Let the generator automatically determine the optimal size using interpolation.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Create a memory stream to hold the JPEG image.
            using (MemoryStream memoryStream = new MemoryStream())
            {
                // Save the generated barcode into the memory stream as a JPEG.
                generator.Save(memoryStream, BarCodeImageFormat.Jpeg);

                // Reset the stream position to the beginning for reading.
                memoryStream.Position = 0;

                // Convert the JPEG bytes to a Base64 string for demonstration (e.g., HTTP response).
                string base64Image = Convert.ToBase64String(memoryStream.ToArray());
                Console.WriteLine("DataMatrix JPEG (Base64):");
                Console.WriteLine(base64Image);
            }
        }
    }
}