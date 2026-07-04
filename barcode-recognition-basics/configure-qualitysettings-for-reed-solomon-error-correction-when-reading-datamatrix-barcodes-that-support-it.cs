// Title: Reading DataMatrix with Reed‑Solomon Error Correction
// Description: Demonstrates configuring QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes, improving detection of damaged codes.
// Prompt: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes that support it.
// Tags: datamatrix, read, qualitysettings, reed-solomon, error-correction, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a DataMatrix barcode, saves it as an image,
/// and then reads it back using QualitySettings configured for Reed‑Solomon error correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a DataMatrix barcode, saves it, and reads it with high‑quality settings.
    /// </summary>
    static void Main()
    {
        // Define the output image file path
        string imagePath = "datamatrix.png";

        // -------------------------------------------------
        // Generate a DataMatrix barcode and save it as PNG
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData"))
        {
            // Set optional image dimensions (in points)
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode image to the specified path
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // -------------------------------------------------
        // Verify that the barcode image was successfully created
        // -------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // -------------------------------------------------
        // Read the DataMatrix barcode with Reed‑Solomon error correction enabled
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Apply a high‑quality preset to improve detection of damaged barcodes
            reader.QualitySettings = QualitySettings.HighQuality;

            // Allow reading of barcodes that may have incorrect checksums or damaged data
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Perform the recognition and output details for each detected barcode
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}