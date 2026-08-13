// Title: Obtain DotCode version and error correction level from scanned barcode
// Description: Demonstrates how to read a DotCode barcode, generate it if missing, and retrieve extended version and error‑correction information via the Aspose.BarCode API.
// Category-Description: This example belongs to the Aspose.BarCode barcode recognition category, focusing on extracting extended metadata from DotCode symbols. It uses BarcodeGenerator for creation, BarCodeReader for decoding, and the Extended.DotCode property to access version, error correction level and other parameters. Developers working with high‑density 2‑D barcodes often need to verify symbol version and ECC settings for quality control or compliance.
// Prompt: Obtain DotCode version information and error correction level from a scanned DotCode barcode.
// Tags: dotcode, barcode, recognition, version, error correction, aspnet, aspnetcore, aspose.barcode, c#

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates (if needed) and reads a DotCode barcode,
/// then extracts version and error‑correction information using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates a sample DotCode image if it does not exist,
    /// reads the barcode, and prints extended DotCode metadata.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a sample DotCode image (self‑contained example)
        // --------------------------------------------------------------------
        const string imagePath = "dotcode.png";
        const string codeText = "SampleDotCode";

        // --------------------------------------------------------------------
        // Generate DotCode barcode if it does not already exist on disk
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            using (var generator = new BarcodeGenerator(EncodeTypes.DotCode, codeText))
            {
                // Optional: set generation parameters (e.g., number of columns)
                generator.Parameters.Barcode.DotCode.Columns = 20; // rows are auto‑determined
                generator.Save(imagePath);
                Console.WriteLine($"Generated sample barcode: {imagePath}");
            }
        }

        // --------------------------------------------------------------------
        // Verify the image file exists before attempting recognition
        // --------------------------------------------------------------------
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Error: Barcode image '{imagePath}' not found.");
            return;
        }

        // --------------------------------------------------------------------
        // Recognize the DotCode barcode and output extended information
        // --------------------------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.DotCode))
        {
            bool found = false;

            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                found = true;
                Console.WriteLine($"CodeText: {result.CodeText}");

                // Access extended DotCode information via reflection (properties may vary by version)
                var dotCodeInfo = result.Extended?.DotCode;
                if (dotCodeInfo != null)
                {
                    Type infoType = dotCodeInfo.GetType();
                    PropertyInfo[] properties = infoType.GetProperties(BindingFlags.Public | BindingFlags.Instance);
                    Console.WriteLine("DotCode extended information:");
                    foreach (PropertyInfo prop in properties)
                    {
                        try
                        {
                            object value = prop.GetValue(dotCodeInfo);
                            Console.WriteLine($"  {prop.Name}: {value}");
                        }
                        catch
                        {
                            // Ignore any property that throws during get
                        }
                    }
                }
                else
                {
                    Console.WriteLine("No extended DotCode information available.");
                }
            }

            if (!found)
            {
                Console.WriteLine("No DotCode barcode detected in the image.");
            }
        }
    }
}