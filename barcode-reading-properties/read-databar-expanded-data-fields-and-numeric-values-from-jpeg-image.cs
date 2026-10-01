// Title: Read DataBar Expanded barcode data and numeric values from a JPEG image
// Description: Demonstrates how to load a JPEG file, detect a DataBar Expanded barcode, and extract its code text along with any extended numeric fields.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, showcasing the use of BarCodeReader with a specific BaseDecodeType (DatabarExpanded). It illustrates typical scenarios such as scanning product images, extracting GS1 DataBar Expanded information, and handling extended properties via reflection. Developers working with barcode scanning, inventory systems, or retail applications often need to read and process these symbologies using the Aspose.BarCode API.
/// Prompt: Read DataBar expanded data fields and numeric values from a JPEG image.
// Tags: databar expanded, barcode recognition, jpeg, aspose.barcode, c#, reading extended data

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that reads DataBar Expanded barcode data from a JPEG image using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Loads the image, decodes DataBar Expanded barcodes, and prints code text and extended fields.
    /// </summary>
    static void Main()
    {
        // Path to the JPEG image containing a DataBar Expanded barcode
        string imagePath = Path.Combine(Directory.GetCurrentDirectory(), "databar_expanded.jpg");

        // Verify that the file exists before attempting to read it
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Image file not found: {imagePath}");
            return;
        }

        // Decode only DataBar Expanded symbology to improve performance
        BaseDecodeType decodeType = DecodeType.DatabarExpanded;

        // Initialize the barcode reader with the image path and the specific decode type
        using (var reader = new BarCodeReader(imagePath, decodeType))
        {
            // Read all barcodes that match the specified symbology
            BarCodeResult[] results = reader.ReadBarCodes();

            // Handle case where no matching barcode is found
            if (results == null || results.Length == 0)
            {
                Console.WriteLine("No DataBar Expanded barcode detected.");
                return;
            }

            // Iterate through each detected barcode result
            foreach (var result in results)
            {
                // Output the primary decoded text
                Console.WriteLine($"Code Text: {result.CodeText}");

                // Access extended data via reflection (covers any available extended properties)
                var extended = result.Extended;
                if (extended != null)
                {
                    var extProps = extended.GetType().GetProperties();
                    bool anyExtended = false;

                    foreach (var prop in extProps)
                    {
                        var extValue = prop.GetValue(extended);
                        if (extValue == null) continue;

                        anyExtended = true;
                        Console.WriteLine($"Extended {prop.Name}:");

                        var subProps = extValue.GetType().GetProperties();
                        foreach (var subProp in subProps)
                        {
                            try
                            {
                                var subVal = subProp.GetValue(extValue);
                                Console.WriteLine($"  {subProp.Name}: {subVal}");
                            }
                            catch
                            {
                                // Ignore unreadable properties
                            }
                        }
                    }

                    if (!anyExtended)
                    {
                        Console.WriteLine("No extended DataBar Expanded information available.");
                    }
                }
                else
                {
                    Console.WriteLine("No extended DataBar Expanded information available.");
                }

                // Separator for readability between multiple results
                Console.WriteLine(new string('-', 40));
            }
        }
    }
}