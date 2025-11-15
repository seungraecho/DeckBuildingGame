using System;
using System.Collections.Generic;

public class LIst<T>
{
    public static implicit operator LIst<T>(List<int> v)
    {
        throw new NotImplementedException();
    }
}