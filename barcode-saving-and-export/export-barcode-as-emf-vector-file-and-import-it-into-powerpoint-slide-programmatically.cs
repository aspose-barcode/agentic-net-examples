// Title: Export Barcode to EMF and Insert into PowerPoint
// Description: Demonstrates generating a Code39 barcode, saving it as an EMF vector file, and embedding the image into a PowerPoint slide using Aspose APIs.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category. It showcases the use of BarcodeGenerator (Aspose.BarCode.Generation) to create barcodes, BarCodeImageFormat for vector export, and the Presentation class (Aspose.Slides) to manipulate PowerPoint files. Developers often need to programmatically generate barcodes and include them in documents or presentations, making this pattern a common requirement for reporting, labeling, and marketing automation scenarios.
// Prompt: Export a barcode as an EMF vector file and import it into a PowerPoint slide programmatically.
// Tags: barcode, code39, generation, export, emf, vector, powerpoint, aspose.barcode, aspose.slides, presentation

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;

/// <summary>
/// Generates a barcode, saves it as an EMF file, and embeds it into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the example. Executes the barcode creation, EMF export, and PowerPoint insertion workflow.
    /// </summary>
    static void Main()
    {
        // --------------------------------------------------------------------
        // Prepare a unique temporary output directory for the generated files.
        // --------------------------------------------------------------------
        string outputDir = Path.Combine(Path.GetTempPath(), "BarcodeDemo_" + Guid.NewGuid().ToString("N"));
        Directory.CreateDirectory(outputDir);

        // Define file paths for the EMF image and the resulting PowerPoint file.
        string emfPath = Path.Combine(outputDir, "barcode.emf");
        string pptxPath = Path.Combine(outputDir, "BarcodePresentation.pptx");

        // --------------------------------------------------------------
        // Generate a Code39 barcode and save it as an EMF vector image.
        // --------------------------------------------------------------
        using (var generator = new BarcodeGenerator(EncodeTypes.Code39, "123456789"))
        {
            try
            {
                generator.Save(emfPath, BarCodeImageFormat.Emf);
                Console.WriteLine($"Barcode saved as EMF to: {emfPath}");
            }
            catch (Exception ex)
            {
                // Handle licensing or other errors that may occur during EMF export.
                if (ex.Message.Contains("evaluation", StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine("EMF export requires a valid Aspose.BarCode license.");
                }
                else
                {
                    Console.WriteLine($"Error saving EMF: {ex.Message}");
                }
                return;
            }
        }

        // Verify that the EMF file was created before proceeding.
        if (!File.Exists(emfPath))
        {
            Console.WriteLine("EMF file was not created.");
            return;
        }

        // --------------------------------------------------------------
        // Create a new PowerPoint presentation and embed the EMF barcode.
        // --------------------------------------------------------------
        using (var presentation = new Presentation())
        {
            // Access the first (default) slide.
            var slide = presentation.Slides[0];

            // Load the EMF file into a byte array and add it to the presentation's image collection.
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            var image = presentation.Images.AddImage(emfBytes);

            // Insert the barcode image onto the slide as a picture frame.
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 50, 50, 400, 300, image);

            // Save the presentation to the specified PPTX file.
            presentation.Save(pptxPath, SaveFormat.Pptx);
            Console.WriteLine($"Presentation saved to: {pptxPath}");
        }

        Console.WriteLine("Process completed successfully.");
    }
}