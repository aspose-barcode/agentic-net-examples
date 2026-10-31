// Title: Restartable barcode scanning with state persistence
// Description: Demonstrates generating a Code128 barcode, reading it, exporting the reader state to XML, and restoring the reader after a simulated restart.
// Category-Description: This example belongs to the Aspose.BarCode scanning and state management category. It showcases the BarCodeReader and BarcodeGenerator classes for barcode generation, reading, and persistence. Developers often need to pause and resume scanning operations, saving configuration and progress to XML for reliability in long‑running or crash‑prone applications.
// Prompt: Implement a restartable barcode scanning service that saves its state to XML and restores it after a crash.
// Tags: barcode, code128, scanning, state persistence, xml, aspose.barcode, generation, recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a restartable barcode scanning service using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a barcode, reads it, saves reader state, restores it, and reads again.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder to store generated files and state.
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeRestartable_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths for the barcode image and the exported reader state.
        string barcodeFile = Path.Combine(workFolder, "barcode.png");
        string stateFile = Path.Combine(workFolder, "readerState.xml");

        // Generate a simple Code128 barcode and save it as a PNG image.
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodeFile, BarCodeImageFormat.Png);
        }

        // First read: configure the reader, decode the barcode, and export its state to XML.
        using (BarCodeReader reader = new BarCodeReader())
        {
            // Enable stripping of FNC characters during decoding.
            reader.BarcodeSettings.StripFNC = true;

            // Load the barcode image and specify the decode type.
            reader.SetBarCodeImage(barcodeFile);
            reader.SetBarCodeReadType(DecodeType.Code128);

            // Perform the read operation.
            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine("First read results:");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Export the current reader configuration and state to an XML file.
            reader.ExportToXml(stateFile);
        }

        // Simulate an application restart by importing the previously saved state.
        using (BarCodeReader restoredReader = BarCodeReader.ImportFromXml(stateFile))
        {
            // Re‑associate the same barcode image and decode type with the restored reader.
            restoredReader.SetBarCodeImage(barcodeFile);
            restoredReader.SetBarCodeReadType(DecodeType.Code128);

            // Perform the read operation again using the restored state.
            BarCodeResult[] results = restoredReader.ReadBarCodes();
            Console.WriteLine("Restored read results:");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // Cleanup temporary files and folder (optional; failures are ignored).
        try
        {
            File.Delete(barcodeFile);
            File.Delete(stateFile);
            Directory.Delete(workFolder);
        }
        catch
        {
            // Ignored - cleanup not critical for demonstration.
        }
    }
}