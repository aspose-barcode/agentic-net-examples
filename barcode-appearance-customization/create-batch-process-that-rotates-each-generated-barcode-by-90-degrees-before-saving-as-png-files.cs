using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a small batch of rotated barcodes using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, rotates them, and saves as PNG files.
    /// </summary>
    static void Main()
    {
        // Define a small batch of barcodes to generate (type and corresponding text)
        var barcodes = new (BaseEncodeType Type, string CodeText)[]
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.EAN13, "1234567890128"),
            (EncodeTypes.DataMatrix, "DataMatrixSample"),
            (EncodeTypes.Pdf417, "PDF417 Sample Text")
        };

        // Determine output directory path and ensure it exists
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        Directory.CreateDirectory(outputDir);

        int index = 1; // Counter for unique file naming

        // Iterate over each barcode definition
        foreach (var (type, codeText) in barcodes)
        {
            // Initialize generator with the specified encoding type and text
            using (var generator = new BarcodeGenerator(type, codeText))
            {
                // Set rotation angle to 90 degrees (clockwise)
                generator.Parameters.RotationAngle = 90f;

                // Construct file name using type name and index, then combine with output directory
                string fileName = $"{type.TypeName}_{index}.png";
                string outputPath = Path.Combine(outputDir, fileName);

                // Save the generated barcode as a PNG image
                generator.Save(outputPath);

                // Inform the user about the saved file
                Console.WriteLine($"Saved rotated barcode to: {outputPath}");
            }

            index++; // Increment index for the next file
        }
    }
}