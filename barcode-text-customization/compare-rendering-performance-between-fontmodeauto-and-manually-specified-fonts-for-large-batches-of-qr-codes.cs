using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates performance measurement of QR code generation using Aspose.BarCode
/// with different font sizing modes (Auto vs Manual).
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates QR codes, measures execution time for
    /// automatic and manual font modes, and writes the results to the console.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // Prepare sample data: generate 5 random GUID strings for QR codes
        // ------------------------------------------------------------
        string[] data = new string[5];
        for (int i = 0; i < data.Length; i++)
        {
            data[i] = Guid.NewGuid().ToString();
        }

        // ------------------------------------------------------------
        // Measure performance when using FontMode.Auto (automatic font sizing)
        // ------------------------------------------------------------
        var stopwatchAuto = Stopwatch.StartNew(); // start timing

        for (int i = 0; i < data.Length; i++)
        {
            // Create a barcode generator for a QR code with the current data string
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, data[i]))
            {
                // Enable automatic font sizing
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Auto;

                // Generate the barcode image and write it to a memory stream
                // (avoids disk I/O for a fair performance comparison)
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        stopwatchAuto.Stop(); // stop timing for Auto mode

        // ------------------------------------------------------------
        // Measure performance when using FontMode.Manual (explicit font settings)
        // ------------------------------------------------------------
        var stopwatchManual = Stopwatch.StartNew(); // start timing

        for (int i = 0; i < data.Length; i++)
        {
            // Create a barcode generator for a QR code with the current data string
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, data[i]))
            {
                // Set manual font sizing and specify font details
                generator.Parameters.Barcode.CodeTextParameters.FontMode = FontMode.Manual;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 12f;

                // Generate the barcode image and write it to a memory stream
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                }
            }
        }

        stopwatchManual.Stop(); // stop timing for Manual mode

        // ------------------------------------------------------------
        // Output the elapsed times for both font modes
        // ------------------------------------------------------------
        Console.WriteLine($"FontMode.Auto elapsed time: {stopwatchAuto.ElapsedMilliseconds} ms");
        Console.WriteLine($"FontMode.Manual elapsed time: {stopwatchManual.ElapsedMilliseconds} ms");
    }
}