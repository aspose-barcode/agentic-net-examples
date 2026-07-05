// Title: Barcode detection accuracy comparison between color and grayscale images
// Description: Generates barcodes in color and grayscale, then reads them to compare detection confidence and reading quality across multiple symbologies.
// Prompt: Compare barcode detection accuracy between grayscale and full‑color input images across multiple symbologies.
// Tags: barcode, symbology, detection, grayscale, color, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates how to generate barcodes in both color and grayscale,
/// then reads them back to compare detection confidence and reading quality
/// across several symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates sample barcodes, reads them, and outputs a comparison
    /// of detection results for color versus grayscale images.
    /// </summary>
    static void Main()
    {
        // Define the set of symbologies and corresponding sample texts
        var symbologies = new (BaseEncodeType Encode, string CodeText)[]
        {
            (EncodeTypes.Code128, "1234567890"),
            (EncodeTypes.QR, "Hello QR"),
            (EncodeTypes.DataMatrix, "DM12345"),
            (EncodeTypes.Pdf417, "PDF417 Sample")
        };

        // Prepare a directory to store generated barcode images
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeSamples");
        if (!Directory.Exists(outputDir))
        {
            Directory.CreateDirectory(outputDir);
        }

        // Process each symbology
        foreach (var (encode, codeText) in symbologies)
        {
            // -------------------------------------------------
            // Generate a color barcode (blue bars on yellow background)
            // -------------------------------------------------
            string colorPath = Path.Combine(outputDir, $"{encode.TypeName}_color.png");
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
                generator.Parameters.BackColor = Aspose.Drawing.Color.Yellow;
                generator.Save(colorPath);
            }

            // -------------------------------------------------
            // Generate a grayscale barcode (default black on white)
            // -------------------------------------------------
            string grayPath = Path.Combine(outputDir, $"{encode.TypeName}_gray.png");
            using (var generator = new BarcodeGenerator(encode, codeText))
            {
                // No explicit color settings -> black/white output
                generator.Save(grayPath);
            }

            // -------------------------------------------------
            // Read and evaluate the color image
            // -------------------------------------------------
            var colorResult = ReadBarcode(colorPath);

            // -------------------------------------------------
            // Read and evaluate the grayscale image
            // -------------------------------------------------
            var grayResult = ReadBarcode(grayPath);

            // -------------------------------------------------
            // Output a side‑by‑side comparison of detection results
            // -------------------------------------------------
            Console.WriteLine($"Symbology: {encode.TypeName}");
            Console.WriteLine($"  Color Image   -> Detected: {colorResult.Detected}, Confidence: {colorResult.Confidence}, ReadingQuality: {colorResult.ReadingQuality}");
            Console.WriteLine($"  Grayscale Image-> Detected: {grayResult.Detected}, Confidence: {grayResult.Confidence}, ReadingQuality: {grayResult.ReadingQuality}");
            Console.WriteLine();
        }
    }

    // Helper struct to hold detection information for a single barcode read operation
    private struct DetectionInfo
    {
        public bool Detected;
        public BarCodeConfidence Confidence;
        public double ReadingQuality;
    }

    /// <summary>
    /// Reads the first barcode from the specified image file and returns detection details.
    /// </summary>
    /// <param name="imagePath">Path to the image containing the barcode.</param>
    /// <returns>A <see cref="DetectionInfo"/> struct with detection status, confidence, and reading quality.</returns>
    private static DetectionInfo ReadBarcode(string imagePath)
    {
        var info = new DetectionInfo
        {
            Detected = false,
            Confidence = BarCodeConfidence.None,
            ReadingQuality = 0.0
        };

        // Verify that the image file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Warning: File not found - {imagePath}");
            return info;
        }

        // Use Aspose.BarCode reader to decode any supported barcode type
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                // Capture details from the first detected barcode and exit loop
                info.Detected = true;
                info.Confidence = result.Confidence;
                info.ReadingQuality = result.ReadingQuality;
                break;
            }
        }

        return info;
    }
}