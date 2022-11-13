using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public static class TypeExtensions
{
    /// <summary>
    /// Convert object to integer...
    /// </summary>
    /// <param name="value"></param>
    /// <returns></returns>
    public static int ToInt(this object value)
    {
        try
        {
            if (value != null)
                return Convert.ToInt32(value);
            return 0;
        }
        catch
        {
            return 0;
        }
    }
}

