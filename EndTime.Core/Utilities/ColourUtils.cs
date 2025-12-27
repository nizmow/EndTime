
using Microsoft.Xna.Framework;

namespace EndTime.Core.Utilities;

public static class ColourUtils
{
    public static Color FromHex(string hexString)
    {
        if (hexString.StartsWith('#'))
        {
            hexString = hexString.Substring(1);
        }

        uint rgba = uint.Parse(hexString, System.Globalization.NumberStyles.HexNumber);

        // Always nice to shift some bits
        return new Color(
            // red
            (byte)((rgba >> 16) & 255), 
            // green
            (byte)((rgba >> 8) & 255),
            // blue
            (byte)(rgba & 255)
        );
    }
}