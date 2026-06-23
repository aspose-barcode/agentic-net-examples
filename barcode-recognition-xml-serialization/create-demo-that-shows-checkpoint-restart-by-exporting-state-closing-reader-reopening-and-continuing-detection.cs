using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates generating a Code128 barcode, reading it,
/// exporting the reader state to XML, and restoring the reader
/// from the saved state.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    /// <param name="args">Command‑line arguments (not used).</param>
    static void Main(string[] args)
    {
        // Path for the generated barcode image
        const string imagePath = "barcode.png";

        // -------------------------------------------------
        // 1. Generate a simple Code128 barcode and save it
        // -------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Demo123"))
        {
            // Save the generated barcode image to the specified path
            generator.Save(imagePath);
        }

        // -------------------------------------------------
        // 2. First reading session – read the barcode,
        //    then export the reader state to an XML stream
        // -------------------------------------------------
        using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
        {
            // Read all barcodes from the image
            var firstResults = reader.ReadBarCodes();
            Console.WriteLine("First read results:");
            foreach (var result in firstResults)
            {
                // Output each detected barcode type and its text
                Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
            }

            // Export the current reader configuration/state to XML
            using (var stateStream = new MemoryStream())
            {
                reader.ExportToXml(stateStream);
                stateStream.Position = 0; // Rewind the stream for subsequent reading

                // -------------------------------------------------
                // 3. Simulate closing the reader and creating a new one
                // -------------------------------------------------
                // Import the previously saved state into a new reader instance
                var restoredReader = BarCodeReader.ImportFromXml(stateStream);
                using (restoredReader)
                {
                    // The image must be set again after import
                    restoredReader.SetBarCodeImage(imagePath);

                    // Continue detection with the restored reader
                    var secondResults = restoredReader.ReadBarCodes();
                    Console.WriteLine("After restart read results:");
                    foreach (var result in secondResults)
                    {
                        // Output each detected barcode type and its text after restoration
                        Console.WriteLine($"{result.CodeTypeName}: {result.CodeText}");
                    }
                }
            }
        }
    }
}