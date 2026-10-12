// Title: DataMatrix barcode generation and reading example
// Description: Demonstrates how to generate a DataMatrix barcode image, save it, and then read it back using Aspose.BarCode, restricting decoding to DataMatrix symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating DataMatrix codes and BarCodeReader with DecodeType filtering to read specific symbologies. Developers often need to generate barcodes for inventory or tracking and then validate them by reading only the expected type, improving performance and accuracy.
// Prompt: Set BarCodeReader.DecodeType to DecodeType.DataMatrix before invoking the Read method on the image.
// Tags: datamatrix, barcode generation, barcode reading, decode type, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a DataMatrix barcode, saving it to a temporary file, and reading it back using Aspose.BarCode with DecodeType set to DataMatrix.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Creates a temporary folder, generates a DataMatrix barcode image, reads it using BarCodeReader with DecodeType.DataMatrix, outputs the result, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the demo
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "datamatrix.png");

        // Generate a DataMatrix barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "123456789"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the image was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Read the barcode, restricting decoding to DataMatrix only
        using (BarCodeReader reader = new BarCodeReader(barcodePath))
        {
            // Set the decode type to DataMatrix (as required by the prompt)
            reader.SetBarCodeReadType(DecodeType.DataMatrix);

            // Perform the read operation and output each detected result
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored - cleanup failure should not affect demo execution
        }
    }
}