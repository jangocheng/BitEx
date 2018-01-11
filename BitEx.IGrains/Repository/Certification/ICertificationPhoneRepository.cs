using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Coin.Model.Certification;

namespace BitEx.IGrain.Repository.Certification
{
    public interface ICertificationPhoneRepository
    {
        Task<bool> CreateAsync(CertificationPhone certificationPhone);
        Task<bool> ExistsCertPhoneAsync(string idcard, string realname, string phone);
        Task<CertificationPhone> GetCertificationPhoneAsync(string idcard, string realname, string phone);
    }
}
