using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

namespace BarcodeLoggingExample
{
    /// <summary>
    /// Represents a single log entry for barcode generation.
    /// </summary>
    public class LogEntry
    {
        public DateTime Timestamp { get; set; }
        public string Symbology { get; set; }
        public string CodeText { get; set; }
        public string OutputPath { get; set; }
        public bool Success { get; set; }
        public string ErrorMessage { get; set; }

        // Example of a few generation parameters.
        public float? XDimension { get; set; }
        public float? BarHeight { get; set; }
        public float? ImageWidth { get; set; }
        public float? ImageHeight { get; set; }
    }

    class Program
    {
        /// <summary>
        /// Entry point of the application. Generates a barcode, saves it, and logs the operation.
        /// </summary>
        static void Main()
        {
            // Sample barcode data.
            string codeText = "123ABC";
            BaseEncodeType symbology = EncodeTypes.Code128;
            string outputFile = "barcode.png";
            string logFile = "barcode_log.json";

            // Prepare a log entry with initial data.
            var entry = new LogEntry
            {
                Timestamp = DateTime.UtcNow,
                Symbology = symbology.GetType().Name + "." + symbology.ToString(),
                CodeText = codeText,
                OutputPath = Path.GetFullPath(outputFile)
            };

            try
            {
                // Generate and save the barcode.
                using (var generator = new BarcodeGenerator(symbology, codeText))
                {
                    // Set a few generation parameters.
                    generator.Parameters.AutoSizeMode = AutoSizeMode.None;
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarHeight.Point = 40f;
                    generator.Parameters.ImageWidth.Point = 300f;
                    generator.Parameters.ImageHeight.Point = 150f;

                    // Capture parameter values for logging.
                    entry.XDimension = generator.Parameters.Barcode.XDimension.Point;
                    entry.BarHeight = generator.Parameters.Barcode.BarHeight.Point;
                    entry.ImageWidth = generator.Parameters.ImageWidth.Point;
                    entry.ImageHeight = generator.Parameters.ImageHeight.Point;

                    // Save the generated barcode image to file.
                    generator.Save(outputFile);
                }

                // Mark the operation as successful.
                entry.Success = true;
                entry.ErrorMessage = null;
            }
            catch (Exception ex)
            {
                // Record failure details.
                entry.Success = false;
                entry.ErrorMessage = ex.Message;
            }

            // Append the log entry as a JSON line to the log file.
            try
            {
                string json = JsonSerializer.Serialize(entry);
                using (var stream = new FileStream(logFile, FileMode.Append, FileAccess.Write, FileShare.Read))
                using (var writer = new StreamWriter(stream))
                {
                    writer.WriteLine(json);
                }
            }
            catch
            {
                // If logging fails, write to console but do not crash the program.
                Console.WriteLine("Failed to write log entry.");
            }
        }
    }
}