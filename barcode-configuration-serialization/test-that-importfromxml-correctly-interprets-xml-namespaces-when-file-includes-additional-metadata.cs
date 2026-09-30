// Title: ImportFromXml with Custom XML Namespace Handling
// Description: Demonstrates exporting a barcode configuration to XML, adding custom metadata with a namespace, and importing it back to verify settings.
// Category-Description: Shows how to use Aspose.BarCode's ExportToXml and ImportFromXml methods, part of the configuration management category. Typical use cases include persisting barcode settings, editing XML manually, and ensuring namespace compatibility. Developers often need to manipulate XML for custom metadata while preserving barcode parameters.
// Prompt: Test that ImportFromXml correctly interprets XML namespaces when the file includes additional metadata.
// Tags: barcode symbology, configuration, xml, namespace, import, export, qrcode, aspose.barcode

using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Example program that exports a QR code configuration to XML, injects custom metadata with a custom namespace,
/// and then imports the configuration back to verify that original settings are retained.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the export‑modify‑import workflow and saves the generated barcode image.
    /// </summary>
    static void Main()
    {
        // Create a unique temporary folder for the test files
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeTest_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        string xmlFilePath = Path.Combine(tempFolder, "barcode_config.xml");
        string outputImagePath = Path.Combine(tempFolder, "generated_barcode.png");

        // Step 1: Generate a barcode and export its configuration to XML
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Test123"))
        {
            // Set distinct parameters to verify after import
            generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Blue;
            generator.Parameters.Barcode.XDimension.Point = 3f;
            generator.Parameters.Barcode.CodeTextParameters.Font.FamilyName = "Arial";
            generator.Parameters.Barcode.CodeTextParameters.Font.Size.Point = 14f;

            // Export configuration to a memory stream
            using (var exportStream = new MemoryStream())
            {
                generator.ExportToXml(exportStream);

                // Reset stream position for reading
                exportStream.Position = 0;

                // Step 2: Load the XML, add extra metadata with a custom namespace
                XDocument doc;
                using (var reader = new StreamReader(exportStream, leaveOpen: true))
                {
                    string xmlContent = reader.ReadToEnd();
                    doc = XDocument.Parse(xmlContent);
                }

                // Define a custom namespace for additional metadata
                XNamespace customNs = "http://example.com/custom";

                // Create and add custom metadata element under the root
                XElement customMetadata = new XElement(customNs + "CustomMetadata",
                    new XElement(customNs + "Info", "Additional test metadata"));
                doc.Root.Add(customMetadata);

                // Save the modified XML to another memory stream
                using (var modifiedStream = new MemoryStream())
                {
                    doc.Save(modifiedStream);
                    modifiedStream.Position = 0;

                    // Step 3: Import the configuration from the modified XML
                    using (var importedGenerator = BarcodeGenerator.ImportFromXml(modifiedStream))
                    {
                        // Output imported settings to verify they match the original values
                        Console.WriteLine("Imported BarColor: " + importedGenerator.Parameters.Barcode.BarColor);
                        Console.WriteLine("Imported XDimension (points): " + importedGenerator.Parameters.Barcode.XDimension.Point);
                        Console.WriteLine("Imported Font Family: " + importedGenerator.Parameters.Barcode.CodeTextParameters.Font.FamilyName);
                        Console.WriteLine("Imported Font Size (points): " + importedGenerator.Parameters.Barcode.CodeTextParameters.Font.Size.Point);

                        // Generate the barcode image and save it to the temporary folder
                        using (Bitmap bitmap = importedGenerator.GenerateBarCodeImage())
                        {
                            bitmap.Save(outputImagePath, ImageFormat.Png);
                        }

                        Console.WriteLine("Barcode image saved to: " + outputImagePath);
                    }
                }
            }
        }

        // Cleanup: optionally delete the temporary folder and its contents
        // Commented out to allow inspection of generated files after execution
        // Directory.Delete(tempFolder, true);
    }
}