using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeDao.MOV.Models
{
  public  class ResponseData<T>
    {
        public string Msg { get; set; }
        public int statusCode { get; set; }
        public T data { get; set; }

    }
}
