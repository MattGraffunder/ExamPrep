using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ExamPrep.Attributes;
using System.Globalization;

namespace ExamPrep.Chapter_3
{
    [ChapterTestClass(3)]
    class Chapter3Tests
    {
        [ChapterTest(1, Description = "Parse with Formatting")]
        public static void Objective_1_Parsing()
        {
            ParseTesting.Parsing("1234.56", new CultureInfo("en-US"));
            ParseTesting.Parsing("$1,234.56", new CultureInfo("en-US"));
            ParseTesting.Parsing("$1,234.56", new CultureInfo("fr-FR"));
            ParseTesting.Parsing("1.234,56", new CultureInfo("de-DE"));
            ParseTesting.Parsing("1.234", new CultureInfo("de-DE"));
            ParseTesting.Parsing("1,234", new CultureInfo("de-DE"));
        }

        [ChapterTest(1, Description = "Regex Testing")]
        public static void Objective_1_Regex()
        {
            Console.WriteLine("{0} is a valid ZipCode: {1}", "55420", ParseTesting.RegexZipCodes("55420"));
            Console.WriteLine("{0} is a valid ZipCode: {1}", "55420", ParseTesting.RegexZipCodes("90210"));
            Console.WriteLine("{0} is a valid ZipCode: {1}", "05420", ParseTesting.RegexZipCodes("05420"));
            Console.WriteLine("{0} is a valid ZipCode: {1}", "420-AB", ParseTesting.RegexZipCodes("420-AB"));
            Console.WriteLine("{0} is a valid ZipCode: {1}", "55 129", ParseTesting.RegexZipCodes("55 129"));
            Console.WriteLine("{0} is a valid ZipCode: {1}", "5546Z", ParseTesting.RegexZipCodes("5546Z"));
            Console.WriteLine("{0} is a valid ZipCode: {1}", "6", ParseTesting.RegexZipCodes("6"));
        }

        [ChapterTest(2, Description = "Symmetric Algorithm Testing")]
        public static void Objective_2_Symmetric()
        {
            string phrase = "Ham is good!";
            SymmetricEncryption sym = new SymmetricEncryption();
                                    
            Console.WriteLine("Encrypting \"{0}\"", phrase);

            byte[] encrypted = sym.Encrypt(phrase);
            string b64Encrypted = Convert.ToBase64String(encrypted);

            Console.WriteLine("Encrypted to (Base64): {0}", b64Encrypted);

            string decrypted = sym.Decrypt(encrypted);

            Console.WriteLine("Decrypted back to \"{0}\"", decrypted);
        }

        [ChapterTest(2, Description = "Asymmetric Algorithm Testing")]
        public static void Objective_2_Asymmetric()
        {
            AsymmetricEncryption algo = new AsymmetricEncryption();

            string phrase = "Ham is good!";
            //string phraseB64 = Convert.ToBase64String(UnicodeEncoding.UTF8.GetBytes(phrase));
            string phraseB64 = Base64Converter.GetBase64StringFromString(phrase);
            byte[] phraseB64Bytes = Convert.FromBase64String(phraseB64);

            string xmlPrivateKey = algo.GetPrivateKey();
            string xmlPublicKey = algo.GetPublicKey();

            Console.WriteLine("Encrypting: {0} (\"{1}\")", phraseB64, phrase);

            byte[] encryptedWithPublicKey = algo.Encrypt(phraseB64Bytes, xmlPublicKey);
            string encryptedWithPublicKeyB64 = Convert.ToBase64String(encryptedWithPublicKey);

            Console.WriteLine();
            Console.WriteLine("Encrypted using Public Key to (Base64): {0}", encryptedWithPublicKeyB64);

            byte[] decryptedWithPrivateKey = algo.Decrypt(encryptedWithPublicKey, xmlPrivateKey);
            string decryptedWithPrivateKeyB64 = Convert.ToBase64String(decryptedWithPrivateKey);

            Console.WriteLine();
            Console.WriteLine("Decrypted using Private Key to (Base64): {0}", decryptedWithPrivateKeyB64);

            byte[] signedWithPrivateKey = algo.Sign(phraseB64Bytes, xmlPrivateKey);
            string signedWithPublicKeyB64 = Convert.ToBase64String(signedWithPrivateKey);

            Console.WriteLine();
            Console.WriteLine("Signed using Private Key (Base64): {0}", signedWithPublicKeyB64);

            bool verifiedWithPublicKey = algo.Verify(phraseB64Bytes, signedWithPrivateKey, xmlPublicKey);

            Console.WriteLine();
            Console.WriteLine("Verifed using Public Key to (Base64): {0}", verifiedWithPublicKey);
        }

        [ChapterTest(2, Description = "Hash Algorithm Testing")]
        public static void Objective_2_Hash()
        {
            CryptoHash hash = new CryptoHash();

            string phrase = "Ham Time!";

            string phraseB64 = Base64Converter.GetBase64StringFromString(phrase);
            byte[] phraseB64Bytes = Convert.FromBase64String(phraseB64);

            Console.WriteLine("Encrypting: {0} (\"{1}\")", phraseB64, phrase);

            byte[] hashed = hash.GetHash(phraseB64Bytes);
            string hashedString = Convert.ToBase64String(hashed);

            Console.WriteLine();
            Console.WriteLine("Hashed to (Base64): {0}", hashedString);
        }

        [ChapterTest(2, Description = "Cert Store Testing")]
        public static void Objective_2_Certs()
        {
            foreach (var cert in CertReader.GetCert())
            {
                Console.WriteLine("Cert {0}: Issued By: {1}, Valid from {2:d} to {3:d}", cert.FriendlyName, cert.Issuer, cert.NotBefore, cert.NotAfter);
            }
        }

        [ChapterTest(5, Description = "Tracing Test")]
        public static void Objective_5_Tracing()
        {
            Tracer.DoStuff(1, "Stuff");

            Tracer.DoStuff(9, "OtherStuff");

            Console.WriteLine("Check Output Window");
        }

        [ChapterTest(5, Description = "PerformanceCounters")]
        public static void Objective_5_Counters()
        {
            PerformanceCounters p = new PerformanceCounters();

            Console.WriteLine("Current Available Memory is: {0}", p.ReadMemory());

            Console.WriteLine();
            Console.WriteLine("Creating Counters if Needed");

            p.CreatePerformanceCounters();

            Console.WriteLine();
            Console.WriteLine("Updateing Performance Counters");

            p.IncrementPerformanceCounters();
        }
    }
}