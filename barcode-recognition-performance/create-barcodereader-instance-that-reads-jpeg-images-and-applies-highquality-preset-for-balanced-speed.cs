// Title: Read JPEG barcode image with HighQuality preset
// Description: Demonstrates creating a BarCodeReader for a JPEG file and applying the HighQuality preset for balanced speed and accuracy.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing how to configure QualitySettings while reading barcodes. It uses BarCodeReader, DecodeType, and QualitySettings classes, typical for developers needing fast yet reliable barcode detection in image files such as JPEGs.
// Prompt: Create a BarCodeReader instance that reads JPEG images and applies HighQuality preset for balanced speed.
// Tags: barcode symbology, barcode reading, jpeg, highquality, qualitysettings, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates reading a JPEG barcode image using Aspose.BarCode with HighQuality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode image if missing, then reads it using BarCodeReader with HighQuality preset.
    /// </summary>
    static void Main()
    {
        // Path to the sample barcode image (JPEG format)
        string imagePath = "sample_barcode.jpg";

        // Generate a sample barcode image if it does not already exist
        if (!File.Exists(imagePath))
        {
            // Create a BarcodeGenerator for Code128 symbology with sample text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Save the generated barcode as a JPEG file
                generator.Save(imagePath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Generated sample barcode image: {imagePath}");
            }
        }

        // Ensure the JPEG file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // Initialize BarCodeReader for the JPEG image, enabling all supported decode types
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Apply the HighQuality preset for a balance between speed and accuracy
            reader.QualitySettings = QualitySettings.HighQuality;

            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }
    }
}