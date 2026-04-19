using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Path to the PDF file containing MaxiCode symbols
        string pdfPath = "sample.pdf";

        if (!File.Exists(pdfPath))
        {
            Console.WriteLine($"File not found: {pdfPath}");
            return;
        }

        // Initialize the barcode reader for MaxiCode type
        using (BarCodeReader reader = new BarCodeReader(pdfPath, DecodeType.MaxiCode))
        {
            // Read all barcodes from the document
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Retrieve the MaxiCode mode from extended information
                var extended = result.Extended;
                if (extended?.MaxiCode == null)
                {
                    Console.WriteLine("Extended MaxiCode information not available.");
                    continue;
                }

                MaxiCodeMode mode = extended.MaxiCode.MaxiCodeMode;
                Console.WriteLine($"Detected MaxiCode mode: {mode}");

                // Decode the raw codetext using the appropriate mode
                MaxiCodeCodetext decoded = ComplexCodetextReader.TryDecodeMaxiCode(mode, result.CodeText);
                if (decoded == null)
                {
                    Console.WriteLine("Failed to decode MaxiCode codetext.");
                    continue;
                }

                // For modes 2 and 3 the decoded object is a structured codetext that contains the postal code
                if (decoded is MaxiCodeStructuredCodetext structured)
                {
                    Console.WriteLine($"Postal Code: {structured.PostalCode}");
                }
                else
                {
                    Console.WriteLine("Decoded MaxiCode does not contain postal code information.");
                }

                Console.WriteLine(); // Blank line between results
            }
        }
    }
}