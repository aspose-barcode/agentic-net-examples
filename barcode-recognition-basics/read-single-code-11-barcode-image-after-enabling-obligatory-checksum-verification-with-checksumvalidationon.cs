// Title: Read Code 11 barcode with checksum validation
// Description: Demonstrates generating a Code 11 barcode image, saving it as PNG, and reading it back while enforcing mandatory checksum verification.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating 1D barcodes and BarCodeReader for decoding them, highlighting how to enable checksum validation (ChecksumValidation.On) to ensure data integrity. Developers working with inventory, tracking, or legacy systems often need to generate and validate Code 11 barcodes, making this pattern a common requirement.
// Prompt: Read a single Code 11 barcode image after enabling obligatory checksum verification with ChecksumValidation.On.
// Tags: code11, barcode, checksum, read, generate, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a Code 11 barcode, saves it to a temporary PNG file,
/// and then reads the barcode back with checksum validation enabled.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a barcode, reads it with checksum verification,
    /// outputs the results, and cleans up temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder and file path for the barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "Code11Sample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "code11.png");

        // --------------------------------------------------------------
        // Generate a Code 11 barcode image and save it as PNG to disk
        // --------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code11, "123456"))
        {
            // Set the X-dimension (module width) to 2 pixels for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------
        // Verify that the image file was created before attempting to read it
        // --------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Barcode image not found: " + imagePath);
            return;
        }

        // --------------------------------------------------------------
        // Read the barcode from the image with checksum validation turned on
        // --------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code11))
        {
            // Enable mandatory checksum verification; reading fails if checksum is invalid
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool anyResult = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                anyResult = true;
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                Console.WriteLine($"1D Value: {result.Extended.OneD.Value}");
                Console.WriteLine($"1D CheckSum: {result.Extended.OneD.CheckSum}");
            }

            if (!anyResult)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }

        // --------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}