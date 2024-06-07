using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace KandM_Clothes.Models.Common
{
    public class Filter
    {
        public static string FilterChar(string str)
        {
            // Trim the input string
            str = str.Trim();
            str = str.ToLower();

            // Arrays of Vietnamese characters and their unaccented counterparts
            string[][] VietNamChar = new string[][]
            {
        new string[] { "a", "á", "à", "ả", "ã", "ạ", "â", "ấ", "ầ", "ẩ", "ẫ", "ậ", "ă", "ắ", "ằ", "ẳ", "ẵ", "ặ" },
        new string[] { "e", "é", "è", "ẻ", "ẽ", "ẹ", "ê", "ế", "ề", "ể", "ễ", "ệ" },
        new string[] { "i", "í", "ì", "ỉ", "ĩ", "ị" },
        new string[] { "o", "ó", "ò", "ỏ", "õ", "ọ", "ô", "ố", "ồ", "ổ", "ỗ", "ộ", "ơ", "ớ", "ờ", "ở", "ỡ", "ợ" },
        new string[] { "u", "ú", "ù", "ủ", "ũ", "ụ", "ư", "ứ", "ừ", "ử", "ữ", "ự" },
        new string[] { "y", "ý", "ỳ", "ỷ", "ỹ", "ỵ" },
        new string[] { "d", "đ" },
            };

            // Replace each Vietnamese character with its unaccented counterpart
            for (int i = 0; i < VietNamChar.Length; i++)
            {
                for (int j = 1; j < VietNamChar[i].Length; j++)
                {
                    str = str.Replace(VietNamChar[i][j], VietNamChar[i][0]);
                }
            }

            // List of special characters to be removed
            char[] specialChars = { '#', '$', '%', '&', '!', '*', '@', '^', '(', ')', '+', '=', '{', '}', '[', ']', '|', '\\', ':', ';', '"', '\'', '<', '>', ',', '.', '?', '/' };

            // Replace special characters with empty string
            foreach (char c in specialChars)
            {
                str = str.Replace(c.ToString(), "");
            }

            str=str.Replace(" ", "-");
            // Return the filtered string
            return str;
        }
    }

}