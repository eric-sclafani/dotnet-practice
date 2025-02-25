namespace JobSearch.Utils;

using Microsoft.AspNetCore.Cryptography.KeyDerivation;
using System.Security.Cryptography;

public static class PasswordHelper
{
	public static string HashPassword(string password)
	{
		// A salt is a random value added to each password before hashing.
		// This prevents attackers from using precomputed hash databases
		var salt = new byte[16];
		using (var rng = RandomNumberGenerator.Create())
		{
			rng.GetBytes(salt);
		}

		// ASP.NET Core uses PBKDF2 (Password-Based Key Derivation Function 2),
		// which applies hashing multiple times (key stretching) to slow down brute-force attacks.
		var hash = KeyDerivation.Pbkdf2(
			password: password,
			salt: salt,
			prf: KeyDerivationPrf.HMACSHA256,
			iterationCount: 10000,
			numBytesRequested: 32
		);

		return Convert.ToBase64String(salt) + ":" + Convert.ToBase64String(hash);
	}

	public static bool VerifyPassword(string password, string storedHash)
	{
		var parts = storedHash.Split(':');
		var salt = Convert.FromBase64String(parts[0]);
		var hash = Convert.FromBase64String(parts[1]);

		var testHash = KeyDerivation.Pbkdf2(
			password: password,
			salt: salt,
			prf: KeyDerivationPrf.HMACSHA256,
			iterationCount: 10000,
			numBytesRequested: 32
		);

		return testHash.SequenceEqual(hash);
	}
}