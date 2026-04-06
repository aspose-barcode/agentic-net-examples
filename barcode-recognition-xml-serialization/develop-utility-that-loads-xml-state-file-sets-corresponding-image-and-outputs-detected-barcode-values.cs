using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Define temporary file paths
        string xmlPath = Path.Combine(Path.GetTempPath(), "barcode_state.xml");
        string imagePath = Path.Combine(Path.GetTempPath(), "barcode.png");

        // -----------------------------------------------------------------
        // Step 1: Create a barcode generator, configure it, and export to XML
        // -----------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Example of setting a unit-based property (optional)
            generator.Parameters.ImageWidth.Point = 300f;
            generator.Parameters.ImageHeight.Point = 150f;

            // Export the generator settings to an XML file
            generator.ExportToXml(xmlPath);
        }

        // -----------------------------------------------------------------
        // Step 2: Import the generator settings from the XML file
        // -----------------------------------------------------------------
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(xmlPath);
        if (importedGenerator == null)
        {
            Console.WriteLine("Failed to import barcode generator from XML.");
            return;
        }

        // Save the barcode image generated from the imported settings
        importedGenerator.Save(imagePath);

        // -----------------------------------------------------------------
        // Step 3: Read and decode the barcode image
        // -----------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            foreach (var result in reader.ReadBarCodes())
            {
                Console.WriteLine($"Detected Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Detected CodeText: {result.CodeText}");
            }
        }

        // Clean up temporary files (optional)
        try
        {
            File.Delete(xmlPath);
            File.Delete(imagePath);
        }
        catch
        {
            // Ignore any cleanup errors
        }
    }
}