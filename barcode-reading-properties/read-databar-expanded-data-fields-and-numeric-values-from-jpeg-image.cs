// Title: Read DataBar Expanded barcode fields from a JPEG image
// Description: Demonstrates how to generate a DataBar Expanded barcode, save it as JPEG, and then read its expanded data fields and numeric values using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition and generation category. It showcases the use of BarcodeGenerator for creating barcodes and BarCodeReader for extracting information such as CodeTypeName and CodeText. Developers working with retail or inventory systems often need to read DataBar Expanded symbology from image files, making this pattern a common requirement.
// Prompt: Read DataBar expanded data fields and numeric values from a JPEG image.
// Tags: databar expanded, read, jpeg, aspose.barcode, barcodegenerator, barcodereader

using System;
using System.IO;
using System.Text.RegularExpressions;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a DataBar Expanded barcode, saves it as a JPEG,
/// then reads the barcode back to extract expanded data fields and numeric values.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Creates temporary files, generates a barcode if needed,
    /// reads it, displays extracted information, and cleans up resources.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary directory for the demo files
        string tempDir = Path.Combine(Path.GetTempPath(), "DataBarDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempDir);
        string imagePath = Path.Combine(tempDir, "databar_expanded.jpg");

        // Try to obtain EncodeTypes.DataBarExpanded via reflection (may not exist in older versions)
        var encodeField = typeof(EncodeTypes).GetField("DataBarExpanded");
        if (encodeField == null)
        {
            Console.WriteLine("EncodeTypes.DataBarExpanded is not available in this Aspose.BarCode version.");
            return;
        }
        BaseEncodeType encodeType = (BaseEncodeType)encodeField.GetValue(null);

        // Generate a sample DataBar Expanded barcode if the JPEG does not already exist
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(encodeType, "123456789012"))
            {
                generator.Parameters.Barcode.XDimension.Pixels = 2; // Set barcode resolution
                generator.Save(imagePath, BarCodeImageFormat.Jpeg);
                Console.WriteLine($"Generated sample barcode at: {imagePath}");
            }
        }

        // Read barcodes from the generated image
        using (var reader = new BarCodeReader(imagePath))
        {
            bool found = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Identify DataBar Expanded barcodes by checking the CodeTypeName
                if (result.CodeTypeName != null && result.CodeTypeName.IndexOf("DataBarExpanded", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    found = true;
                    Console.WriteLine($"CodeTypeName: {result.CodeTypeName}");
                    Console.WriteLine($"CodeText: {result.CodeText}");

                    // Extract numeric values from the CodeText using a regular expression
                    var matches = Regex.Matches(result.CodeText, @"\d+");
                    Console.WriteLine("Numeric values found:");
                    foreach (Match m in matches)
                    {
                        Console.WriteLine($"  {m.Value}");
                    }
                }
            }

            if (!found)
            {
                Console.WriteLine("No DataBar Expanded barcode detected in the image.");
            }
        }

        // Clean up temporary files and directory
        try
        {
            if (File.Exists(imagePath))
                File.Delete(imagePath);
            if (Directory.Exists(tempDir))
                Directory.Delete(tempDir, true);
        }
        catch
        {
            // Ignored - cleanup failure should not affect program exit
        }
    }
}