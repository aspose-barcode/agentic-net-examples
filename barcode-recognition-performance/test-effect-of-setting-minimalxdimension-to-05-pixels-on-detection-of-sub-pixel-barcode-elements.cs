using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        const string imagePath = "subpixel_barcode.png";

        // Generate a barcode with a minimal XDimension (1 pixel, the smallest allowed)
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Test123"))
        {
            // Ensure manual sizing is used
            generator.Parameters.AutoSizeMode = AutoSizeMode.None;

            // Set XDimension to the smallest positive value accepted by the API
            generator.Parameters.Barcode.XDimension.Pixels = 1f;

            // Set a modest bar height so the barcode is visible
            generator.Parameters.Barcode.BarHeight.Pixels = 30f;

            // Save the generated image
            generator.Save(imagePath);
        }

        // Verify the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Read the barcode using default settings (for comparison)
        Console.WriteLine("Reading with default QualitySettings:");
        using (var readerDefault = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            foreach (BarCodeResult result in readerDefault.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
            }
        }

        // Read the barcode using MinimalXDimension = 1 pixel and UseMinimalXDimension mode
        Console.WriteLine("\nReading with MinimalXDimension = 1 pixel (UseMinimalXDimension mode):");
        using (var readerCustom = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            // Configure recognition to use the minimal XDimension we set during generation
            readerCustom.QualitySettings.XDimension = XDimensionMode.UseMinimalXDimension;
            readerCustom.QualitySettings.MinimalXDimension = 1f;

            foreach (BarCodeResult result in readerCustom.ReadBarCodes())
            {
                Console.WriteLine($"  CodeText: {result.CodeText}");
            }
        }
    }
}