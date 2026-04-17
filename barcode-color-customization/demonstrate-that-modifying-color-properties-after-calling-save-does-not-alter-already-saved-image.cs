using System;
using System.IO;
using System.Security.Cryptography;
using Aspose.BarCode.Generation;
using Aspose.Drawing;
using Aspose.Drawing.Imaging;

class Program
{
    static void Main()
    {
        const string file1 = "barcode1.png";
        const string file2 = "barcode2.png";

        // Clean up any previous files
        if (File.Exists(file1)) File.Delete(file1);
        if (File.Exists(file2)) File.Delete(file2);

        // Create barcode generator and set initial colors
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Demo123";
            generator.Parameters.BackColor = Color.White;
            generator.Parameters.Barcode.BarColor = Color.Black;

            // Save first image
            generator.Save(file1, BarCodeImageFormat.Png);
        }

        // Compute hash of the first saved file
        byte[] originalHash;
        using (var stream = File.OpenRead(file1))
        using (var sha = SHA256.Create())
        {
            originalHash = sha.ComputeHash(stream);
        }

        // Modify color properties after the first save
        using (var generator = new BarcodeGenerator(EncodeTypes.Code128))
        {
            generator.CodeText = "Demo123";
            generator.Parameters.BackColor = Color.Yellow;
            generator.Parameters.Barcode.BarColor = Color.Red;

            // Save second image
            generator.Save(file2, BarCodeImageFormat.Png);
        }

        // Re‑compute hash of the first file to verify it hasn't changed
        byte[] newHash;
        using (var stream = File.OpenRead(file1))
        using (var sha = SHA256.Create())
        {
            newHash = sha.ComputeHash(stream);
        }

        bool unchanged = true;
        if (originalHash.Length != newHash.Length) unchanged = false;
        else
        {
            for (int i = 0; i < originalHash.Length; i++)
            {
                if (originalHash[i] != newHash[i])
                {
                    unchanged = false;
                    break;
                }
            }
        }

        Console.WriteLine(unchanged
            ? "First saved image remained unchanged after modifying colors."
            : "First saved image was altered, which should not happen.");

        // Optional: load both images to demonstrate visual difference (no verification needed)
        using (var img1 = (Bitmap)Image.FromFile(file1))
        using (var img2 = (Bitmap)Image.FromFile(file2))
        {
            Console.WriteLine($"First image size: {img1.Width}x{img1.Height}");
            Console.WriteLine($"Second image size: {img2.Width}x{img2.Height}");
        }
    }
}