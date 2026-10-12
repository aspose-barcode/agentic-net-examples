// Title: Barcode Generation with JSON Logging Example
// Description: Demonstrates creating a Code128 barcode image and recording generation parameters and results to a structured JSON log file.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to use the BarcodeGenerator class to produce barcodes, configure visual parameters, and persist operation details. Developers often need to log barcode creation settings and outcomes for auditing, debugging, or analytics; this snippet illustrates capturing those details in a JSON file using System.Text.Json.
// Prompt: Implement logging of barcode generation parameters and outcomes to a structured JSON log file.
// Tags: barcode, code128, json logging, aspose.barcode, image generation, parameters

using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Holds visual parameter values of a generated barcode for logging purposes.
/// </summary>
class ParametersInfo
{
    public float XDimension { get; set; }
    public float BarHeight { get; set; }
    public string BarColor { get; set; }
    public string BackColor { get; set; }
    public float RotationAngle { get; set; }
}

/// <summary>
/// Represents a complete log entry for a barcode generation operation.
/// </summary>
class BarcodeLog
{
    public DateTime Timestamp { get; set; }
    public string Symbology { get; set; }
    public string CodeText { get; set; }
    public ParametersInfo Parameters { get; set; }
    public string Outcome { get; set; }
    public string OutputPath { get; set; }
    public string ErrorMessage { get; set; }
}

/// <summary>
/// Entry point for the barcode generation demo.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a barcode image, logs parameters and outcome to JSON, and saves both files.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for output files.
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define paths for the barcode image and the JSON log file.
        string imagePath = Path.Combine(outputDir, "barcode.png");
        string logPath = Path.Combine(outputDir, "log.json");

        // Choose barcode symbology and the text to encode.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "1234567890";

        // Initialize the log entry with static information.
        var log = new BarcodeLog
        {
            Timestamp = DateTime.UtcNow,
            Symbology = encodeType.TypeName,
            CodeText = codeText,
            Outcome = "Success",
            OutputPath = imagePath
        };

        try
        {
            // Create the barcode generator with the selected symbology and data.
            using (var generator = new BarcodeGenerator(encodeType, codeText))
            {
                // Configure visual parameters of the barcode.
                generator.Parameters.Barcode.XDimension.Point = 2f;
                generator.Parameters.Barcode.BarHeight.Point = 30f;
                generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                generator.Parameters.BackColor = Aspose.Drawing.Color.White;
                generator.Parameters.RotationAngle = 0f;

                // Capture the configured parameters for logging.
                log.Parameters = new ParametersInfo
                {
                    XDimension = generator.Parameters.Barcode.XDimension.Point,
                    BarHeight = generator.Parameters.Barcode.BarHeight.Point,
                    BarColor = generator.Parameters.Barcode.BarColor.ToString(),
                    BackColor = generator.Parameters.BackColor.ToString(),
                    RotationAngle = generator.Parameters.RotationAngle
                };

                // Save the generated barcode image to the specified path.
                generator.Save(imagePath, BarCodeImageFormat.Png);
            }
        }
        catch (Exception ex)
        {
            // Record failure details in the log if an exception occurs.
            log.Outcome = "Failure";
            log.ErrorMessage = ex.Message;
        }

        // Serialize the log object to a formatted JSON string.
        string json = JsonSerializer.Serialize(log, new JsonSerializerOptions { WriteIndented = true });

        // Write the JSON log to the file system.
        File.WriteAllText(logPath, json);

        // Output the locations of the generated files for user reference.
        Console.WriteLine($"Barcode image saved to: {imagePath}");
        Console.WriteLine($"Log written to: {logPath}");
    }
}