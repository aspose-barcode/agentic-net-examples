using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Prepare a temporary image file path
        string imagePath = Path.Combine(Path.GetTempPath(), "sample.png");

        // Generate a simple Code128 barcode and save it
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            generator.Save(imagePath);
        }

        // Verify the file exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Read the barcode and evaluate ReadingQuality
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                double quality = result.ReadingQuality; // ReadingQuality is a double representing percent

                if (quality >= 1 && quality <= 99)
                {
                    // Map to moderate quality and log a warning
                    Console.WriteLine($"WARNING: ReadingQuality {quality} mapped to Moderate quality.");
                }
                else
                {
                    // For other quality values (e.g., 0 or 100)
                    Console.WriteLine($"ReadingQuality {quality} is outside moderate range.");
                }
            }
        }

        // Clean up the temporary file
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program flow
        }
    }
}