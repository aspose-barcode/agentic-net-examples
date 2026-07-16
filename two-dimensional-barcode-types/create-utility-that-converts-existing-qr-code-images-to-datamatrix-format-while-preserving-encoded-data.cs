// Title: QR to DataMatrix Conversion Utility
// Description: Demonstrates converting QR code images to DataMatrix barcodes while preserving the encoded data.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases how to use BarcodeGenerator (EncodeTypes) and BarCodeReader (DecodeType) to read existing QR codes, extract their text, and re‑encode it as DataMatrix images. Developers working with barcode format migration, data migration, or multi‑symbology support will find this pattern useful for batch processing and format conversion tasks.
// Prompt: Create utility that converts existing QR code images to DataMatrix format while preserving encoded data.
// Tags: barcode symbology, conversion, png, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a console utility that reads QR code images from a folder,
/// decodes their content, and generates equivalent DataMatrix barcodes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates sample QR codes,
    /// decodes them, and creates corresponding DataMatrix images.
    /// </summary>
    static void Main()
    {
        // Define folders for input QR codes and output DataMatrix codes
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "InputQRCodes");
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "OutputDataMatrix");

        // Ensure the input and output directories exist
        Directory.CreateDirectory(inputFolder);
        Directory.CreateDirectory(outputFolder);

        // -----------------------------------------------------------------
        // Step 1: Generate sample QR code images (self‑contained example)
        // -----------------------------------------------------------------
        for (int i = 1; i <= 3; i++)
        {
            string qrText = $"Sample QR {i}";
            string qrPath = Path.Combine(inputFolder, $"qr{i}.png");

            // Create a QR code with the specified text
            using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, qrText))
            {
                // Set a modest image size (200x200 points)
                qrGenerator.Parameters.ImageWidth.Point = 200f;
                qrGenerator.Parameters.ImageHeight.Point = 200f;

                // Save the QR code as a PNG file
                qrGenerator.Save(qrPath, BarCodeImageFormat.Png);
            }

            Console.WriteLine($"Generated QR code: {qrPath}");
        }

        // -----------------------------------------------------------------
        // Step 2: Convert each QR code image to a DataMatrix barcode
        // -----------------------------------------------------------------
        string[] qrFiles = Directory.GetFiles(inputFolder, "*.png");
        foreach (string qrFile in qrFiles)
        {
            if (!File.Exists(qrFile))
            {
                Console.WriteLine($"File not found: {qrFile}");
                continue;
            }

            // Decode the QR code to obtain the encoded text
            using (var reader = new BarCodeReader(qrFile, DecodeType.QR))
            {
                bool decoded = false;

                // Iterate through all detected barcodes (normally one per image)
                foreach (var result in reader.ReadBarCodes())
                {
                    string codeText = result.CodeText ?? string.Empty;
                    Console.WriteLine($"Decoded QR from '{Path.GetFileName(qrFile)}': {codeText}");

                    // Prepare the output file name for the DataMatrix image
                    string dmFileName = Path.GetFileNameWithoutExtension(qrFile) + "_dm.png";
                    string dmPath = Path.Combine(outputFolder, dmFileName);

                    // Generate a DataMatrix barcode using the same text
                    using (var dmGenerator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
                    {
                        // Use the same image dimensions as the source QR code
                        dmGenerator.Parameters.ImageWidth.Point = 200f;
                        dmGenerator.Parameters.ImageHeight.Point = 200f;

                        // Save the DataMatrix barcode as a PNG file
                        dmGenerator.Save(dmPath, BarCodeImageFormat.Png);
                    }

                    Console.WriteLine($"Saved DataMatrix: {dmPath}");
                    decoded = true;
                }

                if (!decoded)
                {
                    Console.WriteLine($"No QR code detected in file: {qrFile}");
                }
            }
        }

        Console.WriteLine("Conversion process completed.");
    }
}