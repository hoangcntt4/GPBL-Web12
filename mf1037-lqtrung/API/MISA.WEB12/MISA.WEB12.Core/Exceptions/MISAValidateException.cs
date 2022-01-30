using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MISA.WEB12.Core.Exceptions
{
    public class MISAValidateException : Exception
    {
        string? MsgErrorValidate = null;
        public MISAValidateException(string msg)
        {
            this.MsgErrorValidate = msg;
        }
        public override string Message
        {
            get { return MsgErrorValidate; }
        }
    }
}
