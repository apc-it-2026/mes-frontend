using System.Net;
using System.Net.Security;
using System.Security.Cryptography.X509Certificates;

namespace CommanClassLib.Util
{
    /// <summary>
    ///     关闭验证服务器证书
    /// </summary>
    public class CloseValidateServerCertificate
    {
        /// <summary>
        ///     启用服务器验证
        /// </summary>
        public static void EnableValidateServerCertificate()
        {
            ServicePointManager.ServerCertificateValidationCallback = ValidateServerCertificate; //新增屏蔽证书验证
        }

        private static bool ValidateServerCertificate(object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors)
        {
            return true;
        }
    }
}