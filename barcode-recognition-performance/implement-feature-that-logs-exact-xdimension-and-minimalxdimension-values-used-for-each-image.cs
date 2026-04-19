using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Prepare output directory
        string outputDir = "output";
        Directory.CreateDirectory(outputDir);
        string imagePath = Path.Combine(outputDir, "code128.png");

        // ---------- Barcode generation ----------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Set exact XDimension (in points)
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated image
            generator.Save(imagePath);

            // Log the XDimension used during generation
            Console.WriteLine($"Generated barcode saved to: {imagePath}");
            Console.WriteLine($"Generation XDimension: {generator.Parameters.Barcode.XDimension.Point} pt");
        }

        // ---------- Barcode recognition ----------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine("Generated image not found. Exiting.");
            return;
        }

        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Use MinimalXDimension mode for recognition
            reader.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            // Set the minimal XDimension value (must match generation value for accurate logging)
            reader.QualitySettings.MinimalXDimension = 2f;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Recognized CodeText: {result.CodeText}");
                Console.WriteLine($"Recognition XDimension mode: {reader.QualitySettings.XDimension}");
                Console.WriteLine($"Recognition MinimalXDimension: {reader.QualitySettings.MinimalXDimension}");
            }
        }
    }
}