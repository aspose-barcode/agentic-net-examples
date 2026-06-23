using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;

/// <summary>
/// Demonstrates loading barcode symbology from an XML file,
/// validating it against an expected value, and generating a barcode image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // Define the expected barcode symbology (Code128 in this case)
        BaseEncodeType expectedSymbology = EncodeTypes.Code128;

        // Path to the optional XML state file that may contain the symbology name
        string xmlPath = "state.xml";

        // Load the XML document: either from file or use a sample XML string if the file is missing
        XDocument doc;
        if (File.Exists(xmlPath))
        {
            try
            {
                doc = XDocument.Load(xmlPath);
            }
            catch (Exception ex)
            {
                // Report any errors that occur while loading the XML file and exit
                Console.WriteLine($"Failed to load XML file: {ex.Message}");
                return;
            }
        }
        else
        {
            // Sample XML used when the state file does not exist
            string sampleXml = @"<State><BarcodeSymbology>Code128</BarcodeSymbology></State>";
            doc = XDocument.Parse(sampleXml);
        }

        // Retrieve the symbology name from the XML document
        string symbologyName = doc.Root?.Element("BarcodeSymbology")?.Value?.Trim();
        if (string.IsNullOrEmpty(symbologyName))
        {
            // If the element is missing or empty, inform the user and exit
            Console.WriteLine("Symbology element not found in XML.");
            return;
        }

        // Use reflection to map the string name to the corresponding EncodeTypes field
        var fieldInfo = typeof(EncodeTypes).GetField(symbologyName);
        if (fieldInfo == null)
        {
            // If the name does not correspond to a known symbology, report and exit
            Console.WriteLine($"Unknown symbology name in XML: {symbologyName}");
            return;
        }

        // Cast the reflected value to BaseEncodeType
        BaseEncodeType xmlSymbology = (BaseEncodeType)fieldInfo.GetValue(null);

        // Compare the symbology from XML with the expected symbology
        if (xmlSymbology.TypeName != expectedSymbology.TypeName)
        {
            // Mismatch detected – report details and exit
            Console.WriteLine($"Symbology mismatch. Expected: {expectedSymbology.TypeName}, Found: {xmlSymbology.TypeName}");
            return;
        }

        // Symbology matches the expectation
        Console.WriteLine($"Symbology validated: {xmlSymbology.TypeName}");

        // Example barcode generation using the validated symbology
        string codeText = "1234567890";               // Data to encode in the barcode
        string outputPath = "validated_barcode.png"; // Destination file for the generated image

        // Create a BarcodeGenerator with the validated symbology and data
        using (var generator = new BarcodeGenerator(xmlSymbology, codeText))
        {
            // Optional: set image resolution (dots per inch)
            generator.Parameters.Resolution = 300f;

            // Save the generated barcode image to the specified path
            generator.Save(outputPath);
        }

        // Inform the user that the barcode has been successfully generated
        Console.WriteLine($"Barcode generated and saved to '{outputPath}'.");
    }
}