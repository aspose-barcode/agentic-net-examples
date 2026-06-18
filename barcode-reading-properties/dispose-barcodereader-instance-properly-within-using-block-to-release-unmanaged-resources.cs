using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a barcode image (if missing) and reading its contents using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image if it does not exist, then reads and displays barcode information.
    /// </summary>
    static void Main()
    {
        // Path to the barcode image file
        string imagePath = "barcode.png";

        // Generate a barcode image if it does not already exist on disk
        if (!File.Exists(imagePath))
        {
            // Create a BarcodeGenerator for Code128 with the specified text
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
            {
                // Save the generated barcode image to the specified path
                generator.Save(imagePath);
            }
        }

        // Open the barcode image for reading using BarCodeReader
        // The using block ensures the reader is properly disposed after use
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes in the image
            foreach (var result in reader.ReadBarCodes())
            {
                // Output the type of the detected barcode (e.g., Code128)
                Console.WriteLine($"BarCode Type: {result.CodeTypeName}");
                // Output the decoded text/value of the barcode
                Console.WriteLine($"BarCode CodeText: {result.CodeText}");

                // Retrieve the rectangular region where the barcode was detected
                var region = result.Region.Rectangle;
                // Round the region coordinates and dimensions to integer values for display
                int x = (int)Math.Round((double)region.X);
                int y = (int)Math.Round((double)region.Y);
                int width = (int)Math.Round((double)region.Width);
                int height = (int)Math.Round((double)region.Height);
                // Display the region's position and size
                Console.WriteLine($"Region - X:{x}, Y:{y}, Width:{width}, Height:{height}");
            }
        }
    }
}