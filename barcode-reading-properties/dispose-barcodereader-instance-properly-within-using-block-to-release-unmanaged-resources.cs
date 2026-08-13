// Title: Generate and Read a Code128 Barcode Using Aspose.BarCode
// Description: This example creates a Code128 barcode image, saves it to disk, then reads and displays the barcode data, demonstrating proper disposal of BarCodeReader.
// Category-Description: This sample belongs to the Aspose.BarCode generation and recognition category, illustrating how to use BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Developers commonly need to generate barcodes for labeling and later validate them by reading the encoded information, requiring proper resource management of unmanaged handles.
// Prompt: Dispose BarCodeReader instance properly within a using block to release unmanaged resources.
// Tags: barcode generation, barcode recognition, code128, aspose.barcode, csharp, using, disposal

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a file, and reading it back using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, reads it, outputs the decoded information, and cleans up.
    /// </summary>
    static void Main()
    {
        // Define the output file path for the generated barcode image
        string filePath = "barcode.png";

        // Generate a Code128 barcode and save it to the specified file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(filePath);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(filePath))
        {
            Console.WriteLine("Barcode image was not created.");
            return;
        }

        // Read the barcode from the image using BarCodeReader within a using block to ensure proper disposal
        using (var reader = new BarCodeReader(filePath, DecodeType.Code128))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine("Detected Type: " + result.CodeTypeName);
                Console.WriteLine("Detected Text: " + result.CodeText);
            }
        }

        // Optional cleanup: delete the generated barcode image file
        try
        {
            File.Delete(filePath);
        }
        catch
        {
            // Suppress any exceptions that occur during file cleanup
        }
    }
}