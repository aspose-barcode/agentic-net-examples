// Title: Export Barcode to EMF and Insert into PowerPoint
// Description: Demonstrates generating a Code39 barcode as an EMF vector file using Aspose.BarCode and embedding it into a PowerPoint slide with Aspose.Slides.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category, showcasing how to create barcode images (using BarcodeGenerator, BarCodeImageFormat) and programmatically add them to presentation files (using Presentation, ISlide, IPictureFrame). Typical use cases include automating report generation, labeling, and dynamic slide creation where high‑quality vector barcodes are required.
// Prompt: Export a barcode as an EMF vector file and import it into a PowerPoint slide programmatically.
// Tags: barcode, code39, generation, emf, vector, powerpoint, aspose.barcode, aspose.slides, export, import

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;

/// <summary>
/// Example program that creates a Code39 barcode, saves it as an EMF file,
/// and inserts the vector image into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point. Generates the EMF barcode, verifies its creation,
    /// then builds a PPTX file containing the barcode image.
    /// </summary>
    static void Main()
    {
        // Determine output directory relative to the current working folder.
        string outputDir = Path.Combine(Directory.GetCurrentDirectory(), "Output");
        if (!Directory.Exists(outputDir))
        {
            // Create the directory if it does not already exist.
            Directory.CreateDirectory(outputDir);
        }

        // Path for the generated EMF barcode file.
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        GenerateBarcodeEmf(emfPath);

        // Verify that the EMF file was successfully created before proceeding.
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("EMF file was not created. Exiting.");
            return;
        }

        // Path for the resulting PowerPoint presentation.
        string pptxPath = Path.Combine(outputDir, "BarcodePresentation.pptx");
        CreatePresentationWithEmf(pptxPath, emfPath);
        Console.WriteLine($"Presentation saved to: {pptxPath}");
    }

    /// <summary>
    /// Generates a Code39 barcode and saves it as an EMF vector file.
    /// </summary>
    /// <param name="filePath">Full file path where the EMF image will be saved.</param>
    static void GenerateBarcodeEmf(string filePath)
    {
        // Initialize the barcode generator with the desired symbology and value.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "12345"))
        {
            try
            {
                // Export the barcode to EMF format.
                generator.Save(filePath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                // Provide a friendly message if the operation fails due to licensing.
                if (ex.Message.Contains("evaluation", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                }
                else
                {
                    Console.WriteLine($"Error generating EMF: {ex.Message}");
                }
            }
        }
    }

    /// <summary>
    /// Creates a new PowerPoint presentation and inserts the EMF barcode image onto the first slide.
    /// </summary>
    /// <param name="presentationPath">Full file path where the PPTX will be saved.</param>
    /// <param name="emfFilePath">Path to the EMF barcode image to embed.</param>
    static void CreatePresentationWithEmf(string presentationPath, string emfFilePath)
    {
        // Create a new, empty presentation.
        using (var presentation = new Presentation())
        {
            // Access the first (default) slide.
            var slide = presentation.Slides[0];

            // Load the EMF file bytes.
            byte[] emfBytes = File.ReadAllBytes(emfFilePath);

            // Add the EMF image to the presentation's image collection.
            var image = presentation.Images.AddImage(emfBytes);

            // Insert the image as a picture frame at position (0,0) with size 400x300 points.
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 0f, 0f, 400f, 300f, image);

            // Save the presentation in PPTX format.
            presentation.Save(presentationPath, SaveFormat.Pptx);
        }
    }
}