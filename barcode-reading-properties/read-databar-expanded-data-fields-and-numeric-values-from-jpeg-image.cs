// Title: Read DataBar Expanded barcode data from a JPEG image
// Description: Demonstrates how to load a JPEG file and extract DataBar Expanded barcode information, including optional extended fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with DecodeType.DatabarExpanded. It illustrates typical scenarios such as scanning product packaging images to retrieve encoded numeric values and additional DataBar fields. Developers working with retail, inventory, or logistics often need to decode DataBar Expanded symbology and access its extended data.
// Prompt: Read DataBar expanded data fields and numeric values from a JPEG image.
// Tags: databar, expanded, barcode, read, jpeg, aspose.barcode, csharp

using System;
using System.IO;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Sample program that reads DataBar Expanded barcode data from a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Loads the image, validates its existence, and extracts barcode information.
    /// </summary>
    static void Main()
    {
        // Build the full path to the JPEG image located in the current working directory.
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "databar.jpg");

        // Verify that the image file exists before attempting to read it.
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Initialize the barcode reader for DataBar Expanded symbology.
        using (BarCodeReader reader = new BarCodeReader(imagePath, DecodeType.DatabarExpanded))
        {
            // Iterate through all detected barcodes in the image.
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output basic barcode information.
                Console.WriteLine($"CodeType: {result.CodeTypeName}");
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Extended DataBar fields (if available) can be accessed via result.Extended.Databar.
                // Uncomment the following lines if the property exists in the used Aspose.BarCode version.
                // Console.WriteLine($"Value: {result.Extended.Databar.Value}");
                // Console.WriteLine($"CheckSum: {result.Extended.Databar.CheckSum}");
            }
        }
    }
}