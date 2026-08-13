// Title: Barcode checkpoint/restart demo using Aspose.BarCode
// Description: Demonstrates exporting a BarCodeReader state to XML, closing it, then importing and continuing detection on the same image.
// Category-Description: This example belongs to the Aspose.BarCode recognition category, illustrating how to use BarCodeReader's checkpoint feature. It covers exporting reader settings with ExportToXml, importing with ImportFromXml, and resuming barcode detection. Developers working with large image batches or needing to pause/resume processing can use these APIs to manage state efficiently.
// Prompt: Create a demo that shows checkpoint/restart by exporting state, closing the reader, reopening, and continuing detection.
// Tags: barcode, checkpoint, restart, export, import, aspose.barcode, coderecognition, code128, xml

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates checkpoint/restart functionality of Aspose.BarCode's BarCodeReader.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, saves reader state, reloads it, and continues detection.
    /// </summary>
    static void Main()
    {
        // Paths for the barcode image and the checkpoint file
        string barcodePath = "barcode.png";
        string checkpointPath = "reader_state.xml";

        // -------------------------------------------------
        // Step 1: Generate a sample barcode image (Code128)
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode to a PNG file
            generator.Save(barcodePath);
        }

        // Verify that the barcode image was created successfully
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // -------------------------------------------------
        // Step 2: Create a reader, set image, and export state
        // -------------------------------------------------
        using (var reader = new BarCodeReader())
        {
            // Restrict detection to Code128 symbology
            reader.SetBarCodeReadType(DecodeType.Code128);

            // Load the barcode image into the reader
            reader.SetBarCodeImage(barcodePath);

            // Export the reader's configuration (checkpoint) to an XML file
            // Note: The image itself is not saved; it must be reloaded after import
            reader.ExportToXml(checkpointPath);
        }

        // -------------------------------------------------
        // Step 3: Reopen the reader from the checkpoint and continue detection
        // -------------------------------------------------
        if (!File.Exists(checkpointPath))
        {
            Console.WriteLine("Checkpoint file not found.");
            return;
        }

        // Import the saved settings; this creates a new BarCodeReader instance
        using (var resumedReader = BarCodeReader.ImportFromXml(checkpointPath))
        {
            // Reassign the image because ImportFromXml restores only settings
            resumedReader.SetBarCodeImage(barcodePath);

            // Perform barcode detection using the resumed reader
            foreach (var result in resumedReader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
            }
        }
    }
}