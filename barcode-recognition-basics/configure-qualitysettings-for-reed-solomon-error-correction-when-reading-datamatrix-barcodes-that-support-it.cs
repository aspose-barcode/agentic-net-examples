// Title: Read DataMatrix barcode with Reed‑Solomon error correction using QualitySettings
// Description: Demonstrates configuring QualitySettings to enable high‑quality Reed‑Solomon error correction when reading a DataMatrix barcode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating DataMatrix symbols and BarCodeReader with QualitySettings to decode them. Developers working with error‑prone scans, such as low‑resolution images or damaged codes, commonly need to enable Reed‑Solomon error correction to improve read reliability.
// Prompt: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes that support it.
// Tags: datamatrix, reed-solomon, error-correction, qualitysettings, barcode-reading, barcode-generation, aspnet, c#

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a DataMatrix barcode, then reads it using high‑quality
/// settings to enable Reed‑Solomon error correction.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a temporary DataMatrix image, reads it with
    /// <see cref="QualitySettings.HighQuality"/>, and outputs detection details to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the barcode image
        string tempDir = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string barcodePath = Path.Combine(tempDir, "datamatrix.png");

        // Generate a DataMatrix barcode with specific version and ECC type
        string codeText = "HelloWorld123";
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, codeText))
        {
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Read the barcode using high‑quality settings (enables full Reed‑Solomon error correction)
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            reader.QualitySettings = QualitySettings.HighQuality;

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
                    Console.WriteLine($"Reading Quality: {result.ReadingQuality}");
                    Console.WriteLine($"Confidence: {result.Confidence}");
                }
            }
        }

        // Optional cleanup: uncomment to delete the temporary folder after execution
        // Directory.Delete(tempDir, true);
    }
}