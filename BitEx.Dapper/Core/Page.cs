﻿using ProtoBuf;
using System.Collections.Generic;

namespace BitEx.Dapper.Core
{
    [ProtoContract(ImplicitFields = ImplicitFields.AllPublic)]
    public class Page<T>
    {
        /// <summary>
        ///     The current page number contained in this page of result set
        /// </summary>
        public long CurrentPage { get; set; }
        /// <summary>
        /// all page count
        /// </summary>
        public long TotalPage { get; set; }
        /// <summary>
        ///     The total number of records in the full result set
        /// </summary>
        public long TotalItems { get; set; }

        /// <summary>
        ///     The number of items per page
        /// </summary>
        public long PageSize { get; set; }

        /// <summary>
        ///     The actual records on this page
        /// </summary>
        public List<T> Items { get; set; }
    }
}
