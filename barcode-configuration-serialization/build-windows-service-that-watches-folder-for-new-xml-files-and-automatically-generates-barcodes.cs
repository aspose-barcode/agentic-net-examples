using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates reading barcode definitions from XML files and generating PNG images using Aspose.BarCode.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Processes all XML files in the Input folder and creates barcode images in the Output folder.
    /// </summary>
    static void Main()
    {
        // Determine the input and output directories relative to the executable location.
        string inputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Input");
        string outputFolder = Path.Combine(AppDomain.CurrentDomain.BaseDirectory, "Output");

        // Verify that the input folder exists; otherwise, inform the user and exit.
        if (!Directory.Exists(inputFolder))
        {
            Console.WriteLine($"Input folder not found: {inputFolder}");
            return;
        }

        // Ensure the output folder exists (creates it if necessary).
        Directory.CreateDirectory(outputFolder);

        // Retrieve all XML files from the input directory.
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML files to process.");
            return;
        }

        // Process each XML file individually.
        foreach (string xmlPath in xmlFiles)
        {
            try
            {
                // Load the XML document.
                XDocument doc = XDocument.Load(xmlPath);

                // Locate the <Barcode> element; if missing, skip this file.
                XElement barcodeElem = doc.Root.Element("Barcode");
                if (barcodeElem == null)
                {
                    Console.WriteLine($"Missing <Barcode> element in {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Extract required values: symbology type and the code text.
                string symbologyName = (string)barcodeElem.Element("Symbology");
                string codeText = (string)barcodeElem.Element("CodeText");

                // Validate extracted data.
                if (string.IsNullOrWhiteSpace(symbologyName) || string.IsNullOrWhiteSpace(codeText))
                {
                    Console.WriteLine($"Invalid data in {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Resolve the symbology name to an EncodeTypes field.
                var field = typeof(EncodeTypes).GetField(symbologyName);
                if (field == null)
                {
                    Console.WriteLine($"Unknown symbology '{symbologyName}' in {Path.GetFileName(xmlPath)}");
                    continue;
                }

                // Cast the field value to BaseEncodeType.
                BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

                // Build the output file path (same name as input, but with .png extension).
                string outputPath = Path.Combine(outputFolder, Path.GetFileNameWithoutExtension(xmlPath) + ".png");

                // Generate the barcode image.
                GenerateBarcode(encodeType, codeText, outputPath);

                // Inform the user of successful generation.
                Console.WriteLine($"Generated barcode for {Path.GetFileName(xmlPath)} -> {Path.GetFileName(outputPath)}");
            }
            catch (Exception ex)
            {
                // Report any errors encountered while processing the current file.
                Console.WriteLine($"Error processing {Path.GetFileName(xmlPath)}: {ex.Message}");
            }
        }
    }

    /// <summary>
    /// Generates a barcode image using the specified encoding type and text, then saves it to the given path.
    /// </summary>
    /// <param name="type">The barcode symbology to use.</param>
    /// <param name="codeText">The text to encode in the barcode.</param>
    /// <param name="outputPath">The file path where the PNG image will be saved.</param>
    static void GenerateBarcode(BaseEncodeType type, string codeText, string outputPath)
    {
        // Initialize the barcode generator with the desired type and text.
        using (var generator = new BarcodeGenerator(type, codeText))
        {
            // Set a modest XDimension for readability (2 points).
            generator.Parameters.Barcode.XDimension.Point = 2f;

            // Save the generated barcode as a PNG file.
            generator.Save(outputPath);
        }
    }
}