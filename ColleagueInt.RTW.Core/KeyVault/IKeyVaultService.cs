
namespace ColleagueInt.RTW.Core
{
	using System;
	using System.Collections.Generic;
	using System.Threading.Tasks;

	public interface IKeyVaultService
	{
		public Task<string> GetSecretValue(string secretKey);

		public Task<string> GetCertificate(string secretName);
	}
}
