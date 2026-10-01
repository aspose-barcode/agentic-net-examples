// Title: Configure Reed‑Solomon QualitySettings for DataMatrix barcode reading
// Description: Demonstrates how to generate a DataMatrix barcode and tune QualitySettings to improve Reed‑Solomon error‑correction detection during reading.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator, BarCodeReader, and QualitySettings classes to create a DataMatrix (ECC200) image and read it with high‑quality settings. Developers working with error‑correction‑enabled symbologies often need to adjust quality presets, XDimension handling, and deconvolution modes to achieve reliable scans.
// Prompt: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes that support it.
// Tags: datamatrix, reading, png, barcodegenerator, barcodereader, qualitysettings

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a DataMatrix barcode, then reads it using high‑quality settings optimized for Reed‑Solomon error correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Creates a temporary DataMatrix image, configures QualitySettings, reads the barcode, and cleans up.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string barcodePath = Path.Combine(tempFolder, "datamatrix.png");
        string codeText = "ABC123";

        // --------------------------------------------------------------------
        // Generate a DataMatrix barcode (ECC200 uses Reed‑Solomon error correction by default)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            // Optional: set a modest XDimension for clearer image
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Verify the image was created before attempting to read it
        // --------------------------------------------------------------------
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Read the DataMatrix barcode with QualitySettings tuned for robust Reed‑Solomon handling
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            // Apply a high‑quality preset to improve error‑correction detection
            reader.QualitySettings = QualitySettings.HighQuality;

            // Use minimal XDimension to allow the engine to adapt to small modules
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Fast deconvolution reduces processing time while keeping sufficient quality
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected CodeText: {result.CodeText}");
                    Console.WriteLine($"Detected Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files (optional)
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}