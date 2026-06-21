using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates QR code generation and recognition using Aspose.BarCode.
/// Shows the effect of the XDimension mode on detection performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, then reads it twice:
    /// once with the default XDimension mode and once with the minimal XDimension mode.
    /// </summary>
    static void Main()
    {
        // Sample QR code text to encode.
        const string qrText = "https://example.com";

        // Create a QR code generator and configure error correction level.
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, qrText))
        {
            // Set error correction level to Medium (LevelM).
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code into a memory stream as PNG.
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position for reading.

                // Load the PNG image into a Bitmap for barcode recognition.
                using (var bitmap = new Bitmap(ms))
                {
                    // ------------------------------------------------------------
                    // Detection with UseMinimalXDimension disabled (default mode)
                    // ------------------------------------------------------------
                    using (var readerDefault = new BarCodeReader(bitmap, DecodeType.QR))
                    {
                        // Ensure the reader uses the automatic XDimension mode.
                        readerDefault.QualitySettings.XDimension = XDimensionMode.Auto;

                        // Measure detection time.
                        var swDefault = Stopwatch.StartNew();
                        var resultsDefault = readerDefault.ReadBarCodes();
                        swDefault.Stop();

                        // Output results.
                        Console.WriteLine("UseMinimalXDimension disabled:");
                        Console.WriteLine($"  Detected {resultsDefault.Length} barcode(s) in {swDefault.ElapsedMilliseconds} ms");
                        foreach (var result in resultsDefault)
                        {
                            Console.WriteLine($"    Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }

                    // ------------------------------------------------------------
                    // Detection with UseMinimalXDimension enabled
                    // ------------------------------------------------------------
                    using (var readerMinimal = new BarCodeReader(bitmap, DecodeType.QR))
                    {
                        // Enable minimal XDimension mode and set a minimal size (e.g., 2 pixels).
                        readerMinimal.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                        readerMinimal.QualitySettings.MinimalXDimension = 2f;

                        // Measure detection time.
                        var swMinimal = Stopwatch.StartNew();
                        var resultsMinimal = readerMinimal.ReadBarCodes();
                        swMinimal.Stop();

                        // Output results.
                        Console.WriteLine("UseMinimalXDimension enabled:");
                        Console.WriteLine($"  Detected {resultsMinimal.Length} barcode(s) in {swMinimal.ElapsedMilliseconds} ms");
                        foreach (var result in resultsMinimal)
                        {
                            Console.WriteLine($"    Type: {result.CodeTypeName}, Text: {result.CodeText}");
                        }
                    }
                }
            }
        }
    }
}