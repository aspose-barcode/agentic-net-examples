using System;
using System.Collections.Generic;
using System.IO;
using System.Xml.Serialization;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Represents a scanned barcode result with its text, type, and region coordinates.
/// </summary>
public class ScanResult
{
    public string CodeText { get; set; }
    public string CodeTypeName { get; set; }
    public float RegionX { get; set; }
    public float RegionY { get; set; }
    public float RegionWidth { get; set; }
    public float RegionHeight { get; set; }
}

/// <summary>
/// Provides methods to persist and retrieve scanning results to/from an XML file.
/// </summary>
public static class StatePersistence
{
    /// <summary>
    /// Serializes a list of <see cref="ScanResult"/> objects to the specified file path.
    /// </summary>
    /// <param name="filePath">The file path where the XML will be saved.</param>
    /// <param name="results">The list of scan results to serialize.</param>
    public static void Save(string filePath, List<ScanResult> results)
    {
        var serializer = new XmlSerializer(typeof(List<ScanResult>));
        using (var stream = new FileStream(filePath, FileMode.Create, FileAccess.Write))
        {
            serializer.Serialize(stream, results);
        }
    }

    /// <summary>
    /// Deserializes a list of <see cref="ScanResult"/> objects from the specified file path.
    /// </summary>
    /// <param name="filePath">The file path of the XML to read.</param>
    /// <returns>A list of scan results; empty if the file does not exist.</returns>
    public static List<ScanResult> Load(string filePath)
    {
        if (!File.Exists(filePath))
        {
            Console.WriteLine($"State file not found: {filePath}");
            return new List<ScanResult>();
        }

        var serializer = new XmlSerializer(typeof(List<ScanResult>));
        using (var stream = new FileStream(filePath, FileMode.Open, FileAccess.Read))
        {
            return (List<ScanResult>)serializer.Deserialize(stream);
        }
    }
}

/// <summary>
/// Demonstrates barcode generation, scanning, state persistence, and restoration.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a barcode, scans it, saves state, clears memory, and restores state.
    /// </summary>
    static void Main()
    {
        const string imagePath = "barcode.png";
        const string statePath = "state.xml";

        // Step 1: Generate a sample barcode image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            generator.Save(imagePath);
        }

        // Step 2: Scan the barcode and collect results.
        var scannedResults = new List<ScanResult>();
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                var region = result.Region.Rectangle;
                scannedResults.Add(new ScanResult
                {
                    CodeText = result.CodeText,
                    CodeTypeName = result.CodeTypeName,
                    RegionX = (float)region.X,
                    RegionY = (float)region.Y,
                    RegionWidth = (float)region.Width,
                    RegionHeight = (float)region.Height
                });
                Console.WriteLine($"Scanned: {result.CodeTypeName} - {result.CodeText}");
            }
        }

        // Step 3: Persist the scanning state to XML.
        StatePersistence.Save(statePath, scannedResults);
        Console.WriteLine($"State saved to {statePath}");

        // Simulate a crash by clearing the in‑memory list.
        scannedResults.Clear();

        // Step 4: Restore the state from XML after "restart".
        var restoredResults = StatePersistence.Load(statePath);
        Console.WriteLine($"State restored from {statePath}");

        // Step 5: Display restored results.
        foreach (var res in restoredResults)
        {
            Console.WriteLine($"Restored: {res.CodeTypeName} - {res.CodeText} (Region: {res.RegionX},{res.RegionY},{res.RegionWidth},{res.RegionHeight})");
        }
    }
}