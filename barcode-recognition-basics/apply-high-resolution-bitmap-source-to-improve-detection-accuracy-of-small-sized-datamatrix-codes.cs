// Title: High‑Resolution DataMatrix Barcode Generation and Recognition
// Description: Demonstrates generating a DataMatrix barcode with increased DPI and then recognizing it using high‑quality settings to improve detection of small‑sized codes.
// Prompt: Apply a high‑resolution bitmap source to improve detection accuracy of small‑sized DataMatrix codes.
// Tags: datamatrix, generation, recognition, highresolution, qualitysettings, barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a high‑resolution DataMatrix barcode image
/// and reads it back using high‑quality recognition settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a DataMatrix barcode with high DPI,
    /// saves it to a file, and then reads it using enhanced quality settings.
    /// </summary>
    static void Main()
    {
        // Define the text to encode in the DataMatrix barcode.
        string codeText = "ABC123";

        // Specify the output file path for the generated barcode image.
        string imagePath = "datamatrix.png";

        // ------------------------------------------------------------
        // Generate a high‑resolution DataMatrix barcode image.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set image resolution to 300 DPI for better quality.
            generator.Parameters.Resolution = 300; // 300 DPI

            // Enable automatic sizing using interpolation to preserve detail.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Define a larger image size (in points) to obtain a high‑resolution bitmap.
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 200f;

            // Save the generated barcode image to the specified path.
            generator.Save(imagePath);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using high‑quality recognition settings.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Apply high‑quality settings and enable detection of small XDimension.
            reader.QualitySettings = QualitySettings.HighQuality;
            reader.QualitySettings.XDimension = XDimensionMode.Small;

            // Iterate through all detected barcodes and output their details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
            }
        }
    }
}