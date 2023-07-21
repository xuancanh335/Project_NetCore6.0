using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Dynamic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using static Common.Constants;
using Image = System.Drawing.Image;

namespace Common.Commons
{
    public static class CommonFuncMain
    {
        static public object GetValueObject(this object obj, string propertyName)
        {
            return obj.GetType().GetProperty(propertyName).GetValue(obj, null);
        }
        public static List<IDictionary<string, object>> JoinList(List<IDictionary<string, object>> source, List<IDictionary<string,
                            object>> dest, string keySource, TYPE_DATA_CAMPARE typeCampare, string keyDest = null)
        {
            keyDest = keyDest == null ? keySource : keyDest;
            List<IDictionary<string, object>> results = new List<IDictionary<string, object>>();

            foreach (var itemSource in source)
            {
                foreach (var itemDest in dest)
                {
                    if (CampareObject(itemDest[keyDest], itemSource[keySource], typeCampare))
                    {
                        results.Add(Merge(itemSource, itemDest));
                    }
                }
            }
            return results;
        }
        public static IDictionary<string, object> Merge(IDictionary<string, object> itemSource, IDictionary<string, object> itemDest)
        {
            IDictionary<string, object> result = new ExpandoObject();

            foreach (var pair in itemSource.Concat(itemDest))
            {
                result[pair.Key] = pair.Value;
            }

            return result;
        }


        public static bool CampareObject(object source, object dest, TYPE_DATA_CAMPARE typeData)
        {
            bool resultCampare = false;

            if (source == null || dest == null) return false;

            string strSource = source.ToString();
            string strDest = dest.ToString();

            switch (typeData)
            {
                case TYPE_DATA_CAMPARE.STRING:
                    resultCampare = strSource == strDest ? true : false;
                    break;
                case TYPE_DATA_CAMPARE.INT:
                    resultCampare = int.Parse(strSource) == int.Parse(strDest) ? true : false;
                    break;
                case TYPE_DATA_CAMPARE.FLOAT:
                    resultCampare = float.Parse(strSource) == float.Parse(strDest) ? true : false;
                    break;
                case TYPE_DATA_CAMPARE.BOOL:
                    resultCampare = bool.Parse(strSource) == bool.Parse(strDest) ? true : false;
                    break;
                case TYPE_DATA_CAMPARE.DATE_TIME:
                    resultCampare = DateTime.Parse(strSource).ToLocalTime() == DateTime.Parse(strDest).ToLocalTime() ? true : false;
                    break;
                case TYPE_DATA_CAMPARE.DATE:
                    resultCampare = DateTime.Parse(strSource).ToLocalTime().Date == DateTime.Parse(strDest).ToLocalTime().Date ? true : false;
                    break;

            }
            return resultCampare;
        }
        public static dynamic DictionaryToObject(IDictionary<String, Object> dictionary)
        {
            var expandoObj = new ExpandoObject();
            var expandoObjCollection = (ICollection<KeyValuePair<String, Object>>)expandoObj;

            foreach (var keyValuePair in dictionary)
            {
                expandoObjCollection.Add(keyValuePair);
            }
            dynamic eoDynamic = expandoObj;
            return eoDynamic;
        }
        public static T ToObject<T>(Object fromObject)
        {
            return JsonConvert.DeserializeObject<T>(JsonConvert.SerializeObject(fromObject, new JsonSerializerSettings { ReferenceLoopHandling = ReferenceLoopHandling.Ignore,PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
        }


        public static List<T> ToObjectList<T>(Object fromObject)
        {
            return JsonConvert.DeserializeObject<List<T>>(JsonConvert.SerializeObject(fromObject, new JsonSerializerSettings { PreserveReferencesHandling = PreserveReferencesHandling.Objects }));
        }

        public static string GenerateCoupon()
        {
            Random random = new Random();
            int length = random.Next(50, 80);
            string characters = "0123456789ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz";
            StringBuilder result = new StringBuilder(length);
            for (int i = 0; i < length; i++)
            {
                result.Append(characters[random.Next(characters.Length)]);
            }
            return result.ToString();
        }
        public static bool IsUnicode(string input)
        {
            return Encoding.ASCII.GetByteCount(input) != Encoding.UTF8.GetByteCount(input);
        }
        public static string utf8Convert3(string s)
        {
            Regex regex = new Regex("\\p{IsCombiningDiacriticalMarks}+");
            string temp = s.Normalize(NormalizationForm.FormD);
            return regex.Replace(temp, String.Empty).Replace('\u0111', 'd').Replace('\u0110', 'D');
        }

        public static string GenerateUniqueNumber()
        {
            List<int> randomNumberList = new List<int>();
            int myNumber = 0;
            Random a = new Random();
            myNumber = a.Next(0, 9999999);
            while (randomNumberList.Contains(myNumber))
                myNumber = a.Next(0, 9999999);
            return myNumber.ToString();
        }

        public static string Encrypt(string data)
        {
            byte[] toEncodeAsBytes = ASCIIEncoding.ASCII.GetBytes(data);
            return Convert.ToBase64String(toEncodeAsBytes);
        }

        public static string Decrypt(string data)
        {
            return ASCIIEncoding.ASCII.GetString(Convert.FromBase64String(data));
        }

        public static string GetFileNameFromRequestURLMain(string requestURL)
        {
            // Get string sau dấu / cuối trong url
            int positionNameInUrl = requestURL.LastIndexOf("/") + 1;
            // Get full file name (có đuôi file)
            string fileNameWithWav = requestURL.Substring(positionNameInUrl, requestURL.Length - positionNameInUrl);
            // Get file name bỏ đuôi file
            string fileName = CommonFuncMain.GetUntilOrEmptyMain(fileNameWithWav, ".");
            return fileName;
        }

        /// <summary>
        ///  Trim string until to specific character
        /// </summary>
        /// <param name=""></param>
        /// <returns></returns>
        public static string GetUntilOrEmptyMain(string text, string stopAt)
        {
            if (!String.IsNullOrWhiteSpace(text))
            {
                int charLocation = text.IndexOf(stopAt, StringComparison.Ordinal);

                if (charLocation > 0)
                {
                    return text.Substring(0, charLocation);
                }
            }

            return String.Empty;
        }

        public static string GenerateAvatarMain(string username, string pathSaveFile)
        {
            string avatarString = username.Substring(0, 1).ToUpper();
            var randomIndex = new Random().Next(0, Constants.COLOR_CODES.Count - 1);
            Color backgroundColor = ColorTranslator.FromHtml(Constants.COLOR_CODES[randomIndex]);
            Color textColor = ColorTranslator.FromHtml("#FFF");
            Font font = new Font(FontFamily.GenericSansSerif, 45);

            GenerateAvatarImageMain(avatarString, font, textColor, backgroundColor, pathSaveFile);

            return pathSaveFile;
        }


        private static Image GenerateAvatarImageMain(string text, Font font, Color textColor, Color backColor, string pathSaveFile)
        {
            //First, create a dummy bitmap just to get a graphics object  
            Image img = new Bitmap(1, 1);
            Graphics drawing = Graphics.FromImage(img);
            drawing.PageUnit = GraphicsUnit.Pixel;

            //Measure the string to see how big the image needs to be  
            SizeF textSize = drawing.MeasureString(text, font);

            //Free up the dummy image and old graphics object  
            img.Dispose();
            drawing.Dispose();

            //Create a new image of the right size  
            img = new Bitmap(100, 100);
            drawing = Graphics.FromImage(img);

            //Paint the background  
            drawing.Clear(backColor);

            //Create a brush for the text  
            Brush textBrush = new SolidBrush(textColor);

            //Drawing.DrawString(text, font, textBrush, 0, 0);  
            drawing.DrawString(text, font, textBrush, new PointF((int)((100 - (textSize.Width)) / 2), (int)((100 - (textSize.Height)) / 2)), new StringFormat());
            drawing.Save();

            textBrush.Dispose();
            drawing.Dispose();

            img.Save(pathSaveFile);

            return img;

        }
       
    }
}
