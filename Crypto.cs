using Nancy.Json;
using SpiderMusic.model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;

namespace SpiderMusic
{
    /// <summary>
    /// 弃用
    /// </summary>
    class Crypto
    {
        public string iv = "0102030405060708";
        public string presetKey = "0CoJUm6Qyw8W8jud";
        public string linuxapiKey = "rFgB&h#%2?^eDg:Q";
        public string base62 = "abcdefghijklmnopqrstuvwxyzABCDEFGHIJKLMNOPQRSTUVWXYZ0123456789";
        public string publicKey = "-----BEGIN PUBLIC KEY-----\nMIGfMA0GCSqGSIb3DQEBAQUAA4GNADCBiQKBgQDgtQn2JZ34ZC28NWYpAUd98iZ37BUrX/aKzmFbt7clFSs6sXqHauqKWqdtLkF2KexO40H1YTX8z2lSgBBOAxLsvaklV8k4cBFK9snQXE9/DDaFt6Rr7iVZMldczhC0JNgTz+SHXT6CBHuX3e9SdB1Ua44oncaTWz7OBGLbCiK45wIDAQAB\n-----END PUBLIC KEY-----";
        public string eapiKey = "e82ckenh8dichen8";

        public returnValue Weapi(string s)
        {
            var phone=new phone {
                Phone = "13201661424",
                Password = "asdliuyang123",
                Countrycode="86",
                RememberLogin = "true"
            };
            JavaScriptSerializer json = new JavaScriptSerializer();
            string phonejson=json.Serialize(phone);
            var n = new Random().Next(10, 26);
            //var secretKey = base62.Substring(n, n + 16);
            //第一次
            string aesEncrypt = AesEncrypt(phonejson, presetKey, iv);
            //第二次
            aesEncrypt = AesEncrypt(aesEncrypt, "FFFFFFFFFFFFFFFF", iv);


            return new returnValue { Params = aesEncrypt, encSecKey = "" };
        }
        public string AesEncrypt(string phonejson, string presetKey, string iv)
        {
            var pad = 16 - (phonejson.Length % 16);
            Random suiji = new Random();
            int j = suiji.Next(0, 256);
            phonejson = phonejson + pad * j;


            Byte[] plainBytes = Encoding.UTF8.GetBytes(phonejson);
            //用指定的密钥和初始化向量创建CBC模式的DES加密标准
            AesCryptoServiceProvider aesAlg = new AesCryptoServiceProvider();
            aesAlg.Mode = CipherMode.CBC;
            aesAlg.Key = Encoding.UTF8.GetBytes(presetKey);
            aesAlg.IV = Encoding.UTF8.GetBytes(iv);

            ICryptoTransform encryptor = aesAlg.CreateEncryptor(aesAlg.Key, aesAlg.IV);
            // Create the streams used for encryption.
            using (MemoryStream msEncrypt = new MemoryStream())
            {
                using (CryptoStream csEncrypt = new CryptoStream(msEncrypt, encryptor, CryptoStreamMode.Write))
                {
                    using (StreamWriter swEncrypt = new StreamWriter(csEncrypt))
                    {
                        //Write all data to the stream.
                        swEncrypt.Write(phonejson);
                    }
                    byte[] bytes = msEncrypt.ToArray();
                    var ddc = Convert.ToBase64String(bytes);
                    return ddc;
                }
            }

        }
        //public string RsaEncrypt(string presetKey,string publicKey) 
        //{
        //    presetKey=presetKey.Reverse().ToString();
        //    byte[] myByteArray = Enumerable.Repeat((byte)00, 128- presetKey.Length).ToArray();
            
        //}

    }
}
