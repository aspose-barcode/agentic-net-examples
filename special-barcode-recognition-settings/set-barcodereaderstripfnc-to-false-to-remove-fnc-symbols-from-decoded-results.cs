// Title: BarCodeReader StripFNC Example
// Description: Demonstrates how to configure BarCodeReader to retain (or remove) FNC symbols when decoding a Code128 barcode, illustrating the effect of the StripFNC property.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on the BarCodeReader class and its BarcodeSettings. Developers often need to control whether function characters (FNC) are stripped from decoded results, especially when working with symbologies like Code128 that may embed such characters for application-specific purposes. The snippet shows generating a barcode, reading it with StripFNC set, and outputting the decoded information.
// Prompt: Set BarCodeReader.StripFNC to false to remove FNC symbols from decoded results.
// Tags: barcode, stripfnc, code128, decoding, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple demonstration of using BarCodeReader with the StripFNC setting.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, reads it with StripFNC set to false,
    /// and writes the decoded information to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempDir = Path.Combine(Path.GetTempPath(), "StripFNCDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "code128.png");

        // Generate a simple Code128 barcode with the text "Aspose".
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            // Set the X-dimension (module width) to 2 pixels for better visibility.
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            // Save the barcode as a PNG image.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Read the barcode image with StripFNC set to false (removes FNC symbols from the result).
        Console.WriteLine("Reading barcode with StripFNC = false (remove FNC symbols):");
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure the reader to not strip FNC characters.
            reader.BarcodeSettings.StripFNC = false;

            // Iterate through all detected barcodes and output their type and text.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the temporary folder and its contents.
        // Directory.Delete(tempDir, true);
    }
}