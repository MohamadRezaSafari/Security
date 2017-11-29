using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Providers
{
    public class AES
    {              
        private readonly string Str = @"ib{n]`RnX|ao8Xf<-|skNz(Np+*2%,7si?XVT=FjkNV<%+#;Nk.dJoJ{4qquPG'|N04L.BSm/dY-VCFmV;Pi!gS0Mi]4oq#A%1$jA~ni.v6VD;]o8U\e6)6Gmc&-M(pL*}^4pTidg/DGkk2dmog@uvRc+GXm-Lrzl;%k~OK({Q(Hh2CcBm-9'5h*D~.(rIO|w7.,k!^kN1|6w'.gT!l{$RQ67np[`Kc)jEnR;cVJVTIA`M6GlH)T!p,N'esA]z7_2q~r--Zu][K`/eMj_Rp'6%<pf|Tl00M&AQPTEOg:%z|f:}-IjTxKz?;HN2mB0$+dk$ruS[G2O-M\#=FXRZSD.g-_@7$03l5J[`1B^8BcMLYkIz$UjSe$gSOEjS[+.l$A8IHpR^@}}[95`Lq8)Wk0a$Os4],Lx!7?PQPsXPJF14.-WH1Ab&z@RDq82AsRacw)7c(^VH)p51'YF@\\AeRy.z5|)0_@>,7W[<W'4-qjjk^1Ue(<dcaA1j!~0%F<oS4J)gRF:|h(/u,2-Uk*2Pq}t`x10`Ox?B(9n_p_JAfaNe|99iU*H!@rY4PENw=]zhp|1`ZN:20(~>bsfh8F8~:H-]?:8b1gL\MF-INkIVM2`kh\5!5gt!>8bfC9L%LUE)QsU^~<^R/m9/q'{|EbTAvS%gYQ!JIV3SD0z1ZET~'xDgOU.%Rl>urbH*4*Gi942O35=sVL>,F=y=,qvEWW/},|(x^>kM[1Z.E|(`f]:u;EK3P,ZD*,ff-E|?JyBzV:-@uv0}|g1_=G_0ZwT&m:!aMP}u$rv-,:T:BH$ow<M]*1<(ZHX.^DyUPF3-.I[(BE3}H+v#@*PkJ*a=''2]'CpWr1G7zfEN7XM+duS2c4eW*?3k;)FhDPQnFc:!~<Kt=.R`iOzmaoAPxJw<'uq-sb&nABtfRcb(Hu;Z>d!UmS'axgjWwLYJemh[$e<C\#OM{M`xY.b3W4}V_F'MZhxVHTZLRnweP4}9q8]-t52hZE+E*W";
        private readonly string _iv = "~^WF]7#$d^vmR[5n";
        private byte[] Key;
        private byte[] IV;
        Aes encryptor = Aes.Create();

        public AES()
        {
            SHA256 mySHA256 = SHA256Managed.Create();
            Key = mySHA256.ComputeHash(Encoding.ASCII.GetBytes(Str));
            IV = Encoding.ASCII.GetBytes(_iv);
        }

        public string EncryptString(string plainText)
        {
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = Key;
            encryptor.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesEncryptor = encryptor.CreateEncryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesEncryptor, CryptoStreamMode.Write);
            byte[] plainBytes = Encoding.ASCII.GetBytes(plainText);
            cryptoStream.Write(plainBytes, 0, plainBytes.Length);
            cryptoStream.FlushFinalBlock();
            byte[] cipherBytes = memoryStream.ToArray();
            memoryStream.Close();
            cryptoStream.Close();

            string cipherText = Convert.ToBase64String(cipherBytes, 0, cipherBytes.Length);
            return cipherText;
        }



        public string DecryptString(string cipherText)
        {
            encryptor.Mode = CipherMode.CBC;
            encryptor.Key = Key;
            encryptor.IV = IV;

            MemoryStream memoryStream = new MemoryStream();
            ICryptoTransform aesDecryptor = encryptor.CreateDecryptor();
            CryptoStream cryptoStream = new CryptoStream(memoryStream, aesDecryptor, CryptoStreamMode.Write);
            string plainText = String.Empty;
            try
            {
                byte[] cipherBytes = Convert.FromBase64String(cipherText);
                cryptoStream.Write(cipherBytes, 0, cipherBytes.Length);
                cryptoStream.FlushFinalBlock();
                byte[] plainBytes = memoryStream.ToArray();
                plainText = Encoding.ASCII.GetString(plainBytes, 0, plainBytes.Length);
            }
            finally
            {
                memoryStream.Close();
                cryptoStream.Close();
            }
            return plainText;
        }
    }
}
