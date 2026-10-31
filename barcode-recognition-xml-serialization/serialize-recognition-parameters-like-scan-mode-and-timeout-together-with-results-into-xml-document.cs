// Title: Serialize barcode recognition parameters and results to XML
// Description: Demonstrates generating a Code128 barcode, reading it with custom recognition settings, and exporting both the settings and detection results to an XML file.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category. It showcases the use of BarcodeGenerator for creating barcodes, BarCodeReader for scanning with configurable parameters, and the ExportToXml method to persist scan configuration and results. Developers often need to store recognition settings (e.g., scan mode, timeout) alongside results for auditing, debugging, or later processing, making this pattern common in automated scanning solutions.
// Prompt: Serialize recognition parameters like scan mode and timeout together with results into an XML document.
// Tags: barcode, symbology, generation, recognition, xml, export, aspose.barcode, code128

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Example program that generates a barcode, reads it with custom settings,
/// and serializes the recognition parameters and results to an XML document.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes barcode generation, recognition,
    /// and XML export of the reader state.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for all output files
        string tempFolder = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Define file paths for the generated barcode image and the XML state file
        string barcodePath = Path.Combine(tempFolder, "sample.png");
        string xmlPath = Path.Combine(tempFolder, "readerState.xml");

        // ------------------------------------------------------------
        // Generate a simple Code128 barcode and save it as PNG
        // ------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
        {
            // Adjust the X-dimension (module width) for better readability
            generator.Parameters.Barcode.XDimension.Pixels = 2f;
            generator.Save(barcodePath, BarCodeImageFormat.Png);
        }

        // Verify that the barcode image was successfully created
        if (!File.Exists(barcodePath))
        {
            Console.WriteLine("Failed to generate barcode image.");
            return;
        }

        // ------------------------------------------------------------
        // Read the barcode using specific recognition parameters
        // ------------------------------------------------------------
        using (var reader = new BarCodeReader(barcodePath, DecodeType.Code128))
        {
            // Configure recognition settings
            reader.BarcodeSettings.StripFNC = true;                 // Remove FNC characters from the result
            reader.QualitySettings.XDimension = XDimensionMode.Small; // Expect small modules
            reader.Timeout = 5000;                                 // Set scan timeout to 5 seconds

            // Perform the barcode scan
            BarCodeResult[] results = reader.ReadBarCodes();

            // Output the scan results to the console
            Console.WriteLine($"Barcodes found: {results.Length}");
            foreach (var result in results)
            {
                Console.WriteLine($"Type: {result.CodeTypeName}, Text: {result.CodeText}, Quality: {result.ReadingQuality}");
            }

            // Serialize both the configured parameters and the scan results to XML
            reader.ExportToXml(xmlPath);
        }

        // ------------------------------------------------------------
        // Confirm that the XML file containing the reader state was created
        // ------------------------------------------------------------
        if (File.Exists(xmlPath))
        {
            Console.WriteLine($"Recognition state saved to XML: {xmlPath}");
        }
        else
        {
            Console.WriteLine("Failed to export recognition state to XML.");
        }
    }
}