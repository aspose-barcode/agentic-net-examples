using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading barcode reader settings from an XML file,
/// processing an image, and outputting detected barcode information.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Accepts optional command‑line arguments for the XML state file and image file.
    /// </summary>
    /// <param name="args">
    /// args[0] – path to the XML state file (default: "state.xml").
    /// args[1] – path to the image file (default: "sample.png").
    /// </param>
    static void Main(string[] args)
    {
        // Resolve the XML state file path; use default if not supplied.
        string xmlPath = args.Length > 0 ? args[0] : "state.xml";

        // Resolve the image file path; use default if not supplied.
        string imagePath = args.Length > 1 ? args[1] : "sample.png";

        // Verify that the XML state file exists before proceeding.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML state file not found: {xmlPath}");
            return;
        }

        // Verify that the image file exists before proceeding.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader within a using block to ensure proper disposal.
        using (var reader = new BarCodeReader())
        {
            // Load reader configuration from the specified XML file.
            BarCodeReader.ImportFromXml(xmlPath);

            // Assign the image that will be processed for barcode detection.
            reader.SetBarCodeImage(imagePath);

            // Ensure the reader scans for all supported barcode symbologies.
            reader.BarCodeReadType = DecodeType.AllSupportedTypes;

            // Execute the barcode recognition process.
            var results = reader.ReadBarCodes();

            // Check if any barcodes were detected and output appropriate messages.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes detected.");
            }
            else
            {
                // Iterate through each detected barcode and display its type and text.
                foreach (var result in results)
                {
                    Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                    Console.WriteLine($"Code Text: {result.CodeText}");
                }
            }
        }
    }
}