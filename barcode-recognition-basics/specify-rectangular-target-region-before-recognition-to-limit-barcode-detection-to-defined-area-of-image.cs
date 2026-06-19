using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a Code128 barcode image and recognizing it within a specific region.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image if missing, then reads it from a defined region.
    /// </summary>
    static void Main()
    {
        // Path for the generated barcode image
        string imagePath = "sample_barcode.png";

        // ------------------------------------------------------------
        // Generate a barcode image if it does not already exist
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Create a BarcodeGenerator for Code128 with sample data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Save the generated barcode to the specified file using default settings
                generator.Save(imagePath);
                Console.WriteLine($"Barcode image created at: {Path.GetFullPath(imagePath)}");
            }
        }

        // ------------------------------------------------------------
        // Verify the image file exists before attempting recognition
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Error: Barcode image file not found.");
            return;
        }

        // ------------------------------------------------------------
        // Load the image and define a rectangular region for recognition
        // ------------------------------------------------------------
        using (var bitmap = new Bitmap(imagePath))
        {
            // Define a region (central half of the image) to limit the scanning area
            int regionWidth = bitmap.Width / 2;
            int regionHeight = bitmap.Height / 2;
            int regionX = (bitmap.Width - regionWidth) / 2;
            int regionY = (bitmap.Height - regionHeight) / 2;
            var targetRegion = new Rectangle(regionX, regionY, regionWidth, regionHeight);

            // --------------------------------------------------------
            // Initialize the barcode reader, configure it, and read
            // --------------------------------------------------------
            using (var reader = new BarCodeReader())
            {
                // Restrict decoding to Code128 for this example
                reader.BarCodeReadType = DecodeType.Code128;

                // Assign the image and the region to be scanned
                reader.SetBarCodeImage(bitmap, targetRegion);

                // Perform the recognition operation
                var results = reader.ReadBarCodes();

                // ----------------------------------------------------
                // Output the recognition results
                // ----------------------------------------------------
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected in the specified region.");
                }
                else
                {
                    foreach (var result in results)
                    {
                        Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                        Console.WriteLine($"Detected Code Text: {result.CodeText}");

                        // Display the region of the detected barcode
                        var rect = result.Region.Rectangle;
                        Console.WriteLine($"Barcode Region - X:{rect.X}, Y:{rect.Y}, Width:{rect.Width}, Height:{rect.Height}");
                    }
                }
            }
        }
    }
}