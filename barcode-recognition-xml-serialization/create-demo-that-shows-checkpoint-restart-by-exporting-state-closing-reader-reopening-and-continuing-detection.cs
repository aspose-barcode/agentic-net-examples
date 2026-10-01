// Title: Barcode checkpoint/restart demo using Aspose.BarCode
// Description: Demonstrates exporting a BarCodeReader state to XML, closing it, reopening, and continuing detection on the same image.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category, showcasing state persistence with BarCodeReader. It uses BarcodeGenerator to create a barcode, BarCodeReader to detect it, and the ExportToXml/ImportFromXml methods for checkpointing. Developers often need to pause and resume barcode scanning in long‑running or distributed applications, and this snippet illustrates the typical workflow.
// Prompt: Create a demo that shows checkpoint/restart by exporting state, closing the reader, reopening, and continuing detection.
// Tags: code128, checkpoint, restart, xml, barcodereader, barcodegenerator, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates how to checkpoint a barcode reader, close it, and later resume detection
/// using Aspose.BarCode's ExportToXml and ImportFromXml functionality.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, reads it, checkpoints the reader,
    /// simulates an application restart, and continues reading from the saved state.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Prepare a temporary working directory for demo files
        // ------------------------------------------------------------
        string workDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeCheckpointDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workDir);

        // Paths for the generated barcode image and the checkpoint XML file
        string barcodePath = Path.Combine(workDir, "sample.png");
        string checkpointPath = Path.Combine(workDir, "reader_state.xml");

        // ------------------------------------------------------------
        // 2. Generate a simple Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // 3. Initial read: create a BarCodeReader, read the barcode, and export its state
        // ------------------------------------------------------------
        BaseDecodeType decodeType = DecodeType.Code128;
        using (var reader = new BarCodeReader(barcodePath, decodeType))
        {
            // Read all barcodes in the image and output the result
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"[Initial] Detected: {result.CodeText} ({result.CodeTypeName})");
            }

            // Export the internal state of the reader to an XML checkpoint file
            reader.ExportToXml(checkpointPath);
        }

        // Ensure the checkpoint file was created
        if (!File.Exists(checkpointPath))
        {
            Console.WriteLine("Failed to export checkpoint.");
            return;
        }

        // ------------------------------------------------------------
        // 4. Simulate application restart: import the reader from XML and continue detection
        // ------------------------------------------------------------
        using (var resumedReader = BarCodeReader.ImportFromXml(checkpointPath))
        {
            // The imported reader does not have an image attached; set it explicitly
            resumedReader.SetBarCodeImage(barcodePath);

            // Continue reading barcodes using the resumed reader
            foreach (BarCodeResult result in resumedReader.ReadBarCodes())
            {
                Console.WriteLine($"[Resumed] Detected: {result.CodeText} ({result.CodeTypeName})");
            }
        }

        // ------------------------------------------------------------
        // 5. Cleanup temporary files (optional)
        // ------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            File.Delete(checkpointPath);
            Directory.Delete(workDir);
        }
        catch
        {
            // Ignored – cleanup failures should not affect demo outcome
        }
    }
}