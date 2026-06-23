using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates creating a Code128 barcode, exporting its state to XML,
/// and logging the generated file paths.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define a temporary output directory for all generated files.
        string outputDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo");
        Directory.CreateDirectory(outputDir); // Ensure the directory exists.

        // Build full file paths for the barcode image, XML state, and log file.
        string imagePath = Path.Combine(outputDir, "barcode.png");
        string xmlPath   = Path.Combine(outputDir, "barcode_state.xml");
        string logPath   = Path.Combine(outputDir, "log.txt");

        // Create a simple Code128 barcode with the specified data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum calculation for the barcode.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode as a PNG image.
            generator.Save(imagePath);

            // Export the generator's internal state to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // Prepare a log message containing the locations of the generated files.
        string logContent = $"Barcode image saved to: {imagePath}{Environment.NewLine}" +
                            $"Generator state exported to XML: {xmlPath}{Environment.NewLine}" +
                            $"Log generated at: {logPath}{Environment.NewLine}";

        // Output the log message to the console for immediate feedback.
        Console.WriteLine(logContent);

        // Persist the log message to a text file.
        File.WriteAllText(logPath, logContent);
    }
}