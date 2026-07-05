// Title: Barcode Generation, Detection, and Checkpoint Export
// Description: Generates a Code128 barcode image, reads it, and exports a checkpoint XML after each detection.
// Prompt: Implement checkpoint functionality by exporting the state to XML after each successful barcode detection.
// Tags: barcode, code128, generation, detection, xml, checkpoint, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates creating a barcode, reading it, and saving a checkpoint XML after each detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode image, reads it, and exports checkpoints.
    /// </summary>
    static void Main()
    {
        // Define the file path for the generated barcode image
        string imagePath = "barcode.png";

        // ------------------------------------------------------------
        // Generate a Code128 barcode and save it to the specified file
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath);
        }

        // Verify that the barcode image file was successfully created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // ------------------------------------------------------------
        // Initialize a barcode reader that supports all barcode types
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            int index = 0; // Counter for detected barcodes

            // Iterate through each detected barcode in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output detection details to the console
                Console.WriteLine($"Detected [{index}]: Type = {result.CodeTypeName}, Text = {result.CodeText}");

                // Export the current state of the reader to an XML checkpoint file
                string checkpointFile = $"checkpoint_{index}.xml";
                reader.ExportToXml(checkpointFile);
                Console.WriteLine($"Checkpoint saved to '{checkpointFile}'.");

                index++;
            }

            // If no barcodes were found, inform the user
            if (index == 0)
            {
                Console.WriteLine("No barcodes were detected in the image.");
            }
        }
    }
}