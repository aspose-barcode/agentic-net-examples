// Title: Export Barcode to EMF and Embed in PowerPoint
// Description: Demonstrates how to generate a Code39 barcode, save it as an EMF vector file, and programmatically insert it into a PowerPoint slide.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category, showcasing the use of BarcodeGenerator for barcode creation, BarCodeImageFormat for vector export, and Presentation for PowerPoint manipulation. Typical use cases include automated report generation, batch creation of marketing materials, and embedding high‑resolution barcodes in slide decks. Developers often need to combine barcode rendering with document automation, and this snippet provides a concise reference.
// Prompt: Export a barcode as an EMF vector file and import it into a PowerPoint slide programmatically.
// Tags: barcode, code39, export, emf, vector, powerpoint, aspose.barcode, aspose.slides, generation, import

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

/// <summary>
/// Generates a barcode, saves it as an EMF file, and embeds the image into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Creates temporary folders, generates the barcode EMF, builds the presentation, and reports file locations.
    /// </summary>
    static void Main()
    {
        // Define a unique temporary folder for all generated files.
        string tempFolder = Path.Combine(Path.GetTempPath(), "AsposeBarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(tempFolder);

        // Paths for the EMF barcode image and the resulting PowerPoint file.
        string emfPath = Path.Combine(tempFolder, "barcode.emf");
        string pptxPath = Path.Combine(tempFolder, "BarcodePresentation.pptx");

        // Generate the barcode and save it as an EMF vector image.
        GenerateBarcodeEmf(emfPath);

        // Create a new PowerPoint presentation and embed the EMF barcode image.
        CreatePresentationWithBarcode(pptxPath, emfPath);

        // Output the locations of the generated files.
        Console.WriteLine("Barcode EMF saved to: " + emfPath);
        Console.WriteLine("PowerPoint presentation saved to: " + pptxPath);
    }

    /// <summary>
    /// Generates a Code39 barcode and saves it to the specified path in EMF format.
    /// </summary>
    /// <param name="outputPath">Full file path where the EMF image will be saved.</param>
    static void GenerateBarcodeEmf(string outputPath)
    {
        // Code39 is supported for EMF export in evaluation mode.
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123ABC"))
        {
            // Adjust barcode appearance (optional).
            generator.Parameters.Barcode.XDimension.Point = 2f;
            generator.Parameters.Barcode.BarHeight.Point = 30f;

            try
            {
                // Save the barcode as an EMF vector file.
                generator.Save(outputPath, BarCodeImageFormat.Emf);
            }
            catch (Exception ex)
            {
                // Provide a clearer message if the failure is due to licensing restrictions.
                if (ex.Message != null && ex.Message.IndexOf("evaluation", StringComparison.OrdinalIgnoreCase) >= 0)
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                }
                else
                {
                    Console.WriteLine("Error saving EMF: " + ex.Message);
                }
                throw;
            }
        }
    }

    /// <summary>
    /// Creates a PowerPoint presentation and inserts the specified EMF barcode image onto the first slide.
    /// </summary>
    /// <param name="presentationPath">Full file path where the PowerPoint file will be saved.</param>
    /// <param name="emfFilePath">Full file path of the EMF barcode image to embed.</param>
    static void CreatePresentationWithBarcode(string presentationPath, string emfFilePath)
    {
        if (!File.Exists(emfFilePath))
        {
            Console.WriteLine("EMF file not found: " + emfFilePath);
            return;
        }

        // Load the EMF image bytes into memory.
        byte[] emfBytes = File.ReadAllBytes(emfFilePath);

        using (var presentation = new Presentation())
        {
            // Retrieve the first (blank) slide.
            ISlide slide = presentation.Slides[0];

            // Add the EMF image to the presentation's image collection.
            IPPImage emfImage = presentation.Images.AddImage(emfBytes);

            // Define picture frame position and size (in points).
            float pictureX = 50f;   // Left offset
            float pictureY = 50f;   // Top offset
            float pictureWidth = 400f;
            float pictureHeight = 150f;

            // Insert the picture frame containing the EMF barcode.
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, pictureX, pictureY, pictureWidth, pictureHeight, emfImage);

            // Save the presentation in PPTX format.
            presentation.Save(presentationPath, Aspose.Slides.Export.SaveFormat.Pptx);
        }
    }
}