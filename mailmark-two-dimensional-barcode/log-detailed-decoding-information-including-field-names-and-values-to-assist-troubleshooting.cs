using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it to a file,
/// and then reading it back to display detailed decoding information.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates a barcode image, verifies its creation, and reads the barcode data.
    /// </summary>
    static void Main()
    {
        // Define the output path for the generated barcode image.
        string imagePath = "barcode.png";

        // -------------------------------------------------
        // Generate a sample Code128 barcode
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Enable checksum for demonstration purposes.
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;

            // Save the generated barcode image to the specified file.
            generator.Save(imagePath);
        }

        // Verify that the image file was successfully created.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // -------------------------------------------------
        // Read the barcode and log detailed decoding info
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes found in the image.
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user and exit.
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            // Iterate through each detected barcode and display its properties.
            foreach (BarCodeResult result in results)
            {
                Console.WriteLine("=== Barcode Detected ===");
                Console.WriteLine($"Type               : {result.CodeTypeName}");
                Console.WriteLine($"CodeText           : {result.CodeText}");
                Console.WriteLine($"Confidence         : {result.Confidence}");
                Console.WriteLine($"ReadingQuality     : {result.ReadingQuality}");

                // Extract region bounds (X, Y, Width, Height) and round to integers.
                var rect = result.Region.Rectangle;
                int x = (int)Math.Round((double)rect.X);
                int y = (int)Math.Round((double)rect.Y);
                int width = (int)Math.Round((double)rect.Width);
                int height = (int)Math.Round((double)rect.Height);
                Console.WriteLine($"Region (X,Y,W,H)   : X={x}, Y={y}, Width={width}, Height={height}");

                // Output the orientation angle of the detected barcode.
                Console.WriteLine($"Orientation Angle  : {result.Region.Angle}");

                // If the barcode is a 1D type, display extended 1D-specific parameters.
                if (result.Extended?.OneD != null)
                {
                    Console.WriteLine($"OneD Value         : {result.Extended.OneD.Value}");
                    Console.WriteLine($"OneD CheckSum      : {result.Extended.OneD.CheckSum}");
                }

                // If the barcode is a QR code, display extended QR-specific parameters.
                if (result.Extended?.QR != null)
                {
                    Console.WriteLine($"QR Version         : {result.Extended.QR.Version}");
                    Console.WriteLine($"QR ErrorLevel      : {result.Extended.QR.ErrorLevel}");
                }

                Console.WriteLine();
            }
        }
    }
}