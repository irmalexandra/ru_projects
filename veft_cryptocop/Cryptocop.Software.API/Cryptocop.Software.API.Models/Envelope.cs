using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Cryptocop.Software.API.Models
{
    public class Envelope<T> where T : class
    {
        public Envelope(int pageNumber, IEnumerable<T> items)
        {
            Items = items.Skip((pageNumber - 1) * 20).Take(20);
            PageNumber = pageNumber;
        }
        public int PageNumber { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
