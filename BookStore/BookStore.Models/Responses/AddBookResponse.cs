using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class AddBookResponse : BaseResponse
    {
        public Book? Book { get; set; }
    }
}
