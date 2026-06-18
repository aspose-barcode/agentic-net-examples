using System;
using System.IO;
using System.Collections.Generic;
using System.Text.Json;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Represents barcode information including the decoded text and the bounding rectangle
/// coordinates (in pixel units) of the barcode within the source image.
/// </summary>
class BarcodeInfo
{
    public string CodeText { get; set; }
    public int Left { get; set; }
    public int Top { get; set; }
    public int Right { get; set; }
    public int Bottom { get; set; }
}

/// <summary>
/// Application entry point that reads barcodes from an image, extracts their regions,
/// and saves the data to a JSON file.
/// </summary>
class Program
{
    /// <summary>
    /// Executes the barcode extraction workflow.
    /// </summary>
    static void Main()
    {
        // Path to the PNG image containing barcodes
        string imagePath = "barcode.png";

        // Verify that the image file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Collection to hold extracted barcode region data
        var barcodeRegions = new List<BarcodeInfo>();

        // Initialize a BarCodeReader that can decode all supported symbologies
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate over each detected barcode in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Retrieve the rectangle that describes the barcode's location
                RectangleF rect = result.Region.Rectangle;

                // Convert floating‑point rectangle coordinates to integer pixel values
                int left   = (int)Math.Round((double)rect.X);
                int top    = (int)Math.Round((double)rect.Y);
                int right  = left + (int)Math.Round((double)rect.Width);
                int bottom = top  + (int)Math.Round((double)rect.Height);

                // Add the extracted information to the collection
                barcodeRegions.Add(new BarcodeInfo
                {
                    CodeText = result.CodeText,
                    Left     = left,
                    Top      = top,
                    Right    = right,
                    Bottom   = bottom
                });

                // Output details to the console for verification
                Console.WriteLine($"Detected barcode: {result.CodeText}");
                Console.WriteLine($"Region - Left:{left}, Top:{top}, Right:{right}, Bottom:{bottom}");
            }
        }

        // Serialize the collected barcode data to a JSON file (simple storage substitute for a database)
        string jsonPath = "barcode_regions.json";
        string json = JsonSerializer.Serialize(
            barcodeRegions,
            new JsonSerializerOptions { WriteIndented = true });

        File.WriteAllText(jsonPath, json);
        Console.WriteLine($"Barcode region data saved to {jsonPath}");

        // NOTE: In a real scenario, replace the JSON storage with database code
        // using an appropriate ORM or ADO.NET provider. The above implementation
        // demonstrates the core barcode extraction logic required by the task.
    }
}