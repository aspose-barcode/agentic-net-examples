using System;
using System.Diagnostics;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeDiagnosticLogger
{
    // Simple logger that writes messages to a log file.
    internal static class Logger
    {
        private static readonly string LogFilePath = "barcode_log.txt";

        public static void LogInfo(string message)
        {
            WriteLog("INFO", message);
        }

        public static void LogWarning(string message)
        {
            WriteLog("WARN", message);
        }

        public static void LogError(string message)
        {
            WriteLog("ERROR", message);
        }

        private static void WriteLog(string level, string message)
        {
            string entry = $"{DateTime.UtcNow:O} [{level}] {message}";
            try
            {
                File.AppendAllText(LogFilePath, entry + Environment.NewLine);
            }
            catch
            {
                // If logging fails, fallback to console.
                Console.WriteLine(entry);
            }
        }
    }

    internal class Program
    {
        // Generates a barcode, logs diagnostic information and saves the image.
        private static void GenerateBarcode(string symbologyName, string codeText, string outputFile)
        {
            // Resolve symbology name to EncodeTypes static field using reflection.
            FieldInfo field = typeof(EncodeTypes).GetField(symbologyName, BindingFlags.Public | BindingFlags.Static);
            if (field == null)
            {
                Logger.LogWarning($"Symbology '{symbologyName}' not found. Skipping generation.");
                return;
            }

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);
            Stopwatch sw = new Stopwatch();

            try
            {
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Example of setting some parameters.
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;
                    generator.Parameters.Resolution = 96;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    sw.Start();
                    using (Bitmap bitmap = generator.GenerateBarCodeImage())
                    {
                        sw.Stop();
                        // Save the image.
                        bitmap.Save(outputFile, ImageFormat.Png);
                    }

                    // Log diagnostic details.
                    Logger.LogInfo($"Generated '{symbologyName}' barcode.");
                    Logger.LogInfo($"CodeText: {codeText}");
                    Logger.LogInfo($"OutputFile: {outputFile}");
                    Logger.LogInfo($"GenerationTimeMs: {sw.ElapsedMilliseconds}");
                    Logger.LogInfo($"ImageWidthPt: {generator.Parameters.ImageWidth.Point}");
                    Logger.LogInfo($"ImageHeightPt: {generator.Parameters.ImageHeight.Point}");
                    Logger.LogInfo($"ResolutionDpi: {generator.Parameters.Resolution}");
                }
            }
            catch (Exception ex)
            {
                sw.Stop();
                Logger.LogError($"Failed to generate '{symbologyName}' barcode. Exception: {ex.Message}");
            }
        }

        private static void Main()
        {
            // Ensure the log file starts fresh.
            try
            {
                if (File.Exists("barcode_log.txt"))
                    File.Delete("barcode_log.txt");
            }
            catch { /* ignore */ }

            // Sample barcodes to generate.
            GenerateBarcode("Code128", "ABC123456", "code128.png");
            GenerateBarcode("QR", "https://example.com", "qr.png");
            GenerateBarcode("DataMatrix", "SampleDataMatrix", "datamatrix.png");

            Logger.LogInfo("Barcode generation completed.");
        }
    }
}