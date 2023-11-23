using ComBase.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ComBase.Msg
{
    /// <summary>
    /// メッセージ基底クラス
    /// </summary>
    public class MsgBase : EventArgs
    {
        /// <summary>
        /// 電文ID
        /// </summary>
        public virtual short MessageId { get; }

        /// <summary>
        /// 電文メッセージ
        /// </summary>
        public byte[] Message { get; set; }

        /// <summary>
        /// メッセージ読み取りインスタンス
        /// </summary>
        protected MsgReader? MsgReader;

        /// <summary>
        /// メッセージ生成インスタンス
        /// </summary>
        protected MsgWriter? MsgWriter;

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsgBase()
        {
            // 派生クラスで定義している全てのサイズで初期化
            Message = new byte[GetLength()];
            MsgWriter = new MsgWriter(Message);
        }

        /// <summary>
        /// コンストラクタ
        /// </summary>
        public MsgBase(byte[] message)
        {
            MsgReader = new MsgReader(message);
            MessageId = MsgReader.RdShort();
            Message = message;
        }

        /// <summary>
        /// 電文データ取得
        /// </summary>
        /// <remarks>
        /// 送信するメッセージのbyte情報を全て取得する
        /// </remarks>
        /// <returns>プロパティ値をbyte配列に変換した値</returns>
        public byte[] BytesRead()
        {
            // メモリストリームからbyte配列を取得
            // https://yuzutan-hnk.hatenablog.com/entry/2017/05/29/020348
            if (MsgWriter != null)
            {
                // 0バイト目から派生クラスで定義している全てのサイズ分読み出す
                MsgWriter.Writer.BaseStream.Position = 0;
                _ = MsgWriter.Writer.BaseStream.Read(Message, 0, Message.Length);
                MsgWriter.Dispose();
            }
            // 読み出したバイト配列を返却
            return Message;
        }

        /// <summary>
        /// 変数ごとに確保するサイズを取得
        /// </summary>
        /// <param name="obj">サイズ取得対象インスタンス</param>
        /// <param name="size">加算時のサイズ</param>
        /// <returns>型サイズを加算した値</returns>
        protected int GetSize(object obj, int size = 0)
        {
            int calc = size;
            if (obj is string) return calc += 1024;
            else if (obj is bool) return calc += sizeof(bool);
            else if (obj is short) return calc += sizeof(short);
            else if (obj is int) return calc += sizeof(int);
            else if (obj is long) return calc += sizeof(long);
            else if (obj is byte) return calc += sizeof(byte);
            return calc;
        }

        /// <summary>
        /// 送信メッセージをメモリストリームに書き込み
        /// </summary>
        public virtual void MsgWrite() { }

        /// <summary>
        /// 電文長取得
        /// </summary>
        /// <returns>プロパティのサイズを全て加算した電文長</returns>
        protected virtual int GetLength() { return default; }
    }
}
