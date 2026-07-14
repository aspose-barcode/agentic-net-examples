// Title: QR Code Batch Reader to JSON Export
// Description: Reads QR code images from a folder, extracts barcode data, and writes payment details to a JSON file.
// Category-Description: Demonstrates Aspose.BarCode barcode recognition for QR codes, covering image file enumeration, barcode extraction using BarCodeReader, and JSON serialization with System.Text.Json. This example belongs to the “Barcode Recognition and Data Export” category, useful for developers automating payment processing or inventory tracking by converting scanned QR codes into structured data.
// Prompt: Create a console app that reads QR code images from a folder and writes payment details to JSON.
// Tags: qr, barcode, recognition, json, aspose.barcode, system.text.json

using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Represents extracted payment information from a QR code image.
/// </summary>
class PaymentInfo
{
    public string FileName { get; set; }
    public string CodeText { get; set; }
    public string CodeTypeName { get; set; }
    public int X { get; set; }
    public int Y { get; set; }
    public int Width { get; set; }
    public int Height { get; set; }
}

/// <summary>
/// Entry point of the console application that processes QR code images and outputs JSON.
/// </summary>
class Program
{
    /// <summary>
    /// Main method parses optional input folder argument, reads QR codes, and writes results to JSON.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can specify the input folder.</param>
    static void Main(string[] args)
    {
        // Determine the folder containing QR code images; allow override via command‑line argument.
        string inputFolder = "QrImages";
        if (args.Length > 0 && !string.IsNullOrWhiteSpace(args[0]))
        {
            inputFolder = args[0];
        }

        // Verify that the input folder exists.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Folder not found: {inputFolder}");
            return;
        }

        // Define supported image extensions.
        string[] imageExtensions = new[] { ".png", ".jpg", ".jpeg", ".bmp", ".gif" };
        var files = Directory.GetFiles(inputFolder);
        var paymentList = new List<PaymentInfo>();

        // Iterate over each file in the folder.
        foreach (var file in files)
        {
            // Skip files that do not have a supported image extension.
            string ext = Path.GetExtension(file).ToLowerInvariant();
            if (Array.IndexOf(imageExtensions, ext) < 0)
                continue;

            // Ensure the file still exists before processing.
            if (!File.Exists(file))
                continue;

            // Open the image with BarCodeReader configured for QR codes.
            using (var reader = new BarCodeReader(file, DecodeType.QR))
            {
                // Read all barcodes found in the image.
                var results = reader.ReadBarCodes();
                foreach (var result in results)
                {
                    // Extract the bounding rectangle of the detected barcode.
                    var rect = result.Region.Rectangle;

                    // Populate a PaymentInfo instance with extracted data.
                    var info = new PaymentInfo
                    {
                        FileName = Path.GetFileName(file),
                        CodeText = result.CodeText,
                        CodeTypeName = result.CodeTypeName,
                        X = rect.X,
                        Y = rect.Y,
                        Width = rect.Width,
                        Height = rect.Height
                    };

                    // Add the info to the collection.
                    paymentList.Add(info);
                }
            }
        }

        // Serialize the collected payment details to a formatted JSON string.
        string outputPath = Path.Combine(inputFolder, "payment_details.json");
        var jsonOptions = new JsonSerializerOptions { WriteIndented = true };
        string json = JsonSerializer.Serialize(paymentList, jsonOptions);
        File.WriteAllText(outputPath, json);

        // Inform the user of the processing result.
        Console.WriteLine($"Processed {paymentList.Count} barcode(s). Output written to {outputPath}");
    }
}