// Title: Validate barcode symbology from imported XML state
// Description: Demonstrates how to generate a barcode, export the reader state to XML, import it back, and verify that the XML contains the expected symbology before processing results.
// Category-Description: This example belongs to the Aspose.BarCode state management category, showing how to use BarCodeGenerator, BarCodeReader, and the ImportFromXml/ExportToXml methods. Typical use cases include persisting reader configuration, sharing barcode processing settings across services, and ensuring the correct symbology is applied when re‑reading barcodes. Developers often need to validate imported XML state to avoid mismatched decoding types.
// Prompt: Write code to validate that an imported XML state contains the expected barcode symbology before processing results.
// Tags: barcode symbology, validation, xml state, import, export, aspose.barcode, generation, recognition

using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Example program that generates a barcode, exports the reader state to XML,
/// re‑imports the state, and validates that the expected symbology is present.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates a sample barcode, saves its reader state, and validates the symbology.
    /// </summary>
    static void Main()
    {
        // Prepare a temporary working folder for generated files
        string workFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Define file paths and expected symbology name
        string barcodePath = Path.Combine(workFolder, "barcode.png");
        string xmlPath = Path.Combine(workFolder, "readerState.xml");
        string expectedSymbology = "Code128";

        // Generate a sample barcode image (Code128, value "123456")
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.Code128, "123456"))
        {
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Create a reader for the generated image, optionally adjust settings, and export its state to XML
        using (BarCodeReader reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Example setting: strip FNC characters from the result
            reader.BarcodeSettings.StripFNC = true;
            reader.ExportToXml(xmlPath);
        }

        // Validate that the imported XML state contains the expected symbology before further processing
        ValidateSymbology(xmlPath, barcodePath, expectedSymbology);
    }

    /// <summary>
    /// Imports a reader state from an XML file, assigns the corresponding image,
    /// reads barcodes, and checks whether the detected symbology matches the expected one.
    /// </summary>
    /// <param name="xmlFilePath">Path to the exported reader state XML.</param>
    /// <param name="imageFilePath">Path to the barcode image file.</param>
    /// <param name="expectedSymbologyName">Name of the expected symbology (e.g., "Code128").</param>
    static void ValidateSymbology(string xmlFilePath, string imageFilePath, string expectedSymbologyName)
    {
        // Verify that the XML state file exists
        if (!File.Exists(xmlFilePath))
        {
            Console.WriteLine($"XML state file not found: {xmlFilePath}");
            return;
        }

        // Verify that the barcode image file exists
        if (!File.Exists(imageFilePath))
        {
            Console.WriteLine($"Barcode image file not found: {imageFilePath}");
            return;
        }

        // Resolve the expected symbology name to a BaseDecodeType value using reflection
        FieldInfo field = typeof(DecodeType).GetField(expectedSymbologyName);
        if (field == null)
        {
            Console.WriteLine($"Unknown symbology: {expectedSymbologyName}");
            return;
        }

        BaseDecodeType expectedDecode = (BaseDecodeType)field.GetValue(null);

        // Import the reader state from the XML file
        using (BarCodeReader importedReader = BarCodeReader.ImportFromXml(xmlFilePath))
        {
            // Assign the barcode image to the imported reader (state does not store the image)
            importedReader.SetBarCodeImage(imageFilePath);

            // Perform barcode detection
            BarCodeResult[] results = importedReader.ReadBarCodes();

            if (results.Length == 0)
            {
                Console.WriteLine("No barcodes were detected.");
                return;
            }

            // Iterate through detected results and compare symbology
            foreach (BarCodeResult result in results)
            {
                bool matches = result.CodeType.Equals(expectedDecode);
                Console.WriteLine($"Detected symbology: {result.CodeTypeName}");
                Console.WriteLine($"Matches expected ({expectedSymbologyName}): {matches}");
                Console.WriteLine($"Code text: {result.CodeText}");
            }
        }
    }
}