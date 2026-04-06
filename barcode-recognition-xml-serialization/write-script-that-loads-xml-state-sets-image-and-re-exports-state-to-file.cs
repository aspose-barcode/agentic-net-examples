using System;
using System.IO;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Paths for temporary files
        string initialXml = Path.Combine(Path.GetTempPath(), "barcode_state_initial.xml");
        string updatedXml = Path.Combine(Path.GetTempPath(), "barcode_state_updated.xml");
        string barcodeImage = Path.Combine(Path.GetTempPath(), "barcode.png");

        // 1. Create a barcode generator, set some properties, and export its state to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Initial123"))
        {
            // Example: enable checksum and show it
            generator.Parameters.Barcode.IsChecksumEnabled = EnableChecksum.Yes;
            generator.Parameters.Barcode.ChecksumAlwaysShow = true;

            // Export the current state to an XML file
            bool exportSuccess = generator.ExportToXml(initialXml);
            if (!exportSuccess)
                throw new InvalidOperationException("Failed to export initial barcode state to XML.");

            // Save the barcode image (optional, just to have an image file)
            generator.Save(barcodeImage);
        }

        // 2. Import the barcode generator state from the previously saved XML
        BarcodeGenerator importedGenerator = BarcodeGenerator.ImportFromXml(initialXml);
        if (importedGenerator == null)
            throw new InvalidOperationException("Failed to import barcode state from XML.");

        // 3. Modify the imported generator (e.g., change the CodeText)
        importedGenerator.CodeText = "Updated456";

        // 4. Save the modified barcode image
        importedGenerator.Save(barcodeImage);

        // 5. Export the modified state back to a new XML file
        bool updatedExportSuccess = importedGenerator.ExportToXml(updatedXml);
        if (!updatedExportSuccess)
            throw new InvalidOperationException("Failed to export updated barcode state to XML.");

        // Clean up
        importedGenerator.Dispose();

        Console.WriteLine("Initial XML: " + initialXml);
        Console.WriteLine("Updated XML: " + updatedXml);
        Console.WriteLine("Barcode image saved to: " + barcodeImage);
    }
}