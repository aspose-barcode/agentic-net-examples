// Title: Batch barcode generation from XML specifications
// Description: Demonstrates reading barcode definition XML files, converting dimensions from millimeters to points, and saving PNG images.
// Category-Description: This example belongs to the Aspose.BarCode generation category, illustrating how to import settings from XML, apply unit conversions, and produce barcode images. It uses BarcodeGenerator, its Parameters, and image saving APIs—common tasks for developers automating barcode creation in batch processes.
// Prompt: Develop batch job reading barcode specs from XML, applying unit conversions, and saving PNGs to directory.
// Tags: barcode generation, xml import, unit conversion, png output, aspose.barcode, batch processing

using System;
using System.IO;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;

/// <summary>
/// Provides a console application that reads barcode specifications from XML files,
/// converts measurement units, and generates PNG barcode images.
/// </summary>
class Program
{
    // Conversion factor from millimeters to points (1 mm = 2.83465 points)
    private const float MmToPoint = 2.83465f;

    /// <summary>
    /// Entry point. Processes up to 10 XML specification files, converts units, and saves PNGs.
    /// </summary>
    static void Main()
    {
        // Input folder containing XML specifications
        string inputFolder = Path.Combine(Directory.GetCurrentDirectory(), "BarcodeSpecs");
        // Output folder for generated PNG images
        string outputFolder = Path.Combine(Directory.GetCurrentDirectory(), "GeneratedBarcodes");

        // Ensure input folder exists; if not, create and place a sample XML
        if (!Directory.Exists(inputFolder))
        {
            Directory.CreateDirectory(inputFolder);
            // Sample XML (minimal) – in a real scenario replace with actual specs
            string sampleXml = Path.Combine(inputFolder, "SampleSpec.xml");
            File.WriteAllText(sampleXml,
@"<BarcodeGenerator>
    <CodeText>Sample123</CodeText>
    <EncodeType>Code128</EncodeType>
    <Parameters>
        <ImageWidth>
            <Millimeters>50</Millimeters>
        </ImageWidth>
        <ImageHeight>
            <Millimeters>20</Millimeters>
        </ImageHeight>
        <Barcode>
            <XDimension>
                <Millimeters>0.5</Millimeters>
            </XDimension>
            <BarHeight>
                <Millimeters>10</Millimeters>
            </BarHeight>
            <Padding>
                <Left>
                    <Millimeters>2</Millimeters>
                </Left>
                <Top>
                    <Millimeters>2</Millimeters>
                </Top>
                <Right>
                    <Millimeters>2</Millimeters>
                </Right>
                <Bottom>
                    <Millimeters>2</Millimeters>
                </Bottom>
            </Padding>
        </Barcode>
    </Parameters>
</BarcodeGenerator>");
        }

        // Ensure output folder exists
        if (!Directory.Exists(outputFolder))
        {
            Directory.CreateDirectory(outputFolder);
        }

        // Process each XML file in the input folder (max 10 for safety)
        string[] xmlFiles = Directory.GetFiles(inputFolder, "*.xml");
        int processedCount = 0;
        foreach (string xmlPath in xmlFiles)
        {
            if (processedCount >= 10) break; // safety cap

            try
            {
                // Import barcode generator settings from XML
                using (BarcodeGenerator generator = BarcodeGenerator.ImportFromXml(xmlPath))
                {
                    // Apply unit conversions from millimeters to points where applicable
                    ConvertUnits(generator);

                    // Determine output file name (same as XML but with .png)
                    string fileNameWithoutExt = Path.GetFileNameWithoutExtension(xmlPath);
                    string outputPath = Path.Combine(outputFolder, fileNameWithoutExt + ".png");

                    // Save the barcode image as PNG
                    generator.Save(outputPath, BarCodeImageFormat.Png);
                    Console.WriteLine($"Generated barcode saved to: {outputPath}");
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error processing '{xmlPath}': {ex.Message}");
            }

            processedCount++;
        }

        // If no XML files were found, inform the user
        if (xmlFiles.Length == 0)
        {
            Console.WriteLine("No XML specification files found in the input folder.");
        }
    }

    // Converts relevant unit properties from millimeters to points
    private static void ConvertUnits(BarcodeGenerator generator)
    {
        // Image width
        if (generator.Parameters.ImageWidth.Millimeters > 0)
        {
            generator.Parameters.ImageWidth.Point = generator.Parameters.ImageWidth.Millimeters * MmToPoint;
        }

        // Image height
        if (generator.Parameters.ImageHeight.Millimeters > 0)
        {
            generator.Parameters.ImageHeight.Point = generator.Parameters.ImageHeight.Millimeters * MmToPoint;
        }

        // X dimension (module size)
        if (generator.Parameters.Barcode.XDimension.Millimeters > 0)
        {
            generator.Parameters.Barcode.XDimension.Point = generator.Parameters.Barcode.XDimension.Millimeters * MmToPoint;
        }

        // Bar height (for 1D barcodes)
        if (generator.Parameters.Barcode.BarHeight.Millimeters > 0)
        {
            generator.Parameters.Barcode.BarHeight.Point = generator.Parameters.Barcode.BarHeight.Millimeters * MmToPoint;
        }

        // Padding
        var padding = generator.Parameters.Barcode.Padding;
        if (padding.Left.Millimeters > 0)
        {
            padding.Left.Point = padding.Left.Millimeters * MmToPoint;
        }
        if (padding.Top.Millimeters > 0)
        {
            padding.Top.Point = padding.Top.Millimeters * MmToPoint;
        }
        if (padding.Right.Millimeters > 0)
        {
            padding.Right.Point = padding.Right.Millimeters * MmToPoint;
        }
        if (padding.Bottom.Millimeters > 0)
        {
            padding.Bottom.Point = padding.Bottom.Millimeters * MmToPoint;
        }
    }
}