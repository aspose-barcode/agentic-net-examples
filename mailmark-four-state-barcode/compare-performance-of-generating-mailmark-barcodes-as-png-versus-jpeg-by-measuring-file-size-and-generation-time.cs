// Title: Mailmark Barcode Generation Performance: PNG vs JPEG
// Description: Demonstrates generating a Mailmark 2D barcode as PNG and JPEG, measuring generation time and file size to compare performance.
// Category-Description: This example belongs to the Aspose.BarCode performance testing category, showcasing how to use ComplexBarcodeGenerator and Mailmark2DCodetext to create postal Mailmark barcodes. Developers often need to evaluate output formats (PNG, JPEG) for size and speed when integrating barcode generation into high‑throughput mailing systems.
// Prompt: Compare performance of generating Mailmark barcodes as PNG versus JPEG by measuring file size and generation time.
// Tags: mailmark, barcode, performance, png, jpeg, file size, generation time, aspose.barcode, complexbarcodegenerator

using System;
using System.IO;
using System.Diagnostics;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates a Mailmark 2D barcode in PNG and JPEG formats,
/// then reports the generation time and resulting file sizes.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary files, generates the barcode,
    /// measures performance, and writes results to the console.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the output files
        string tempDir = Path.Combine(Path.GetTempPath(), "MailmarkPerf_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define output file paths for PNG and JPEG images
        string pngPath = Path.Combine(tempDir, "mailmark.png");
        string jpegPath = Path.Combine(tempDir, "mailmark.jpg");

        // Prepare Mailmark 2D codetext with required fields
        Mailmark2DCodetext mailmark = new Mailmark2DCodetext();
        mailmark.UPUCountryID = "JGB ";
        mailmark.InformationTypeID = "0";
        mailmark.VersionID = "1";
        mailmark.Class = "1";
        mailmark.SupplyChainID = 123;
        mailmark.ItemID = 1234;
        mailmark.DestinationPostCodeAndDPS = "EF61AH8T ";
        mailmark.RTSFlag = "0";
        mailmark.ReturnToSenderPostCode = " ";
        mailmark.CustomerContent = "CUSTOM";
        mailmark.DataMatrixType = Mailmark2DType.Type_7;

        // -------------------- Generate PNG --------------------
        Stopwatch swPng = new Stopwatch();
        swPng.Start();
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Set module size (pixel dimension) for the barcode
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the barcode as a PNG image
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }
        swPng.Stop();

        // -------------------- Generate JPEG --------------------
        Stopwatch swJpeg = new Stopwatch();
        swJpeg.Start();
        using (ComplexBarcodeGenerator generator = new ComplexBarcodeGenerator(mailmark))
        {
            // Reuse the same module size for consistency
            generator.Parameters.Barcode.XDimension.Pixels = 4;
            // Save the barcode as a JPEG image
            generator.Save(jpegPath, BarCodeImageFormat.Jpeg);
        }
        swJpeg.Stop();

        // Retrieve file sizes for both generated images
        long pngSize = new FileInfo(pngPath).Length;
        long jpegSize = new FileInfo(jpegPath).Length;

        // Output performance results to the console
        Console.WriteLine($"PNG  - Time: {swPng.ElapsedMilliseconds} ms, Size: {pngSize} bytes");
        Console.WriteLine($"JPEG - Time: {swJpeg.ElapsedMilliseconds} ms, Size: {jpegSize} bytes");
    }
}