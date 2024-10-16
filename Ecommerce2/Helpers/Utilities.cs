using System;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace Ecommerce2.Helpers
{
    public class Utilities
    {
        public static int PAGE_SIZE = 10;

        public static void CreateIfMissing(string path)
        {
            bool folderExists = Directory.Exists(path);
            if (!folderExists)
            {
                Directory.CreateDirectory(path);
            }
        }

        public static string ToTitleCase(string str)
        {
            string result = str;
            if(!string.IsNullOrEmpty(str))
            {
                var words = str.Split(' ');
                for (int i = 0; i < words.Length; i++)
                {
                    var s = words[i];
                    if (s.Length > 0)
                    {
                        words[i] = s[0].ToString().ToUpper() + s.Substring(1);
                    }
                }
                result = string.Join(" ", words);
            }  
            return result;
        }

        public static bool IsInteger(string str)
        {
            Regex regex = new Regex(@"^[0-9]+$");
            try
            {
                if(string.IsNullOrWhiteSpace(str))
                {
                    return false;
                }    
                if(!regex.IsMatch(str))
                {
                    return false;
                }    
                return true;
            }
            catch
            {

            }
            return false;
        }

        public static string GetRandomKey(int length = 5)
        {
            string pattern = @"0123456789zxcvbnmasdfghjklqwertyuiop[]{}:~!@#$%^&*()+";
            Random rd = new Random();
            StringBuilder sb = new StringBuilder();
            for(int i = 0; i < length; i++)
            {
                sb.Append(pattern[rd.Next(0, pattern.Length)]);
            }
            return sb.ToString();
        }

        public static string SEOUrl(string url)
        {
            var result = url.ToLower();
            result = Regex.Replace(result, @"[áàạảãâấầậẩẫăắằặẳẵ]", "a");
            result = Regex.Replace(result, @"[éèẹẻẽêếềệểễ]", "e");
            result = Regex.Replace(result, @"[óòọỏõôốồộổỗơớờợởỡ]", "o");
            result = Regex.Replace(result, @"[úùụủũưứừựửữ]", "u");
            result = Regex.Replace(result, @"[íìịỉĩ]", "i");
            result = Regex.Replace(result, @"[ýỳỵỷỹ]", "y");
            result = Regex.Replace(result, @"[đ]", "d");

            url = Regex.Replace(url.Trim(), @"[^0-9a-z-\s]", "").Trim();
            //xử lý nhiều hơn 1 khoảng trắng
            url = Regex.Replace(url.Trim(), @"\s+", "-");
            //thay khoảng trắng bằng -
            url = Regex.Replace(url, @"\s", "-");

            while(true)
            {
                if(url.IndexOf("--") != -1)
                {
                    url = url.Replace("--", "-");
                }    
                else
                {
                    break;
                }    
            }   
            return url;
        }

        public static async Task<string> UploadFile(Microsoft.AspNetCore.Http.IFormFile file, string sDirectory, string newname)
        {
            try
            {
                if(newname == null) newname = file.FileName;
                string path = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", sDirectory);
                CreateIfMissing(path);
                string pathFile = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot", "Images", sDirectory, newname);
                var supportedTypes = new[] { "jpg", "jpeg", "png", "gif" };
                var fileExt = System.IO.Path.GetExtension(file.FileName).Substring(1);
                if(!supportedTypes.Contains(fileExt.ToLower()))
                {
                    return null;
                }    
                else
                {
                    using (var stream = new FileStream(pathFile, FileMode.Create))
                    {
                        await file.CopyToAsync(stream);
                    }
                    return newname;
                }    
            }
            catch(Exception ex)
            {
                return null;
            }
        }
    }
}
