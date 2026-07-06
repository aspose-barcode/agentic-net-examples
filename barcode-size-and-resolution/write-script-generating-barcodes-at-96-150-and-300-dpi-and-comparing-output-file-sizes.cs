// Title: Barcode generation at multiple DPI settings with file size comparison
// Description: Demonstrates creating Code128 barcodes at 96, 150, and 300 dpi, saving as PNG, and comparing the resulting file sizes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to configure the Resolution property of BarcodeGenerator, save images, and analyze output size. Developers working with barcode image rendering often need to balance image quality against file size, and this snippet shows typical usage of EncodeTypes, BarcodeGenerator, and file I/O for such assessments.
// Prompt: Write script generating barcodes at 96, 150, and 300 dpi and comparing output file sizes.
// Tags: code128, barcode generation, png, resolution, file size, aspose.barcode, barcodegenerator

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating Code128 barcodes at various DPI settings and comparing the resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates barcodes at 96, 150, and 300 dpi, saves them as PNG files, and reports file size statistics.
    /// </summary>
    static void Main()
    {
        // Sample barcode data to encode
        const string codeText = "1234567890";

        // Resolutions (dots per inch) to test
        float[] resolutions = new float[] { 96f, 150f, 300f };

        // Array to store generated file sizes for each resolution
        long[] fileSizes = new long[resolutions.Length];

        // Loop through each resolution, generate barcode, and record file size
        for (int i = 0; i < resolutions.Length; i++)
        {
            // Build output file name based on current DPI
            string fileName = $"barcode_{(int)resolutions[i]}dpi.png";

            // Create and configure the barcode generator
            using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, codeText))
            {
                // Set the desired image resolution
                generator.Parameters.Resolution = resolutions[i];

                // Save the generated barcode as a PNG image
                generator.Save(fileName);
            }

            // Verify that the file was created and capture its size
            if (File.Exists(fileName))
            {
                fileSizes[i] = new FileInfo(fileName).Length;
                Console.WriteLine($"Generated {fileName}: {fileSizes[i]} bytes (Resolution: {resolutions[i]} dpi)");
            }
            else
            {
                Console.WriteLine($"Failed to generate {fileName}");
                fileSizes[i] = -1;
            }
        }

        // Output a simple comparison of file sizes across resolutions
        Console.WriteLine();
        Console.WriteLine("File size comparison:");
        for (int i = 0; i < resolutions.Length; i++)
        {
            Console.WriteLine($"{(int)resolutions[i]} dpi -> {fileSizes[i]} bytes");
        }

        // Determine which resolution produced the smallest and largest files
        long minSize = long.MaxValue;
        long maxSize = long.MinValue;
        int minIndex = -1;
        int maxIndex = -1;

        for (int i = 0; i < fileSizes.Length; i++)
        {
            if (fileSizes[i] >= 0 && fileSizes[i] < minSize)
            {
                minSize = fileSizes[i];
                minIndex = i;
            }
            if (fileSizes[i] > maxSize)
            {
                maxSize = fileSizes[i];
                maxIndex = i;
            }
        }

        // Report the resolutions with the smallest and largest file sizes
        if (minIndex >= 0 && maxIndex >= 0)
        {
            Console.WriteLine();
            Console.WriteLine($"Smallest file: {resolutions[minIndex]} dpi ({minSize} bytes)");
            Console.WriteLine($"Largest  file: {resolutions[maxIndex]} dpi ({maxSize} bytes)");
        }
    }
}