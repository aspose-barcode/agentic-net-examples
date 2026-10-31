// Title: Code128 Barcode Generation and Decoding with StripFNC Option
// Description: This example generates a Code128 barcode image, then decodes it twice—first retaining FNC symbols and then stripping them—while logging each decoding operation.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition APIs, focusing on the StripFNC setting for Code128 barcodes. It uses BarcodeGenerator to create barcodes and BarCodeReader with BarcodeSettings.StripFNC to control FNC symbol handling. Developers working with barcode scanning often need to log decoding results and decide whether to keep or remove FNC characters for downstream processing.
// Prompt: Implement logging of each barcode decoding operation, indicating whether FNC symbols were stripped or retained.
// Tags: code128, fnc, stripfnc, barcode-generation, barcode-recognition, logging, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation and decoding with optional stripping of FNC symbols, logging each operation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode image, then decodes it with StripFNC false and true, logging results.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "Code128FNC.png");

        // Generate a Code128 barcode containing the sample text "Aspose"
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            // Set barcode module size (X-dimension) to 2 pixels for better visibility
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created successfully
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Decode the image while retaining FNC symbols (StripFNC = false)
        DecodeAndLog(imagePath, stripFnc: false);

        // Decode the image while stripping FNC symbols (StripFNC = true)
        DecodeAndLog(imagePath, stripFnc: true);
    }

    /// <summary>
    /// Decodes a barcode image using the specified StripFNC setting and logs the results.
    /// </summary>
    /// <param name="imagePath">Full path to the barcode image file.</param>
    /// <param name="stripFnc">If true, FNC symbols are removed during decoding; otherwise they are retained.</param>
    static void DecodeAndLog(string imagePath, bool stripFnc)
    {
        // Log the decoding operation and the current StripFNC configuration
        Console.WriteLine($"Decoding '{Path.GetFileName(imagePath)}' with StripFNC = {stripFnc}");

        // Initialize the barcode reader for Code128 symbology
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the StripFNC setting
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Iterate through all detected barcodes and log their type and text
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Add an empty line for readability between decoding runs
        Console.WriteLine();
    }
}