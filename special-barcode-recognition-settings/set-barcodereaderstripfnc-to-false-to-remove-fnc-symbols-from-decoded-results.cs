// Title: BarCodeReader StripFNC Property Example
// Description: Demonstrates how to configure BarCodeReader to retain FNC symbols by setting StripFNC to false while decoding a Code128 barcode.
// Category-Description: This example belongs to the Aspose.BarCode reading operations collection. It illustrates the use of BarCodeReader and its BarcodeSettings to control FNC symbol handling. Developers working with barcode scanning, especially those needing precise control over decoded data such as retaining or stripping Function (FNC) characters, will find this pattern useful. Typical use cases include inventory systems, document processing, and custom data extraction where FNC symbols carry meaning.
// Prompt: Set BarCodeReader.StripFNC to false to remove FNC symbols from decoded results.
// Tags: barcode, stripfnc, code128, reading, aspose.barcode, fnc

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Provides a simple demonstration of configuring BarCodeReader to keep FNC symbols
/// by setting the <c>StripFNC</c> property to <c>false</c>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a Code128 barcode, reads it with
    /// <c>StripFNC</c> disabled, and outputs the decoded information.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder to store the generated barcode image.
        string tempFolder = Path.Combine(Path.GetTempPath(), "FncDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the barcode image file.
        string barcodePath = Path.Combine(tempFolder, "code128.png");

        // Generate a Code128 barcode with simple text.
        // (If needed, FNC symbols could be embedded in the text string.)
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the barcode as a PNG image.
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Initialize the reader for the generated barcode image.
        // DecodeType.Code128 ensures the reader expects Code128 symbology.
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Disable stripping of FNC symbols so they appear in the decoded result.
            reader.BarcodeSettings.StripFNC = false;

            // Iterate through all detected barcodes (typically one in this example).
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Optional cleanup: delete the generated image and temporary folder.
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);

            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any exceptions that occur during cleanup.
        }
    }
}