using System;
using Aspose.BarCode;
using Aspose.BarCode.Generation;

class Program
{
    static void Main()
    {
        // Sample non‑customer data to encode
        string nonCustomerData = "ABC123 XYZ";

        // Validate that the data conforms to the C40 character set
        ValidateC40(nonCustomerData);

        // Generate a DataMatrix barcode with the validated data
        using (BarcodeGenerator generator = new BarcodeGenerator(EncodeTypes.DataMatrix, nonCustomerData))
        {
            generator.Save("datamatrix.png");
        }

        Console.WriteLine("Barcode generated successfully.");
    }

    // Throws ArgumentException if any character is not allowed in C40
    static void ValidateC40(string text)
    {
        foreach (char ch in text)
        {
            if (!IsC40Char(ch))
                throw new ArgumentException($"Character '{ch}' is not allowed in the C40 character set.");
        }
    }

    // C40 allowed characters: uppercase A‑Z, digits 0‑9, space, and common punctuation
    static bool IsC40Char(char ch)
    {
        if (ch == ' ') return true;
        if (ch >= '0' && ch <= '9') return true;
        if (ch >= 'A' && ch <= 'Z') return true;

        // Additional punctuation permitted in C40
        const string extra = "!\"%&'()*+,-./:;=?";
        return extra.IndexOf(ch) >= 0;
    }
}