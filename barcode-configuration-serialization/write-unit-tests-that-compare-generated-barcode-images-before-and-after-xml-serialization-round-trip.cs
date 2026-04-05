using System;
using System.IO;
using System.Linq;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Define file paths
        string tempDir = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest");
        Directory.CreateDirectory(tempDir);
        string originalImage = Path.Combine(tempDir, "original.png");
        string xmlFile = Path.Combine(tempDir, "barcode.xml");
        string roundTripImage = Path.Combine(tempDir, "roundtrip.png");

        // Create barcode, set some unit-based properties, save image and export to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123ABC"))
        {
            // Example of setting unit-based properties
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 40f;
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            generator.Save(originalImage);
            generator.ExportToXml(xmlFile);
        }

        // Import from XML and save the image again
        using (var importedGenerator = BarcodeGenerator.ImportFromXml(xmlFile))
        {
            importedGenerator.Save(roundTripImage);
        }

        // Compare the two images byte by byte
        byte[] originalBytes = File.ReadAllBytes(originalImage);
        byte[] roundTripBytes = File.ReadAllBytes(roundTripImage);
        bool imagesEqual = originalBytes.SequenceEqual(roundTripBytes);

        // Output the result
        Console.WriteLine("Original image path: " + originalImage);
        Console.WriteLine("Round‑trip image path: " + roundTripImage);
        Console.WriteLine("Images are identical after XML round‑trip: " + imagesEqual);
    }
}