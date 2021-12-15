using System;

namespace Manager_Request.Ultilities
{
    public static class RandomUtility
    {
        public static int RandomNumber(int min, int max)
        {
            int seed = Guid.NewGuid().GetHashCode();
            Random item = new Random(seed);
            int result = item.Next(min, max);
            return result;
        }

        /// <summary>
        /// Tạo ra một chuỗi ngẫu nhiên với độ dài cho trước
        /// </summary>
        /// <param name="size">Kích thước của chuỗi </param>
        /// <param name="lowerCase">Nếu đúng, tạo ra chuỗi chữ thường</param>
        /// <returns>Random string</returns>
        public static string RandomString(int minLength, int maxLenngth, bool hasNumber = false, bool hasSymbol = false, bool hasUppercase = false)
        {
            //Nếu minLength,maxLength <0 và minLength > maxLength, thì trả về empty;
            if ((minLength <= 0 || maxLenngth <= 0) || minLength > maxLenngth)
            {
                return string.Empty;
            }

            //Tính chiều dài (Ngẫu nhiên, trong khoản min-max) của chuổi kết quả
            int length = RandomNumber(minLength, maxLenngth);

            //Khai báo chuổi chứa các kí tự có thể có
            string letters = "abcdefghijklmnopqrstuvwxyz";

            //Khai báo chuỗi chứa các kí tự số có thể có
            string numbers = "0123456789";

            //Khai báo chuỗi chứa các kí tự Ký hiệu có thể có
            string symbols = "!@#$%^&*()";

            //Khai báo chuỗi chứa các kí tự Chữ hoa có thể có
            string lettersUpers = letters.ToUpper();

            //Khai báo biến chứa kết quả là các ký tự ngẫu nhiên chọn được ,
            string result = string.Empty;

            //Khai báo chuỗi chứa các ksi tự tổng hợp có thể có;
            string allChars = letters;
            if (hasNumber)
            {
                allChars += numbers;
            }
            if (hasSymbol)
            {
                allChars += symbols;
            }
            if (hasUppercase)
            {
                allChars += lettersUpers;
            }

            //Tiến hành chạy vòng lặp

            //để chọn lần lượt  các ký tự , mỗi lần bốc 1 kí tự ngẫu nhiên trong chuỗi quy định
            for (int i = 0; i < length; i++)
            {
                //Chỉ định một vị trí ngẫu nhiên , nằm trong khoảng chiều dài của chuổi quy định
                int index = RandomNumber(0, allChars.Length - 1);

                //Lấy kí tự tại vị trí đã chỉ định
                char item = allChars[index];

                //Cộng dồn kí tự đã lấy vào kết quả
                result += item;
            }
            //Trả về kết quả
            return result;
        }

        public static string RandomID()
        {
            return Guid.NewGuid().ToString();
        }
    }
}