using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Define output file
        string outputPath = Path.Combine(Directory.GetCurrentDirectory(), "barcode.png");

        // Generate barcode with hidden human‑readable text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Hide the code text (human readable) – only bars will be rendered
            generator.Parameters.Barcode.CodeTextParameters.Location = CodeLocation.None;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify that the barcode can be read correctly
        using (var reader = new BarCodeReader(outputPath, DecodeType.Code128))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected barcode type: {result.CodeTypeName}, code text: {result.CodeText}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("No barcode detected in the generated image.");
                return;
            }
        }

        // Confirm that the image contains only black bars on a white background
        bool onlyBarsAndBackground = true;
        using (var bitmap = new Bitmap(outputPath))
        {
            for (int y = 0; y < bitmap.Height && onlyBarsAndBackground; y++)
            {
                for (int x = 0; x < bitmap.Width && onlyBarsAndBackground; x++)
                {
                    Color pixel = bitmap.GetPixel(x, y);
                    // Accept only pure black or pure white pixels
                    if (!pixel.Equals(Color.Black) && !pixel.Equals(Color.White))
                    {
                        onlyBarsAndBackground = false;
                    }
                }
            }
        }

        Console.WriteLine(onlyBarsAndBackground
            ? "Verification passed: image contains only bars and background."
            : "Verification failed: image contains colors other than bars and background.");
    }
}