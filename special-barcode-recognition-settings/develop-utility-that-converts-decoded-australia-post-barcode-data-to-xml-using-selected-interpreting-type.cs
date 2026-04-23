using System;
using System.IO;
using System.Xml.Linq;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Sample data for Australia Post barcode
        const string barcodeData = "5912345678ABCde";
        const CustomerInformationInterpretingType interpretingType = CustomerInformationInterpretingType.CTable;

        // File names
        const string imagePath = "AustraliaPost.png";
        const string xmlPath = "AustraliaPost.xml";

        // Generate the barcode image
        using (var generator = new BarcodeGenerator(EncodeTypes.AustraliaPost, barcodeData))
        {
            // Set interpreting type for customer information
            generator.Parameters.Barcode.AustralianPost.AustralianPostEncodingTable = interpretingType;

            // Save the image to a file
            generator.Save(imagePath, BarCodeImageFormat.Png);
        }

        // Verify that the image was created
        if (!File.Exists(imagePath))
        {
            Console.WriteLine($"Failed to create barcode image at '{imagePath}'.");
            return;
        }

        // Decode the barcode and build XML
        using (var image = (Bitmap)Image.FromFile(imagePath))
        using (var reader = new BarCodeReader(image, DecodeType.AustraliaPost))
        {
            // Apply the same interpreting type used during generation
            reader.BarcodeSettings.AustraliaPost.CustomerInformationInterpretingType = interpretingType;

            // Prepare XML root
            var root = new XElement("AustraliaPostBarcode",
                new XElement("InterpretingType", interpretingType.ToString()));

            // Read all detected barcodes (should be one)
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Add decoded information to XML
                root.Add(new XElement("CodeText", result.CodeText));
                root.Add(new XElement("Symbology", result.CodeType));
            }

            // Save XML to file
            var doc = new XDocument(root);
            doc.Save(xmlPath);
        }

        // Output result paths
        Console.WriteLine($"Barcode image saved to: {Path.GetFullPath(imagePath)}");
        Console.WriteLine($"Decoded XML saved to: {Path.GetFullPath(xmlPath)}");
    }
}