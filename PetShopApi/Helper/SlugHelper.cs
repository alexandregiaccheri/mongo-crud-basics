using System.Text;
using System.Text.RegularExpressions;

namespace PetShopApi.Helper
{
    public static class SlugHelper
    {
        // Credits to John Roland and Kamran Ayub
        // From: http://predicatet.blogspot.com/2009/04/improved-c-slug-generator-or-how-to.html
        // Accessed on September 1, 2022

        public static string GenerateSlug(this string phrase)
        {
            string str = phrase.RemoveAccent().ToLower();

            str = Regex.Replace(str, @"[^a-z0-9\s-]", ""); // invalid chars          
            str = Regex.Replace(str, @"\s+", " ").Trim(); // convert multiple spaces into one space  
            str = str.Substring(0, str.Length <= 45 ? str.Length : 45).Trim(); // cut and trim it  
            str = Regex.Replace(str, @"\s", "-"); // hyphens  

            return str;
        }

        public static string RemoveAccent(this string txt)
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            byte[] bytes = System.Text.Encoding.GetEncoding("ISO-8859-8").GetBytes(txt);
            return System.Text.Encoding.ASCII.GetString(bytes);
        }
    }
}
