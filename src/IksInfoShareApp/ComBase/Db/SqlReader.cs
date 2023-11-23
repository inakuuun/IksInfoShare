using System;
using System.Collections.Generic;
using System.Data.Common;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Db
{
    /// <summary>
    /// SQL実行結果読み取りクラス
    /// </summary>
    public class SqlReader : IDisposable
    {
        /// <summary>
        /// SQL実行結果管理用変数
        /// </summary>
        public DbDataReader Reader;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="reader">DB情報読み取りインスタンス</param>
        public SqlReader(DbDataReader reader)
        {
            Reader = reader;
        }

        public string ToStr(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return string.Empty;
            }
            return val.ToString() ?? string.Empty;
        }

        public bool ToBoolean(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = bool.TryParse(val.ToString(), out bool result);
            return result;
        }

        public char ToChar(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = char.TryParse(val.ToString(), out char result);
            return result;
        }

        public sbyte ToSByte(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = sbyte.TryParse(val.ToString(), out sbyte result);
            return result;
        }

        public byte ToByte(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = byte.TryParse(val.ToString(), out byte result);
            return result;
        }

        public short ToShort(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = short.TryParse(val.ToString(), out short result);
            return result;
        }

        public int ToInt(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = int.TryParse(val.ToString(), out int result);
            return result;
        }

        public long ToLong(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = long.TryParse(val.ToString(), out long result);
            return result;
        }

        public double ToDouble(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = double.TryParse(val.ToString(), out double result);
            return result;
        }

        public decimal ToDecimal(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = decimal.TryParse(val.ToString(), out decimal result);
            return result;
        }

        public DateTime ToDateTime(string value)
        {
            var val = Reader[value];
            if (DBNull.Value == val)
            {
                return default;
            }
            _ = DateTime.TryParse(val.ToString(), out DateTime result);
            return result;
        }

        public void Dispose()
        {
            Reader.Close();
            Reader.Dispose();
        }
    }
}
