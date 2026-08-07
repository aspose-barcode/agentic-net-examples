// Title: Compare Mailmark Type 7 and Type 29 barcode generation performance
// Description: Demonstrates generating Mailmark 2‑D barcodes of type 7 and type 29, measuring the time taken and the resulting PNG image size.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, focusing on complex 2‑D symbologies such as Mailmark. It shows how to configure Mailmark2DCodetext, use ComplexBarcodeGenerator, and evaluate performance metrics—common tasks for developers creating high‑volume mailing solutions or optimizing barcode rendering.
// Prompt: Compare generation time and image size between Mailmark type 7 and type 29 barcodes.
// Tags: mailmark, barcode, generation, performance, png, aspose.barcode, complexbarcode

using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Generates Mailmark type 7 and type 29 barcodes, measures generation time,
/// and reports the PNG image sizes. Useful for performance comparison of different
/// Mailmark module configurations.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Prepares Mailmark data, creates two barcode variants,
    /// measures their generation times and output sizes, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // Prepare common Mailmark2D data (shared between both barcode types)
        const string destinationPostCode = "EF61AH8T "; // trailing space required by the specification
        const string versionId = "1";
        const string informationTypeId = "0";
        const string mailClass = "0";
        const int supplyChainId = 384224;
        const int itemId = 16563762;

        // Configure Mailmark type 7 (24x24 modules)
        var mailmark7 = new Mailmark2DCodetext
        {
            VersionID = versionId,
            InformationTypeID = informationTypeId,
            Class = mailClass,
            SupplyChainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodeAndDPS = destinationPostCode,
            DataMatrixType = Mailmark2DType.Type_7
        };

        // Configure Mailmark type 29 (16x48 modules)
        var mailmark29 = new Mailmark2DCodetext
        {
            VersionID = versionId,
            InformationTypeID = informationTypeId,
            Class = mailClass,
            SupplyChainID = supplyChainId,
            ItemID = itemId,
            DestinationPostCodeAndDPS = destinationPostCode,
            DataMatrixType = Mailmark2DType.Type_29
        };

        // Measure generation time and image size for Type 7
        var stopwatch = new Stopwatch();
        long size7;
        stopwatch.Start();
        using (var generator7 = new ComplexBarcodeGenerator(mailmark7))
        {
            using (var ms7 = new MemoryStream())
            {
                generator7.Save(ms7, BarCodeImageFormat.Png);
                size7 = ms7.Length; // capture PNG byte length
            }
        }
        stopwatch.Stop();
        long time7 = stopwatch.ElapsedMilliseconds;

        // Measure generation time and image size for Type 29
        stopwatch.Reset();
        long size29;
        stopwatch.Start();
        using (var generator29 = new ComplexBarcodeGenerator(mailmark29))
        {
            using (var ms29 = new MemoryStream())
            {
                generator29.Save(ms29, BarCodeImageFormat.Png);
                size29 = ms29.Length; // capture PNG byte length
            }
        }
        stopwatch.Stop();
        long time29 = stopwatch.ElapsedMilliseconds;

        // Output the performance results
        Console.WriteLine($"Mailmark Type 7: Generation time = {time7} ms, Image size = {size7} bytes");
        Console.WriteLine($"Mailmark Type 29: Generation time = {time29} ms, Image size = {size29} bytes");
    }
}