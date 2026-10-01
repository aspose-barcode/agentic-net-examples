// Title: Check PDF417 barcode for reader initialization flag
// Description: Demonstrates generating a PDF417 barcode, saving it as PNG, then reading it to inspect the IsReaderInitialization flag which indicates if the barcode contains scanner initialization instructions.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use BarcodeGenerator to create a PDF417 symbology, BarCodeReader with DecodeType.Pdf417 to decode the image, and how to access extended PDF417 properties such as IsReaderInitialization. Developers working with PDF417 barcodes for scanner configuration or data initialization can use this pattern to verify barcode content programmatically. Suitable for search snippet.
// Prompt: Check PDF417 IsReaderInitialization flag to determine if barcode contains initialization instructions for the scanner.
// Tags: pdf417, barcode, readerinitialization, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a PDF417 barcode, reads it back, and checks the IsReaderInitialization flag.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary PNG barcode, decodes it, and outputs the initialization flag.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary folder and file paths
        string tempFolder = Path.Combine(Path.GetTempPath(), "Pdf417Demo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "pdf417.png");

        // Generate a PDF417 barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample Text"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Set the decode type to PDF417 and read the barcode
        BaseDecodeType decodeType = DecodeType.Pdf417;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            bool anyFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyFound = true;
                // The flag indicates whether the barcode contains initialization instructions for the scanner
                bool isInit = result.Extended.Pdf417.IsReaderInitialization;
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"IsReaderInitialization: {isInit}");
            }

            if (!anyFound)
            {
                Console.WriteLine("No PDF417 barcode detected in the image.");
            }
        }

        // Clean up temporary files and directory
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failure should not affect program outcome
        }
    }
}