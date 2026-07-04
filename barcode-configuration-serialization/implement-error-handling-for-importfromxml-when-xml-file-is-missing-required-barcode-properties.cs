// Title: Import Barcode Generator from XML with Validation
// Description: Demonstrates importing a barcode configuration from an XML file, validating required properties, and handling errors.
// Prompt: Implement error handling for ImportFromXml when the XML file is missing required barcode properties.
// Tags: barcode symbology, import, xml, error handling, aspose.barcodes

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

/// <summary>
/// Example program that imports a barcode generator configuration from an XML file,
/// validates required properties, and generates a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the program.
    /// </summary>
    static void Main()
    {
        // Path to the XML configuration file
        string xmlPath = "barcodeConfig.xml";

        // Verify that the file exists before attempting to import
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"Error: XML file '{xmlPath}' does not exist.");
            return;
        }

        // Attempt to import the BarcodeGenerator from the XML file
        BarcodeGenerator generator = null;
        try
        {
            generator = BarcodeGenerator.ImportFromXml(xmlPath);
        }
        catch (BarCodeException ex)
        {
            // Handle known barcode-specific errors during import
            Console.WriteLine($"BarCodeException while importing XML: {ex.Message}");
            return;
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors during import
            Console.WriteLine($"Unexpected error while importing XML: {ex.Message}");
            return;
        }

        // Ensure the generator was created successfully
        if (generator == null)
        {
            Console.WriteLine("Error: ImportFromXml returned null.");
            return;
        }

        // Validate required barcode properties
        // For this example, EncodeType (BarcodeType) and CodeText are required
        bool hasError = false;

        // Check that the EncodeType (BarcodeType) is present
        if (generator.BarcodeType == null)
        {
            Console.WriteLine("Error: Encode type is missing in the XML configuration.");
            hasError = true;
        }

        // Check that the CodeText (data to encode) is present and not empty
        if (string.IsNullOrWhiteSpace(generator.CodeText))
        {
            Console.WriteLine("Error: CodeText (data to encode) is missing or empty in the XML configuration.");
            hasError = true;
        }

        // If any validation errors were found, clean up and exit
        if (hasError)
        {
            generator.Dispose();
            return;
        }

        // Validation passed – generate and save the barcode image
        string outputPath = "generatedBarcode.png";
        try
        {
            // Use a using block to ensure proper disposal of the generator
            using (generator)
            {
                generator.Save(outputPath);
                Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
            }
        }
        catch (BarCodeException ex)
        {
            // Handle barcode-specific errors during generation or saving
            Console.WriteLine($"BarCodeException while generating/saving barcode: {ex.Message}");
        }
        catch (Exception ex)
        {
            // Handle any other unexpected errors during generation or saving
            Console.WriteLine($"Unexpected error while generating/saving barcode: {ex.Message}");
        }
    }
}