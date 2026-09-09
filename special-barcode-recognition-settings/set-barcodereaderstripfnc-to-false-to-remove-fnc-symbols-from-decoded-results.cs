// Title: Demonstrate disabling FNC stripping in BarCodeReader
// Description: Shows how to set BarCodeReader.StripFNC to false so decoded results retain FNC symbols. The example generates a Code128 barcode, reads it, and outputs the raw text.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, illustrating the use of BarCodeReader and its BarcodeSettings to control FNC character handling. Developers working with barcode decoding often need to preserve or remove function characters (FNC) depending on application requirements, and this snippet demonstrates the typical API usage for that scenario.
// Prompt: Set BarCodeReader.StripFNC to false to remove FNC symbols from decoded results.
// Tags: barcode symbology, stripfnc, code128, decoding, aspose.barcode, barcodereader

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that generates a Code128 barcode and reads it with StripFNC disabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it without stripping FNC symbols, and displays the results.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder for the example
        string tempFolder = Path.Combine(Path.GetTempPath(), "StripFNCExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "Code128.png");

        // Generate a simple Code128 barcode and save it as PNG
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f; // Set barcode module size
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Read the barcode with StripFNC set to false (retain FNC characters)
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            reader.BarcodeSettings.StripFNC = false;

            // Iterate through all detected barcodes (only one in this case)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
            }
        }

        // Clean up temporary files and folder
        try
        {
            if (File.Exists(barcodePath))
                File.Delete(barcodePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}