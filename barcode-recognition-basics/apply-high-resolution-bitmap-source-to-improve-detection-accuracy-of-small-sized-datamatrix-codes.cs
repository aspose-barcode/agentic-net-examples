// Title: High‑Resolution DataMatrix Barcode Generation and Recognition
// Description: Demonstrates generating a high‑resolution DataMatrix barcode and reading it with enhanced detection settings to improve accuracy for small‑sized codes.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and QualitySettings classes to create high‑DPI images and configure recognition parameters such as XDimension. Developers working with small or low‑contrast barcodes often need to increase image resolution and adjust detection settings to achieve reliable scanning.
// Prompt: Apply a high‑resolution bitmap source to improve detection accuracy of small‑sized DataMatrix codes.
// Tags: datamatrix, barcode, generation, recognition, high resolution, quality settings, aspose.barcode, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a high‑resolution DataMatrix barcode, saves it to a temporary file,
/// and then reads it back using high‑quality recognition settings to demonstrate
/// improved detection of small‑sized codes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, verification, reading,
    /// and cleanup of temporary resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary folder for the demo files
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define barcode content and output image path
        // --------------------------------------------------------------------
        string codeText = "ABCD";
        string imagePath = Path.Combine(tempFolder, "datamatrix.png");

        // --------------------------------------------------------------------
        // Generate a high‑resolution DataMatrix barcode image
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Set a high DPI to improve detection of small modules
            generator.Parameters.Resolution = 600f; // DPI

            // Reduce the module (X‑dimension) size for a compact barcode
            generator.Parameters.Barcode.XDimension.Point = 0.5f;

            // Optional: define foreground and background colors
            generator.Parameters.Barcode.BarColor = Color.Black;
            generator.Parameters.BackColor = Color.White;

            // Save the generated barcode as a PNG file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify that the barcode image was successfully created
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the barcode using high‑quality recognition settings
        // --------------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.DataMatrix;
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Apply high‑quality settings to enhance detection
            reader.QualitySettings = QualitySettings.HighQuality;

            // Configure X‑dimension handling for small barcodes
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the recognition results
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files and folder
        // --------------------------------------------------------------------
        try
        {
            File.Delete(imagePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore any errors during cleanup
        }
    }
}