using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Providers
{
    public class Encryption
    {
        private readonly string EncryptionKey =
            @"1'QS[Y'?&D)*PK@6z+Myx-;K2/hA;f+1b0s;2zFcqGgmeQ>;K)H*zFcqGgmeQ>;K)H*d32r
            {MO#`!)x_f1$5=a63gYW.\`c;NAf(>^6I\iN/)@@2194870943-/#1,B/_HT=Q{=W)>%m5y,G
            3@C0=IQs~KlK&9^ED8<&I/z,qYx}j#x#}0E\6mW93Bs3/C*`.O@4M^;h;;X[<ddopIt$i/.3F
            q/V]hxiC0c4U@X)8b,$}!|.}42RC4>pw5*,}L5dxFh|^Z?+Y{&crt/oA%>>^~\pD[7X6345K<
            9dU>a0[IK#;$B6SJi27!H0v\E#{@;-y`$}@;q]s}|Bp#s)MhWHY!YfhG-V:f~?^kGhit%YM8F
            pkj%HG\R*hAL?nVJlRwXrH^.OIx#Q@fVR`3H`/hqj0^>)a:t$[\paXGZ-v=lAAoI.I-Uftc>c
            u^0$gV/gm'\!6\C&<pm5&5M~8cdmKNiH1@c&=D=lt3@P9PwF~l@;.dSczpkw,=.T,3\f3{]GD
            @L::e5(?PlEy]^~=BoH'fnsgk.S}G>k~N/B!Z]tf$AxlH6;[4rL{I7%l~LOO`l!kToT:.ohYN
            A`N7d9y(cz8Tv)4F4rwk1Pv?]je^}wtge](0&$p(&HlWS9L5.9G8PycMWK{q@9n`5gO]T.m(y
            ROrxaFZC9u3a8[GJA@yG~\gO14AjH.B5+i23f%Q'>hu'sb4kwln~2vPbB/Dhq>I{ZnBT]fz*R
            ![LIfco{KOyUp7sjBl621c07.zfl!C*b7B;#JEqi*|$zP&+CYE#a>J'NQ&cgr)t*h*PDhx2U~
            S7#_)p,=yxZbr'OO#5@XDNyRelXXNf@tJ&-E7U!=fElc9*J/;R8pyC@Ta#Xcyp|f!`s:c?R9`
            ULNon?&$~WgA9IvX]+B)TJ$tv|qt)ypo0!'k-#:+-wsI35J?im]6UrY-5I8oNr<X-?*?*5=m-
            :{Exq+'K~^Yq+x80a8Kyc.-boet\AsaLi8(A;-b?RKuJ\OB#X3)\M{hK!5mU)G)<9Y@1&TRQ#
            b0M|`7soX+[)(y%1s.N!h(U^h~AKvY$YGh~AKvY$YGh~AKvY$YGh~AKvY$YGh~AKvY$YG%^&*
            uhDnkLk5mLVqecbdof6kQigGPxkFKC8dOBNTDpmjrclhq6f8kZdXho7VpQCp1yCDlZ16jJsMr
            gU6JwWnUBDFt1HL0XjV5eZJipcBmkgYITNpffSpJSC813Gy2AwOn9pU4CAankcfyv3JF1Mdwm
            GXoDRtEFYYAXruL60S1CUY3gJN0iccXUUrssE0wOKiKqESaT0znpqi$$$$$$$$$$$$$$$$$$$
            2ejo9aH4Xp3yDg2bRXw4OtuHac5hhd9xs3OHv9cSCfUPjGTxnPP9MuiMnRuM3o0KQmb8VPoGM
            8dZeCzmk5zF0EFbbdh1lf9kuhITMTN1noiXP7xVY2vtA8tCrOavs7RwQ7dLCNLVDagqGw16kg
            ovNYJCmHy2Oes8A93Q1cRhOQxaL0vCwMEalOqRXAh4MAYE0COVXkLcilF6HfAfy9vndD9OrfH
            Rcit3TuyvmqlXLaplwekhSB9gXRTnL7Ya7PEzZEEfywmMbdG4EAScDVytu6epOAnkkJMNsVmt
            F7MC3DQukvoiPbm1EXamoCWioqEYY2jzgFDNQMoVICFSoKHMHX4Ja3r7tz2ilG6lFpZ71t0oi
            HwftRfSsZlagx1XNNgLmquNeYb3rhW48CJ8@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@@#
            ";


        public string Encrypt(string clearText)
        {
            byte[] clearBytes = Encoding.Unicode.GetBytes(clearText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateEncryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(clearBytes, 0, clearBytes.Length);
                        cs.Close();
                    }
                    clearText = Convert.ToBase64String(ms.ToArray());
                }
            }
            return clearText;
        }


        public string Decrypt(string cipherText)
        {
            byte[] cipherBytes = Convert.FromBase64String(cipherText);
            using (Aes encryptor = Aes.Create())
            {
                Rfc2898DeriveBytes pdb = new Rfc2898DeriveBytes(EncryptionKey, new byte[] { 0x49, 0x76, 0x61, 0x6e, 0x20, 0x4d, 0x65, 0x64, 0x76, 0x65, 0x64, 0x65, 0x76 });
                encryptor.Key = pdb.GetBytes(32);
                encryptor.IV = pdb.GetBytes(16);
                using (MemoryStream ms = new MemoryStream())
                {
                    using (CryptoStream cs = new CryptoStream(ms, encryptor.CreateDecryptor(), CryptoStreamMode.Write))
                    {
                        cs.Write(cipherBytes, 0, cipherBytes.Length);
                        cs.Close();
                    }
                    cipherText = Encoding.Unicode.GetString(ms.ToArray());
                }
            }
            return cipherText;
        }
    }
}
