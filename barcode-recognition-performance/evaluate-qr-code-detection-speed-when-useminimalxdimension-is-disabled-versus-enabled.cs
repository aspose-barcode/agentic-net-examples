using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a QR code image in memory
        using (var ms = new MemoryStream())
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
            {
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Ensure the stream is ready for reading
            ms.Position = 0;

            // Measure detection time with default XDimension (disabled UseMinimalXDimension)
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Use normal detection mode
                reader.QualitySettings.XDimension = XDimensionMode.Normal;

                var stopwatch = Stopwatch.StartNew();
                foreach (var result in reader.ReadBarCodes())
                {
                    // No processing needed; just force detection
                }
                stopwatch.Stop();

                Console.WriteLine($"Detection time (XDimension = Normal): {stopwatch.ElapsedMilliseconds} ms");
            }

            // Reset stream for the second test
            ms.Position = 0;

            // Measure detection time with UseMinimalXDimension enabled
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Enable UseMinimalXDimension mode and set a minimal size
                reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                reader.QualitySettings.MinimalXDimension = 2; // pixels

                var stopwatch = Stopwatch.StartNew();
                foreach (var result in reader.ReadBarCodes())
                {
                    // No processing needed; just force detection
                }
                stopwatch.Stop();

                Console.WriteLine($"Detection time (XDimension = UseMinimalXDimension): {stopwatch.ElapsedMilliseconds} ms");
            }
        }
    }
}