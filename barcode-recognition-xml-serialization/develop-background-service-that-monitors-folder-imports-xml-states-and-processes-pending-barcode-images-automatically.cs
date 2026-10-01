// Title: Generate and Verify Barcodes from XML Definitions
// Description: Demonstrates reading barcode specifications from XML files, generating corresponding images, and verifying them using Aspose.BarCode.
// Category-Description: This example belongs to the Aspose.BarCode generation and recognition category, showcasing how to use BarcodeGenerator, BarCodeReader, EncodeTypes, and DecodeType classes. Typical use cases include batch barcode creation from data sources, automated verification pipelines, and integration with document workflows. Developers often need to import barcode parameters from external files, generate images in common formats, and confirm readability programmatically.
// Prompt: Develop a background service that monitors a folder, imports XML states, and processes pending barcode images automatically.
// Tags: barcode generation, barcode recognition, xml import, aspose.barcode, encode types, decode types, png output

using System;
using System.IO;
using System.Text;
using System.Xml.Linq;
using System.Reflection;
using System.Collections.Generic;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Demonstrates a simple workflow that reads barcode definitions from XML files,
/// generates barcode images, and verifies them using Aspose.BarCode APIs.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary folders, processes sample XML files,
    /// generates PNG barcode images, and reads them back for verification.
    /// </summary>
    static void Main()
    {
        // Create a unique working folder in the system temp directory
        string workFolder = Path.Combine(Path.GetTempPath(), "BarcodeService_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(workFolder);

        // Subfolder for generated barcode images
        string imageFolder = Path.Combine(workFolder, "Images");
        Directory.CreateDirectory(imageFolder);

        // Generate a few sample XML files that describe barcodes
        List<string> xmlFiles = CreateSampleXmlFiles(workFolder);

        // Process each XML file sequentially
        foreach (string xmlPath in xmlFiles)
        {
            if (!File.Exists(xmlPath))
            {
                Console.WriteLine($"File not found: {xmlPath}");
                continue;
            }

            try
            {
                // Load the XML document
                XDocument doc = XDocument.Load(xmlPath);
                XElement barcodeElem = doc.Root.Element("Barcode");
                if (barcodeElem == null)
                {
                    Console.WriteLine($"Invalid format in {xmlPath}");
                    continue;
                }

                // Extract symbology name and code text
                string symbologyName = barcodeElem.Element("Symbology")?.Value?.Trim();
                string codeText = barcodeElem.Element("CodeText")?.Value?.Trim();

                if (string.IsNullOrEmpty(symbologyName) || string.IsNullOrEmpty(codeText))
                {
                    Console.WriteLine($"Missing data in {xmlPath}");
                    continue;
                }

                // Resolve the symbology name to an EncodeTypes field via reflection
                FieldInfo field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in {xmlPath}");
                    continue;
                }

                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Build the output image path (same name as XML, but .png)
                string imagePath = Path.Combine(imageFolder, Path.GetFileNameWithoutExtension(xmlPath) + ".png");

                // Generate the barcode image
                using (var generator = new BarcodeGenerator(encodeType, codeText))
                {
                    // Set basic visual parameters
                    generator.Parameters.Barcode.XDimension.Point = 2f;
                    generator.Parameters.Barcode.BarColor = Aspose.Drawing.Color.Black;
                    generator.Parameters.BackColor = Aspose.Drawing.Color.White;

                    // Save the image as PNG
                    generator.Save(imagePath, BarCodeImageFormat.Png);
                }

                // Verify the generated barcode by reading it back
                using (var reader = new BarCodeReader(imagePath, DecodeType.AllSupportedTypes))
                {
                    BarCodeResult[] results = reader.ReadBarCodes();
                    if (results.Length == 0)
                    {
                        Console.WriteLine($"No barcode detected in image: {imagePath}");
                    }
                    else
                    {
                        foreach (var result in results)
                        {
                            Console.WriteLine($"File: {Path.GetFileName(imagePath)} | Detected: {result.CodeTypeName} | Text: {result.CodeText}");
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing {xmlPath}: {ex.Message}");
            }
        }

        // Optional cleanup: delete the temporary working folder
        // Directory.Delete(workFolder, true);
    }

    /// <summary>
    /// Creates a few sample XML files that describe barcodes and returns their file paths.
    /// </summary>
    /// <param name="folder">The folder where the XML files will be written.</param>
    /// <returns>List of full paths to the created XML files.</returns>
    private static List<string> CreateSampleXmlFiles(string folder)
    {
        var files = new List<string>();

        // Example 1: QR code
        string xml1 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <Barcode>
    <Symbology>QR</Symbology>
    <CodeText>Hello Aspose</CodeText>
  </Barcode>
</Root>";
        string path1 = Path.Combine(folder, "barcode1.xml");
        File.WriteAllText(path1, xml1, Encoding.UTF8);
        files.Add(path1);

        // Example 2: Code128
        string xml2 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <Barcode>
    <Symbology>Code128</Symbology>
    <CodeText>1234567890</CodeText>
  </Barcode>
</Root>";
        string path2 = Path.Combine(folder, "barcode2.xml");
        File.WriteAllText(path2, xml2, Encoding.UTF8);
        files.Add(path2);

        // Example 3: DataMatrix
        string xml3 = @"<?xml version=""1.0"" encoding=""utf-8""?>
<Root>
  <Barcode>
    <Symbology>DataMatrix</Symbology>
    <CodeText>DataMatrixSample</CodeText>
  </Barcode>
</Root>";
        string path3 = Path.Combine(folder, "barcode3.xml");
        File.WriteAllText(path3, xml3, Encoding.UTF8);
        files.Add(path3);

        return files;
    }
}