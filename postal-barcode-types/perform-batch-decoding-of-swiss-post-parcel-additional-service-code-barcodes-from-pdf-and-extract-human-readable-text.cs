using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;

class Program
{
    static void Main(string[] args)
    {
        // Input PDF file – can be passed as a command‑line argument or defaults to a sample name.
        string pdfPath = args.Length > 0 ? args[0] : "SampleSwissPostParcel.pdf";

        // Verify that the PDF exists before attempting to read it.
        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"Error: PDF file not found at '{pdfPath}'.");
            return;
        }

        // Create a BarCodeReader that looks for Swiss Post Parcel barcodes in the PDF.
        using (var reader = new BarCodeReader(pdfPath, DecodeType.SwissPostParcel))
        {
            // Enable checksum validation for better reliability (optional).
            reader.BarcodeSettings.ChecksumValidation = ChecksumValidation.On;

            // Perform the recognition.
            BarCodeResult[] results = reader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No Swiss Post Parcel barcodes were detected in the document.");
                return;
            }

            Console.WriteLine($"Detected {results.Length} Swiss Post Parcel barcode(s):");
            for (int i = 0; i < results.Length; i++)
            {
                BarCodeResult result = results[i];
                Console.WriteLine($"Barcode #{i + 1}:");
                Console.WriteLine($"  Type    : {result.CodeTypeName}");
                Console.WriteLine($"  CodeText: {result.CodeText}");
            }
        }
    }
}