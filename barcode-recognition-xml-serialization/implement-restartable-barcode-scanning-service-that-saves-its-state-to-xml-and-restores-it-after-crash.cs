// Title: Restartable Barcode Scanning Service with XML State Persistence
// Description: Demonstrates generating a QR barcode, scanning it, exporting the reader's configuration to XML, and restoring the scanner after a simulated restart.
// Category-Description: This example belongs to the Aspose.BarCode scanning and state management category. It showcases the BarCodeGenerator for creating barcodes, BarCodeReader for decoding, and the ExportToXml/ImportFromXml APIs for persisting reader settings. Developers building long‑running or crash‑resilient scanning services use these patterns to save and restore scanner state without re‑configuring each time.
// Prompt: Implement a restartable barcode scanning service that saves its state to XML and restores it after a crash.
// Tags: barcode, qr, scanning, state, xml, export, import, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Sample program that illustrates how to create a QR barcode, read it,
/// export the reader's configuration to XML, and later restore the reader
/// from that XML to continue scanning without re‑initialising settings.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Executes the generate‑scan‑export‑import workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Setup: create a unique temporary folder for all demo artefacts.
        // --------------------------------------------------------------------
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeServiceDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the generated barcode image and the exported reader state.
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string statePath   = Path.Combine(tempFolder, "readerState.xml");

        // --------------------------------------------------------------------
        // Step 1: Generate a QR barcode containing the text "HelloWorld".
        // --------------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "HelloWorld"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created.
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to create barcode image.");
            return;
        }

        // --------------------------------------------------------------------
        // Step 2: Perform the initial scan and export the reader's state to XML.
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.QR))
        {
            // Read all barcodes from the image.
            var results = reader.ReadBarCodes();
            Console.WriteLine("Initial scan results:");
            foreach (var result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Persist the current reader configuration (e.g., decoding options) to an XML file.
            reader.ExportToXml(statePath);
        }

        // Verify that the state file was successfully written.
        if (!File.Exists(statePath))
        {
            Console.WriteLine("Failed to export reader state.");
            return;
        }

        // --------------------------------------------------------------------
        // Step 3: Simulate a service restart by importing the saved state.
        // --------------------------------------------------------------------
        using (var restoredReader = BarCodeReader.ImportFromXml(statePath))
        {
            // The XML does not contain the image source or decode type, so set them explicitly.
            restoredReader.SetBarCodeImage(barcodePath);
            restoredReader.SetBarCodeReadType(DecodeType.QR);

            // Re‑read the barcode using the restored configuration.
            var restoredResults = restoredReader.ReadBarCodes();
            Console.WriteLine("Restored scan results after restart:");
            foreach (var result in restoredResults)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // --------------------------------------------------------------------
        // Cleanup: delete temporary files and folder (optional).
        // --------------------------------------------------------------------
        try
        {
            File.Delete(barcodePath);
            File.Delete(statePath);
            Directory.Delete(tempFolder);
        }
        catch
        {
            // Suppress any exceptions during cleanup to avoid breaking the demo flow.
        }
    }
}