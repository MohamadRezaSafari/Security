using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Web;

namespace Providers
{
    public class AES_Basic
    {
        private readonly string Str = @"%oxA>S+m{?P4Fk[~zJidw,q}VoG37{{~DIk[IjK@ygw,`Z=>/0?W1VGzE6V(W,k13SH62B}rz%:&l1?8qr^n,-gt-d)WN1nO!%YJQoF[:Hepd<CznpZvue7)pGd8)T%EcV+vkE*wS97|%[|er,t\(h[rOf5j5rgu,cFS$gCv!t%6QH)0J7TVcb#B+$a5j=Jl/_<$!mb^+oWu<o=4?\#3ecJm;qtFtV?T{D/eVZ5QK)d,RbhDrJ`)TUnz$+]sPNgHx}+gPYqKw|Rs{%p`rI?uhKhyjKly|8ic3?2~0kVtgbyd9Hi-YSyxFsaytEs|*CXg-_l9xVru%N[sl^c'Oko9l6{S1G-|e[V+HJZ2wa|DnMUZZ@a-@!\&`Us`HgKo^k[R,#R8g`W[;+'chpqVV[#K`1G8m=]]?9-%RVm=FON5q<=TG?:,;E)N%yF/.s{C,#(RVGMe;H+oh`3qh]ty(yN+3w\JOCIehX}5V^GR`PMPFY2KTLnMhUh}GF[@HS-!ofm_[:Wx|q_XA{F]Kl*l/ueXi`^Q.9j+qa^#qGP)i1$sP1\IQfm]}kKZg|Srw_%iG-{IdNo1|9G3hl</QP_u/pF@;c~oRiJby__ZC06V$,6gdS#&cuFBlfB-1G>.5=&[a1'5NS,gf'vV6wY(3AH`jp$TeIj:SjvdJ&2pZ3|oUWI0i]J4Vkfy\{4qM6P9*9]IV|7WT[x>sql06>9zY8.G*Pyv?/-{M3^c_ktj<&nkt7&\kW_s!8*]`F1{XZ3Tf5]-#uv3&Y(K}bFBWdtjs!jc2Pbz09|pqpugj*{')icGo2nG[wKI}IN.BIq,o@Q1?^gZ,(cy'~W4KD/>Mp;{qW)lThk/>k!'5Vjno?|~[@OkLC42V?<&hp/Z'z$j=aVh^\m{FmO4[[TIOuO?hJ&\f-8DDw3)`Cu#ZDKEg{?Au?w<t:`=\MDPat/-wHab%Z~'[PQ*zVDe9>kA|1n.E0fCY/S9Wxso0[OzB)twDz(cF?zZ<bO%";
        private byte[] Key;
        static Aes encryptor = Aes.Create();


        public AES_Basic()
        {
            MD5 _md5 = MD5.Create();
            Key = _md5.ComputeHash(Encoding.ASCII.GetBytes(Str));
        }


        public string EncryptString(string plainText)
        {
            encryptor.Mode = CipherMode.CFB;
            encryptor.Key = Key;

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
            encryptor.Mode = CipherMode.CFB;
            encryptor.Key = Key;

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
