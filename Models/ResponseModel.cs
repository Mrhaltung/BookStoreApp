namespace Models
{
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class ResponseModel<T>
    {
        public bool Status { get; set; }
        public T Data { get; set; }
        public string Message { get; set; }
    }
}
