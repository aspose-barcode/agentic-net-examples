using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Slides;
using Aspose.Slides.Export;
using Aspose.Drawing.Imaging;

/// <summary>
/// Demonstrates generating a QR code image in memory and embedding it into a PowerPoint presentation.
/// </summary>
class Program
{
    /// <summary>
    /// Entry point of the application. Generates a QR code, adds it to a slide, and saves the presentation.
    /// </summary>
    static void Main()
    {
        // Generate QR code image in memory
        byte[] qrImageBytes;
        using (var generator = new BarcodeGenerator(EncodeTypes.QR, "https://example.com"))
        {
            // Set QR error correction level (optional)
            generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM;

            // Save the generated QR code to a memory stream as PNG
            using (var ms = new MemoryStream())
            {
                generator.Save(ms, BarCodeImageFormat.Png);
                ms.Position = 0; // Reset stream position before reading
                qrImageBytes = ms.ToArray(); // Convert stream to byte array
            }
        }

        // Create a new PowerPoint presentation and embed the QR code image
        using (var presentation = new Presentation())
        {
            // Access the first slide (add a new slide if the collection is empty)
            var slide = presentation.Slides[0];

            // Add the QR code image bytes to the presentation's image collection
            var pptImage = presentation.Images.AddImage(qrImageBytes);

            // Insert the image onto the slide as a picture frame
            slide.Shapes.AddPictureFrame(
                ShapeType.Rectangle, // Shape type
                50f,                 // X position (points)
                50f,                 // Y position (points)
                300f,                // Width (points)
                300f,                // Height (points)
                pptImage);           // Image reference

            // Save the presentation to a file in PPTX format
            presentation.Save("QRCodePresentation.pptx", SaveFormat.Pptx);
        }

        // Inform the user that the operation completed successfully
        Console.WriteLine("Presentation with QR code generated successfully.");
    }
}