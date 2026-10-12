// Title: Generate Rectangular DataMatrix Barcode and Decode It
// Description: Demonstrates how to create a DataMatrix barcode with a rectangular shape (approximately 10 rows by 30 columns) and then read back the encoded text.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use the BarcodeGenerator class to configure DataMatrix parameters such as version and module size, and how to employ BarCodeReader to decode the generated image. Developers working with 2‑D barcodes often need to produce non‑square DataMatrix symbols for space‑constrained layouts and verify them programmatically.
// Prompt: Configure DataMatrix barcode to use rectangular shape with 10 rows and 30 columns.
// Tags: datamatrix, rectangular, barcode generation, barcode recognition, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a rectangular DataMatrix barcode,
/// saves it as an image, and then reads the encoded text back.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a DataMatrix barcode
    /// with a rectangular version, saves it to PNG, and decodes the image to verify the content.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for output files
        string outputDir = Path.Combine(Path.GetTempPath(), "DataMatrixExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the full path for the barcode image and the text to encode
        string barcodePath = Path.Combine(outputDir, "datamatrix.png");
        string codeText = "SampleData";

        // Generate DataMatrix barcode with rectangular shape (approximate 10x30)
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Choose a rectangular version that closely matches the requested dimensions
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_12x36;

            // Optional: set the module (dot) size for better readability
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize a reader to decode the saved DataMatrix image
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            // Iterate through all detected barcodes (should be only one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the decoded text to the console
                Console.WriteLine("Decoded CodeText: " + result.CodeText);
            }
        }

        // Inform the user where the barcode image was saved
        Console.WriteLine("Barcode saved to: " + barcodePath);
    }
}