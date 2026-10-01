// Title: Barcode detection with checkpoint export to XML
// Description: Demonstrates generating a Code128 barcode, reading it, and exporting a checkpoint XML after each successful detection.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It shows how to use BarcodeGenerator to create barcodes, BarCodeReader to detect them, and the ExportToXml method to save the reader’s state after each detection. Developers working with barcode scanning workflows often need to persist intermediate results for debugging, auditing, or resuming processing, making checkpoint functionality essential.
// Prompt: Implement checkpoint functionality by exporting the state to XML after each successful barcode detection.
// Tags: barcode, code128, checkpoint, xml, export, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, reads it, and creates XML checkpoints after each detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Create a temporary working directory to hold generated files
        string workDir = Path.Combine(Path.GetTempPath(), "BarcodeCheckpoint_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Define paths for the barcode image and the folder that will store checkpoint XML files
        string barcodePath = Path.Combine(workDir, "sample.png");
        string checkpointDir = Path.Combine(workDir, "Checkpoints");
        Directory.CreateDirectory(checkpointDir);

        // Generate a simple Code128 barcode and save it as a PNG image
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "ABC123456"))
        {
            // Optional: set foreground (barcode) and background colors
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
            generator.Parameters.BackColor = Aspose.Drawing.Color.White;

            // Persist the barcode image to disk
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was created before attempting to read it
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // Initialize a reader for the generated image, configured to decode Code128 symbology
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            int detectionIndex = 0;

            // Iterate over all detected barcodes in the image
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                detectionIndex++;

                // Output detection details to the console
                Console.WriteLine($"Detection {detectionIndex}:");
                Console.WriteLine($"  Code Text : {result.CodeText}");
                Console.WriteLine($"  Symbology : {result.CodeTypeName}");

                // Build the checkpoint file path for the current detection
                string checkpointPath = Path.Combine(checkpointDir, $"checkpoint_{detectionIndex}.xml");

                // Export the reader's current state (settings and last result) to an XML file
                try
                {
                    reader.ExportToXml(checkpointPath);
                    Console.WriteLine($"  Checkpoint saved to: {checkpointPath}");
                }
                catch (Exception ex)
                {
                    Console.WriteLine($"  Failed to export checkpoint: {ex.Message}");
                }
            }
        }

        // Optional cleanup: delete the temporary working directory and its contents
        // Directory.Delete(workDir, true);
    }
}