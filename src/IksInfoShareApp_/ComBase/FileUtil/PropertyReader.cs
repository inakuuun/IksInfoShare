using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace ComBase.FileUtil
{
    public class PropertyReader
    {
        /// <summary>
        /// プロパティファイル情報格納用ディクショナリ
        /// </summary>
        private static Dictionary<string, string> _propertyDic = new();

        /// <summary>
        /// プロパティファイル情報格納処理
        /// </summary>
        /// <param name="filePath"></param>
        public static void SetPropertyAll(string filePath)
        {
            XDocument? xd = XDocument.Load(filePath);
            XElement? info = xd.Element("Info");
            if (info != null)
            {
                IEnumerable items = info.Elements("Item");
                foreach (XElement item in items)
                {
                    if (item != null)
                    {
                        SetProperty(item.Attribute("key")?.Value, item.Attribute("value")?.Value);
                    }
                }
            }
        }

        /// <summary>
        /// プロパティ取得処理
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static string GetProperty(string? key)
        {
            string value = string.Empty;
            if (key != null && _propertyDic.ContainsKey(key))
            {
                value = _propertyDic[key];
            }
            return value;
        }

        /// <summary>
        /// プロパティ格納処理
        /// </summary>
        /// <param name="key"></param>
        /// <returns></returns>
        public static void SetProperty(string? key, string? value)
        {
            if (key != null && !_propertyDic.ContainsKey(key))
            {
                _propertyDic[key] = value ?? string.Empty;
            }
        }
    }
}
