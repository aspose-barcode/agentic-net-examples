// Title: Codabar barcode generation with Mod16 checksum and validation
// Description: Demonstrates how to generate a Codabar barcode using the Mod16 checksum mode, save it as PNG, and then read it back while validating the checksum.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for decoding them. Typical scenarios include creating barcodes with specific checksum requirements and verifying their integrity during scanning. Developers often need to configure checksum modes, enable validation, and handle image output, which this sample illustrates.
// Prompt: Configure barcode to use Mod16 checksum mode and validate the checksum after generation.
// Tags: codabar, checksum, mod16, barcode generation, barcode recognition, png, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a Codabar barcode with Mod16 checksum,
/// saves it to a PNG file, and then reads the barcode back while validating the checksum.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Performs barcode generation, saving, and validation.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a unique temporary folder to store the generated barcode image.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeExample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "codabar_mod16.png");

        // --------------------------------------------------------------
        // Generate a Codabar barcode with Mod16 checksum enabled.
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Codabar, "-12345-"))
        {
            // Set barcode visual parameters.
            generator.Parameters.Barcode.XDimension.Pixels = 2;

            // Enable checksum calculation.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Specify Mod16 checksum mode for Codabar.
            generator.Parameters.Barcode.Codabar.ChecksumMode = CodabarChecksumMode.Mod16;

            // Save the barcode image as PNG.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        Console.WriteLine($"Barcode image saved to: {imagePath}");

        // Verify that the image file was created successfully.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------
        // Read the barcode image and validate the checksum.
        // --------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Codabar;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Turn on checksum validation during decoding.
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            bool found = false;
            foreach (var result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Checksum value: {result.Extended.OneD.CheckSum}");
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected or checksum validation failed.");
            }
        }
    }
}