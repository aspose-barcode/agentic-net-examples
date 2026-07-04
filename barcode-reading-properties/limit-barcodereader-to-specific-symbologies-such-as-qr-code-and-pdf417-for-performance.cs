// Title: Limit BarCodeReader to Specific Symbologies (QR and PDF417)
// Description: Demonstrates generating QR and PDF417 barcodes, then reading them while restricting the BarCodeReader to those symbologies for better performance.
// Prompt: Limit BarCodeReader to specific symbologies such as QR Code and PDF417 for performance.
// Tags: barcode symbology, read, qr, pdf417, performance, aspnet

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates QR Code and PDF417 barcode images,
/// then reads them back while limiting the reader to those two symbologies
/// to improve decoding performance.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcode images and decodes them using a restricted BarCodeReader.
    /// </summary>
    static void Main()
    {
        // ---------- Generate sample QR Code image ----------
        string qrPath = "qr.png";
        using (var qrGenerator = new BarcodeGenerator(EncodeTypes.QR, "Sample QR Text"))
        {
            // Save the QR Code image to disk
            qrGenerator.Save(qrPath);
        }

        // ---------- Generate sample PDF417 image ----------
        string pdf417Path = "pdf417.png";
        using (var pdf417Generator = new BarcodeGenerator(EncodeTypes.Pdf417, "Sample PDF417 Text"))
        {
            // Save the PDF417 image to disk
            pdf417Generator.Save(pdf417Path);
        }

        // ---------- Prepare list of images to process ----------
        string[] imageFiles = { qrPath, pdf417Path };

        // ---------- Iterate over each image and decode ----------
        foreach (var imageFile in imageFiles)
        {
            // Verify that the image file exists before attempting to read it
            if (!File.Exists(imageFile))
            {
                Console.WriteLine($"File not found: {imageFile}");
                continue;
            }

            // Load the image into a Bitmap object
            using (var bitmap = new Bitmap(imageFile))
            {
                // Initialize BarCodeReader limited to QR and PDF417 symbologies
                using (var reader = new BarCodeReader(bitmap, DecodeType.QR, DecodeType.Pdf417))
                {
                    // Read all barcodes detected in the image
                    foreach (var result in reader.ReadBarCodes())
                    {
                        Console.WriteLine($"Image: {imageFile}");
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");
                        Console.WriteLine();
                    }
                }
            }
        }

        // Indicate that the program has completed successfully
        Console.WriteLine("Processing completed.");
    }
}