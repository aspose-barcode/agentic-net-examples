// Title: DecodeType vs MultiDecodeType performance comparison
// Description: Demonstrates how limiting the DecodeType to a single symbology versus using MultiDecodeType affects barcode recognition speed.
// Prompt: Measure the impact of limiting DecodeType versus using MultyDecodeType on overall recognition speed.
// Tags: barcode, decode, multidecode, performance, aspnet, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that generates several barcode images and measures the
/// recognition time when using a single <see cref="DecodeType"/> versus a
/// <see cref="MultiDecodeType"/> that includes multiple symbologies.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates barcodes, reads them with
    /// different decode configurations, and reports average recognition times.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a temporary folder for the generated barcode images.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeSample");
        if (!Directory.Exists(tempFolder))
            Directory.CreateDirectory(tempFolder);

        // --------------------------------------------------------------------
        // Define the barcode specifications: symbology, text, and output file name.
        // --------------------------------------------------------------------
        var specs = new List<(BaseEncodeType encode, string text, string fileName)>
        {
            (EncodeTypes.Code128, "CODE128_SAMPLE", "code128.png"),
            (EncodeTypes.QR, "QR_SAMPLE", "qr.png"),
            (EncodeTypes.DataMatrix, "DATAMATRIX_SAMPLE", "datamatrix.png")
        };

        // --------------------------------------------------------------------
        // Generate barcode images based on the specifications.
        // --------------------------------------------------------------------
        var imagePaths = new List<string>();
        foreach (var spec in specs)
        {
            string path = Path.Combine(tempFolder, spec.fileName);
            using (var generator = new BarcodeGenerator(spec.encode, spec.text))
            {
                generator.Save(path);
            }
            imagePaths.Add(path);
        }

        // --------------------------------------------------------------------
        // Prepare decode configurations:
        //   - limitedDecode: only Code128 is allowed.
        //   - multiDecode:   Code128, QR, and DataMatrix are allowed.
        // --------------------------------------------------------------------
        var limitedDecode = DecodeType.Code128; // example limited to Code128
        var multiDecode = new MultiDecodeType(DecodeType.Code128, DecodeType.QR, DecodeType.DataMatrix);

        // --------------------------------------------------------------------
        // Containers for timing results.
        // --------------------------------------------------------------------
        var limitedTimes = new List<long>();
        var multiTimes = new List<long>();

        // --------------------------------------------------------------------
        // Iterate over each generated image and measure recognition speed for both
        // decode configurations.
        // --------------------------------------------------------------------
        foreach (string imagePath in imagePaths)
        {
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"File not found: {imagePath}");
                continue;
            }

            // ---- Limited DecodeType (single symbology) ----
            using (var readerLimited = new BarCodeReader(imagePath, limitedDecode))
            {
                var sw = Stopwatch.StartNew();
                var results = readerLimited.ReadBarCodes();
                sw.Stop();

                limitedTimes.Add(sw.ElapsedMilliseconds);
                Console.WriteLine($"Limited decode on '{Path.GetFileName(imagePath)}' found {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms.");
            }

            // ---- MultiDecodeType (multiple symbologies) ----
            using (var readerMulti = new BarCodeReader(imagePath, multiDecode))
            {
                var sw = Stopwatch.StartNew();
                var results = readerMulti.ReadBarCodes();
                sw.Stop();

                multiTimes.Add(sw.ElapsedMilliseconds);
                Console.WriteLine($"Multi decode on '{Path.GetFileName(imagePath)}' found {results.Length} barcode(s) in {sw.ElapsedMilliseconds} ms.");
            }
        }

        // --------------------------------------------------------------------
        // Compute and display average recognition times for both configurations.
        // --------------------------------------------------------------------
        double avgLimited = limitedTimes.Count > 0 ? (double)limitedTimes.Sum() / limitedTimes.Count : 0;
        double avgMulti = multiTimes.Count > 0 ? (double)multiTimes.Sum() / multiTimes.Count : 0;

        Console.WriteLine();
        Console.WriteLine($"Average recognition time (limited DecodeType): {avgLimited:F2} ms");
        Console.WriteLine($"Average recognition time (MultiDecodeType): {avgMulti:F2} ms");
    }
}