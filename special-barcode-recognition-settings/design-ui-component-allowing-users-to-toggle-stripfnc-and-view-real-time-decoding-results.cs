// Title: Code128 Barcode Generation and StripFNC Decoding Demo
// Description: This example creates a Code128 barcode image, then decodes it twice—first without stripping FNC characters and then with stripping enabled—to illustrate the impact of the StripFNC setting.
// Category-Description: Demonstrates Aspose.BarCode generation and recognition workflows, focusing on the StripFNC option. It uses BarcodeGenerator for image creation and BarCodeReader for decoding, common tasks when handling Code128 barcodes that may contain Function (FNC) characters. Developers often need to toggle StripFNC to control whether these characters appear in the decoded result.
// Prompt: Design a UI component allowing users to toggle StripFNC and view real‑time decoding results.
// Tags: barcode, code128, generation, recognition, stripfnc, aspose.barcode, .net

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode and decoding it with different StripFNC settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, then reads it with StripFNC false and true.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the demo files
        string tempFolder = Path.Combine(Path.GetTempPath(), "StripFNCDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2; // Set module width
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Decode the barcode without stripping FNC characters
        ReadAndDisplay(barcodePath, false);

        // Decode the barcode with StripFNC enabled
        ReadAndDisplay(barcodePath, true);
    }

    /// <summary>
    /// Reads a barcode image and writes decoding results to the console.
    /// </summary>
    /// <param name="imagePath">Path to the barcode image file.</param>
    /// <param name="stripFnc">Whether to strip Function (FNC) characters during decoding.</param>
    static void ReadAndDisplay(string imagePath, bool stripFnc)
    {
        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for Code128 symbology
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Apply the StripFNC setting as requested
            reader.BarcodeSettings.StripFNC = stripFnc;

            // Perform the decoding operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the current StripFNC mode and each decoded result
            Console.WriteLine($"StripFNC: {stripFnc}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }
    }
}