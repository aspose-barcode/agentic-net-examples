using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating barcodes, saving them to a memory stream,
/// and then recognizing them using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, reads them back, and prints detection details.
    /// </summary>
    static void Main()
    {
        // Define a set of sample barcodes to generate and later recognize.
        var samples = new (BaseEncodeType type, string text)[]
        {
            (EncodeTypes.Code128, "ABC123"),
            (EncodeTypes.QR, "https://example.com"),
            (EncodeTypes.DataMatrix, "DataMatrixSample")
        };

        // Process each sample barcode.
        foreach (var sample in samples)
        {
            // Create a barcode generator for the current sample.
            using (var generator = new BarcodeGenerator(sample.type, sample.text))
            {
                // Set a high resolution for better recognition accuracy.
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode image into a memory stream.
                using (var ms = new MemoryStream())
                {
                    generator.Save(ms, BarCodeImageFormat.Png);
                    ms.Position = 0; // Reset stream position for reading.

                    // Load the image from the memory stream as a bitmap.
                    using (var bitmap = new Bitmap(ms))
                    {
                        // Initialize a barcode reader that can detect all supported types.
                        using (var reader = new BarCodeReader(bitmap, DecodeType.AllSupportedTypes))
                        {
                            // Perform the recognition.
                            var results = reader.ReadBarCodes();

                            // Output basic information about the generated barcode.
                            Console.WriteLine($"Generated {sample.type.TypeName} with text '{sample.text}'");
                            Console.WriteLine($"Found {reader.FoundCount} barcode(s).");

                            // Iterate over each detection result and display details.
                            foreach (var result in results)
                            {
                                Console.WriteLine($"  Type : {result.CodeTypeName}");
                                Console.WriteLine($"  Text : {result.CodeText}");

                                // Extract and round the region coordinates.
                                var region = result.Region.Rectangle;
                                int x = (int)Math.Round((double)region.X);
                                int y = (int)Math.Round((double)region.Y);
                                int width = (int)Math.Round((double)region.Width);
                                int height = (int)Math.Round((double)region.Height);
                                Console.WriteLine($"  Region : X={x}, Y={y}, W={width}, H={height}");
                                Console.WriteLine($"  Angle  : {result.Region.Angle}");
                            }

                            // Demonstrate direct access to the FoundBarCodes collection.
                            Console.WriteLine("Accessing FoundBarCodes property directly:");
                            foreach (var fb in reader.FoundBarCodes)
                            {
                                Console.WriteLine($"  [Found] Type: {fb.CodeTypeName}, Text: {fb.CodeText}");
                            }
                        }
                    }
                }
            }

            // Separate output for each sample for readability.
            Console.WriteLine(new string('-', 40));
        }
    }
}