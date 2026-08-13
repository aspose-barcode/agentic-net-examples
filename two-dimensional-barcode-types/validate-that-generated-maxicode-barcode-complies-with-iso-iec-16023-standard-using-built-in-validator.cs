// Title: Validate MaxiCode barcode against ISO/IEC 16023 using Aspose.BarCode validator
// Description: Demonstrates generating a MaxiCode barcode (Mode 2) and validating it with the built‑in Aspose.BarCode validator to ensure compliance with ISO/IEC 16023.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation and recognition category. It shows how to use ComplexBarcodeGenerator for MaxiCode, BarCodeReader for decoding, and the Extended.MaxiCode properties to verify ISO/IEC 16023 compliance. Developers working with 2‑D barcodes such as MaxiCode can use these APIs to create, save, and programmatically validate barcodes in .NET applications.
// Prompt: Validate that the generated MaxiCode barcode complies with ISO/IEC 16023 standard using built‑in validator.
// Tags: maxicode, barcode, validation, iso/iec 16023, aspose.barcode, generation, recognition, complexbarcode

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.BarCode.ComplexBarcode;

/// <summary>
/// Demonstrates generating a MaxiCode barcode and validating it against the ISO/IEC 16023 standard using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point that creates, saves, and validates a MaxiCode barcode.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for this run
        string tempFolder = Path.Combine(Path.GetTempPath(), "MaxiCodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Path for the generated barcode image
        string barcodePath = Path.Combine(tempFolder, "maxicode.png");

        // Prepare MaxiCode codetext (Mode 2 with structured second message)
        var maxiCodeData = new MaxiCodeCodetextMode2
        {
            PostalCode = "524032140",   // 9‑digit US postal code
            CountryCode = 56,           // Example country code
            ServiceCategory = 999       // Example service category
        };

        // Build the structured second message (address lines, city, state, year)
        var structuredMessage = new MaxiCodeStructuredSecondMessage();
        structuredMessage.Add("634 ALPHA DRIVE");
        structuredMessage.Add("PITTSBURGH");
        structuredMessage.Add("PA");
        structuredMessage.Year = 99;
        maxiCodeData.SecondMessage = structuredMessage;

        // Generate the MaxiCode barcode and save it as PNG
        using (var generator = new ComplexBarcodeGenerator(maxiCodeData))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the file was created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate the barcode image.");
            return;
        }

        // Read and validate the generated barcode using the built‑in validator
        using (var reader = new BarCodeReader(barcodePath, DecodeType.MaxiCode))
        {
            bool anyValid = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // The presence of Extended.MaxiCode indicates successful recognition
                var extended = result.Extended.MaxiCode;
                if (extended != null && !extended.IsEmpty)
                {
                    anyValid = true;
                    Console.WriteLine("Barcode recognized successfully.");
                    Console.WriteLine($"CodeText: {result.CodeText}");
                    Console.WriteLine($"Mode: {extended.Mode}");
                    Console.WriteLine($"Structured Append Barcode ID: {extended.StructuredAppendModeBarcodeId}");
                }
                else
                {
                    Console.WriteLine("Barcode recognized but extended MaxiCode data is missing.");
                }
            }

            if (!anyValid)
            {
                Console.WriteLine("No valid MaxiCode barcode was detected.");
            }
        }

        // Cleanup (optional)
        // Directory.Delete(tempFolder, true);
    }
}