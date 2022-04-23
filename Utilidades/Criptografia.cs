using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Utilidades
{
    public static class Criptografia
    {
        /// <summary>
        /// O método estático <c>setChave(string chave)</c> é responsável por alterar a chave que será usada no algoritmo de criptografia.
        /// </summary>
        /// <param name="chave">
        /// String contendo a nova chave de criptografia.
        /// </param>
        private static string StrChave
        {
            get
            {
                return "Cr1pt0gr4f14";
            }
        }
        private static string StrIV
        {
            get
            {
                return "qwrwqe7qv4f6sad54r8wqe4fsda4v8rfwe4f";
            }
        }

        /// <summary>
        /// Método para Criptografia em 3Des
        /// </summary>
        public static string CriptografarTripleDES(string texto, string senha)
        {

            byte[] results; UTF8Encoding UTF8 = new UTF8Encoding();
            // Passo 1. Calculamos o hash da senha usando MD5
            // Usamos o gerador de hash MD5 como o resultado é um array de bytes de 128 bits
            // que é um comprimento válido para o codificador TripleDES usado abaixo
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tDESKey = hashProvider.ComputeHash(UTF8.GetBytes(senha));

            // Passo 2. Cria um objeto new TripleDESCryptoServiceProvider
            TripleDESCryptoServiceProvider tDESAlgorithm = new TripleDESCryptoServiceProvider();

            // Passo 3. Configuração do codificador
            tDESAlgorithm.Key = tDESKey;
            tDESAlgorithm.Mode = CipherMode.ECB;
            tDESAlgorithm.Padding = PaddingMode.PKCS7;

            // Passo 4. Converta a seqüência de entrada para um byte []
            byte[] dataToEncrypt = UTF8.GetBytes(texto);
            // Passo 5. Tentativa para criptografar a seqüência de caracteres
            try
            {
                ICryptoTransform encryptor = tDESAlgorithm.CreateEncryptor();
                results = encryptor.TransformFinalBlock(dataToEncrypt, 0, dataToEncrypt.Length);
            }
            finally
            {
                // Limpe as tripleDES e serviços hashProvider de qualquer informação sensível
                tDESAlgorithm.Clear();
                hashProvider.Clear();
            }
            // Passo 6. Volte a seqüência criptografada como uma string base64 codificada
            return Convert.ToBase64String(results);
        }

        /// <summary>
        /// Método para Descriptografia em 3Des
        /// </summary>
        public static string DescriptografarTripleDES(string texto, string senha)
        {
            byte[] results;
            UTF8Encoding UTF8 = new UTF8Encoding();

            // Passo 1. Calculamos o hash da senha usando MD5
            // Usamos o gerador de hash MD5 como o resultado é um array de bytes de 128 bits
            // que é um comprimento válido para o codificador TripleDES usado abaixo
            MD5CryptoServiceProvider hashProvider = new MD5CryptoServiceProvider();
            byte[] tDESKey = hashProvider.ComputeHash(UTF8.GetBytes(senha));

            // Passo 2. Cria um objeto new TripleDESCryptoServiceProvider 
            var tDESAlgorithm = new TripleDESCryptoServiceProvider();
            // Passo 3. Configuração do codificador
            tDESAlgorithm.Key = tDESKey;
            tDESAlgorithm.Mode = CipherMode.ECB;
            tDESAlgorithm.Padding = PaddingMode.PKCS7;
            // Passo 4. Converta a seqüência de entrada para um byte []
            byte[] dataToDecrypt = Convert.FromBase64String(texto);
            // Passo 5. Tentativa para criptografar a seqüência de caracteres
            try
            {
                ICryptoTransform Decryptor = tDESAlgorithm.CreateDecryptor();
                results = Decryptor.TransformFinalBlock(dataToDecrypt, 0, dataToDecrypt.Length);
            }
            finally
            {
                // Limpe as tripleDES e serviços hashProvider de qualquer informação sensível
                tDESAlgorithm.Clear();
                hashProvider.Clear();
            }

            // Passo 6. Volte a seqüência criptografada como uma string base64 codificada 
            return UTF8.GetString(results);
        }

        /// <summary>
        /// Método para Criptografia em 3Des
        /// </summary>
        public static string CriptografarTripleDES(string texto)
        {
            return CriptografarTripleDES(texto, StrChave);
        }

        /// <summary>
        /// Método para Descriptografia em 3Des
        /// </summary>
        public static string DescriptografarTripleDES(string texto)
        {
            return DescriptografarTripleDES(texto, StrChave);
        }

        /// <summary>
        /// Método para Criptografia em AES
        /// </summary>
        public static string CriptografarAES(string texto)
        {
            return CriptografarAES(texto, (StrChave + StrChave + StrChave).Substring(0, 32), StrIV, 256);
        }

        /// <summary>
        /// Método para Criptografia em AES
        /// </summary>
        public static string DescriptografarAES(string texto)
        {
            return DescriptografarAES(texto, (StrChave + StrChave + StrChave).Substring(0, 32), StrIV, 256);
        }

        /// <summary>
        /// Método para Criptografia em AES
        /// </summary>
        public static string CriptografarAES(string texto, string Key, string IV, int keySize = 128)
        {
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            MemoryStream msEncrypt = null;
            CryptoStream csEncrypt = null;
            StreamWriter swEncrypt = null;

            AesManaged AesManagedAlg = null;

            try
            {
                AesManagedAlg = new AesManaged();
                AesManagedAlg.KeySize = keySize;
                if (Key.Length < (keySize / 8))
                {
                    throw new ArgumentNullException("Key");
                }
                byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(Key.Substring(0, keySize / 8));
                AesManagedAlg.Key = keyBytes;
                IV = ajustaIV(IV);
                if (IV.Length < (AesManagedAlg.BlockSize / 8))
                {
                    throw new ArgumentNullException("Key");
                }

                byte[] ivBytes = Convert.FromBase64String(IV);
                AesManagedAlg.IV = ivBytes.Take(AesManagedAlg.BlockSize / 8).ToArray();

                ICryptoTransform encryptor = AesManagedAlg.CreateEncryptor(AesManagedAlg.Key, AesManagedAlg.IV);

                msEncrypt = new MemoryStream();
                csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write);
                swEncrypt = new StreamWriter(csEncrypt);

                swEncrypt.Write(texto);
            }
            finally
            {
                if (swEncrypt != null)
                    swEncrypt.Close();
                if (csEncrypt != null)
                    csEncrypt.Close();
                if (msEncrypt != null)
                    msEncrypt.Close();
                if (AesManagedAlg != null)
                    AesManagedAlg.Clear();
            }

            return Convert.ToBase64String(msEncrypt.ToArray());
        }

        /// <summary>
        /// Método para Criptografia em AES
        /// </summary>
        public static string DescriptografarAES(string texto, string Key, string IV, int keySize = 128)
        {
            if (Key == null || Key.Length <= 0)
                throw new ArgumentNullException("Key");
            if (IV == null || IV.Length <= 0)
                throw new ArgumentNullException("Key");

            MemoryStream msDecrypt = null;
            CryptoStream csDecrypt = null;
            StreamReader srDecrypt = null;

            AesManaged AesManagedAlg = null;

            string plaintext = null;

            try
            {
                AesManagedAlg = new AesManaged();
                AesManagedAlg.KeySize = keySize;
                if (Key.Length < (keySize / 8))
                {
                    throw new ArgumentNullException("Key");
                }
                byte[] keyBytes = UTF8Encoding.UTF8.GetBytes(Key.Substring(0, keySize / 8));
                AesManagedAlg.Key = keyBytes;
                IV = ajustaIV(IV);
                if (IV.Length < (AesManagedAlg.BlockSize / 8))
                {
                    throw new ArgumentNullException("Key");
                }

                byte[] ivBytes = Convert.FromBase64String(IV);
                AesManagedAlg.IV = ivBytes.Take(AesManagedAlg.BlockSize / 8).ToArray();

                ICryptoTransform decryptor = AesManagedAlg.CreateDecryptor(AesManagedAlg.Key, AesManagedAlg.IV);

                msDecrypt = new MemoryStream(Convert.FromBase64String(texto));
                csDecrypt = new CryptoStream(msDecrypt, decryptor, CryptoStreamMode.Read);
                srDecrypt = new StreamReader(csDecrypt);

                plaintext = srDecrypt.ReadToEnd();
            }
            finally
            {
                if (srDecrypt != null)
                    srDecrypt.Close();
                if (csDecrypt != null)
                    csDecrypt.Close();
                if (msDecrypt != null)
                    msDecrypt.Close();

                if (AesManagedAlg != null)
                    AesManagedAlg.Clear();
            }

            return plaintext;
        }

        /// <summary>
        /// Método para Criptografia em MD5
        /// </summary>
        public static string CriptografarMD5(string texto)
        {
            MD5 md5 = MD5.Create();
            byte[] inputBytes = Encoding.ASCII.GetBytes(texto);
            byte[] hash = md5.ComputeHash(inputBytes);
            StringBuilder sb = new StringBuilder();
            for (int i = 0; i < hash.Length; i++)
            {
                sb.Append(hash[i].ToString("X2"));
            }
            return sb.ToString();
        }

        /// <summary>
        /// Método para Criptografia em SHA256
        /// </summary>
        public static string CriptografarSHA256(string texto)
        {
            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(texto);
            SHA256Managed SHhash = new SHA256Managed();
            string strHex = "";

            HashValue = SHhash.ComputeHash(MessageBytes);
            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }
            return strHex;
        }

        /// <summary>
        /// Método para Criptografia em SHA1
        /// </summary>
        public static string CriptografarSHA1(string texto)
        {

            UnicodeEncoding UE = new UnicodeEncoding();
            byte[] HashValue, MessageBytes = UE.GetBytes(texto);
            SHA1Managed SHhash = new SHA1Managed();
            string strHex = "";
            HashValue = SHhash.ComputeHash(MessageBytes);

            foreach (byte b in HashValue)
            {
                strHex += String.Format("{0:x2}", b);
            }

            return strHex;
        }

        public static string GetStringSha256Hash(string texto)
        {
            if (String.IsNullOrEmpty(texto))
                return String.Empty;

            using (var sha = new SHA256Managed())
            {
                byte[] textData = System.Text.Encoding.UTF8.GetBytes(texto);
                byte[] hash = sha.ComputeHash(textData);
                return BitConverter.ToString(hash).Replace("-", String.Empty);
            }
        }

        public static void CriptografarArquivo(string arquivoOrigem, string arquivoDestino, string senha)
        {
            //generate random salt
            byte[] salt = GenerateRandomSalt();

            //create output file name
            //FileStream fsCrypt = new FileStream(inputFile + ".aes", FileMode.Create);
            FileStream fsCrypt = new FileStream(arquivoDestino, FileMode.Create);

            //convert password string to byte arrray
            byte[] passwordBytes = Encoding.UTF8.GetBytes(senha);

            //Set Rijndael symmetric encryption algorithm
            RijndaelManaged AES = new RijndaelManaged();
            AES.KeySize = 256;
            AES.BlockSize = 128;
            AES.Padding = PaddingMode.PKCS7;

            //http://stackoverflow.com/questions/2659214/why-do-i-need-to-use-the-rfc2898derivebytes-class-in-net-instead-of-directly
            //"What it does is repeatedly hash the user password along with the salt." High iteration counts.
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);

            //Cipher modes: http://security.stackexchange.com/questions/52665/which-is-the-best-cipher-mode-and-padding-mode-for-aes-encryption
            AES.Mode = CipherMode.CFB;

            // write salt to the begining of the output file, so in this case can be random every time
            fsCrypt.Write(salt, 0, salt.Length);

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateEncryptor(), CryptoStreamMode.Write);

            FileStream fsIn = new FileStream(arquivoOrigem, FileMode.Open);

            //create a buffer (1mb) so only this amount will allocate in the memory and not the whole file
            byte[] buffer = new byte[1048576];
            int read;

            try
            {
                while ((read = fsIn.Read(buffer, 0, buffer.Length)) > 0)
                {
                    cs.Write(buffer, 0, read);
                }

                // Close up
                fsIn.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }
            finally
            {
                cs.Close();
                fsCrypt.Close();
            }
        }

        public static void DescriptografarArquivo(string arquivoOrigem, string arquivoDestino, string senha)
        {
            byte[] passwordBytes = Encoding.UTF8.GetBytes(senha);
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(arquivoOrigem, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128
            };
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            FileStream fsOut = new FileStream(arquivoDestino, FileMode.Create);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                {
                    fsOut.Write(buffer, 0, read);
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                throw new CryptographicException("CryptographicException: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro para fechar o CryptoStream: " + ex.Message);
            }
            finally
            {
                fsOut.Close();
                fsCrypt.Close();
            }
        }

        public static byte[] DescriptografarArquivoArray(string arquivoOrigem, string senha)
        {
            byte[] retorno = null;
            byte[] passwordBytes = Encoding.UTF8.GetBytes(senha);
            byte[] salt = new byte[32];

            FileStream fsCrypt = new FileStream(arquivoOrigem, FileMode.Open);
            fsCrypt.Read(salt, 0, salt.Length);

            RijndaelManaged AES = new RijndaelManaged
            {
                KeySize = 256,
                BlockSize = 128
            };
            var key = new Rfc2898DeriveBytes(passwordBytes, salt, 50000);
            AES.Key = key.GetBytes(AES.KeySize / 8);
            AES.IV = key.GetBytes(AES.BlockSize / 8);
            AES.Padding = PaddingMode.PKCS7;
            AES.Mode = CipherMode.CFB;

            CryptoStream cs = new CryptoStream(fsCrypt, AES.CreateDecryptor(), CryptoStreamMode.Read);

            int read;
            byte[] buffer = new byte[1048576];

            try
            {
                using (var output = new MemoryStream())
                {
                    while ((read = cs.Read(buffer, 0, buffer.Length)) > 0)
                    {
                        output.Write(buffer, 0, read);
                        read = cs.Read(buffer, 0, buffer.Length);
                    }
                    cs.Flush();

                    retorno = output.ToArray();
                }
            }
            catch (CryptographicException ex_CryptographicException)
            {
                throw new CryptographicException("CryptographicException: " + ex_CryptographicException.Message);
            }
            catch (Exception ex)
            {
                throw new Exception("Erro: " + ex.Message);
            }

            try
            {
                cs.Close();
            }
            catch (Exception ex)
            {
                throw new Exception("Erro para fechar o CryptoStream: " + ex.Message);
            }
            finally
            {
                fsCrypt.Close();
            }

            return retorno;
        }

        public static void CriptografarArquivo(string arquivoOrigem, string arquivoDestino)
        {
            CriptografarArquivo(arquivoOrigem, arquivoDestino, StrChave);
        }

        public static void DescriptografarArquivo(string arquivoOrigem, string arquivoDestino)
        {
            DescriptografarArquivo(arquivoOrigem, arquivoDestino, StrChave);
        }

        public static byte[] DescriptografarArquivoArray(string arquivoOrigem)
        {
            return DescriptografarArquivoArray(arquivoOrigem, StrChave);
        }

        public static string GerarHash(object parametro)
        {
            string cripto = CriptografarTripleDES($"{parametro}");
            byte[] byteEnconding = Encoding.UTF8.GetBytes(cripto);
            string retorno = Convert.ToBase64String(byteEnconding);

            return retorno;
        }

        public static string DescriptografarHash(string hash)
        {
            var base64 = Convert.FromBase64String(hash);
            var tripleDes = Encoding.UTF8.GetString(base64);
            string retorno = DescriptografarTripleDES(tripleDes);

            return retorno;
        }

        private static string ajustaIV(string IV)
        {
            IV = IV.Replace(" ", "");
            var aux = IV.Length % 4;
            if (aux != 0)
            {
                for (int i = 0; i < 4 - aux; i++)
                {
                    IV += "+";
                }
            }

            return IV;
        }

        private static byte[] GenerateRandomSalt()
        {
            byte[] data = new byte[32];

            using (RNGCryptoServiceProvider rng = new RNGCryptoServiceProvider())
            {
                for (int i = 0; i < 10; i++)
                {
                    rng.GetBytes(data);
                }
            }

            return data;
        }
    }
}
