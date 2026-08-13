// Title: Generate barcodes at multiple DPI settings and compare file sizes
// Description: Demonstrates creating Code128 barcodes at 96, 150, and 300 dpi, saving them as PNG, and reporting the resulting file sizes.
// Category-Description: This example belongs to the Aspose.BarCode image generation category, illustrating how to configure the Resolution property of BarcodeGenerator, save images in various formats, and analyze output size. Developers working with barcode rendering, DPI optimization, or storage considerations can use these patterns to balance quality and file size.
// Prompt: Write script generating barcodes at 96, 150, and 300 dpi and comparing output file sizes.
// Tags: barcode, code128, resolution, dpi, png, file-size, aspose.barcode, generation

using System;
using System.Collections.Generic;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes at different DPI settings and comparing the resulting PNG file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates barcodes, saves them, and prints size comparisons.
    /// </summary>
    static void Main()
    {
        // Barcode content and symbology
        string codeText = "1234567890";
        BaseEncodeType encodeType = EncodeTypes.Code128;

        // Resolutions (dots per inch) to test
        float[] resolutions = { 96f, 150f, 300f };

        // Dictionary to store file size for each DPI
        Dictionary<float, long> fileSizes = new Dictionary<float, long>();

        // Iterate over each resolution, generate and save the barcode
        foreach (float dpi in resolutions)
        {
            string fileName = $"barcode_{dpi}.png";

            // Create a generator instance and configure it
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Apply the desired DPI resolution
                generator.Parameters.Resolution = dpi;

                // Save the barcode as a PNG image
                generator.Save(fileName, BarCodeImageFormat.Png);
            }

            // Record the size of the generated file
            if (File.Exists(fileName))
            {
                FileInfo info = new FileInfo(fileName);
                fileSizes[dpi] = info.Length;
                Console.WriteLine($"Resolution {dpi} dpi: file size = {info.Length} bytes");
            }
            else
            {
                Console.WriteLine($"Failed to create file for resolution {dpi} dpi.");
            }
        }

        // Output a summary of all recorded sizes
        Console.WriteLine();
        Console.WriteLine("Size comparison:");
        foreach (var kvp in fileSizes)
        {
            Console.WriteLine($"{kvp.Key} dpi -> {kvp.Value} bytes");
        }

        // Determine and display the smallest and largest files
        if (fileSizes.Count > 0)
        {
            float minDpi = 0f, maxDpi = 0f;
            long minSize = long.MaxValue, maxSize = long.MinValue;

            foreach (var kvp in fileSizes)
            {
                if (kvp.Value < minSize)
                {
                    minSize = kvp.Value;
                    minDpi = kvp.Key;
                }
                if (kvp.Value > maxSize)
                {
                    maxSize = kvp.Value;
                    maxDpi = kvp.Key;
                }
            }

            Console.WriteLine();
            Console.WriteLine($"Smallest file: {minDpi} dpi ({minSize} bytes)");
            Console.WriteLine($"Largest file: {maxDpi} dpi ({maxSize} bytes)");
        }
    }
}