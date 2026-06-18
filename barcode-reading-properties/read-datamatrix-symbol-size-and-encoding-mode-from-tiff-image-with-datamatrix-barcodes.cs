using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to read DataMatrix barcodes from a TIFF image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Path to the TIFF image containing DataMatrix barcodes.
        string imagePath = "datamatrix.tif";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Load the image as a Bitmap (Aspose.Drawing) and create a BarCodeReader
        // configured to decode only DataMatrix barcodes.
        using (Bitmap bitmap = new Bitmap(imagePath))
        using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the detected barcode.
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}");
                // Output the decoded text of the barcode.
                Console.WriteLine($"Code text: {result.CodeText}");

                // The Aspose.BarCode API does not expose DataMatrix symbol size
                // (DataMatrixVersion) or encoding mode (DataMatrixEncodeMode) via
                // the recognition result, so these details cannot be retrieved.
                Console.WriteLine("DataMatrix symbol size and encoding mode are not available via the recognition API.");
                Console.WriteLine();
            }
        }
    }
}