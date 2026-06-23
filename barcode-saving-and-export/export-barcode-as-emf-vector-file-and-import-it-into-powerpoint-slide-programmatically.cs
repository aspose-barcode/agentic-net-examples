using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing;

/// <summary>
/// Demonstrates generating a barcode, saving it as EMF, and embedding it into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a Code128 barcode, saves it as an EMF file,
    /// inserts the EMF into a new PowerPoint slide, and saves the presentation.
    /// </summary>
    static void Main()
    {
        // Define temporary file path for the EMF image and final PPTX output path
        string emfPath = Path.Combine(Path.GetTempPath(), "barcode.emf");
        string pptxPath = Path.Combine(Directory.GetCurrentDirectory(), "BarcodePresentation.pptx");

        // ------------------------------------------------------------
        // 1. Generate a barcode and save it as EMF
        // ------------------------------------------------------------
        try
        {
            // Initialize the barcode generator with Code128 symbology and data
            using (var generator = new BarcodeGenerator(EncodeTypes.Code128, "1234567890"))
            {
                // Set image resolution (dots per inch) for higher quality output
                generator.Parameters.Resolution = 300f;

                // Save the generated barcode to the EMF file (vector format)
                generator.Save(emfPath, BarCodeImageFormat.Emf);
            }
        }
        catch (Exception ex)
        {
            // The evaluation version may throw an exception for unsupported EMF export.
            // Detect license limitation and inform the user, then exit gracefully.
            if (ex.Message != null && ex.Message.Contains("evaluation"))
            {
                Console.WriteLine("A valid Aspose.BarCode license is required to export EMF for this barcode type.");
                return;
            }

            // Re-throw any other unexpected exceptions
            throw;
        }

        // ------------------------------------------------------------
        // 2. Create a PowerPoint presentation and insert the EMF image
        // ------------------------------------------------------------
        using (var presentation = new Presentation())
        {
            // Retrieve the default first slide created with the presentation
            var slide = presentation.Slides[0];

            // Read the EMF file bytes and add the image to the presentation's image collection
            byte[] emfBytes = File.ReadAllBytes(emfPath);
            var pptImage = presentation.Images.AddImage(emfBytes);

            // Define the position (X, Y) and size (Width, Height) of the picture frame in points
            float pictureX = 50f;      // Left margin
            float pictureY = 50f;      // Top margin
            float pictureWidth = 300f; // Width of the barcode image
            float pictureHeight = 150f;// Height of the barcode image

            // Insert the EMF barcode into the slide as a picture frame
            slide.Shapes.AddPictureFrame(ShapeType.Rectangle, pictureX, pictureY, pictureWidth, pictureHeight, pptImage);

            // Save the PowerPoint file to the specified path
            presentation.Save(pptxPath, SaveFormat.Pptx);
        }

        // Output the locations of the generated files for user reference
        Console.WriteLine($"Barcode EMF saved to: {emfPath}");
        Console.WriteLine($"PowerPoint presentation created at: {pptxPath}");
    }
}