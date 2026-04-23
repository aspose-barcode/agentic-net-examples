using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the SVG file containing the Swiss Post Parcel barcode
        string svgPath = "sample.svg";

        // Verify that the file exists
        if (!File.Exists(svgPath))
        {
            Console.WriteLine($"File not found: {svgPath}");
            return;
        }

        // Create a BarCodeReader for Swiss Post Parcel barcode type
        using (var reader = new BarCodeReader(svgPath, DecodeType.SwissPostParcel))
        {
            // Read all barcodes found in the SVG
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Output the raw codetext
                Console.WriteLine($"Detected CodeText: {result.CodeText}");

                // The additional service description is typically embedded in the codetext.
                // For demonstration, we simply display the codetext as the description.
                // In a real scenario, you would parse the codetext according to Swiss Post specifications.
                Console.WriteLine($"Service Description: {result.CodeText}");
            }
        }
    }
}