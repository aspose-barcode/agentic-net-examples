using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main()
    {
        // Path to the PDF file containing barcodes
        string pdfPath = "sample.pdf";

        // Verify that the file exists before processing
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Open the PDF as a read‑only stream
        using (FileStream pdfStream = new FileStream(pdfPath, FileMode.Open, FileAccess.Read))
        {
            // Initialize the barcode reader with the PDF stream
            using (BarCodeReader reader = new BarCodeReader(pdfStream))
            {
                // Detect all supported barcode symbologies
                reader.BarCodeReadType = DecodeType.AllSupportedTypes;

                // Perform the recognition
                BarCodeResult[] results = reader.ReadBarCodes();

                // Output the detection results
                if (results.Length == 0)
                {
                    Console.WriteLine("No barcodes detected.");
                }
                else
                {
                    foreach (BarCodeResult result in results)
                    {
                        Console.WriteLine($"BarCode Type: {result.CodeTypeName}, CodeText: {result.CodeText}");
                    }
                }
            }
        }
    }
}