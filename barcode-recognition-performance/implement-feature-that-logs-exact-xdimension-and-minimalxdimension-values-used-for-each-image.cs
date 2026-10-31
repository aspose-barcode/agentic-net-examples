// Title: Log XDimension and MinimalXDimension for Generated Barcodes
// Description: Demonstrates generating Code128 barcodes with specific XDimension values, reading them with MinimalXDimension mode, and logging the exact dimensions used.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes with custom XDimension settings and BarCodeReader with QualitySettings to apply MinimalXDimension mode. Developers often need to control and verify barcode module sizes for printing accuracy, scanning reliability, and compliance with standards; this snippet provides a clear pattern for logging those values during automated processing.
// Prompt: Implement a feature that logs the exact XDimension and MinimalXDimension values used for each image.
// Tags: code128, barcode generation, barcode reading, xdimension, minimalxdimension, aspose.barcode, png

using System;
using System.IO;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates Code128 barcodes with varying XDimension values, reads them using MinimalXDimension mode,
/// and logs the generation and reader dimension settings for each image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary barcode images, reads them, logs dimension information,
    /// and cleans up the temporary files.
    /// </summary>
    static void Main()
    {
        // Create a dedicated temporary folder for generated barcode images
        string tempFolder = Path.Combine(Path.GetTempPath(), "XDimLog_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define XDimension values to use (in points) for barcode generation
        List<float> xDimensions = new List<float> { 2f, 3f, 4f };
        List<string> generatedFiles = new List<string>();

        // Generate barcode images with the specified XDimension values
        int index = 1;
        foreach (float xDim in xDimensions)
        {
            string filePath = Path.Combine(tempFolder, $"barcode_{index}.png");
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, $"Sample{index}"))
            {
                // Set the XDimension for the generated barcode
                generator.Parameters.Barcode.XDimension.Point = xDim;
                // Save the barcode image as PNG
                generator.Save(filePath, BarCodeImageFormat.Png);
            }
            generatedFiles.Add(filePath);
            index++;
        }

        // Read each generated image, apply MinimalXDimension mode, and log dimension values
        foreach (string file in generatedFiles)
        {
            using (BarCodeReader reader = new BarCodeReader(file, DecodeType.Code128))
            {
                // Enable MinimalXDimension mode for the reader
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                // Set a demonstration MinimalXDimension value (1 pixel)
                reader.QualitySettings.MinimalXDimension = 1f;

                // Perform barcode reading (results are not used further in this example)
                BarCodeResult[] results = reader.ReadBarCodes();

                // Retrieve the generation XDimension based on the file name index
                string fileName = Path.GetFileNameWithoutExtension(file);
                int fileIndex = int.Parse(fileName.Split('_')[1]);
                float generationXDim = xDimensions[fileIndex - 1];

                // Log the file name, generation XDimension, reader MinimalXDimension, and count of barcodes read
                Console.WriteLine($"File: {Path.GetFileName(file)} | Generation XDimension: {generationXDim} pt | Reader MinimalXDimension: {reader.QualitySettings.MinimalXDimension} pt | Barcodes read: {results.Length}");
            }
        }

        // Cleanup: delete the temporary folder and its contents
        try
        {
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Ignore any cleanup errors (e.g., files in use)
        }
    }
}