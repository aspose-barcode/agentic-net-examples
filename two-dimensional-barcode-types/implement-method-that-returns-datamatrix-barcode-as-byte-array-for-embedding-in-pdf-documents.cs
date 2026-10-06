// Title: Generate DataMatrix barcode as byte array
// Description: Demonstrates how to create a DataMatrix barcode image and return it as a byte array, suitable for embedding in PDF documents.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator, EncodeTypes, and BarCodeImageFormat classes to produce bitmap images of barcodes. Typical use cases include creating barcodes for invoices, shipping labels, or PDF reports where the image data must be handled in memory. Developers often need to customize dimensions, versions, and output formats while retrieving the image as a byte array for further processing.
// Prompt: Implement method that returns DataMatrix barcode as byte array for embedding in PDF documents.
// Tags: datamatrix, barcode, generation, bytearray, pdf, aspose.barcode, encoding, image

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix barcode and retrieving it as a byte array.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that generates a sample DataMatrix barcode and writes it to a temporary file.
    /// </summary>
    static void Main()
    {
        // Sample DataMatrix code text
        string codeText = "Aspose.DataMatrix";

        // Generate barcode bytes using the helper method
        byte[] barcodeBytes = GenerateDataMatrixBarcode(codeText);

        // Write the byte array to a temporary PNG file to verify the output (optional)
        string outputPath = Path.Combine(Path.GetTempPath(), "DataMatrix.png");
        File.WriteAllBytes(outputPath, barcodeBytes);
        Console.WriteLine($"DataMatrix barcode saved to: {outputPath}");
    }

    /// <summary>
    /// Creates a DataMatrix barcode image in PNG format and returns it as a byte array.
    /// </summary>
    /// <param name="codeText">The text to encode in the DataMatrix barcode.</param>
    /// <returns>Byte array containing the PNG image of the generated barcode.</returns>
    static byte[] GenerateDataMatrixBarcode(string codeText)
    {
        if (string.IsNullOrEmpty(codeText))
            throw new ArgumentException("Code text must be non-empty.", nameof(codeText));

        // Initialize the barcode generator with DataMatrix symbology and the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set the module (pixel) size for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Optional: specify a common DataMatrix version (size) to control dimensions
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;

            // Save the generated barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image data as a byte array
                return ms.ToArray();
            }
        }
    }
}