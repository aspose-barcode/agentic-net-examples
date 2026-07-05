// Title: Barcode XDimension Logging Example
// Description: Demonstrates generating a Code128 barcode with a specific XDimension and recognizing it using minimal XDimension mode while logging the exact values used.
// Prompt: Implement a feature that logs the exact XDimension and MinimalXDimension values used for each image.
// Tags: barcode, code128, xdimension, recognition, logging, aspose.barcode, aspose.drawing

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that creates a barcode with a defined XDimension,
/// reads it back using minimal XDimension mode, and logs the dimension values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Initialize a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set the XDimension (module width) to 2 points for generation
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Create a memory stream to hold the generated barcode image
            using (var imageStream = new MemoryStream())
            {
                // Generate the barcode image and save it as PNG into the stream
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    bitmap.Save(imageStream, ImageFormat.Png);
                }

                // Reset stream position to the beginning for reading
                imageStream.Position = 0;

                // Initialize a barcode reader for recognition
                using (var reader = new BarCodeReader())
                {
                    // Assign the generated image to the reader
                    reader.SetBarCodeImage(imageStream);

                    // Configure the reader to use minimal XDimension mode
                    reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
                    // Set the minimal XDimension value (in points) for recognition
                    reader.QualitySettings.MinimalXDimension = 3f;

                    // Iterate over all detected barcodes in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Log the XDimension used during barcode generation
                        Console.WriteLine($"Generated XDimension (points): {generator.Parameters.Barcode.XDimension.Point}");

                        // Log the MinimalXDimension used during barcode recognition
                        Console.WriteLine($"Recognition MinimalXDimension (points): {reader.QualitySettings.MinimalXDimension}");

                        // Output details of the detected barcode region
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Detected barcode region: X={bounds.X}, Y={bounds.Y}, Width={bounds.Width}, Height={bounds.Height}");

                        // Output the decoded text from the barcode
                        Console.WriteLine($"Decoded text: {result.CodeText}");
                        Console.WriteLine();
                    }
                }
            }
        }
    }
}