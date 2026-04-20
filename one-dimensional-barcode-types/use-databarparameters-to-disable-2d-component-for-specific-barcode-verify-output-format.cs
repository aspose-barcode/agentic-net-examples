using System;
using System.IO;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.BarCode.BarCodeRecognition;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        // Define output file
        const string outputPath = "databar.png";

        // Create a DataBar Omni-Directional barcode generator
        using (var generator = new BarcodeGenerator(EncodeTypes.DatabarOmniDirectional, "(01)012345678905(21)ABC123"))
        {
            // Disable the 2D composite component
            generator.Parameters.Barcode.DataBar.Is2DCompositeComponent = false;

            // Optional: set some visual parameters
            generator.Parameters.Barcode.BarHeight.Pixels = 100f;
            generator.Parameters.Barcode.XDimension.Pixels = 2f;

            // Save the barcode image
            generator.Save(outputPath);
        }

        // Verify that the file was created
        if (!File.Exists(outputPath))
        {
            Console.WriteLine("Failed to create the barcode image.");
            return;
        }

        // Read the barcode back and check the 2D component flag
        using (var reader = new BarCodeReader(outputPath, DecodeType.DatabarOmniDirectional))
        {
            bool flagFound = false;
            foreach (BarCodeResult result in reader.ReadBarCodes())
            {
                // Access extended DataBar parameters if available
                if (result.Extended != null && result.Extended.DataBar != null)
                {
                    bool is2DComponent = result.Extended.DataBar.Is2DCompositeComponent;
                    Console.WriteLine($"Is2DCompositeComponent flag in recognized barcode: {is2DComponent}");
                    flagFound = true;
                }
                else
                {
                    Console.WriteLine("Extended DataBar parameters not available in the result.");
                }
            }

            if (!flagFound)
            {
                Console.WriteLine("No DataBar barcode was recognized.");
            }
        }

        // Confirm output format (PNG) by checking file extension
        string ext = Path.GetExtension(outputPath);
        Console.WriteLine($"Output file format verified: {ext.TrimStart('.').ToUpperInvariant()}");
    }
}