using System;
using System.Text;

namespace TicketmasterSystems.Encryption
{
	/// <summary>
	/// Handles simple and only pseudo "encryption" and "decryption" of text. Used to obscure both account passwords and creditcard numbers.
	/// </summary>
	public static class PseudoEncryption
	{
		private static int[] letter = new int[60];

		/// <summary>
		/// Perform some first initialisation work that would normally be done everytime an encryption was
		/// </summary>
		static PseudoEncryption()
		{
			// Cache the "Encrypt" scrambling key
			for(int x = 0; x < 60; x++)
			{
				if((x % 2) == 1)
					letter[x] = -x % 3;
				else
					letter[x] = x % 3;
			}
		}

		/// <summary>
		/// Encrypt either a password or a creditcard number
		/// </summary>
		/// <param name="s"></param>
		/// <returns></returns>
		public static string Encrypt(string s)
		{
			if(s == null) return null;

			StringBuilder sb = new StringBuilder();
			for(int x = 0; x < s.Length; x++)
			{
				int chr = (int)s[x];
				sb.Append((char)(chr + letter[x + 1]));
			}
			string temp = sb.ToString();

			sb = new StringBuilder();
			int i = temp.Length - 1;
			int j = 0;
			while(i > j)
			{
				sb.Append(temp[i]);
				sb.Append(temp[j]);
				i--;
				j++;
			}
			if((temp.Length % 2) == 1) sb.Append(temp[i]);

			return sb.ToString();
		}

		public static string Decrypt(string s)
		{
			if(s == null) return null;

			StringBuilder sb = new StringBuilder();
			int lX = 2;
			int i = 2;
			while(sb.Length < s.Length)
			{
				sb.Append(s.Substring(lX - 1, 1));
				if(lX + 2 > s.Length && i > 0)
				{
					i = -2;
					lX = (s.Length % 2) == 1 ? lX + 1 : lX - 1;
				}
				else
					lX += i;
			}

			string temp = sb.ToString();
			sb = new StringBuilder();

			for(int x = 0; x < temp.Length; x++)
			{
				int chr = (int)temp[x];
				sb.Append((char)(chr - letter[x + 1]));
			}

			return sb.ToString();
		}
	}
}
