﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BlazorSozluk.Common.Models.Queries
{
    public  class GetEntiresViewModel
    {
        public Guid Id { get; set; }
        public string Subject { get; set; }
        public int CommentCount { get; set; }
    }
}
