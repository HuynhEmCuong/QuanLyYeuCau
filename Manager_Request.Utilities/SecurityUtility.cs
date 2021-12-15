using System;
using System.Security.Cryptography;
using System.Text;

namespace Manager_Request.Ultilities
{
    public static class SecurityUtility
    {
        public static string ToMD5(this string value)
        {
            //Tạo MD5
            MD5 md5 = MD5.Create();
            //Chuyển kiểu chuổi thành kiểu byte
            byte[] inputBytes = Encoding.ASCII.GetBytes(value);
            //mã hóa chuỗi đã chuyển
            byte[] hashBytes = md5.ComputeHash(inputBytes);
            //tạo đối tượng StringBuilder (làm việc với kiểu dữ liệu lớn)
            StringBuilder stringBuilder = new StringBuilder();
            for (int i = 0; i < hashBytes.Length; i++)
            {
                stringBuilder.Append(hashBytes[i].ToString("X2"));
                //Nếu muốn giá trị là chữ thường, thay X2 thành x2
            }
            return stringBuilder.ToString();
        }

        public static string ToMD5(this string value, string salt)
        {
            string newValue = value + salt;
            return ToMD5(newValue);
        }

        public static string ToSHA256(this string value)
        {
            //Khai báo chuỗi chứa kết quả
            StringBuilder stringBuilder = new StringBuilder();
            //Khai báo đối tượng hỗ trợ băm theo hàm SHA256
            SHA256 hash = SHA256.Create();
            //Khai báo bảng mã sẽ sử dụng
            Encoding encoding = Encoding.UTF8;
            //Chuyển giá trị đầu vào thành mảng byte
            byte[] inputBytes = encoding.GetBytes(value);
            //Băm mảng byte đầu vào
            Byte[] result = hash.ComputeHash(inputBytes);
            //Thu hồi đối tượng băm
            hash.Dispose();
            //Xử lý kết quả
            foreach (Byte Byte in result)
                stringBuilder.Append(Byte.ToString("x2"));
            //Trả về kết quả
            return stringBuilder.ToString();
        }

        public static string ToSHA256(this string value, string salt)
        {
            string newValue = value + salt;
            return ToSHA256(newValue);
        }

        public static string ToSHA512(this string value)
        {
            //Khai báo chuỗi chứa kết quả
            StringBuilder stringBuilder = new StringBuilder();
            //Khai báo đối tượng hỗ trợ băm theo hàm SHA256
            SHA512 hash = SHA512.Create();
            //Khai báo bảng mã sẽ sử dụng
            Encoding encoding = Encoding.UTF8;
            //Chuyển giá trị đầu vào thành mảng byte
            byte[] inputBytes = encoding.GetBytes(value);
            //Băm mảng byte đầu vào
            Byte[] result = hash.ComputeHash(inputBytes);
            //Thu hồi đối tượng băm
            hash.Dispose();
            //Xử lý kết quả
            foreach (Byte Byte in result)
                stringBuilder.Append(Byte.ToString("x2"));
            //Trả về kết quả
            return stringBuilder.ToString();
        }

        public static string ToSHA512(this string value, string salt)
        {
            string newValue = value + salt;
            return ToSHA512(newValue);
        }

        public static string GetSHA512(string text)
        {
            System.Text.UnicodeEncoding UE = new System.Text.UnicodeEncoding();
            byte[] hashValue;
            byte[] message = UE.GetBytes(text);

            System.Security.Cryptography.SHA512Managed hashString = new System.Security.Cryptography.SHA512Managed();
            string hex = "";

            hashValue = hashString.ComputeHash(message);

            foreach (byte x in hashValue)
            {
                hex += String.Format("{0:x2}", x);
            }
            return hex;
        }

        public static string Encrypt(this string value,string salt = "ABC@123@2021")
        {

            byte[] data = UTF8Encoding.UTF8.GetBytes(value);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider() { 
                    Key=keys,
                    Mode=CipherMode.ECB,
                    Padding=PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateEncryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return Convert.ToBase64String(results, 0, results.Length);
                }
            }
        }

        public static string Decrypt(this string value, string salt = "ABC@123@2021")
        {
            byte[] data = Convert.FromBase64String(value);
            using (MD5CryptoServiceProvider md5 = new MD5CryptoServiceProvider())
            {
                byte[] keys = md5.ComputeHash(UTF8Encoding.UTF8.GetBytes(salt));
                using (TripleDESCryptoServiceProvider tripDes = new TripleDESCryptoServiceProvider()
                {
                    Key = keys,
                    Mode = CipherMode.ECB,
                    Padding = PaddingMode.PKCS7
                })
                {
                    ICryptoTransform transform = tripDes.CreateDecryptor();
                    byte[] results = transform.TransformFinalBlock(data, 0, data.Length);
                    return UTF8Encoding.UTF8.GetString(results);
                }
            }
        }

        public static string HashPassword(this string password)
        {
            return GetSHA512(password).Substring(0, 75).ToString();//Gen ra 75 ký tự
        }
    }
}