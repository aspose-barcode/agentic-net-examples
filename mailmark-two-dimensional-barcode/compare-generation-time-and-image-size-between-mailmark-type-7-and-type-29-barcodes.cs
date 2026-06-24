using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.ComplexBarcode;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generation and timing of Mailmark 2D barcodes (Type 7 and Type 29) using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates two Mailmark barcodes, measures generation time,
    /// and outputs the elapsed time and image size for each type.
    /// </summary>
    static void Main()
    {
        // Prepare two Mailmark 2D codetext objects: one for Type 7 and one for Type 29
        var mailmark7 = CreateMailmark2DCodetext(Mailmark2DType.Type_7);
        var mailmark29 = CreateMailmark2DCodetext(Mailmark2DType.Type_29);

        // Generate barcode for Type 7, measure time and size, then display results
        var result7 = GenerateAndMeasure(mailmark7);
        Console.WriteLine($"Mailmark Type 7 - Generation Time: {result7.timeMs} ms, Image Size: {result7.sizeBytes} bytes");

        // Generate barcode for Type 29, measure time and size, then display results
        var result29 = GenerateAndMeasure(mailmark29);
        Console.WriteLine($"Mailmark Type 29 - Generation Time: {result29.timeMs} ms, Image Size: {result29.sizeBytes} bytes");
    }

    // Creates a Mailmark2DCodetext instance with required fields and the specified DataMatrix type.
    private static Mailmark2DCodetext CreateMailmark2DCodetext(Mailmark2DType matrixType)
    {
        var mailmark = new Mailmark2DCodetext
        {
            // Required integer fields
            ItemID = 16563762,
            SupplyChainID = 384224,

            // Required string fields
            VersionID = "1",
            InformationTypeID = "0",
            DestinationPostCodeAndDPS = "EF61AH8T ",
            RTSFlag = "0",

            // Set the 2D Mailmark size (type)
            DataMatrixType = matrixType
        };

        // Optional: leave CustomerContent empty (default) and use default encode mode
        return mailmark;
    }

    // Generates the barcode image, measures elapsed time, and returns both the time (ms) and image size (bytes).
    private static (long timeMs, long sizeBytes) GenerateAndMeasure(Mailmark2DCodetext mailmark)
    {
        var stopwatch = new Stopwatch();

        // Use a memory stream to avoid writing to disk
        using (var ms = new MemoryStream())
        {
            stopwatch.Start();

            // Generate the barcode and save it as PNG directly into the memory stream
            using (var generator = new ComplexBarcodeGenerator(mailmark))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            stopwatch.Stop();

            // Determine the size of the generated image in bytes
            long size = ms.Length;
            return (stopwatch.ElapsedMilliseconds, size);
        }
    }
}