// Title: Read DataMatrix Barcode from Generated Image
// Description: Generates a DataMatrix barcode, saves it as a PNG file, and then decodes it using BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader to recognize and decode the barcode. Developers often need to generate barcodes for labeling or tracking and later verify or extract the encoded data, requiring knowledge of setting DecodeType before reading.
// Prompt: Set BarCodeReader.DecodeType to DecodeType.DataMatrix before invoking the Read method on the image.
// Tags: datamatrix, barcode, generation, recognition, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a DataMatrix barcode image and decoding it using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode, saves it, and reads it back.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be saved
        string imagePath = "datamatrix.png";

        // ------------------------------------------------------------
        // Generate a DataMatrix barcode and save it as a PNG file
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello"))
        {
            // Optional: configure additional DataMatrix parameters here
            // generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_10x10;

            // Save the barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{Path.GetFullPath(imagePath)}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read and decode the barcode from the generated image
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath))
        {
            // Set the decode type to DataMatrix before performing the read operation
            reader.BarCodeReadType = DecodeType.DataMatrix;

            // Iterate through all detected barcodes (expected one in this case)
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }
    }
}