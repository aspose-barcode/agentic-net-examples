using System;
using System.IO;
using Aspose.BarCode.Generation;

namespace BarcodeExample
{
    class Program
    {
        static void Main()
        {
            // Example usage
            using (MemoryStream ms = GenerateBarcode("Code128", "Point", 300f))
            {
                // For demonstration, write the size of the generated image stream
                Console.WriteLine($"Generated barcode image size: {ms.Length} bytes");
            }
        }

        /// <summary>
        /// Generates a barcode image and returns it as a MemoryStream.
        /// </summary>
        /// <param name="symbology">Barcode symbology name (e.g., "Code128", "QRCode").</param>
        /// <param name="sizeUnit">Unit for image size: "Point", "Pixel", "Inch", or "Millimeter".</param>
        /// <param name="resolution">Resolution in DPI (must be greater than 0).</param>
        /// <returns>MemoryStream containing the PNG image.</returns>
        public static MemoryStream GenerateBarcode(string symbology, string sizeUnit, float resolution)
        {
            if (resolution <= 0f)
                throw new ArgumentOutOfRangeException(nameof(resolution), "Resolution must be greater than zero.");

            // Resolve symbology to a valid EncodeTypes member
            BaseEncodeType encodeType = ResolveSymbology(symbology);

            // Create the generator with a sample code text
            using (var generator = new BarcodeGenerator(encodeType, "123456"))
            {
                // Set resolution
                generator.Parameters.Resolution = resolution;

                // Set image size (example values)
                const float widthValue = 300f;
                const float heightValue = 150f;

                SetUnitValue(generator.Parameters.ImageWidth, sizeUnit, widthValue);
                SetUnitValue(generator.Parameters.ImageHeight, sizeUnit, heightValue);

                // Save to memory stream as PNG
                var stream = new MemoryStream();
                generator.Save(stream, BarCodeImageFormat.Png);
                stream.Position = 0; // Reset position for reading
                return stream;
            }
        }

        private static BaseEncodeType ResolveSymbology(string symbology)
        {
            if (string.IsNullOrWhiteSpace(symbology))
                throw new ArgumentException("Symbology must be provided.", nameof(symbology));

            switch (symbology.Trim().ToLowerInvariant())
            {
                case "code128":
                    return EncodeTypes.Code128;
                case "code39":
                    return EncodeTypes.Code39;
                case "code39fullascii":
                    return EncodeTypes.Code39FullASCII;
                case "ean13":
                    return EncodeTypes.EAN13;
                case "ean8":
                    return EncodeTypes.EAN8;
                case "qr":
                case "qrcode":
                    return EncodeTypes.QR;
                case "datamatrix":
                    return EncodeTypes.DataMatrix;
                case "pdf417":
                    return EncodeTypes.Pdf417;
                // Add more mappings as needed
                default:
                    throw new ArgumentException($"Unsupported symbology: {symbology}", nameof(symbology));
            }
        }

        private static void SetUnitValue(Unit unit, string sizeUnit, float value)
        {
            if (unit == null)
                throw new ArgumentNullException(nameof(unit));

            if (string.IsNullOrWhiteSpace(sizeUnit))
                throw new ArgumentException("Size unit must be provided.", nameof(sizeUnit));

            switch (sizeUnit.Trim().ToLowerInvariant())
            {
                case "point":
                case "points":
                    unit.Point = value;
                    break;
                case "pixel":
                case "pixels":
                    unit.Pixels = value;
                    break;
                case "inch":
                case "inches":
                    unit.Inches = value;
                    break;
                case "millimeter":
                case "millimeters":
                    unit.Millimeters = value;
                    break;
                default:
                    throw new ArgumentException($"Unsupported size unit: {sizeUnit}", nameof(sizeUnit));
            }
        }
    }
}