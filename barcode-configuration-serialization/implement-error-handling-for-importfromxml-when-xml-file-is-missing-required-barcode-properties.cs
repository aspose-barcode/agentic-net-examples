using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;

/// <summary>
/// Demonstrates importing barcode settings from an XML file and handling missing required properties.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application.
    /// </summary>
    static void Main()
    {
        // ------------------------------------------------------------
        // 1. Create a temporary XML file that intentionally lacks required
        //    barcode properties (e.g., missing CodeText or EncodeType).
        // ------------------------------------------------------------
        string tempXmlPath = Path.Combine(Path.GetTempPath(), "barcode_missing_props.xml");
        string xmlContent = @"<?xml version=""1.0"" encoding=""utf-8""?>
<BarcodeGenerator>
    <Parameters>
        <!-- Intentionally omit required properties like CodeText or EncodeType -->
    </Parameters>
</BarcodeGenerator>";
        File.WriteAllText(tempXmlPath, xmlContent);

        // ------------------------------------------------------------
        // 2. Attempt to import barcode settings from the XML file.
        //    If the import succeeds, generate a barcode image.
        // ------------------------------------------------------------
        try
        {
            using (var generator = BarcodeGenerator.ImportFromXml(tempXmlPath))
            {
                // Provide a fallback CodeText in case the XML did not specify one.
                generator.CodeText = "Sample";

                // Define the output path for the generated barcode image.
                string outputPath = Path.Combine(Path.GetTempPath(), "generated_barcode.png");

                // Save the barcode image to the specified location.
                generator.Save(outputPath);

                // Inform the user of successful generation.
                Console.WriteLine($"Barcode generated successfully: {outputPath}");
            }
        }
        catch (Exception ex)
        {
            // ------------------------------------------------------------
            // 3. Handle any errors caused by missing required properties
            //    or other issues during import.
            // ------------------------------------------------------------
            Console.WriteLine("Failed to import barcode from XML:");
            Console.WriteLine($"Error: {ex.Message}");
        }
        finally
        {
            // ------------------------------------------------------------
            // 4. Clean up the temporary XML file regardless of success or failure.
            // ------------------------------------------------------------
            if (File.Exists(tempXmlPath))
            {
                File.Delete(tempXmlPath);
            }
        }
    }
}