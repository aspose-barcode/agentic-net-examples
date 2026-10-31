// Title: Read DataMatrix barcode from generated image
// Description: Generates a DataMatrix barcode, saves it as a PNG file, then reads and outputs its content using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create barcodes and BarCodeReader to decode them. Typical scenarios include creating barcodes for inventory, shipping, or authentication and later extracting the encoded data programmatically. Developers often need to set specific DecodeType values to target particular symbologies, such as DataMatrix, when reading images.
// Prompt: Set BarCodeReader.DecodeType to DecodeType.DataMatrix before invoking the Read method on the image.
// Tags: datamatrix, barcode, generation, recognition, aspose.barcode, png

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, saving it to a PNG file,
/// and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary folder, generates a DataMatrix barcode,
    /// reads the barcode from the saved image, outputs the result, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode image
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "datamatrix.png");

        // Generate a DataMatrix barcode and save it to a file
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "12345"))
        {
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode using DecodeType.DataMatrix
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the type and text of each detected barcode
                Console.WriteLine($"{result.CodeTypeName}:{result.CodeText}");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors to avoid interrupting the flow
        }
    }
}