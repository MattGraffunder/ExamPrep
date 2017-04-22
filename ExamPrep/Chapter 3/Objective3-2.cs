using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Security.Cryptography;
using System.Security.Cryptography.X509Certificates;
using System.Text;
using System.Threading.Tasks;

namespace ExamPrep.Chapter_3
{
    class SymmetricEncryption
    {
        private readonly byte[] _key;
        private readonly byte[] _iv;

        public SymmetricEncryption()
        {
            using (SymmetricAlgorithm s = GetAlgo())
            {
                _key = s.Key;
                _iv = s.IV;
            }
        }

        public SymmetricEncryption(byte[] key, byte[] iv)
        {
            _key = key;
            _iv = iv;
        }

        public byte[] Encrypt(string text)
        {
            using (SymmetricAlgorithm s = GetAlgo())
            {
                s.Key = _key;
                s.IV = _iv;

                ICryptoTransform crypto = s.CreateEncryptor();

                using (MemoryStream m = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(m, crypto, CryptoStreamMode.Write))
                    {
                        using (StreamWriter writer = new StreamWriter(cs))
                        {
                            writer.WriteLine(text);
                            writer.Flush();
                        }

                        return m.ToArray();                        
                    }
                }
            }
        }

        public string Decrypt(byte[] encrypted)
        {
            using (SymmetricAlgorithm s = GetAlgo())
            {
                s.Key = _key;
                s.IV = _iv;

                ICryptoTransform crypto = s.CreateDecryptor();

                using (MemoryStream m = new MemoryStream(encrypted))
                {
                    using (CryptoStream cs = new CryptoStream(m, crypto, CryptoStreamMode.Read))
                    {
                        using (StreamReader r = new StreamReader(cs))
                        {
                            return r.ReadLine();
                            //return r.ReadToEnd(); // Adds newline character
                        }
                    }
                }
            }
        }

        private SymmetricAlgorithm GetAlgo()
        {
            SymmetricAlgorithm algo = new AesManaged();

            return algo;
        }
    }

    class AsymmetricEncryption
    {
        private readonly RSACryptoServiceProvider _rsa;

        public AsymmetricEncryption()
        {
            CspParameters parameters = new CspParameters() { KeyContainerName = "HamContainer" };
            _rsa = new RSACryptoServiceProvider(parameters);
        }

        //Gen Keys

        public string GetPublicKey()
        {
            return _rsa.ToXmlString(false);
        }

        public string GetPrivateKey()
        {
            return _rsa.ToXmlString(true);
        }

        //Encrypt and Decrypt

        public byte[] Encrypt(byte[] data, string xmlKey)
        {
            _rsa.FromXmlString(xmlKey);
            return _rsa.Encrypt(data, false);
        }

        public byte[] Decrypt(byte[] data, string xmlKey)
        {
            _rsa.FromXmlString(xmlKey);
            return _rsa.Decrypt(data, false);
        }

        public byte[] Sign(byte[] data, string xmlKey)
        {
            _rsa.FromXmlString(xmlKey);
            return _rsa.SignData(data, new SHA1CryptoServiceProvider());
        }

        public bool Verify(byte[] data, byte[] sig, string xmlKey)
        {
            _rsa.FromXmlString(xmlKey);
            return _rsa.VerifyData(data, new SHA1CryptoServiceProvider(), sig);
        }

        //Secure Container
    }

    class CryptoHash
    {
        HashAlgorithm _hash;
        
        public CryptoHash()
        {
            _hash = SHA256CryptoServiceProvider.Create();
        }

        public byte[] GetHash(byte[] data)
        {
            return _hash.ComputeHash(data);
        }
    }

    class CertReader
    {
        public static X509Certificate2Collection GetCert()
        {
            X509Store myCertStore = new X509Store(StoreName.Root, StoreLocation.CurrentUser);
            myCertStore.Open(OpenFlags.ReadOnly);

            return myCertStore.Certificates;
        }
    }

    class SecureStringTester
    {
        public void SecureStringTest()
        {
            SecureString ss = new SecureString();
            //ss.
        }
    }

    public static class Base64Converter
    {
        public static string GetBase64StringFromString(string phrase)
        {
            using (MemoryStream m = new MemoryStream())
            {
                using (StreamWriter s = new StreamWriter(m))
                {
                    s.WriteLine(phrase);
                }

                return Convert.ToBase64String(m.ToArray());
            }
        }
    }
}