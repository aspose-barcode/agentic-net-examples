using System;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Simple logger that appends timestamped messages to a text file.
/// </summary>
class DiagnosticLogger
{
    // Path to the log file (relative to the executable directory)
    private static readonly string LogFilePath = "barcode_log.txt";

    /// <summary>
    /// Appends a message with a UTC timestamp to the log file.
    /// </summary>
    /// <param name="message">The message to log.</param>
    public static void Log(string message)
    {
        // Build log entry with ISO 8601 timestamp
        string entry = $"{DateTime.UtcNow:O} - {message}";
        // Append entry followed by a newline
        File.AppendAllText(LogFilePath, entry + Environment.NewLine);
    }
}

/// <summary>
/// Demonstrates barcode generation using Aspose.BarCode and logs diagnostic information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, saves it, and logs details.
    /// </summary>
    static void Main()
    {
        // Define barcode symbology and data to encode
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC456";

        // Log the start of the barcode generation process
        DiagnosticLogger.Log($"Starting barcode generation. Symbology: {encodeType}, CodeText: \"{codeText}\"");

        // Initialize a stopwatch to measure generation time
        var stopwatch = new Stopwatch();
        stopwatch.Start();

        try
        {
            // Create a BarcodeGenerator with the specified type and text
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // ----- Configure generation parameters -----
                generator.Parameters.Resolution = 300f;                     // DPI
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;              // Width in points
                generator.Parameters.ImageHeight.Point = 150f;             // Height in points
                generator.Parameters.RotationAngle = 0f;                   // No rotation
                generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
                generator.Parameters.Barcode.BarColor = Color.Black;      // Bar color
                generator.Parameters.BackColor = Color.White;             // Background color

                // Save the generated barcode image to a PNG file
                string outputPath = "barcode.png";
                generator.Save(outputPath);
                DiagnosticLogger.Log($"Barcode saved to \"{outputPath}\".");

                // ----- Log the selected parameters for traceability -----
                DiagnosticLogger.Log($"Resolution: {generator.Parameters.Resolution} DPI");
                DiagnosticLogger.Log($"AutoSizeMode: {generator.Parameters.AutoSizeMode}");
                DiagnosticLogger.Log($"ImageWidth: {generator.Parameters.ImageWidth.Point} pt");
                DiagnosticLogger.Log($"ImageHeight: {generator.Parameters.ImageHeight.Point} pt");
                DiagnosticLogger.Log($"RotationAngle: {generator.Parameters.RotationAngle}°");
                DiagnosticLogger.Log($"ChecksumEnabled: {generator.Parameters.Barcode.IsChecksumEnabled}");
                DiagnosticLogger.Log($"BarColor: {generator.Parameters.Barcode.BarColor}");
                DiagnosticLogger.Log($"BackColor: {generator.Parameters.BackColor}");
            }
        }
        catch (Exception ex)
        {
            // Log any warnings or errors that occur during generation
            DiagnosticLogger.Log($"Warning/Error during barcode generation: {ex.GetType().Name} - {ex.Message}");
        }
        finally
        {
            // Stop the stopwatch and log elapsed time
            stopwatch.Stop();
            DiagnosticLogger.Log($"Barcode generation elapsed time: {stopwatch.ElapsedMilliseconds} ms");
        }

        // Inform the user that the process is complete
        Console.WriteLine("Barcode generation completed. See \"barcode_log.txt\" for details.");
    }
}