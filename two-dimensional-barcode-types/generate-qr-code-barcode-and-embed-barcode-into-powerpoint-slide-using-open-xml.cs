// Title: Generate QR Code barcode and embed it into a PowerPoint slide
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to an image, and inserting that image into a PowerPoint presentation using Aspose.Slides.
// Category-Description: This example belongs to the Aspose.BarCode and Aspose.Slides integration category, illustrating how to generate barcodes (QR, DataMatrix, etc.) and embed them into Office documents via Open XML. Key API classes include BarcodeGenerator, BarCodeImageFormat, Presentation, and PictureFrame. Developers often need to automate report generation, marketing materials, or data‑rich presentations that contain scannable barcodes.
// Prompt: Generate QR Code barcode and embed barcode into PowerPoint slide using Open XML.
// Tags: qr code, barcode generation, powerpoint, openxml, aspose.barcode, aspose.slides, image embedding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;
using Aspose.Slides;
using Aspose.Slides.Export;

/// <summary>
/// Entry point for the QR Code to PowerPoint example.
/// </summary>
class Program
{
    /// <summary>
    /// Generates a QR Code, embeds it into a new PowerPoint slide, and saves the presentation.
    /// </summary>
    static void Main()
    {
        // Define output file path for the PowerPoint presentation
        string outputPath = Path.Combine(Path.GetTempPath(), "QrPresentation.pptx");

        // Generate QR Code barcode and save to a memory stream
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello Aspose"))
        {
            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            using (var ms = new MemoryStream())
            {
                // Save the generated barcode image as PNG into the memory stream
                generator.Save(ms, BarCodeImageFormat.Png);
                byte[] barcodeBytes = ms.ToArray();

                // Create a new PowerPoint presentation
                using (var presentation = new Presentation())
                {
                    // Get the first slide (a default slide exists)
                    var slide = presentation.Slides[0];

                    // Add the barcode image to the presentation's image collection
                    var pptImage = presentation.Images.AddImage(barcodeBytes);

                    // Insert the image onto the slide as a picture frame
                    slide.Shapes.AddPictureFrame(ShapeType.Rectangle, 50, 50, 300, 300, pptImage);

                    // Save the presentation to the specified path
                    presentation.Save(outputPath, SaveFormat.Pptx);
                }
            }
        }

        Console.WriteLine($"Presentation saved to: {outputPath}");
    }
}