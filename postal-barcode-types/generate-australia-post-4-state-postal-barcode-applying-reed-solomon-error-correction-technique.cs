using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample data for Australia Post 4‑state barcode (postal code + customer info)
        const string codeText = "5912345678ABCD";

        // Create the barcode generator for Australia Post symbology
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, codeText))
        {
            // Use CTable interpreting type (allows letters, digits, space and #)
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = CustomerInformationInterpretingType.CTable;

            // Optional: adjust short bar height (default is suitable, but shown for completeness)
            generator.Parameters.Barcode.AustralianPost.ShortBarHeight.Point = 5f;

            // Set image size (points) – required when AutoSizeMode is not None
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Set resolution (dpi)
            generator.Parameters.Resolution = 300;

            // Save the generated barcode image
            const string outputPath = "AustraliaPost.png";
            generator.Save(outputPath, BarCodeImageFormat.Png);
            Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(outputPath)}");
        }

        // Verify the barcode by reading it back (if the file exists)
        const string inputPath = "AustraliaPost.png";
        if (File.Exists(inputPath))
        {
            using (Bitmap bitmap = (Bitmap)Image.FromFile(inputPath))
            using (BarCodeReader reader = new BarCodeReader(bitmap, DecodeType.AustraliaPost))
            {
                // Set the same interpreting type for decoding
                reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = CustomerInformationInterpretingType.CTable;

                foreach (BarCodeResult result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Decoded Type: {result.CodeType}");
                    Console.WriteLine($"Decoded Text: {result.CodeText}");
                }
            }
        }
        else
        {
            Console.WriteLine($"Failed to locate the generated image at: {Path.GetFullPath(inputPath)}");
        }
    }
}