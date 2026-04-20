using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define temporary file path for the barcode image
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "sample_barcode.png");

        // Generate a Code128 barcode (moderate confidence) and save it
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "12345"))
        {
            // You can customize generator parameters here if needed
            generator.Save(imagePath);
        }

        // Verify that the image file was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image not found at '{imagePath}'.");
            return;
        }

        // Read the barcode and log detailed decoding information
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.Code39, DecodeType.Code128))
        {
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("=== Decoding Details ===");
                Console.WriteLine($"BarCode Type          : {result.CodeTypeName}");
                Console.WriteLine($"BarCode CodeText      : {result.CodeText}");
                Console.WriteLine($"BarCode Confidence    : {result.Confidence}");
                Console.WriteLine($"BarCode ReadingQuality: {result.ReadingQuality}");
                Console.WriteLine($"BarCode Angle (Region): {result.Region.Angle}");

                // Extended parameters may contain additional data depending on symbology
                if (result.Extended != null)
                {
                    // Example for 1D barcodes (e.g., Code128) – show raw value and checksum if available
                    try
                    {
                        var oneD = result.Extended.OneD;
                        Console.WriteLine($"Extended Value        : {oneD.Value}");
                        Console.WriteLine($"Extended CheckSum     : {oneD.CheckSum}");
                    }
                    catch
                    {
                        // Extended.OneD may not be available for all symbologies
                    }

                    // Example for Code128 specific data portions
                    try
                    {
                        var code128 = result.Extended.Code128;
                        Console.WriteLine($"Code128 Data Portions : {code128}");
                    }
                    catch
                    {
                        // Extended.Code128 may not be available for other symbologies
                    }
                }

                Console.WriteLine();
            }
        }

        // Clean up the generated image file (optional)
        try
        {
            File.Delete(imagePath);
        }
        catch
        {
            // If deletion fails, ignore – the file will remain for inspection
        }
    }
}