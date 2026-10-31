// Title: Compare generation time and image size of Mailmark Type 7 vs Type 29 barcodes
// Description: This example generates Mailmark 2D barcodes of type 7 and type 29, measures the time taken to create each image and reports the resulting file sizes.
// Category-Description: Demonstrates Aspose.BarCode complex barcode generation using the ComplexBarcodeGenerator and Mailmark2DCodetext classes. Typical scenarios include creating Mailmark 2D symbols for postal automation, comparing performance characteristics of different Mailmark types, and evaluating output file sizes for storage or transmission. Developers working with postal barcode standards often need to benchmark generation speed and image footprint across symbology variants.
// Prompt: Compare generation time and image size between Mailmark type 7 and type 29 barcodes.
// Tags: barcode symbology, generation, performance, mailmark, png, aspose.barcode, complexbarcode, csharp

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

/// <summary>
/// Provides an example that generates Mailmark 2D barcodes of two different types,
/// measures their generation time, and reports the resulting image file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files for Mailmark Type 7 and Type 29,
    /// invokes the generation routine, prints performance data, and cleans up the files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the generated images
        string tempDir = Path.Combine(Path.GetTempPath(), "MailmarkCompare_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define output file paths for each Mailmark type
        string pathType7 = Path.Combine(tempDir, "Mailmark2D_Type7.png");
        string pathType29 = Path.Combine(tempDir, "Mailmark2D_Type29.png");

        // Generate the barcodes and capture timing and size information
        var result7 = GenerateMailmark(Mailmark2DType.Type_7, pathType7);
        var result29 = GenerateMailmark(Mailmark2DType.Type_29, pathType29);

        // Output the comparison results to the console
        Console.WriteLine($"Mailmark Type 7 - Generation Time: {result7.Time.TotalMilliseconds} ms, Image Size: {result7.Size} bytes");
        Console.WriteLine($"Mailmark Type 29 - Generation Time: {result29.Time.TotalMilliseconds} ms, Image Size: {result29.Size} bytes");

        // Optional cleanup of temporary files and directory
        try
        {
            File.Delete(pathType7);
            File.Delete(pathType29);
            Directory.Delete(tempDir);
        }
        catch
        {
            // Suppress any errors during cleanup
        }
    }

    /// <summary>
    /// Generates a Mailmark 2D barcode of the specified type, saves it to the given path,
    /// and returns the elapsed generation time and resulting file size.
    /// </summary>
    /// <param name="type">The Mailmark 2D type (e.g., Type_7 or Type_29).</param>
    /// <param name="outputPath">The full file path where the PNG image will be saved.</param>
    /// <returns>A tuple containing the generation <see cref="TimeSpan"/> and the image file size in bytes.</returns>
    private static (TimeSpan Time, long Size) GenerateMailmark(Mailmark2DType type, string outputPath)
    {
        // Prepare Mailmark 2D codetext with sample data
        Mailmark2DCodetext mailmark = new Mailmark2DCodetext
        {
            UPUCountryID = "JGB ",
            InformationTypeID = "0",
            VersionID = "1",
            Class = "1",
            SupplyChainID = 123,
            ItemID = 1234,
            CustomerContent = "CUSTOM",
            ReturnToSenderPostCode = " QWE2 "
        };
        mailmark.DataMatrixType = type;

        // Start timing the barcode generation
        Stopwatch sw = Stopwatch.StartNew();
        using (var generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set the X-dimension (module size) in pixels
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the generated barcode as a PNG image
            generator.Save(outputPath, BarCodeImageFormat.Png);
        }
        sw.Stop();

        // Determine the size of the generated file
        long fileSize = new FileInfo(outputPath).Length;
        return (sw.Elapsed, fileSize);
    }
}