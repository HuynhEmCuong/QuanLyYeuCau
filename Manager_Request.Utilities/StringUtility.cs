using Manager_Request.Ultilities.Dtos;
using System;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Manager_Request.Ultilities
{
    public static class StringUtility
    {
        /// <summary>
        /// Nén khoảng trắng trong value.Thay hay hai hoặc nhiều khoảng trắng gần nhau thành 1 khoảng trắng trong value
        /// Trả về giá trị đã nén khoảng trắng
        /// </summary>
        /// <param name="value">Giá trị cần nén khoảng trắng</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng thừa 2 đầu không?
        /// true có false không
        /// </param>
        /// <returns>Trả về giá trị đã nén khoảng trắng</returns>
        public static string ToCompactSpaces(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = value.ToString();

            if (isTrim)
            {
                result = result.Trim();
            }
            // Cách 1:
            // do
            // {
            //     result = value.ToString().Replace("  ", " ");
            // } while (result.Contains("  "));

            // //Cach 2
            // while (result.Contains("  "))
            // {
            //     result = value.ToString().Replace("  ", " ");
            // }

            result = Regex.Replace(result, @"\s+", " ");

            return result;
        }

        /// <summary>
        /// Xóa tất cả khoảng trắng trong value.
        /// Trả về giá trị đã nén khoảng trắng
        /// </summary>
        /// <param name="value">Giá trị cần nén khoảng trắng</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng thừa 2 đầu không?
        /// true có false không
        /// </param>
        /// <returns>Trả về giá trị đã nén khoảng trắng</returns>
        public static string ToCompactAllSpaces(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = value.ToString();

            if (isTrim)
            {
                result = result.Trim();
            }

            result = Regex.Replace(result, @"\s+", "");

            return result;
        }

        /// <summary>
        /// Chuyển đổi value sang kiểu chữ thường.Trả về giá trị  kiểu chữ thường
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng 2 đầu không? True: có; false: không
        /// </param><returns>Trả về giá trị  kiểu chữ thường</returns>
        public static string ToLowerCase(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = value.ToString();
            if (isTrim)
                result = result.Trim();

            return result.ToLower();
        }

        /// <summary>
        /// Chuyển đổi value sang kiểu chữ hoa.Trả về giá trị  kiểu chữ hoa
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng 2 đầu không? True: có; false: không
        /// </param><returns>Trả về giá trị  kiểu chữ hoa</returns>
        public static string ToUpperCase(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = value.ToString();
            if (isTrim)
                result = result.Trim();

            return result.ToUpper();
        }

        /// <summary>
        /// Chuyển đổi value sang kiểu chữ tiêu đề(in hoa mỗi kí tự đầu tiên).
        /// Trả về giá trị  kiểu chữ tiêu đề(in hoa mỗi kí tự đầu tiên)
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng 2 đầu không? True: có; false: không
        /// </param><returns>Trả về giá trị  kiểu chữ tiêu đề(in hoa mỗi kí tự đầu tiên)</returns>
        public static string ToTitleCase(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = StringUtility.ToLowerCase(value);

            if (isTrim)
                result = result.Trim();

            TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            //string result = value.ToLowerCase(); ĐƯỢC NHƯNG KHÔNG AN TOÀN

            return textInfo.ToTitleCase(result);
        }

        /// <summary>
        /// In hoa ký tự đầu tiên
        /// </summary>
        /// <param name="value"></param>
        /// <param name="isTrim"></param>
        /// <returns></returns>
        public static string ToFistCase(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = StringUtility.ToLowerCase(value);

            if (isTrim)
                result = result.Trim();

            //TextInfo textInfo = CultureInfo.InvariantCulture.TextInfo;
            //string result = value.ToLowerCase(); ĐƯỢC NHƯNG KHÔNG AN TOÀN

            if (result.Length > 1)
                return result[0].ToUpperCase() + result.Substring(1);

            return result.ToUpperCase();
        }

        /// <summary>
        /// Chuyển đổi value thành không dấu
        /// trả về giá trị value không dấu
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng 2 đầu không? True: có; false: không
        /// </param>
        /// <returns>trả về giá trị value không dấu</returns>
        public static string ToNoSignFormat(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }
            string result = value.ToString();
            if (isTrim)
                result = result.Trim();
            //result = Regex.Replace(result, "[óòỏõọôốồổỗộơớờởỡợ]", "o");
            //result = Regex.Replace(result, "[óòỏõọôốồổỗộơớờởỡợ]".ToUpper(), "O");

            //Giúp bỏ dấu tiếng việt
            result = result.Normalize(NormalizationForm.FormD);
            result = Regex.Replace(result, "\\p{IsCombiningDiacriticalMarks}+", String.Empty);
            result = result.Replace('\u0111', 'd').Replace('\u0110', 'D');

            return result;
        }

        /// <summary>
        /// Chuyển đổi giá trị value thành dạng Url
        /// trả về giá trị dạng Url
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng 2 đầu không? True: có; false: không
        /// </param><returns>Giá trị cần chuyển đổi</returns>
        public static string ToUrlFormat(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = value.ToString().ToLower();

            result = ToNoSignFormat(result, isTrim);
            result = result.Replace(" ", "-");
            return result;
        }


        /// <summary>
        /// Chuyển đổi giá trị value thành dạng _file 
        /// trả về giá trị dạng _file
        /// </summary>
        /// <param name="value">Giá trị cần chuyển đổi</param>
        /// <param name="isTrim">Có tự động cắt khoảng trắng 2 đầu không? True: có; false: không
        /// </param><returns>Giá trị cần chuyển đổi</returns>
        public static string ToFileFormat(this object value, bool isTrim = false)
        {
            if (value == null)
            {
                return string.Empty;
            }

            string result = value.ToString().ToLower().ToNoSignFormat();

            result = ToNoSignFormat(result, isTrim);
            result = result.Replace(" ", "_");
            return result;
        }

        /// <summary>
        /// Trích lọc từ bên trái của value một số kí tự.
        /// Trả về chuỗi mới đã trích lọc từ bên trái
        /// </summary>
        /// <param name="value">Giá trị cần trích lọc</param>
        /// <param name="lenght">Số kí tự cần lấy</param>
        /// <param name="isTrim">Có Trim không? true: có; false: không</param>
        /// /// <param name="hasThreeDots">Có thêm ... vào cuối không? true: có; false: không</param>
        /// <returns>Trả về chuỗi mới đã trích lọc từ bên trái</returns>
        public static string Left(this object value, int lenght, bool isTrim = false, bool hasThreeDots = false)
        {
            if (value == null || lenght < 0)
                return string.Empty;

            string result = value.ToString();

            if (isTrim)
                result = result.Trim();

            //Xử lý chiều dài kí tự tối đa
            int currentLengt = result.Length;
            //Nếu chiều dài cần lấy mà lớn hơn chiều dài hiện tại thì cho chiều dài cần lấy bằng chiều dài hiện tại
            if (lenght > currentLengt)
                lenght = currentLengt;

            result = result.Substring(0, lenght);

            if (isTrim)
                result = result.Trim();

            if (hasThreeDots && currentLengt > result.Length)
                result += "...";

            return result;
        }

        /// <summary>
        /// Trích lọc từ bên phải của value một số kí tự.
        /// Trả về chuỗi mới đã trích lọc từ bên phải
        /// </summary>
        /// <param name="value">Giá trị cần trích lọc</param>
        /// <param name="lenght">Số kí tự cần lấy</param>
        /// <param name="isTrim">Có Trim không? true: có; false: không</param>
        /// <param name="hasThreeDots">Có thêm ... vào đầu không? true: có; false: không</param>
        /// <returns>Trả về chuỗi mới đã trích lọc từ bên phải</returns>
        public static string Right(this object value, int lenght, bool isTrim = false, bool hasThreeDots = false)
        {
            if (value == null)
                return string.Empty;

            string result = value.ToString();

            if (isTrim)
                result = result.Trim();

            int CurrentLenght = result.Length;

            if (lenght > CurrentLenght)
                lenght = CurrentLenght;

            result = result.Substring(CurrentLenght - lenght, lenght);

            if (isTrim)
                result = result.Trim();

            if (hasThreeDots && !(CurrentLenght == lenght))
                result = "..." + result;
            return result;
        }

        /// <summary>
        /// Cắt chuổi text một kí tự ngăn cách
        /// Trả về một đối tượng Text
        /// </summary>
        /// <param name="value"></param>
        /// <param name="keySplit"></param>
        /// <returns></returns>
        public static TextValue[] SplitToText(this object value, string keySplit)
        {
            if (value == null)
            {
                return null;
            }
            string obj = value.ToString().Trim();
            string[] resultSplit = obj.Split(char.Parse(keySplit))
                                .Where(x => x != null && x != string.Empty).ToArray();

            TextValue[] textValueStrings = new TextValue[resultSplit.Length];

            for (int i = 0; i < resultSplit.Length; i++)
            {
                textValueStrings[i] = new TextValue(resultSplit[i].Trim());
            }

            return textValueStrings;
        }

        /// <summary>
        /// Cắt chuổi text một 2 kí tự ngăn cách
        /// Trả về hai đối tượng Text và Value
        /// </summary>
        /// <param name="value"></param>
        /// <param name="keyFirst"></param>
        /// <param name="keyLast"></param>
        /// <returns></returns>
        public static TextValue[] SplitToTextValue(this object value, string keyFirst, string keyLast)
        {
            if (value == null)
            {
                return null;
            }
            string obj = value.ToString().Trim();
            string[] resultSplit = obj.Split(char.Parse(keyLast))
                                .Where(x => x != null && x != string.Empty && x.Contains(keyFirst))
                                .ToArray();

            TextValue[] textValueStrings = new TextValue[resultSplit.Length];

            for (int i = 0; i < resultSplit.Length; i++)
            {
                string[] resultItem = resultSplit[i]
                                    .Split(char.Parse(keyFirst))
                                    .Where(x => x != null && x != string.Empty).ToArray();

                if (resultItem.Length == 2)
                {
                    textValueStrings[i] = new TextValue(resultItem[0].Trim(), resultItem[1].Trim());
                }
            }

            return textValueStrings.Where(x => x != null).ToArray();
        }

        /// <summary>
        ///  Format chuổi theo mẫu chuẩn
        /// </summary>
        /// <param name="value"></param>
        /// <param name="args"></param>
        /// <returns></returns>
        public static string StringFormat(this object value, params object[] args)
        {
            string result = value.ToString().Trim();

            result = string.Format(result, args);

            return result;
        }
    }
}