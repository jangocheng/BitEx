using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Framework.ThirdParty;
using BitEx.Model.Certification;

namespace BitEx.IGrain.Repository.Certification
{
    public interface ICertificationBankcardRepository
    {
        Task<bool> CreateAsync(CertificationBankcard certificationBankcard);
        Task<bool> ExistsCertBankAsync(string idcard, string realname, string bankcardnumber);
        Task<CertificationBankcard> GetCertificationBankcard(string idcard, string realname, string bankcardnumber);
    }
}
