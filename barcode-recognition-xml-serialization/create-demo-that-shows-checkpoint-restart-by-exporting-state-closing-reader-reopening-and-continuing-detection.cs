// Title: Demonstrate checkpoint/restart of barcode detection using state export/import
// Description: This example generates a PDF417 barcode, exports the BarCodeReader state to XML, closes the reader, reopens it, and continues detection on the saved image.
// Category-Description: Shows how to use Aspose.BarCode's checkpoint feature for barcode recognition. It covers BarCodeGenerator for creating barcodes, BarCodeReader for configuring settings, ExportToXml and ImportFromXml for persisting reader state, and DecodeType for specifying symbology. Useful for scenarios where long-running scans need to be paused and resumed, such as batch processing or fault‑tolerant pipelines.
// Prompt: Create a demo that shows checkpoint/restart by exporting state, closing the reader, reopening, and continuing detection.
// Tags: pdf417, checkpoint, restart, xml, barcode generation, barcode recognition

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates exporting a BarCodeReader state, closing it, and later importing the state to continue detection.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the demo. Generates a barcode, checkpoints the reader state, and resumes detection.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all demo files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the generated barcode image and the checkpoint XML file.
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string checkpointPath = Path.Combine(tempFolder, "readerState.xml");

        // ------------------------------------------------------------
        // 1. Generate a sample PDF417 barcode and save it as PNG.
        // ------------------------------------------------------------
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Pdf417, "DemoCheckpoint"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 2;
            generator.Parameters.Barcode.Pdf417.Columns = 5;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // ------------------------------------------------------------
        // 2. Initialise a BarCodeReader without an image, configure settings,
        //    and export its state to an XML checkpoint file.
        // ------------------------------------------------------------
        using (BarCodeReader reader = new BarCodeReader())
        {
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.ExportToXml(checkpointPath);
        }

        // ------------------------------------------------------------
        // 3. Import the previously saved reader state, assign the barcode image,
        //    specify the decode type, and continue detection.
        // ------------------------------------------------------------
        using (BarCodeReader reader = BarCodeReader.ImportFromXml(checkpointPath))
        {
            if (!File.Exists(barcodePath))
            {
                Console.WriteLine("Barcode image not found.");
                return;
            }

            reader.SetBarCodeImage(barcodePath);
            reader.SetBarCodeReadType(DecodeType.Pdf417);

            BarCodeResult[] results = reader.ReadBarCodes();
            Console.WriteLine($"Barcodes read: {results.Length}");
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }
        }

        // ------------------------------------------------------------
        // 4. Clean up temporary files and folder.
        // ------------------------------------------------------------
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(checkpointPath)) File.Delete(checkpointPath);
            Directory.Delete(tempFolder, true);
        }
        catch
        {
            // Suppress any cleanup errors to avoid breaking the demo flow.
        }
    }
}