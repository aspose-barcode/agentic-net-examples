// Title: Export barcode XML state, edit YDimension, and re‑render
// Description: Demonstrates exporting a barcode's configuration to XML, modifying the YDimension property, and generating a new image to see the size change.
// Category-Description: This example belongs to the Aspose.BarCode generation and manipulation category, showcasing how to persist barcode settings via XML, adjust dimensional properties, and re‑create images. It uses BarcodeGenerator, its Parameters, and ImportFromXml methods—common tasks for developers needing dynamic barcode customization and state persistence.
// Prompt: Export barcode XML state, edit YDimension, re‑render to observe size change.
// Tags: code128, export, edit, render, png, xml, barcodegenerator, parameters

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode;
using Aspose.Drawing;

/// <summary>
/// Demonstrates exporting a barcode's state to XML, modifying its YDimension, and re‑rendering the image.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Generates an initial barcode, saves its XML state, modifies YDimension, and saves the updated barcode image.
    /// </summary>
    static void Main()
    {
        // Define file paths for the initial image, XML state, and the modified image.
        string initialImagePath = "barcode_initial.png";
        string xmlPath = "barcode_state.xml";
        string modifiedImagePath = "barcode_modified.png";

        // 1. Create a barcode generator for Code128 and save the initial image.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "Sample123"))
        {
            // Set a small size for visibility.
            generator.Parameters.AutoSizeMode = AutoSizeMode.Interpolation;
            generator.Parameters.ImageWidth.Point = 200f;
            generator.Parameters.ImageHeight.Point = 80f;

            // Save the generated barcode image.
            generator.Save(initialImagePath);

            // Export the current generator state to an XML file.
            generator.ExportToXml(xmlPath);
        }

        // 2. Verify that the XML file was created before attempting to import.
        if (!File.Exists(xmlPath))
        {
            Console.WriteLine($"XML file not found: {xmlPath}");
            return;
        }

        // 3. Load the barcode generator from the exported XML.
        using (var generatorFromXml = BarcodeGenerator.ImportFromXml(xmlPath))
        {
            // Attempt to modify the YDimension property if it exists for the barcode type.
            var barcodeParams = generatorFromXml.Parameters.Barcode;
            var yDimProp = barcodeParams.GetType().GetProperty("YDimension");
            if (yDimProp != null)
            {
                // YDimension is a Unit; increase its value to 5 points.
                var unit = (Unit) yDimProp.GetValue(barcodeParams);
                unit.Point = 5f;
                Console.WriteLine("YDimension modified to 5 points.");
            }
            else
            {
                Console.WriteLine("YDimension property not available for this barcode type.");
            }

            // 4. Save the modified barcode image to observe the size change.
            generatorFromXml.Save(modifiedImagePath);
        }

        Console.WriteLine("Processing completed.");
    }
}