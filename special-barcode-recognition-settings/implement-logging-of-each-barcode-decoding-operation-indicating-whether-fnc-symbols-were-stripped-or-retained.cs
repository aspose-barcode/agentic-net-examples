// Title: Code128 Barcode Generation and Decoding with FNC Symbol Handling
// Description: Demonstrates creating a Code128 barcode image, then decoding it twice—once retaining and once stripping FNC symbols—to show how the StripFNC setting affects the result.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category, illustrating the use of BarcodeGenerator, BarCodeReader, and the StripFNC property. Developers often need to control FNC symbol handling when reading Code128 barcodes for inventory, shipping, or data capture applications. The snippet shows typical steps: generate, save, configure reader settings, and process results.
// Prompt: Implement logging of each barcode decoding operation, indicating whether FNC symbols were stripped or retained.
// Tags: code128, barcode, fnc, generation, decoding, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates barcode generation and decoding with optional FNC symbol stripping.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates a barcode image and decodes it with different StripFNC settings.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeFncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at {imagePath}");
            return;
        }

        // Decode the barcode while retaining FNC symbols (StripFNC = false)
        DecodeAndLog(imagePath, false);

        // Decode the barcode while stripping FNC symbols (StripFNC = true)
        DecodeAndLog(imagePath, true);
    }

    /// <summary>
    /// Decodes the specified barcode image and logs the result, indicating whether FNC symbols were stripped.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="stripFnc">True to strip FNC symbols; false to retain them.</param>
    static void DecodeAndLog(string imagePath, bool stripFnc)
    {
        Console.WriteLine($"Decoding '{Path.GetFileName(imagePath)}' with StripFNC = {stripFnc}");

        // Initialize the reader for Code128 barcodes
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the StripFNC setting as requested
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Iterate through all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"FNC symbols {(stripFnc ? "stripped" : "retained")} in this read.");
            }
        }

        Console.WriteLine();
    }
}