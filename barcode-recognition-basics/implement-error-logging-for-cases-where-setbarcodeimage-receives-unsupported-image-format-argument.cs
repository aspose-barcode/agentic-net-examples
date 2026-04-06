using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

class Program
{
    static void Main()
    {
        // Generate a simple Code128 barcode and save it as PNG
        string pngPath = "barcode.png";
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(pngPath, BarCodeImageFormat.Png);
        }

        // Attempt to read the barcode from the valid PNG image
        using (var reader = new BarCodeReader())
        {
            try
            {
                reader.SetBarCodeImage(pngPath);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error reading barcode image: {ex.Message}");
            }
        }

        // Create a dummy text file to simulate an unsupported image format
        string txtPath = "unsupported.txt";
        File.WriteAllText(txtPath, "This is not an image.");

        // Attempt to read the barcode from the unsupported file format
        using (var reader = new BarCodeReader())
        {
            try
            {
                reader.SetBarCodeImage(txtPath);
                foreach (var result in reader.ReadBarCodes())
                {
                    Console.WriteLine($"Detected barcode: Type={result.CodeTypeName}, Text={result.CodeText}");
                }
            }
            catch (Exception ex)
            {
                // Log the error for unsupported image format
                Console.WriteLine($"Error reading unsupported image format: {ex.Message}");
            }
        }
    }
}