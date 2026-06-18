using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a rotated barcode image (if missing) and reading its orientation using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a sample barcode image if it does not exist, then reads and displays barcode information including orientation.
    /// </summary>
    static void Main()
    {
        const string imagePath = "sample.bmp";

        // ------------------------------------------------------------
        // Ensure the sample image exists; generate a rotated barcode if missing.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            // Create a barcode generator for Code128 with the text "Sample123".
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
            {
                // Rotate the barcode by 45 degrees.
                generator.Parameters.RotationAngle = 45f;

                // Save the generated barcode as a BMP image.
                generator.Save(imagePath, BarCodeImageFormat.Bmp);
            }
        }

        // ------------------------------------------------------------
        // Verify the image file exists before attempting to read it.
        // ------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Image file '{imagePath}' not found.");
            return;
        }

        // ------------------------------------------------------------
        // Read barcodes from the BMP image and output their orientation angles.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Iterate through all detected barcodes.
            foreach (var result in reader.ReadBarCodes())
            {
                // Orientation angle in degrees for the detected barcode region.
                double angle = result.Region.Angle;

                // Output barcode details to the console.
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");
                Console.WriteLine($"Orientation : {angle} degrees");
                Console.WriteLine();
            }
        }
    }
}