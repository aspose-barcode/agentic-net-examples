using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a QR barcode, storing it in memory,
/// and then reading it back to extract metadata. This simulates
/// a live‑camera capture scenario in a console application.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a QR code, reads it from a memory stream,
    /// and outputs barcode details to the console.
    /// </summary>
    static void Main()
    {
        // NOTE: Real‑time camera capture is not feasible in a console snippet.
        // This example simulates a live feed by generating a barcode image,
        // then immediately reading it to extract metadata.

        // Generate a sample QR barcode and store it in a memory stream.
        using (var ms = new MemoryStream())
        {
            // Create a barcode generator for a QR code with the specified text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "LiveCameraSimulation"))
            {
                // Optional: adjust appearance if needed (e.g., error correction level).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

                // Save the generated barcode image to the memory stream in PNG format.
                generator.Save(ms, BarCodeImageFormat.Png);
            }

            // Reset the stream position to the beginning before reading.
            ms.Position = 0;

            // Read the barcode from the memory stream using a QR decoder.
            using (var reader = new BarCodeReader(ms, DecodeType.QR))
            {
                // Iterate through all detected barcodes (should be one in this case).
                foreach (var result in reader.ReadBarCodes())
                {
                    // Output basic barcode information.
                    Console.WriteLine("Barcode Type: " + result.CodeTypeName);
                    Console.WriteLine("Code Text   : " + result.CodeText);
                    Console.WriteLine("Confidence  : " + (int)result.Confidence);
                    Console.WriteLine("ReadingQuality: " + result.ReadingQuality);

                    // Region provides the location of the barcode in the image.
                    var region = result.Region.Rectangle;
                    int x = (int)Math.Round((double)region.X);
                    int y = (int)Math.Round((double)region.Y);
                    int width = (int)Math.Round((double)region.Width);
                    int height = (int)Math.Round((double)region.Height);

                    // Output the barcode's bounding rectangle.
                    Console.WriteLine($"Region      : X={x}, Y={y}, Width={width}, Height={height}");
                    Console.WriteLine();
                }
            }
        }
    }
}