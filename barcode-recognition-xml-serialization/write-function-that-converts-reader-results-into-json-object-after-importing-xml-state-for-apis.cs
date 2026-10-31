// Title: Convert BarCodeReader XML state to JSON output
// Description: Demonstrates generating a QR barcode, exporting the BarCodeReader configuration to XML, importing it back, reading the barcode, and serializing the results to JSON.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showing how to use BarcodeGenerator, BarCodeReader, and related settings. Typical use cases include persisting reader configurations, reusing them across sessions, and converting recognition results into JSON for APIs. Developers often need to export/import reader state and format output for web services.
// Prompt: Write a function that converts reader results into a JSON object after importing the XML state for APIs.
// Tags: barcode, qr, json, xml, import, export, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates barcode generation, reader state export/import, and JSON serialization of results.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a QR code, exports reader settings to XML,
    /// imports them back, reads the barcode, and outputs the results as JSON.
    /// </summary>
    static void Main()
    {
        // Prepare a unique temporary directory for demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);

        // Define file paths for the generated barcode image and the exported reader state
        string barcodePath = Path.Combine(tempDir, "sample.png");
        string readerStatePath = Path.Combine(tempDir, "readerState.xml");

        // 1. Generate a sample QR barcode and save it as PNG
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            generator.Parameters.Barcode.XDimension.Pixels = 4f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // 2. Create a BarCodeReader, configure its settings, and export its state to XML
        using (var reader = new BarCodeReader(barcodePath, DecodeType.AllSupportedTypes))
        {
            reader.BarcodeSettings.StripFNC = true;
            reader.QualitySettings.XDimension = XDimensionMode.Small;
            reader.ExportToXml(readerStatePath);
        }

        // 3. Import the previously saved reader state, assign the image, and read barcodes
        using (var importedReader = BarCodeReader.ImportFromXml(readerStatePath))
        {
            importedReader.SetBarCodeImage(barcodePath);
            importedReader.SetBarCodeReadType(DecodeType.QR);

            // Perform barcode recognition
            BarCodeResult[] results = importedReader.ReadBarCodes();

            // Transform recognition results into DTO objects for JSON serialization
            var dtoList = new List<BarcodeResultDto>();
            foreach (var result in results)
            {
                var rect = result.Region.Rectangle;
                var dto = new BarcodeResultDto
                {
                    CodeTypeName = result.CodeTypeName,
                    CodeText = result.CodeText,
                    ReadingQuality = result.ReadingQuality,
                    RegionX = rect.X,
                    RegionY = rect.Y,
                    RegionWidth = rect.Width,
                    RegionHeight = rect.Height,
                    RegionAngle = result.Region.Angle
                };
                dtoList.Add(dto);
            }

            // Serialize DTO list to formatted JSON and write to console
            string json = JsonSerializer.Serialize(dtoList, new JsonSerializerOptions { WriteIndented = true });
            Console.WriteLine(json);
        }

        // Cleanup temporary files and directory
        try
        {
            if (File.Exists(barcodePath)) File.Delete(barcodePath);
            if (File.Exists(readerStatePath)) File.Delete(readerStatePath);
            Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}

/// <summary>
/// Data transfer object representing selected properties of a barcode recognition result.
/// </summary>
class BarcodeResultDto
{
    public string CodeTypeName { get; set; }
    public string CodeText { get; set; }
    public double ReadingQuality { get; set; }
    public float RegionX { get; set; }
    public float RegionY { get; set; }
    public float RegionWidth { get; set; }
    public float RegionHeight { get; set; }
    public double RegionAngle { get; set; }
}