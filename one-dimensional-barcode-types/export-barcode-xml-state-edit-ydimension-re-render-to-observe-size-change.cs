using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Create a barcode generator with sample code text
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            // Save the original barcode image
            generator.Save("original.png");

            // Export the generator's state to an XML file
            generator.ExportToXml("barcode.xml");
        }

        // Ensure the XML file was created before attempting to import
        if (!File.Exists("barcode.xml"))
        {
            Console.WriteLine("Exported XML file not found.");
            return;
        }

        // Import the barcode generator from the exported XML
        using (var importedGenerator = BarcodeGenerator.ImportFromXml("barcode.xml"))
        {
            // Modify the Y dimension (image height) using the ImageHeight property
            importedGenerator.Parameters.ImageHeight.Point = 200f; // increase height to 200 points

            // Save the modified barcode image to observe the size change
            importedGenerator.Save("modified.png");
        }

        Console.WriteLine("Barcode generation completed. Check 'original.png' and 'modified.png'.");
    }
}