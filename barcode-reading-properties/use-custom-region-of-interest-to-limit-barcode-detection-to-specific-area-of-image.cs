using System;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, defining a region of interest,
/// and reading barcodes within that region using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, defines a central ROI, and reads barcodes within that ROI.
    /// </summary>
    static void Main()
    {
        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Generate the barcode image in memory
            using (Bitmap barcodeImage = generator.GenerateBarCodeImage())
            {
                // Calculate coordinates for a central region of interest (ROI)
                int roiX = barcodeImage.Width / 4;          // X offset (quarter width)
                int roiY = barcodeImage.Height / 4;         // Y offset (quarter height)
                int roiWidth = barcodeImage.Width / 2;      // Width (half of image)
                int roiHeight = barcodeImage.Height / 2;    // Height (half of image)

                // Define the ROI rectangle
                var region = new Rectangle(roiX, roiY, roiWidth, roiHeight);

                // Initialize a barcode reader that scans only within the ROI
                using (var reader = new BarCodeReader(barcodeImage, region, DecodeType.AllSupportedTypes))
                {
                    // Iterate through all detected barcodes in the ROI
                    foreach (var result in reader.ReadBarCodes())
                    {
                        // Output barcode type and decoded text
                        Console.WriteLine($"Detected Type: {result.CodeTypeName}");
                        Console.WriteLine($"Code Text: {result.CodeText}");

                        // Retrieve and display the bounding rectangle of the detected barcode
                        var bounds = result.Region.Rectangle;
                        Console.WriteLine($"Barcode Region - X: {bounds.X}, Y: {bounds.Y}, Width: {bounds.Width}, Height: {bounds.Height}");
                    }
                }
            }
        }
    }
}