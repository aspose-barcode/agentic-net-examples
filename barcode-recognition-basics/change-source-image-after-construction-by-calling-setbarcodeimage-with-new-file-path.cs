using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating two barcode images, then reading them using Aspose.BarCode.
/// Shows how to switch the source image of a <see cref="BarCodeReader"/> at runtime.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// Generates sample barcodes, verifies their creation, and reads them sequentially.
    /// </summary>
    static void Main()
    {
        // Define file paths for the two sample barcode images.
        string imagePath1 = "barcode1.png";
        string imagePath2 = "barcode2.png";

        // ------------------------------------------------------------
        // Generate the first barcode (Code128) and save it to imagePath1.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "First123"))
        {
            generator.Save(imagePath1);
        }

        // ------------------------------------------------------------
        // Generate the second barcode (QR) and save it to imagePath2.
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Second456"))
        {
            generator.Save(imagePath2);
        }

        // Verify that both barcode images were successfully created.
        if (!File.Exists(imagePath1) || !File.Exists(imagePath2))
        {
            Console.WriteLine("Failed to create sample barcode images.");
            return;
        }

        // ------------------------------------------------------------
        // Create a BarCodeReader for the first image and read its barcodes.
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath1, DecodeType.AllSupportedTypes))
        {
            Console.WriteLine($"Reading from initial image: {imagePath1}");

            // Iterate through all detected barcodes in the first image.
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
            }

            // ------------------------------------------------------------
            // Switch the reader's source image to the second barcode image.
            // ------------------------------------------------------------
            try
            {
                reader.SetBarCodeImage(imagePath2);
                Console.WriteLine($"Switched to new image: {imagePath2}");

                Console.WriteLine("Reading from new image:");

                // Read and display barcodes from the new image.
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"  Type: {result.CodeTypeName}, Text: {result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                // Handle any errors that occur while changing the image.
                Console.WriteLine($"Error while setting new image: {ex.Message}");
            }
        }
    }
}