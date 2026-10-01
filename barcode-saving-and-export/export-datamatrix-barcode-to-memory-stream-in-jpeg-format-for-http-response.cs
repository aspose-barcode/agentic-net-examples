// Title: Export DataMatrix Barcode to JPEG Memory Stream for HTTP Response
// Description: Demonstrates generating a DataMatrix barcode and exporting it as a JPEG byte array using Aspose.BarCode, suitable for sending in an HTTP response.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to create a DataMatrix symbology, configure image parameters, and save the result directly to a memory stream in JPEG format. Developers working with web APIs often need to embed barcodes in HTTP responses without writing temporary files, and this snippet shows the typical use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes for that purpose.
// Prompt: Export a DataMatrix barcode to a memory stream in JPEG format for HTTP response.
// Tags: datamatrix, barcode generation, jpeg, memory stream, http response, aspose.barcode, image export

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example of exporting a DataMatrix barcode to a JPEG byte array for HTTP responses.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample DataMatrix barcode and outputs the size of the resulting JPEG image.
    /// </summary>
    static void Main()
    {
        // Sample DataMatrix code text to encode
        string codeText = "1234567890";

        // Generate the JPEG byte array from the barcode
        byte[] jpegBytes = ExportDataMatrixToJpeg(codeText);

        // Simulate sending the JPEG in an HTTP response by writing its size to the console
        Console.WriteLine($"Generated JPEG size: {jpegBytes.Length} bytes");
    }

    /// <summary>
    /// Generates a DataMatrix barcode for the specified text and returns the JPEG image as a byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the DataMatrix barcode.</param>
    /// <returns>Byte array containing the JPEG image.</returns>
    static byte[] ExportDataMatrixToJpeg(string codeText)
    {
        // Create a memory stream to hold the JPEG image
        using (MemoryStream ms = new MemoryStream())
        {
            // Initialize the barcode generator for DataMatrix with the provided text
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
            {
                // Optional: set resolution to control JPEG size (lower DPI reduces file size)
                generator.Parameters.Resolution = 72f;

                // Optional: disable anti-aliasing for smaller output
                generator.Parameters.UseAntiAlias = false;

                // Save the barcode directly to the memory stream in JPEG format
                generator.Save(ms, BarCodeImageFormat.Jpeg);
            }

            // Reset the stream position to the beginning before reading
            ms.Position = 0;

            // Return the JPEG bytes from the memory stream
            return ms.ToArray();
        }
    }
}