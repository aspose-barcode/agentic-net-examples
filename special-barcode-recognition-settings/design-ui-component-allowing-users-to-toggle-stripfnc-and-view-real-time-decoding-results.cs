// Title: Toggle StripFNC on Code128 Barcode Decoding
// Description: Demonstrates generating a Code128 barcode, then decoding it twice with the StripFNC setting enabled and disabled to show its effect on the decoded text.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them, focusing on the StripFNC property. Developers often need to control FNC character handling when reading Code128 barcodes to obtain clean data, especially in inventory and logistics applications.
// Prompt: Design a UI component allowing users to toggle StripFNC and view real‑time decoding results.
// Tags: code128, stripfnc, png, barcodereader, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate a Code128 barcode and decode it with different StripFNC settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode image, decodes it with StripFNC false and true,
    /// outputs the results, and cleans up the temporary file.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary file path for the generated barcode image.
        string tempPath = Path.Combine(Path.GetTempPath(), "StripFNC_Demo.png");

        // Generate a Code128 barcode and save it as a PNG file.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Save(tempPath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created successfully.
        if (!File.Exists(tempPath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Define the two StripFNC options to test: false and true.
        bool[] stripOptions = new bool[] { false, true };

        // Iterate over each StripFNC setting and decode the barcode.
        foreach (bool stripFnc in stripOptions)
        {
            Console.WriteLine($"Decode with StripFNC = {stripFnc}");

            // Initialize the barcode reader for Code128 decoding.
            using (BarCodeReader reader = new BarCodeReader(tempPath, DecodeType.Code128))
            {
                // Apply the current StripFNC setting.
                reader.BarcodeSettings.StripFNC = stripFnc;

                // Perform the decoding operation.
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output decoding results.
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcode detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"CodeType: {result.CodeTypeName}");
                        Console.WriteLine($"CodeText: {result.CodeText}");
                        Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                    }
                }
            }

            Console.WriteLine();
        }

        // Clean up the temporary barcode image file.
        try
        {
            File.Delete(tempPath);
        }
        catch
        {
            // Ignore any cleanup errors.
        }
    }
}