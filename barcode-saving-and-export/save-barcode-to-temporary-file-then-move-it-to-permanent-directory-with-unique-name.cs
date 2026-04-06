using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define temporary file path
        string tempFilePath = Path.Combine(Path.GetTempPath(), "barcode_temp.png");

        // Create and save barcode to temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "123ABC";
            generator.Save(tempFilePath);
        }

        // Define permanent directory
        string permanentDir = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Barcodes");
        Directory.CreateDirectory(permanentDir);

        // Generate a unique file name
        string uniqueFileName = Guid.NewGuid().ToString("N") + ".png";
        string destinationPath = Path.Combine(permanentDir, uniqueFileName);

        // Move the file to the permanent location
        File.Move(tempFilePath, destinationPath);

        // Optional: inform the user where the file was saved
        Console.WriteLine($"Barcode saved to: {destinationPath}");
    }
}