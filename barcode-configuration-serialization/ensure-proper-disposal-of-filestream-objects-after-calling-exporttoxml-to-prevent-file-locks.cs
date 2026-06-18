using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Demonstrates generating a Code128 barcode, saving it as an image,
/// and exporting the generator settings to an XML file using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Application entry point.
    /// Generates a barcode, saves the image, and exports settings to XML.
    /// </summary>
    static void Main()
    {
        // Define output file paths for the barcode image and XML settings.
        string imagePath = "barcode.png";
        string xmlPath = "barcode.xml";

        // Initialize a BarcodeGenerator for Code128 with the desired data.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Save the generated barcode as a PNG image.
            generator.Save(imagePath);

            // Export the generator's configuration to an XML file.
            // The FileStream is wrapped in a using block to ensure proper disposal.
            using (var stream = new FileStream(xmlPath, FileMode.Create, FileAccess.Write))
            {
                generator.ExportToXml(stream);
            } // The FileStream is disposed here, releasing the file handle.
        } // The BarcodeGenerator is disposed here, releasing any unmanaged resources.

        // Inform the user that the operation completed successfully.
        Console.WriteLine("Barcode image and XML exported successfully.");
    }
}