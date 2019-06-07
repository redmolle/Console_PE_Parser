using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Console_PE_Parser.Images.Sections;
using System.Text.RegularExpressions;

public static class Packer
{
    #region Поля
    public enum Type
    {
        ApLib = 1,
        ApLibStart = 2,
        ApLibDos = 3,
        UPX = 4
    };
    public static Dictionary<Type, string> PackerName = new Dictionary<Type, string>()
    {
        { Type.ApLib, "ApLib" },
        { Type.UPX, "UPX" }
    };
    private static Dictionary<Type, string> _PackerSignature = new Dictionary<Type, string>()
    {
        { Type.ApLibStart, "AP" },
        { Type.ApLibDos, "M8Z" },
        { Type.UPX, "UPX" }
    };
    public static byte[] PackerSignature(Type p)
    {
        return Encoding.Default.GetBytes(
            _PackerSignature
            .FirstOrDefault(f => f.Key == p)
            .Value
            );
    } 
    #endregion

    #region Check
    public static bool CheckApLib(byte[] buf)
    {
        return Regex.IsMatch(
            Parser.GetStringFromBytes(BitConverter.ToUInt16(buf, 0)),
            Parser.GetStringFromBytes(PackerSignature(Type.ApLibStart))) 
            &&
            Regex.IsMatch(
                Parser.GetStringFromBytes(buf.SubArray(24, 3)),
                Parser.GetStringFromBytes(PackerSignature(Type.ApLibDos))
                );
    }

    public static bool CheckUPX(IMAGE_SECTION_HEADER[] sec)
    {
        return sec
            .Any(a => Regex.IsMatch(Parser.GetStringFromBytes(a.Name),
            Parser.GetStringFromBytes(PackerSignature(Type.UPX))));
    } 
    #endregion
}