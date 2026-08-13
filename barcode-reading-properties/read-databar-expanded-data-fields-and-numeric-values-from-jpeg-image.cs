// Title: Read DataBar Expanded fields from a JPEG image
// Description: Demonstrates how to generate a DataBar Expanded barcode, save it as a JPEG file, and then read its AI data fields and numeric values using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It showcases the use of BarcodeGenerator for creating GS1 DataBar Expanded symbols and BarCodeReader for extracting encoded data. Developers working with product identification, inventory, or retail scanning often need to generate and decode DataBar Expanded barcodes, making these APIs essential for handling AI (Application Identifier) data in .NET applications.
// Prompt: Read DataBar expanded data fields and numeric values from a JPEG image.
// Tags: databar, expanded, read, jpeg, generation, recognition, aspose.barcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that creates a DataBar Expanded barcode image (if missing) and reads its data fields.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample barcode image and extracts its encoded information.
    /// </summary>
    static void Main()
    {
        // Path for the sample JPEG image
        string imagePath = "databar_expanded.jpg";

        // Generate a sample DataBar Expanded barcode if the file does not exist
        if (!File.Exists(imagePath))
        {
            // Example GS1 DataBar Expanded code text with numeric AI values
            string codeText = "(01)12345678901231(3103)001500";

            // Create a barcode generator for DataBar Expanded symbology
            using (var generator = new BarcodeGenerator(EncodeTypes.DatabarExpanded, codeText))
            {
                // Save the generated barcode as a JPEG image
                generator.Save(imagePath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Sample barcode image created at '{imagePath}'.");
            }
        }

        // Verify the image exists before attempting to read
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file '{imagePath}' not found.");
            return;
        }

        // Initialize a barcode reader for DataBar Expanded type
        using (var reader = new BarCodeReader(imagePath, DecodeType.DatabarExpanded))
        {
            // Read all barcodes found in the image
            BarCodeResult[] results = reader.ReadBarCodes();

            // If no barcodes were detected, inform the user
            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected in the image.");
                return;
            }

            // Iterate through each detected barcode and display its details
            foreach (var result in results)
            {
                Console.WriteLine($"Barcode Type: {result.CodeTypeName}");
                Console.WriteLine($"Code Text   : {result.CodeText}");

                // DataBar extended parameters provide additional flags (e.g., composite component)
                Console.WriteLine($"Is 2D Composite Component: {result.Extended.DataBar.Is2DCompositeComponent}");
                Console.WriteLine();
            }
        }
    }
}