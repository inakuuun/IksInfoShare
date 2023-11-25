using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.PortableExecutable;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Msg
{
    /// <summary>
    /// メッセージ生成クラス
    /// </summary>
    public class MsgWriter : IDisposable
    {
        /// <summary>
        /// バイナリ書き込み変数
        /// </summary>
        public BinaryWriter Writer { get => _binWriter; }
        private BinaryWriter _binWriter;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsgWriter(byte[] bytes)
        {
            // BinaryWriterクラスについて => https://learn.microsoft.com/ja-jp/dotnet/api/system.io.binarywriter.write?view=net-7.0
            _binWriter = new BinaryWriter(new MemoryStream(bytes));
        }

        public void WtStr(string param)
        {
            _binWriter.Write(param);
        }

        public void WtBoolean(bool param)
        {
            _binWriter.Write(param);
        }

        public void WtShort(short param)
        {
            _binWriter.Write(param);
        }

        public void WtInt(int param)
        {
            _binWriter.Write(param);
        }
        public void WtLong(long param)
        {
            _binWriter.Write(param);
        }

        public void WtBytes(byte param)
        {
            _binWriter.Write(param);
        }

        /// <summary>
        /// メモリ開放
        /// </summary>
        public void Dispose()
        {
            _binWriter.Close();
            _binWriter.Dispose();
        }
    }
}
