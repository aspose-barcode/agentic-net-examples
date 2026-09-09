// Title: Generate DataMatrix barcode as byte array
// Description: Demonstrates how to create a DataMatrix barcode image using Aspose.BarCode and return it as a byte array, which can be embedded into PDF documents.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating the use of BarcodeGenerator with EncodeTypes.DataMatrix. It shows typical steps such as configuring barcode parameters, rendering to a MemoryStream, and obtaining PNG bytes. Developers working on document automation, PDF generation, or label printing often need to produce barcode images programmatically; this snippet provides a concise reference for those scenarios.
// Prompt: Implement method that returns DataMatrix barcode as byte array for embedding in PDF documents.
// Tags: datamatrix, barcode, generation, byte-array, pdf, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that generates a DataMatrix barcode and saves it as a PNG file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode, saves it to disk, and writes the byte count to the console.
    /// </summary>
    static void Main()
    {
        // Text to encode in the DataMatrix barcode
        string codeText = "Aspose DataMatrix";

        // Generate the barcode image and obtain its PNG bytes
        byte[] barcodeBytes = GenerateDataMatrixBarcode(codeText);

        // Determine the output file path in the current directory
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "datamatrix.png");

        // Write the byte array to a PNG file
        using (var fileStream = new FileStream(outputPath, FileMode.Create, FileAccess.Write))
        {
            fileStream.Write(barcodeBytes, 0, barcodeBytes.Length);
        }

        // Inform the user about the successful generation
        Console.WriteLine($"DataMatrix barcode generated. Bytes: {barcodeBytes.Length}, saved to: {outputPath}");
    }

    static byte[] GenerateDataMatrixBarcode(string text)
    {
        // Initialize the barcode generator for DataMatrix with the provided text
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, text))
        {
            // Set the size of each module (pixel) in the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 8f;

            // Use automatic encoding mode for optimal DataMatrix encoding
            generator.Parameters.Barcode.DataMatrix.EncodeMode = DataMatrixEncodeMode.Auto;

            // Render the barcode to a memory stream in PNG format
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                // Return the image bytes for further use (e.g., embedding in a PDF)
                return ms.ToArray();
            }
        }
    }
}