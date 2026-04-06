using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Output file for the barcode image
        string outputPath = "barcode.png";

        // Create a barcode generator for Code128
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            // Set the text to encode
            generator.CodeText = "1234567890";

            // Configure measurement units using points
            generator.Parameters.ImageWidth.Point = 300f;      // Image width in points
            generator.Parameters.ImageHeight.Point = 150f;     // Image height in points
            generator.Parameters.Barcode.XDimension.Point = 2f;   // X-dimension in points
            generator.Parameters.Barcode.BarHeight.Point = 40f;   // Bar height in points

            // Set resolution (DPI)
            generator.Parameters.Resolution = 300f;

            // Save the barcode image
            generator.Save(outputPath);

            // Log the configuration details
            LogConfiguration(generator, outputPath);
        }
    }

    static void LogConfiguration(BarcodeGenerator generator, string filePath)
    {
        // Retrieve configured values
        float widthPt = generator.Parameters.ImageWidth.Point;
        float heightPt = generator.Parameters.ImageHeight.Point;
        float xDimPt = generator.Parameters.Barcode.XDimension.Point;
        float barHeightPt = generator.Parameters.Barcode.BarHeight.Point;
        float dpi = generator.Parameters.Resolution;

        // Build log entry
        string log = $"Barcode generated: {filePath}{Environment.NewLine}" +
                     $"Image Width: {widthPt} pt{Environment.NewLine}" +
                     $"Image Height: {heightPt} pt{Environment.NewLine}" +
                     $"X-Dimension: {xDimPt} pt{Environment.NewLine}" +
                     $"Bar Height: {barHeightPt} pt{Environment.NewLine}" +
                     $"Resolution (DPI): {dpi}{Environment.NewLine}";

        // Output to console
        Console.WriteLine(log);

        // Append to a log file
        string logFile = "barcode_log.txt";
        File.AppendAllText(logFile, log);
    }
}