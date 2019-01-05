using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ContactsManager.Constant
{
    public class ContactsManagerConstant
    {
        public const string ContactsManagerAPI = "http://localhost:54427/";
        public const string ContactsManagerWebClient = "http://localhost:62482/";

        public const string IdSrv = "https://localhost:44363/Identity";
        public const string IdSrvToken = IdSrv + "/connect/token";
        public const string IdSrvAuthorize = IdSrv + "/connect/authorize";
        public const string IdSrvUserInfo = IdSrv + "/connect/userinfo";
        public const string IdSrvIssuerUri = "https://localhost:44363/Embedded";
    }
}
