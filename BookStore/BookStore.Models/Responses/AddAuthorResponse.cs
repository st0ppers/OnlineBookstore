using System.ComponentModel;
using System.Dynamic;
using BookStore.Models.Models;

namespace BookStore.Models.Responses
{
    public class AddAuthorResponse : BaseResponse
    {
        public Author? Auhtor { get; set; }
    }
}
