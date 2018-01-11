﻿using ProtoBuf;

namespace Coin.Model.Lang
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class LangText
    {
        public short Lang { get; set; }
        public string Text { get; set; }
    }
}
