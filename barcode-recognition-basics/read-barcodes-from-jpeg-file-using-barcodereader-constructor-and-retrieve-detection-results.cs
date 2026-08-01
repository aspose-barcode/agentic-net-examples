// Title: Read barcodes from JPEG using BarCodeReader
// Description: Demonstrates how to load a JPEG image, generate a sample barcode if missing, and read all supported barcode types using Aspose.BarCode's BarCodeReader.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the BarCodeReader class for detecting and extracting barcode data from image files. Typical use cases include inventory scanning, document processing, and automated data capture where developers need to read multiple symbologies from various image formats. The snippet illustrates initializing the reader, iterating over detection results, and accessing barcode type, text, and region information.
// Prompt: Read barcodes from a JPEG file using BarCodeReader constructor and retrieve detection results.
// Tags: barcode, jpeg, read, detection, aspnet, aspnetcore, aspose.barcode, barcodereader, decode, allsupportedtypes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads barcodes from a JPEG image using Aspose.BarCode's <see cref="BarCodeReader"/>.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Accepts an optional image path argument, generates a sample barcode if the file is missing,
    /// and prints detection results for all supported barcode types.
    /// </summary>
    /// <param name="args">Command‑line arguments; first argument can be a custom image path.</param>
    static void Main(string[] args)
    {
        // Determine the image file to process: use the first argument if supplied, otherwise default to "sample.jpg".
        string imagePath = args.Length > 0 ? args[0] : "sample.jpg";

        // If the specified file does not exist, create a simple Code128 barcode image for demonstration purposes.
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                generator.Save(imagePath, BarCodeImageFormat.Jpeg);
            }
            Console.WriteLine($"Generated sample barcode image at '{imagePath}'.");
        }

        // Double‑check that the file now exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"File not found: {imagePath}");
            return;
        }

        // Initialize the reader to scan the image for all supported barcode symbologies.
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Optional: configure quality settings (default is NormalQuality).
            // reader.QualitySettings = QualitySettings.NormalQuality;

            // Iterate through each detected barcode and output its details.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                Console.WriteLine($"BarCode Text: {result.CodeText}");

                // Retrieve and display the bounding rectangle of the detected barcode region.
                var bounds = result.Region.Rectangle;
                Console.WriteLine($"Region - X:{bounds.X}, Y:{bounds.Y}, Width:{bounds.Width}, Height:{bounds.Height}");
                Console.WriteLine();
            }
        }
    }
}