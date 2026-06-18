using System;
using System.IO;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents information about a detected barcode, including its type, text, region, and orientation.
/// </summary>
class BarcodeInfo
{
    /// <summary>Gets or sets the barcode type name.</summary>
    public string Type { get; set; }

    /// <summary>Gets or sets the decoded text of the barcode.</summary>
    public string Text { get; set; }

    /// <summary>Gets or sets the region (position and size) of the barcode within the image.</summary>
    public RegionInfo Region { get; set; }

    /// <summary>Gets or sets the orientation angle of the barcode region.</summary>
    public double Orientation { get; set; }
}

/// <summary>
/// Represents the rectangular region of a barcode within an image.
/// </summary>
class RegionInfo
{
    /// <summary>Gets or sets the X coordinate of the region's top‑left corner.</summary>
    public float X { get; set; }

    /// <summary>Gets or sets the Y coordinate of the region's top‑left corner.</summary>
    public float Y { get; set; }

    /// <summary>Gets or sets the width of the region.</summary>
    public float Width { get; set; }

    /// <summary>Gets or sets the height of the region.</summary>
    public float Height { get; set; }
}

/// <summary>
/// Main program class that reads barcodes from an image and exports their information to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Reads barcodes from a PNG file, extracts details, and writes them to a JSON file.
    /// </summary>
    static void Main()
    {
        // Define input image path and output JSON file path.
        string inputPath = "barcode.png";
        string outputPath = "barcodeInfo.json";

        // Verify that the input file exists before proceeding.
        if (!File.Exists(inputPath))
        {
            Console.WriteLine($"Input file not found: {inputPath}");
            return;
        }

        // Initialize the barcode reader for all supported barcode types.
        using (var reader = new BarCodeReader(inputPath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes present in the image.
            var results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
                return;
            }

            // Prepare a list to hold information about each detected barcode.
            var infoList = new System.Collections.Generic.List<BarcodeInfo>();

            // Iterate over each detection result and map it to our BarcodeInfo model.
            foreach (var result in results)
            {
                // Extract the rectangle defining the barcode's region.
                var regionRect = result.Region.Rectangle;

                // Populate a BarcodeInfo instance with type, text, region, and orientation.
                var info = new BarcodeInfo
                {
                    Type = result.CodeTypeName,
                    Text = result.CodeText,
                    Region = new RegionInfo
                    {
                        X = regionRect.X,
                        Y = regionRect.Y,
                        Width = regionRect.Width,
                        Height = regionRect.Height
                    },
                    Orientation = result.Region.Angle
                };

                // Add the populated info object to the collection.
                infoList.Add(info);
            }

            // Configure JSON serialization options for pretty‑printed output.
            var jsonOptions = new JsonSerializerOptions { WriteIndented = true };

            // Serialize the list of barcode information to a JSON string.
            string json = JsonSerializer.Serialize(infoList, jsonOptions);

            // Write the JSON string to the specified output file.
            File.WriteAllText(outputPath, json);

            // Notify the user that the export was successful.
            Console.WriteLine($"Barcode information exported to {outputPath}");
        }
    }
}