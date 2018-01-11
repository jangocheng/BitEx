﻿using Orleans;
using Coin.Framework.EventSourcing;

namespace BitEx.IGrain.Actors.Denormalize
{
    public interface IDepositCodeDenormalizer : IDenormalize, IGrainWithStringKey
    {
    }
}
