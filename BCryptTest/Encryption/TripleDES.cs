using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Security.Cryptography;
using System.IO;
using log4net;
using System.Reflection;
using TicketmasterSystems.Conversions;

namespace TicketmasterSystems.Encryption
{
	/// <summary>
	/// Encrypt and decrypt data using TripleDES.
	/// </summary>
	public class TripleDES
	{
		private static ILog log = LogManager.GetLogger(MethodBase.GetCurrentMethod().DeclaringType);

		// NOTE: The encrypt and decrypt routines are transcribed from VMNET.dll and converted into appropriate OOP for etix/SSETI

		public TripleDES(byte[] iv, byte[] key)
		{
			this.iv = iv;
			this.key = key;
		}

		protected byte[] iv = null;
		protected byte[] key = null;

		/// <summary>
		/// Encrypt data using TripleDES
		/// </summary>
		/// <param name="plainText"></param>
		/// <returns></returns>
		public byte[] Encrypt(string plainText)
		{
			// Declare a UTF8Encoding object so we may use the GetByte 
			// method to transform the plainText into a byte array
			UTF8Encoding utf8encoder = new UTF8Encoding();
			byte[] inputinbytes = utf8encoder.GetBytes(plainText);

			// Create a new TripleDES service provider
			TripleDESCryptoServiceProvider tdesprovider = new TripleDESCryptoServiceProvider();

			// The ICryptTransform interface uses the TripleDES
			// crypt provider along with encryption key and init vector information
			ICryptoTransform cryptotransform = tdesprovider.CreateEncryptor(key, iv);

			// All cryptographic functions need a stream to output the
			// encrypted information. Here we declare a memory stream
			// for this purpose
			byte[] result;
			using(MemoryStream encryptedstream = new MemoryStream())
			using(CryptoStream cryptstream = new CryptoStream(encryptedstream, cryptotransform, CryptoStreamMode.Write))
			{
				// Write the encrypted information to the stream. Flush the information
				// when done to ensure everything is out of the buffer.
				cryptstream.Write(inputinbytes, 0, inputinbytes.Length);
				cryptstream.FlushFinalBlock();
				encryptedstream.Position = 0;

				// Read the stream back into a Byte array and return it to the calling
				// method.
				result = new byte[encryptedstream.Length];
				encryptedstream.Read(result, 0, Convert.ToInt32(encryptedstream.Length));
			}

			// Return the encrypted value in byte form
			return result;
		}

		/// <summary>
		/// Decrypt data using TripleDES
		/// </summary>
		/// <param name="inputInBytes"></param>
		/// <returns></returns>
		public string Decrypt(byte[] inputInBytes)
		{
			TripleDESCryptoServiceProvider tdesprovider = new TripleDESCryptoServiceProvider();

			// We must provide the encryption/decryption key along with the init vector.
			ICryptoTransform cryptotransform = tdesprovider.CreateDecryptor(key, iv);

			// Provide a memory stream to decrypt information into
			byte[] result;
			using(MemoryStream decryptedstream = new MemoryStream())
			using(CryptoStream cryptstream = new CryptoStream(decryptedstream, cryptotransform, CryptoStreamMode.Write))
			{
				cryptstream.Write(inputInBytes, 0, inputInBytes.Length);
				cryptstream.FlushFinalBlock();
				decryptedstream.Position = 0;

				// Read the memory stream and convert it back into a string
				result = new byte[decryptedstream.Length];
				decryptedstream.Read(result, 0, Convert.ToInt32(decryptedstream.Length));
			}

			// Return the dencrypted value in string form
			UTF8Encoding myutf = new UTF8Encoding();
			return myutf.GetString(result);
		}

		public static string DecryptDEK(byte[] inputinbytes, byte[] iv, byte[] key)
		{
			TripleDESCryptoServiceProvider tdesprovider = new TripleDESCryptoServiceProvider();

			// We must provide the encryption/decryption key along with the init vector
			ICryptoTransform cryptotransform = tdesprovider.CreateDecryptor(key, iv);

			// Provide a memory stream to decrypt information into
			byte[] result;
			using(MemoryStream decryptedstream = new MemoryStream())
			using(CryptoStream cryptstream = new CryptoStream(decryptedstream, cryptotransform, CryptoStreamMode.Write))
			{
				cryptstream.Write(inputinbytes, 0, inputinbytes.Length);
				cryptstream.FlushFinalBlock();
				decryptedstream.Position = 0;

				// Read the memory stream and convert it back into a string
				result = new byte[decryptedstream.Length];
				decryptedstream.Read(result, 0, Convert.ToInt32(decryptedstream.Length));
			}

			// Return the dencrypted value in string form
			UTF8Encoding myutf = new UTF8Encoding();
			return myutf.GetString(result);
		}
	}
}