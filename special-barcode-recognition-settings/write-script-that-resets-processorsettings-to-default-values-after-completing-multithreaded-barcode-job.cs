using System;
using System.IO;
using System.Threading.Tasks;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating Code128 barcodes, reading them in parallel,
/// and resetting processor settings using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates temporary barcode images, reads them concurrently,
    /// resets processing settings, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // 1. Prepare a temporary directory for barcode images
        // --------------------------------------------------------------------
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodes");
        Directory.CreateDirectory(tempDir);

        // --------------------------------------------------------------------
        // 2. Define sample code texts for barcode generation
        // --------------------------------------------------------------------
        string[] codeTexts = new string[] { "ABC123", "DEF456", "GHI789", "JKL012", "MNO345" };
        string[] imagePaths = new string[codeTexts.Length];

        // --------------------------------------------------------------------
        // 3. Generate barcode images and store their file paths
        // --------------------------------------------------------------------
        for (int i = 0; i < codeTexts.Length; i++)
        {
            string imagePath = Path.Combine(tempDir, $"barcode_{i}.png");

            // Create a barcode generator for Code128 with the current text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, codeTexts[i]))
            {
                // Save the generated barcode image using default settings
                generator.Save(imagePath);
            }

            // Keep the path for later processing
            imagePaths[i] = imagePath;
        }

        // --------------------------------------------------------------------
        // 4. Configure ProcessorSettings for multithreaded processing
        // --------------------------------------------------------------------
        // Enable use of all available CPU cores for BarCodeReader operations
        BarCodeReader.ProcessorSettings.UseAllCores = true;

        // --------------------------------------------------------------------
        // 5. Process barcodes in parallel
        // --------------------------------------------------------------------
        Parallel.ForEach(imagePaths, imagePath =>
        {
            // Each parallel task reads its assigned barcode image
            using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
            {
                // Set a high‑performance quality preset (optional)
                reader.QualitySettings = QualitySettings.HighPerformance;

                // Iterate over all detected barcodes in the image
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Detected: {result.CodeText}");
                }
            }
        });

        // --------------------------------------------------------------------
        // 6. Reset ProcessorSettings to their default values
        // --------------------------------------------------------------------
        // Default: UseAllCores = false, UseOnlyThisCoresCount = 0 (no specific core count)
        BarCodeReader.ProcessorSettings.UseAllCores = false;
        BarCodeReader.ProcessorSettings.UseOnlyThisCoresCount = 0;

        Console.WriteLine("ProcessorSettings have been reset to default values.");

        // --------------------------------------------------------------------
        // 7. Clean up temporary files (optional)
        // --------------------------------------------------------------------
        foreach (var file in imagePaths)
        {
            if (File.Exists(file))
            {
                File.Delete(file);
            }
        }

        // Remove the temporary directory
        Directory.Delete(tempDir, true);
    }
}