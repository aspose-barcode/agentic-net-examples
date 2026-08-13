// Title: Configure Reed‑Solomon error correction for DataMatrix barcode reading
// Description: Demonstrates how to set QualitySettings to enable Reed‑Solomon error correction when decoding DataMatrix barcodes, ensuring robust reading of damaged or partially corrupted codes.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on error‑correction configuration. It showcases the use of BarCodeReader, QualitySettings, and DecodeType classes to handle DataMatrix symbology with Reed‑Solomon correction, a common requirement for applications that scan imperfect printed codes.
// Prompt: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes that support it.
// Tags: datamatrix, reed-solomon, error-correction, qualitysettings, barcode-recognition, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a DataMatrix barcode, saves it as an image,
/// and then reads it back using Reed‑Solomon error correction via QualitySettings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a DataMatrix barcode, writes it to disk,
    /// and reads it back with maximum quality settings to demonstrate Reed‑Solomon handling.
    /// </summary>
    static void Main()
    {
        // Path where the generated barcode image will be stored.
        string imagePath = "datamatrix.png";

        // --------------------------------------------------------------------
        // Generate a DataMatrix barcode and save it as a PNG file.
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData"))
        {
            // Additional generation options can be set here if needed.
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using the BarCodeReader with Reed‑Solomon error correction.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Apply the highest quality preset, which enables full Reed‑Solomon correction.
            reader.QualitySettings = QualitySettings.MaxQuality;

            // Allow the reader to process barcodes that may have checksum errors or damage.
            reader.QualitySettings.AllowIncorrectBarcodes = true;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Confidence: {result.Confidence}");
                Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
            }
        }
    }
}