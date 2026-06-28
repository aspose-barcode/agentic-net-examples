using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generation and reading of GS1 Code128 barcodes with FNC1 characters using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images, saves them to a folder, and then reads them back,
    /// displaying barcode type and text (including FNC characters) to the console.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare output folder for barcode images
        // --------------------------------------------------------------------
        string folderPath = Path.Combine(Directory.GetCurrentDirectory(), "Barcodes");
        if (!Directory.Exists(folderPath))
        {
            // Create the folder if it does not exist
            Directory.CreateDirectory(folderPath);
        }

        // --------------------------------------------------------------------
        // Define sample barcode texts containing GS1 Application Identifiers
        // --------------------------------------------------------------------
        string[] sampleTexts = new string[]
        {
            "(01)12345678901231(10)ABC",      // GTIN and batch number
            "(01)98765432109876(21)XYZ123",   // GTIN and serial number
            "(01)55555555555555(17)210101"    // GTIN and expiration date
        };

        // --------------------------------------------------------------------
        // Generate barcode images for each sample text
        // --------------------------------------------------------------------
        for (int i = 0; i < sampleTexts.Length; i++)
        {
            // Build file name for the current barcode image
            string filePath = Path.Combine(folderPath, $"sample{i + 1}.png");

            // Create a barcode generator configured for GS1 Code128
            using (var generator = new BarcodeGenerator(EncodeTypes.GS1Code128, sampleTexts[i]))
            {
                // Save the generated barcode as a PNG file
                generator.Save(filePath);
            }
        }

        // --------------------------------------------------------------------
        // Retrieve all generated PNG images from the folder
        // --------------------------------------------------------------------
        string[] imageFiles = Directory.GetFiles(folderPath, "*.png");
        if (imageFiles.Length == 0)
        {
            Console.WriteLine("No barcode images found to process.");
            return;
        }

        // --------------------------------------------------------------------
        // Read each barcode image and output its details
        // --------------------------------------------------------------------
        foreach (string imagePath in imageFiles)
        {
            if (!File.Exists(imagePath))
            {
                // Skip missing files (should not happen, but guard against it)
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // Initialize a barcode reader for all supported types
            using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
            {
                // Preserve FNC characters in the decoded text
                reader.BarcodeSettings.StripFNC = false;

                // Iterate through all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)}");
                    Console.WriteLine($"  Type: {result.CodeTypeName}");
                    Console.WriteLine($"  CodeText (with FNC): {result.CodeText}");
                }
            }
        }
    }
}