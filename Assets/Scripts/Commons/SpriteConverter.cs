using Loxodon.Framework.Binding.Converters;
using System;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;

public class SpriteConverter : IConverter
{
    SpriteAtlas spriteAtlas;

    public SpriteConverter(SpriteAtlas spriteAtlas)
    {
        this.spriteAtlas = spriteAtlas;
    }

    public object Convert(object value)
    {
        return this.spriteAtlas.GetSprite((string)value);
     
    }

    public object ConvertBack(object value)
    {
        throw new NotImplementedException();
    }
}