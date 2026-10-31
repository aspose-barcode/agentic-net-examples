// Title: Read DataMatrix Symbol Size and Encoding Mode from a TIFF Image
// Description: Demonstrates loading a TIFF file containing DataMatrix barcodes, generating a sample if missing, and reading barcode data using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category. It showcases the use of BarCodeReader to decode DataMatrix symbols from multi‑page image formats (TIFF) and BarcodeGenerator to create sample barcodes. Developers working with document imaging, inventory systems, or automated data capture often need to extract barcode information from existing image files, and this snippet illustrates the typical workflow and key API classes (BarcodeGenerator, BarCodeReader, BarCodeResult, Extended.DataMatrix).
// Prompt: Read DataMatrix symbol size and encoding mode from a TIFF image with DataMatrix barcodes.
// Tags: datamatrix, barcode, recognition, tiff, generation, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that reads DataMatrix barcodes from a TIFF image and displays extended parameters.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample TIFF with a DataMatrix barcode if needed, then reads and prints barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the TIFF image that will be processed
        string tiffPath = "DataMatrixSample.tif";

        // If the TIFF file does not exist, create a simple DataMatrix barcode and save it as a single‑page TIFF
        if (!File.Exists(tiffPath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData"))
            {
                // Define a reasonable module size (pixel dimension) for the generated barcode
                generator.Parameters.Barcode.XDimension.Pixels = 4;

                // Save the generated barcode as a TIFF image (single page)
                generator.Save(tiffPath, BarCodeImageFormat.Tiff);
                Console.WriteLine($"Generated sample TIFF at: {tiffPath}");
            }
        }

        // Open the TIFF image with BarCodeReader, configured to decode only DataMatrix symbols
        using (var reader = new BarCodeReader(tiffPath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes in the first frame of the TIFF
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Basic barcode information
                Console.WriteLine($"Code Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Extended DataMatrix parameters (symbol size and encoding mode are not exposed by the API)
                Console.WriteLine($"IsReaderProgramming: {result.Extended.DataMatrix.IsReaderProgramming}");
                Console.WriteLine($"StructuredAppendBarcodesCount: {result.Extended.DataMatrix.StructuredAppendBarcodesCount}");
                Console.WriteLine($"StructuredAppendBarcodeId: {result.Extended.DataMatrix.StructuredAppendBarcodeId}");
                Console.WriteLine($"StructuredAppendFileId: {result.Extended.DataMatrix.StructuredAppendFileId}");
            }
        }
    }
}