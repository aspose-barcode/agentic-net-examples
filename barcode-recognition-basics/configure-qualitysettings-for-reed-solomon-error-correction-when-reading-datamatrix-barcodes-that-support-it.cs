// Title: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes
// Description: Demonstrates how to generate a DataMatrix barcode and read it using high‑quality settings that enable Reed‑Solomon error correction.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows the use of BarcodeGenerator for creating DataMatrix (ECC200) symbols and BarCodeReader with QualitySettings to improve decoding robustness. Developers working with error‑correction capable symbologies often need to adjust quality presets to handle damaged or low‑contrast images.
// Prompt: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes that support it.
// Tags: datamatrix, reed-solomon, error-correction, qualitysettings, barcode-generation, barcode-recognition, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Generates a DataMatrix barcode and reads it using high‑quality settings to enable Reed‑Solomon error correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary DataMatrix image, reads it with enhanced quality settings,
    /// outputs the decoded information, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Create a temporary folder for the sample barcode image
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string imagePath = Path.Combine(tempFolder, "datamatrix.png");

        // --------------------------------------------------------------------
        // Generate a DataMatrix barcode (ECC200 supports Reed‑Solomon error correction)
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "Hello Aspose"))
        {
            // Optional: set module size (X‑dimension) for better visibility
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG image
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // --------------------------------------------------------------------
        // Read the barcode with QualitySettings configured for high‑quality decoding
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DataMatrix))
        {
            // Apply a high‑quality preset to enable robust Reed‑Solomon error‑correction processing
            reader.QualitySettings = QualitySettings.HighQuality;

            // Optionally, fine‑tune specific quality options (commented out as examples)
            // reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            // reader.QualitySettings.InverseImage = InverseImageMode.Enabled;

            // Perform the recognition
            BarCodeResult[] results = reader.ReadBarCodes();

            // ----------------------------------------------------------------
            // Output the results
            // ----------------------------------------------------------------
            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                foreach (var result in results)
                {
                    Console.WriteLine($"Code Text: {result.CodeText}");
                    Console.WriteLine($"Symbology: {result.CodeTypeName}");
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                }
            }
        }

        // --------------------------------------------------------------------
        // Clean up temporary files
        // --------------------------------------------------------------------
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempFolder))
                Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore cleanup errors
        }
    }
}