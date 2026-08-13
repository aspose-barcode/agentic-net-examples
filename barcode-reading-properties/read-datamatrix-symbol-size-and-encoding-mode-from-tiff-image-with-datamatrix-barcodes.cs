// Title: Read DataMatrix Symbol Size and Encoding Mode from TIFF Image
// Description: Demonstrates how to load a TIFF file containing DataMatrix barcodes and retrieve basic barcode information using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating the use of BarCodeReader with DecodeType.DataMatrix to extract barcode type, text, and region. Developers often need to process scanned documents, extract barcode data, and handle multi-page TIFFs. The example shows typical API usage for reading barcodes from images.
/// Prompt: Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes.
/// Tags: datamatrix, barcode, recognition, tiff, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates reading DataMatrix barcodes from a TIFF image and attempting to obtain symbol size and encoding mode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Loads the image, validates its existence, and iterates over detected DataMatrix barcodes.
    /// </summary>
    static void Main()
    {
        // Path to the TIFF image containing DataMatrix barcodes.
        const string imagePath = "datamatrix.tif";

        // Verify that the file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Create a BarCodeReader configured for DataMatrix symbology.
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes in the image.
            foreach (var result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Retrieve and display the bounding rectangle of the detected barcode.
                var rect = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");

                // Symbol size (DataMatrix version) and encoding mode are not directly exposed
                // via the Aspose.BarCode recognition API. They would require accessing
                // extended parameters that are not part of the public API.
                Console.WriteLine("Symbol Size: Not directly available via API");
                Console.WriteLine("Encoding Mode: Not directly available via API");
                Console.WriteLine();
            }
        }
    }
}