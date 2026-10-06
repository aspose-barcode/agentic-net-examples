// Title: Barcode generation and recognition with minimal XDimension filtering
// Description: Demonstrates generating a Code128 barcode, saving it as a PNG image, and reading it using QualitySettings to filter sub‑pixel noise by enabling UseMinimalXDimension and setting MinimalXDimension to 1 pixel.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for decoding them, and QualitySettings for fine‑tuning scan accuracy. Typical scenarios include producing barcodes for packaging and scanning them in noisy environments where sub‑pixel artifacts must be ignored. Developers often need to adjust XDimension settings to improve read reliability.
// Prompt: Activate QualitySettings.UseMinimalXDimension and set MinimalXDimension to 1 pixel to filter sub‑pixel noise.
// Tags: code128, generation, recognition, minimalxdimension, noise-filter, png, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Sample program that creates a Code128 barcode image, saves it, and reads it back
/// while applying minimal XDimension filtering to reduce sub‑pixel noise.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the sample. Generates a barcode, reads it with quality settings,
    /// outputs the results, and cleans up temporary files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the sample files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeSample_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define the full path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "sample.png");

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it as a PNG image
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "AsposeSample"))
        {
            // Optional: set the XDimension for barcode generation (2 points ≈ 0.28 mm)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Generate the barcode image in memory
            using (Bitmap bitmap = generator.GenerateBarCodeImage())
            {
                // Save the image to the previously defined path
                bitmap.Save(barcodePath, ImageFormat.Png);
            }
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using BarCodeReader with minimal XDimension filtering
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Enable the minimal XDimension mode to ignore sub‑pixel elements
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;

            // Set the minimal barcode element size to 1 pixel
            reader.QualitySettings.MinimalXDimension = 1f;

            // Perform the barcode reading operation
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the number of barcodes detected
            Console.WriteLine($"Barcodes read: {results.Length}");

            // List each detected barcode's type and decoded text
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // Clean up temporary files and directories
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Ignored – cleanup failures should not affect program outcome
        }
    }
}