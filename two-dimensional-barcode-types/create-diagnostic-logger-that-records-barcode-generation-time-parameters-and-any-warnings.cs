// Title: Diagnostic Logger for Barcode Generation
// Description: Demonstrates creating a logger that records barcode generation time, parameters, and warnings while generating various barcodes using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use BarcodeGenerator, EncodeTypes, and related parameter classes to produce different symbologies (Code128, QR, DataMatrix). Typical use cases include batch barcode creation, performance monitoring, and diagnostic logging for troubleshooting. Developers often need to capture generation metrics, configuration details, and handle warnings or errors during the process.
// Prompt: Create a diagnostic logger that records barcode generation time, parameters, and any warnings.
// Tags: barcode, symbology, generation, logging, diagnostics, aspose.barcode, png, csharp

using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Simple diagnostic logger that writes timestamped entries to both console and a log file.
/// </summary>
class DiagnosticLogger
{
    private readonly string _logFilePath;

    /// <summary>
    /// Initializes a new instance of <see cref="DiagnosticLogger"/> and clears the log file.
    /// </summary>
    /// <param name="logFilePath">Path to the log file.</param>
    public DiagnosticLogger(string logFilePath)
    {
        _logFilePath = logFilePath;
        // Ensure the log file starts empty.
        File.WriteAllText(_logFilePath, string.Empty);
    }

    /// <summary>
    /// Writes an informational message.
    /// </summary>
    public void LogInfo(string message) => Log("INFO", message);

    /// <summary>
    /// Writes a warning message.
    /// </summary>
    public void LogWarning(string message) => Log("WARN", message);

    /// <summary>
    /// Writes an error message.
    /// </summary>
    public void LogError(string message) => Log("ERROR", message);

    // Formats and records a log entry.
    private void Log(string level, string message)
    {
        string entry = $"{DateTime.UtcNow:O} [{level}] {message}";
        Console.WriteLine(entry);
        File.AppendAllText(_logFilePath, entry + Environment.NewLine);
    }
}

/// <summary>
/// Generates several barcodes, logs diagnostic information, and saves images to disk.
/// </summary>
class Program
{
    static void Main()
    {
        // Initialize the diagnostic logger.
        var logger = new DiagnosticLogger("barcode_log.txt");
        logger.LogInfo("Barcode generation started.");

        // Define barcode specifications: symbology, text, and custom configuration.
        var barcodes = new List<(BaseEncodeType type, string codeText, Action<BarcodeGenerator> configure)>
        {
            // Code128 example.
            (EncodeTypes.Code128, "123ABC456", generator =>
            {
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 300f;
                generator.Parameters.ImageHeight.Point = 100f;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 10f;
            }),

            // QR code example.
            (EncodeTypes.QR, "https://example.com", generator =>
            {
                generator.Parameters.Barcode.XDimension.Point = 3f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Barcode.QR.ECIEncoding = ECIEncodings.UTF8;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 250f;
                generator.Parameters.ImageHeight.Point = 250f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.Above;
                generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Calibri";
                generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 9f;
            }),

            // DataMatrix example.
            (EncodeTypes.DataMatrix, "DataMatrixSample", generator =>
            {
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.DataMatrix.DataMatrixVersion = DataMatrixVersion.ECC200_20x20;
                generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
                generator.Parameters.ImageWidth.Point = 200f;
                generator.Parameters.ImageHeight.Point = 200f;
                generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None; // hide human‑readable text
            })
        };

        // Iterate over each barcode definition, generate, log, and save.
        foreach (var (type, codeText, configure) in barcodes)
        {
            // Default symbology name fallback.
            string symbologyName = type.GetType().Name;

            try
            {
                // Resolve a friendly symbology name via reflection.
                var field = typeof(EncodeTypes).GetField(type.ToString());
                if (field != null)
                {
                    symbologyName = field.Name;
                }

                logger.LogInfo($"Generating barcode: Symbology={symbologyName}, CodeText=\"{codeText}\"");

                // Start timing the generation.
                var stopwatch = Stopwatch.StartNew();

                using (var generator = new BarcodeGenerator(type, codeText))
                {
                    // Apply custom configuration for this barcode.
                    configure(generator);

                    // Log the effective parameters for diagnostics.
                    logger.LogInfo($"Parameters -> XDimension={generator.Parameters.Barcode.XDimension.Point}pt, " +
                                   $"ImageWidth={generator.Parameters.ImageWidth.Point}pt, " +
                                   $"ImageHeight={generator.Parameters.ImageHeight.Point}pt, " +
                                   $"BarColor={generator.Parameters.Barcode.BarColor}, " +
                                   $"BackColor={generator.Parameters.BackColor}");

                    // Save the generated barcode as a PNG file.
                    string fileName = $"{symbologyName}_{codeText.Replace("/", "_")}.png";
                    generator.Save(fileName, BarCodeImageFormat.Png);
                    logger.LogInfo($"Saved barcode image to \"{fileName}\"");
                }

                // Stop timing and log elapsed time.
                stopwatch.Stop();
                logger.LogInfo($"Generation time: {stopwatch.ElapsedMilliseconds} ms");
            }
            catch (BarCodeException ex)
            {
                // Log known barcode-specific warnings.
                logger.LogWarning($"BarCodeException for symbology {symbologyName}: {ex.Message}");
            }
            catch (Exception ex)
            {
                // Log any unexpected errors.
                logger.LogError($"Unexpected error for symbology {symbologyName}: {ex.Message}");
            }
        }

        logger.LogInfo("Barcode generation completed.");
    }
}