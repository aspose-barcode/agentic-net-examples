// Title: High‑Resolution DataMatrix Barcode Generation and Recognition
// Description: Demonstrates generating a 600 DPI DataMatrix barcode and recognizing it with quality settings to improve detection of small symbols.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating high‑resolution bitmap barcodes and BarCodeReader with QualitySettings for accurate detection of tiny DataMatrix codes. Developers working with scanning small barcodes, printing high‑quality labels, or needing precise image‑based recognition will find these APIs essential.
// Prompt: Apply a high‑resolution bitmap source to improve detection accuracy of small‑sized DataMatrix codes.
// Tags: datamatrix, high-resolution, barcode-generation, barcode-recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a high‑resolution DataMatrix barcode, saves it to a temporary file,
/// and then reads it back using enhanced quality settings to demonstrate improved
/// detection of small‑sized codes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode creation, verification, recognition,
    /// and optional cleanup of temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary directory for the sample files
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "DataMatrixSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define the output path and the text to encode
        string barcodePath = Path.Combine(tempDir, "datamatrix.png");
        string codeText = "ABC123";

        // --------------------------------------------------------------------
        // Generate a high‑resolution DataMatrix barcode
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set a high resolution (e.g., 600 DPI) to produce a high‑resolution bitmap
            generator.Parameters.Resolution = 600f;

            // Optionally increase XDimension for clearer modules
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode as a PNG image
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify the generated file exists
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using high‑resolution recognition settings
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            // Configure quality settings to improve detection of small barcodes
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.QualitySettings.MinimalXDimension = 1f; // 1 pixel minimal element

            // Perform recognition and output results
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeType}");
                Console.WriteLine($"Decoded Text: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}