using System;
using System.IO;
using System.Reflection;
using Aspose.BarCode;
using Aspose.BarCode.Generation;
using Aspose.Drawing.Imaging;

namespace BarcodeSample
{
    class Program
    {
        static void Main()
        {
            // Sample usage
            using (MemoryStream ms = GenerateBarcode("Code128", "Pixels", 300))
            {
                Console.WriteLine($"Generated barcode image size: {ms.Length} bytes");
            }
        }

        /// <summary>
        /// Generates a barcode image and returns it as a MemoryStream.
        /// </summary>
        /// <param name="symbology">Name of the barcode symbology (e.g., "Code128").</param>
        /// <param name="sizeUnit">Unit for image dimensions: "Points", "Pixels", "Inches", or "Millimeters".</param>
        /// <param name="resolution">Resolution in DPI (must be greater than 0).</param>
        /// <returns>MemoryStream containing the PNG image.</returns>
        static MemoryStream GenerateBarcode(string symbology, string sizeUnit, int resolution)
        {
            if (string.IsNullOrWhiteSpace(symbology))
                throw new ArgumentException("Symbology name must be provided.", nameof(symbology));

            if (string.IsNullOrWhiteSpace(sizeUnit))
                throw new ArgumentException("Size unit must be provided.", nameof(sizeUnit));

            if (resolution <= 0)
                throw new ArgumentOutOfRangeException(nameof(resolution), "Resolution must be greater than zero.");

            // Resolve EncodeTypes member via reflection
            Type encodeTypes = typeof(EncodeTypes);
            FieldInfo field = encodeTypes.GetField(symbology, BindingFlags.Public | BindingFlags.Static);
            if (field == null)
                throw new ArgumentException($"Symbology '{symbology}' is not a valid EncodeTypes member.", nameof(symbology));

            BaseEncodeType encodeType = (BaseEncodeType)field.GetValue(null);

            // Create generator with a sample codetext
            using (var generator = new BarcodeGenerator(encodeType, "123456"))
            {
                // Set resolution
                generator.Parameters.Resolution = resolution;

                // Set image size (both width and height) using the specified unit
                const float sizeValue = 300f; // arbitrary size
                switch (sizeUnit.Trim().ToLowerInvariant())
                {
                    case "points":
                        generator.Parameters.ImageWidth.Point = sizeValue;
                        generator.Parameters.ImageHeight.Point = sizeValue;
                        break;
                    case "pixels":
                        generator.Parameters.ImageWidth.Pixels = sizeValue;
                        generator.Parameters.ImageHeight.Pixels = sizeValue;
                        break;
                    case "inches":
                        generator.Parameters.ImageWidth.Inches = sizeValue;
                        generator.Parameters.ImageHeight.Inches = sizeValue;
                        break;
                    case "millimeters":
                        generator.Parameters.ImageWidth.Millimeters = sizeValue;
                        generator.Parameters.ImageHeight.Millimeters = sizeValue;
                        break;
                    default:
                        throw new ArgumentException($"Unsupported size unit '{sizeUnit}'.", nameof(sizeUnit));
                }

                // Save to memory stream in PNG format
                var memoryStream = new MemoryStream();
                generator.Save(memoryStream, BarCodeImageFormat.Png);
                memoryStream.Position = 0; // reset position for reading
                return memoryStream;
            }
        }
    }
}