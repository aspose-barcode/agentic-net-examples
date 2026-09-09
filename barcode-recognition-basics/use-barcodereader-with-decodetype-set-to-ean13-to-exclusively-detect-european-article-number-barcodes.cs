// Title: Detect EAN13 Barcodes Using BarCodeReader
// Description: Demonstrates generating an EAN13 barcode image and reading it with BarCodeReader configured to decode only EAN13 symbology.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator to create a PNG image of an EAN13 barcode and BarCodeReader with DecodeType.EAN13 to restrict detection to European Article Number barcodes. Developers working with product labeling, inventory systems, or retail applications frequently need to generate and read EAN13 codes, making these APIs essential for accurate barcode handling.
// Prompt: Use BarCodeReader with DecodeType set to EAN13 to exclusively detect European Article Number barcodes.
// Tags: ean13, barcode, decode, reader, generation, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that creates an EAN13 barcode image and reads it using BarCodeReader
/// with DecodeType set to EAN13, ensuring only European Article Number barcodes are detected.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a temporary EAN13 barcode image,
    /// reads it with a restricted decoder, outputs the results, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "ean13.png");

        // --------------------------------------------------------------------
        // Generate an EAN13 barcode image and save it as PNG
        // --------------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify the image file exists before attempting to read it
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using DecodeType.EAN13 to restrict detection to EAN13 only
        // --------------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.EAN13))
        {
            try
            {
                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"CheckSum: {result.Extended.OneD.CheckSum}");
                }
            }
            catch (ArgumentException ex)
            {
                Console.WriteLine($"Error reading barcode: {ex.Message}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}