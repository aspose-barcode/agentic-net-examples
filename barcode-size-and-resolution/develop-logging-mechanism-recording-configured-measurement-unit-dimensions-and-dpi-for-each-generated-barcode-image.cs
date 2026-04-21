using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeLoggingDemo
{
    // Simple logger that writes log entries to a text file.
    internal static class BarcodeLogger
    {
        private static readonly string LogFilePath = "barcode_log.txt";

        public static void Log(string message)
        {
            string entry = $"{DateTime.Now:yyyy-MM-dd HH:mm:ss} - {message}{Environment.NewLine}";
            File.AppendAllText(LogFilePath, entry);
        }
    }

    class Program
    {
        static void Main()
        {
            // Ensure previous log is cleared for a clean run.
            if (File.Exists("barcode_log.txt"))
                File.Delete("barcode_log.txt");

            // Generate a set of sample barcodes with different settings.
            GenerateBarcode(EncodeTypes.Code128, "ABC123", "Code128_Sample.png");
            GenerateBarcode(EncodeTypes.QR, "https://example.com", "QR_Sample.png");
            GenerateBarcode(EncodeTypes.EAN13, "1234567890128", "EAN13_Sample.png");
        }

        private static void GenerateBarcode(BaseEncodeType encodeType, string codeText, string fileName)
        {
            // Create the barcode generator.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure measurement units (using points) and DPI.
                generator.Parameters.ImageWidth.Point = 300f;      // Width = 300 points
                generator.Parameters.ImageHeight.Point = 150f;     // Height = 150 points
                generator.Parameters.Barcode.XDimension.Point = 2f; // X-dimension = 2 points
                generator.Parameters.Barcode.BarHeight.Point = 50f; // Bar height = 50 points (for 1D barcodes)

                // Set resolution (DPI).
                generator.Parameters.Resolution = 300; // 300 DPI

                // Optional: set barcode colors.
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                // Save the barcode image.
                generator.Save(fileName);

                // Log the configured settings.
                string logMessage = $"Barcode Type: {encodeType.GetType().Name}, " +
                                    $"File: {fileName}, " +
                                    $"Width: {generator.Parameters.ImageWidth.Point}pt, " +
                                    $"Height: {generator.Parameters.ImageHeight.Point}pt, " +
                                    $"XDimension: {generator.Parameters.Barcode.XDimension.Point}pt, " +
                                    $"BarHeight: {generator.Parameters.Barcode.BarHeight.Point}pt, " +
                                    $"Resolution: {generator.Parameters.Resolution} DPI";
                BarcodeLogger.Log(logMessage);
            }
        }
    }
}