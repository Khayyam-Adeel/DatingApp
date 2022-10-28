using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.ErrorsLogs
{
    public class ErrorExecption
    {
        public ErrorExecption(int _statusCode,string _Message=null,string _details=null){
            StatusCode=_statusCode;
            Message=_Message;
            details=_details;

        }
        public int StatusCode{get;set;}
        public string Message{get;set;}
        public string details{get;set;} 
    }
}