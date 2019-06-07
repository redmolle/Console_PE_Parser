using System;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;
using System.Text;
using System.Linq;

public static partial class Parser
{
    #region SubArray
    public static T[] SubArray<T>(this T[] data, int index, int length)
    {
        length = length >= data.Length - index ? data.Length - index : length;
        T[] result = new T[length];
        Array.Copy(data, index, result, 0, length);
        return result;
    }
    public static T[] SubArray<T>(this T[] data, uint index, int length)
    {
        return data.SubArray((int)index, length);
    }
    public static T[] SubArray<T>(this T[] data, long index, int length)
    {
        return data.SubArray((int)index, length);
    }
    #endregion

    #region GetStringFromBytes
    public static string GetStringFromBytes(byte[] buf)
    {
        return Encoding.Default.GetString(
            buf
            .TakeWhile(w => w != 0)
            .ToArray()
            );
    }
    public static string GetStringFromBytes(uint buf)
    {
        return GetStringFromBytes(BitConverter.GetBytes(buf));
    }
    public static string GetStringFromBytes(ulong buf)
    {
        return GetStringFromBytes(BitConverter.GetBytes(buf));
    } 
    #endregion

    #region GetDecHexAscii
    public static string[] GetDecHexAscii(string name, ulong buf)
    {
        return new string[4]
        {
            name,
            buf.ToString(),
            buf.ToString("X"),
            GetStringFromBytes(buf)
        };
    }
    public static string[] GetDecHexAscii(string name, uint buf)
    {
        return GetDecHexAscii(name, (ulong)buf);
    }
    public static string[] GetDecHexAscii(string name, byte buf)
    {
        return GetDecHexAscii(name, BitConverter.GetBytes(buf));
    }
    public static string[] GetDecHexAscii(string name, byte[] buf)
    {
        return GetDecHexAscii(name, BitConverter.ToUInt64(buf, 0));
    } 
    #endregion

}
