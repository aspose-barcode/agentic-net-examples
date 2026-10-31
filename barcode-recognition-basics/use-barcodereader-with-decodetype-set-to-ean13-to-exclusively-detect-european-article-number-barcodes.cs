// Title: Detect EAN13 Barcodes Using BarCodeReader
// Description: Demonstrates generating an EAN13 barcode image and reading it with BarCodeReader configured to decode only EAN13 symbology.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create a barcode image and BarCodeReader with a specific DecodeType to extract barcode data. Typical scenarios include validating product identifiers, inventory management, and point‑of‑sale systems where developers need to work with European Article Number (EAN13) codes.
// Prompt: Use BarCodeReader with DecodeType set to EAN13 to exclusively detect European Article Number barcodes.
// Tags: ean13, barcode, generation, recognition, decode, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates an EAN13 barcode, saves it to a temporary file,
/// and then reads it back using <see cref="BarCodeReader"/> configured to decode only EAN13 barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the generation, detection, and cleanup steps.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated EAN13 barcode image.
        string barcodePath = Path.Combine(tempFolder, "ean13.png");

        // --------------------------------------------------------------------
        // Generate a sample EAN13 barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.EAN13, "1234567890128"))
        {
            // Set the X‑dimension (module width) to 2 pixels for better readability.
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was created successfully.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Configure the reader to decode only EAN13 barcodes.
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.EAN13;

        // --------------------------------------------------------------------
        // Read the barcode from the generated image.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No EAN13 barcode detected.");
            }
            else
            {
                foreach (BarCodeResult result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Value: {result.Extended.OneD.Value}");
                    Console.WriteLine($"Checksum: {result.Extended.OneD.CheckSum}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder.
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome.
        }
    }
}