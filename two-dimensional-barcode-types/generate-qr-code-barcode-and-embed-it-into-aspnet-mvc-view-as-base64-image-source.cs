// Title: Generate QR Code and embed as Base64 image in ASP.NET MVC view
// Description: Demonstrates creating a QR Code barcode with Aspose.BarCode, converting it to a PNG image, encoding the image to a Base64 string, and showing the Razor syntax to embed the result in an MVC view.
// Category-Description: This example belongs to the Aspose.BarCode barcode generation category, illustrating how to use the BarcodeGenerator class to produce QR Code symbology, customize its parameters, and render the barcode as an image. Typical use cases include generating scannable codes for URLs, contact information, or authentication tokens and delivering them to web clients as inline Base64 images. Developers working with ASP.NET MVC often need to embed such images directly in Razor views without storing physical files.
// Prompt: Generate QR Code barcode and embed it into an ASP.NET MVC view as base64 image source.
// Tags: qr code, barcode generation, base64, aspnet mvc, razor, png, aspose.barcode, image encoding

using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

namespace BarcodeConsoleApp
{
    /// <summary>
    /// Console application that creates a QR Code barcode, converts it to a PNG image,
    /// encodes the image as a Base64 string, and demonstrates how to embed the result
    /// in an ASP.NET MVC Razor view.
    /// </summary>
    class Program
    {
        /// <summary>
        /// Entry point of the application.
        /// </summary>
        static void Main()
        {
            // Initialize a BarcodeGenerator for QR Code with the desired text.
            using (var generator = new BarcodeGenerator(EncodeTypes.QR, "Hello, World!"))
            {
                // Set QR Code specific parameters.
                generator.Parameters.Barcode.XDimension.Pixels = 4f;               // Size of a single module (pixel size).
                generator.Parameters.Barcode.QR.ErrorLevel = QRErrorLevel.LevelM; // Error correction level.

                // Generate the barcode image as a Bitmap.
                using (Bitmap bitmap = generator.GenerateBarCodeImage())
                {
                    // Save the Bitmap to a memory stream in PNG format.
                    using (var ms = new MemoryStream())
                    {
                        bitmap.Save(ms, ImageFormat.Png);

                        // Convert the PNG byte array to a Base64 string.
                        string base64 = Convert.ToBase64String(ms.ToArray());

                        // Output the Base64 string to the console (for demonstration purposes).
                        Console.WriteLine("Base64 QR Code image:");
                        Console.WriteLine(base64);

                        // Example Razor markup for embedding the Base64 image in an MVC view:
                        // <img src="data:image/png;base64,@Model.QrBase64" alt="QR Code" />
                    }
                }
            }
        }
    }
}