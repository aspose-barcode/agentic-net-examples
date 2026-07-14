// Title: Barcode generation with audit logging using Aspose.BarCode
// Description: Demonstrates creating a Code128 barcode image while logging generation parameters and outcomes to a file for audit purposes.
// Category-Description: This example belongs to the Aspose.BarCode generation category, showcasing how to configure barcode settings, save the image, and record detailed audit information. It highlights key API classes such as BarcodeGenerator, BaseEncodeType, and EncodeTypes, which developers commonly use for automated barcode creation, compliance tracking, and troubleshooting in enterprise applications.
// Prompt: Implement logging of barcode generation parameters and outcomes using .NET built‑in logging framework for audit trails.
// Tags: barcode, code128, generation, audit, logging, aspose.barcode, image, .net

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.Drawing;

/// <summary>
/// Generates a Code128 barcode image and logs the generation process for audit trails.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Configures barcode parameters, generates the image, and records audit information.
    /// </summary>
    static void Main()
    {
        // Define the path for the audit log file.
        string logFile = "barcode_audit.log";

        // Ensure the audit log file exists; create it with a header if it does not.
        if (!File.Exists(logFile))
        {
            File.WriteAllText(logFile, $"Barcode generation audit log - {DateTime.UtcNow:u}{Environment.NewLine}");
        }

        // Barcode configuration: type, data, and output file.
        BaseEncodeType encodeType = EncodeTypes.Code128;
        string codeText = "123ABC";
        string outputPath = "code128.png";

        // Log the start of the barcode generation process with key parameters.
        File.AppendAllText(logFile,
            $"[{DateTime.UtcNow:u}] Starting barcode generation. Type: {encodeType.TypeName}, CodeText: \"{codeText}\"{Environment.NewLine}");

        // Create and configure the barcode generator.
        using (var generator = new BarcodeGenerator(encodeType, codeText))
        {
            // Set visual appearance and sizing options.
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;   // Bar color
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;        // Background color
            generator.Parameters.Barcode.XDimension.Point = 2f;                // Module size (points)
            generator.Parameters.Barcode.BarHeight.Point = 50f;                // Bar height for 1D barcodes (points)
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;             // Disable auto-sizing

            // Log the configured generator parameters for traceability.
            File.AppendAllText(logFile,
                $"[{DateTime.UtcNow:u}] Configured parameters:{Environment.NewLine}" +
                $"    BarColor: {generator.Parameters.Barcode.BarColor}{Environment.NewLine}" +
                $"    BackColor: {generator.Parameters.BackColor}{Environment.NewLine}" +
                $"    XDimension: {generator.Parameters.Barcode.XDimension.Point} pt{Environment.NewLine}" +
                $"    BarHeight: {generator.Parameters.Barcode.BarHeight.Point} pt{Environment.NewLine}" +
                $"    AutoSizeMode: {generator.Parameters.AutoSizeMode}{Environment.NewLine}");

            try
            {
                // Save the generated barcode image to the specified file.
                generator.Save(outputPath);
                // Log successful save operation.
                File.AppendAllText(logFile,
                    $"[{DateTime.UtcNow:u}] Barcode saved successfully to \"{outputPath}\".{Environment.NewLine}");
                Console.WriteLine($"Barcode generated and saved to {outputPath}");
            }
            catch (Exception ex)
            {
                // Log any errors that occur during generation or saving.
                File.AppendAllText(logFile,
                    $"[{DateTime.UtcNow:u}] Error during barcode generation: {ex.Message}{Environment.NewLine}");
                Console.WriteLine($"Error: {ex.Message}");
            }
        }

        // Log the completion of the entire barcode generation workflow.
        File.AppendAllText(logFile,
            $"[{DateTime.UtcNow:u}] Barcode generation process completed.{Environment.NewLine}");
    }
}