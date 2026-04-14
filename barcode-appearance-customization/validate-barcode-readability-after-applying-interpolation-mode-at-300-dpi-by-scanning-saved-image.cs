using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define file name for the generated barcode image
        const string imagePath = "barcode.png";

        // Create a barcode generator for Code128 with sample text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Apply interpolation auto‑size mode
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;

            // Set image resolution to 300 dpi
            generator.Parameters.Resolution = 300f;

            // Save the barcode image to a file
            generator.Save(imagePath);
        }

        // Verify that the image file exists before attempting recognition
        if (!System.IO.File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' was not created.");
            return;
        }

        // Read the saved barcode image and attempt to decode it
        using (var reader = new BarCodeReader(imagePath, DecodeType.Code128))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                Console.WriteLine("Barcode successfully read.");
                Console.WriteLine($"Type: {result.CodeType}");
                Console.WriteLine($"CodeText: {result.CodeText}");
                found = true;
            }

            if (!found)
            {
                Console.WriteLine("Barcode could not be read. Interpolation or resolution may have affected readability.");
            }
        }
    }
}