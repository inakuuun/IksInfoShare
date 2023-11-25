using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Msg
{
    /// <summary>
    /// メッセージ読み取りクラス
    /// </summary>
    public class MsgReader : IDisposable
    {
        /// <summary>
        /// バイナリ読み取り変数
        /// </summary>
        private BinaryReader _reader;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        /// <param name="bytesMessage"></param>
        public MsgReader(byte[] bytesMessage)
        {
            // BinaryReader クラスについて => https://learn.microsoft.com/ja-jp/dotnet/api/system.io.binaryreader?view=net-7.0
            _reader = new BinaryReader(new MemoryStream(bytesMessage), Encoding.UTF8, false);
        }

        public string RdStr()
        {
            return _reader.ReadString();
        }

        public bool RdBoolean()
        {
            return _reader.ReadBoolean();
        }

        public short RdShort()
        {
            return _reader.ReadInt16();
        }

        public int RdInt()
        {
            return _reader.ReadInt32();
        }
        public long RdLong()
        {
            return _reader.ReadInt64();
        }

        public byte[] RdBytes(int count)
        {
            return _reader.ReadBytes(count);
        }

        /// <summary>
        /// メモリ開放
        /// </summary>
        public void Dispose()
        {
            _reader.Close();
            _reader.Dispose();
        }
    }
}
