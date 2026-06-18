using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generation and reading of a DotCode barcode using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DotCode barcode, saves it to a temporary file, reads it back,
    /// displays the decoded information, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Sample DotCode text to encode.
        string codeText = "1234567890";

        // Determine a temporary file path for the barcode image.
        string tempPath = Path.GetTempPath();
        string imagePath = Path.Combine(tempPath, "dotcode_sample.png");

        // Generate a DotCode barcode image and save it to the temporary file.
        using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
        {
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the DotCode barcode from the generated image.
        using (var bitmap = new Bitmap(imagePath))
        using (var reader = new BarCodeReader(bitmap, DecodeType.DotCode))
        {
            // Iterate through all detected barcodes (should be only one in this case).
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the symbology type (e.g., DotCode).
                Console.WriteLine($"Symbology: {result.CodeTypeName}");
                // Output the decoded text.
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Note: Aspose.BarCode does not expose version or error‑correction level for DotCode.
                Console.WriteLine("Version information and error correction level are not available via Aspose.BarCode API for DotCode.");
                Console.WriteLine();
            }
        }

        // Attempt to delete the temporary image file.
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // If deletion fails, ignore – the OS will clean up temporary files later.
        }
    }
}