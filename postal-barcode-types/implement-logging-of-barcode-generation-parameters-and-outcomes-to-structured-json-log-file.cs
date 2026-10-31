// Title: Barcode Generation with JSON Logging
// Description: Demonstrates generating a QR barcode using Aspose.BarCode and logging generation parameters and results to a JSON file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to create barcodes, configure key parameters (such as XDimension and error correction level), and persist operation details in a structured JSON log. Developers working with barcode creation, image output, and audit trails can use this pattern to capture metadata for debugging, reporting, or compliance purposes.
// Prompt: Implement logging of barcode generation parameters and outcomes to a structured JSON log file.
// Tags: qr, barcode, generation, json, logging, aspose.barcode, png

using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Represents a log entry for a barcode generation operation.
/// </summary>
class LogEntry
{
    public string Timestamp { get; set; }
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public float XDimensionPixels { get; set; }
    public string ErrorLevel { get; set; }
    public string OutputPath { get; set; }
    public bool Success { get; set; }
    public string Message { get; set; }
}

/// <summary>
/// Demo program that generates a QR barcode and writes a JSON log.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates the barcode, logs details, and writes output files.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary output folder.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define the path for the JSON log file.
        string logFilePath = Path.Combine(outputDir, "barcode_log.json");

        // Sample barcode data.
        string symbologyName = "QR";
        string codeText = "Aspose.BarCode JSON Log Demo";

        // Resolve the symbology enum value via reflection.
        FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {symbologyName}");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

        // Determine the output image file path.
        string imagePath = Path.Combine(outputDir, $"{encodeType.TypeName}.png");

        // Initialize the log entry with basic information.
        LogEntry log = new LogEntry
        {
            Timestamp = DateTime.UtcNow.ToString("o"),
            Symbology = encodeType.TypeName,
            CodeText = codeText,
            OutputPath = imagePath,
            Success = false
        };

        try
        {
            // Create the barcode generator with the selected symbology and text.
            using (BarcodeGenerator generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure barcode appearance and error correction.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;
                generator.Parameters.Barcode.BarColor = Color.Black;
                generator.Parameters.BackColor = Color.White;

                // Save the generated barcode as a PNG image.
                generator.Save(imagePath, BarCodeImageFormat.Png);

                // Populate additional log details after successful generation.
                log.XDimensionPixels = generator.Parameters.Barcode.XDimension.Pixels;
                log.ErrorLevel = generator.Parameters.Barcode.QR.ErrorLevel.ToString();
                log.Success = true;
                log.Message = "Barcode generated successfully.";
            }
        }
        catch (Exception ex)
        {
            // Capture any errors that occurred during generation.
            log.Success = false;
            log.Message = $"Error: {ex.Message}";
        }

        // Serialize the log entry to formatted JSON and append it to the log file.
        var options = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(log, options);
        File.AppendAllText(logFilePath, json + Environment.NewLine, Encoding.UTF8);

        // Output the locations of the generated files for verification.
        Console.WriteLine($"Barcode image: {imagePath}");
        Console.WriteLine($"Log file: {logFilePath}");
    }
}