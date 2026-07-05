// Title: Barcode checkpoint/restart demo
// Description: Demonstrates exporting a reader's state to XML, closing the reader, reopening it, and continuing barcode detection.
// Prompt: Create a demo that shows checkpoint/restart by exporting state, closing the reader, reopening, and continuing detection.
// Tags: barcode, checkpoint, restart, export, import, aspose.barcoderecognition, aspose.barcodegeneration

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates checkpoint/restart functionality for barcode detection using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode image if missing, reads it, exports the reader state,
    /// simulates an application restart by importing the state, and continues detection.
    /// </summary>
    static void Main()
    {
        // Paths for the barcode image and the checkpoint file
        string imagePath = "sample.png";
        string checkpointPath = "reader_state.xml";

        // Ensure a barcode image exists; create one if missing
        if (!File.Exists(imagePath))
        {
            // Generate a simple Code128 barcode and save it to disk
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
            {
                generator.Save(imagePath);
                Console.WriteLine($"Generated barcode image: {imagePath}");
            }
        }

        // First detection pass – read the barcode and export reader state
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Perform detection and process the first result
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"First read – Type: {result.CodeTypeName}, Text: {result.CodeText}");

                // Export current reader settings to XML (checkpoint)
                reader.ExportToXml(checkpointPath);
                Console.WriteLine($"Reader state exported to: {checkpointPath}");
                break; // Demonstrate checkpoint after first result
            }
        }

        // Simulate application restart: import settings, set image again, continue detection
        using (var reader = BarCodeReader.ImportFromXml(checkpointPath))
        {
            if (reader == null)
            {
                Console.WriteLine("Failed to import reader state.");
                return;
            }

            // The imported reader does not retain the image; set it explicitly
            if (!File.Exists(imagePath))
            {
                Console.WriteLine($"Image file not found: {imagePath}");
                return;
            }
            reader.SetBarCodeImage(imagePath);

            // Continue detection from the imported state
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Second read – Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }
        }
    }
}