// Title: Configure QualitySettings for Reed‑Solomon DataMatrix barcode reading
// Description: Demonstrates how to generate a DataMatrix barcode with Reed‑Solomon ECC and read it using high‑quality settings to improve decoding reliability.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator to create DataMatrix symbols, the BarCodeReader for decoding, and the QualitySettings class to fine‑tune decoding parameters such as deconvolution and inverse image handling. Developers working with error‑correction‑enabled barcodes often need to adjust these settings to achieve optimal read rates in challenging imaging conditions.
// Prompt: Configure QualitySettings for Reed‑Solomon error correction when reading DataMatrix barcodes that support it.
// Tags: datamatrix, reed-solomon, qualitysettings, barcode-generation, barcode-recognition, csharp, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Generates a DataMatrix barcode with Reed‑Solomon ECC and reads it using high‑quality settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates a temporary DataMatrix image, configures decoding quality,
    /// reads the barcode, and outputs the result to the console.
    /// </summary>
    static void Main()
    {
        // Create a temporary folder for the sample barcode
        string tempFolder = Path.Combine(Path.GetTempPath(), "DataMatrixDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);
        string barcodePath = Path.Combine(tempFolder, "datamatrix.png");

        // Generate a DataMatrix barcode with ECC 200 (Reed‑Solomon)
        using (var generator = new BarcodeGenerator(EncodeTypes.DataMatrix, "SampleData123"))
        {
            // Set the DataMatrix version and error‑correction type
            generator.Parameters.Barcode.DataMatrix.Version = DataMatrixVersion.ECC200_32x32;
            generator.Parameters.Barcode.DataMatrix.EccType = DataMatrixEccType.Ecc200;

            // Save the barcode image as PNG
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode with QualitySettings configured for high‑quality (Reed‑Solomon) decoding
        using (var reader = new BarCodeReader(barcodePath, DecodeType.DataMatrix))
        {
            // Apply a high‑quality preset and fine‑tune options
            reader.QualitySettings = QualitySettings.HighQuality;
            reader.QualitySettings.Deconvolution = DeconvolutionMode.Fast;
            reader.QualitySettings.InverseImage = InverseImageMode.Auto;

            // Perform the read operation
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcode detected.");
            }
            else
            {
                // Output each detected barcode's details
                foreach (var result in results)
                {
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"CodeType: {result.CodeTypeName}");
                    Console.WriteLine($"ReadingQuality: {result.ReadingQuality}");
                }
            }
        }

        // Clean up temporary files (optional)
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