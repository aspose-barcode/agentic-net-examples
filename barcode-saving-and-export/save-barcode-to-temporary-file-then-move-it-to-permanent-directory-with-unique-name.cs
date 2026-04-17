using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define temporary file path
        string tempFolder = Path.GetTempPath();
        string tempFile = Path.Combine(tempFolder, "tempBarcode.png");

        // Generate barcode and save to temporary file
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            generator.Save(tempFile);
        }

        // Define permanent directory
        string permanentFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Barcodes");
        if (!Directory.Exists(permanentFolder))
        {
            Directory.CreateDirectory(permanentFolder);
        }

        // Create a unique file name
        string uniqueFileName = Guid.NewGuid().ToString("N") + ".png";
        string destinationFile = Path.Combine(permanentFolder, uniqueFileName);

        // Move the file to the permanent location
        File.Move(tempFile, destinationFile);

        // Optional: inform the user
        Console.WriteLine("Barcode saved to: " + destinationFile);
    }
}