using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Challenge.Domain.Common
{
    public class Notify
    {
        public string Code { get; set; }

        public string Property { get; set; }

        public string Message { get; set; }

        public override string ToString()
        {
            DefaultInterpolatedStringHandler defaultInterpolatedStringHandler = new DefaultInterpolatedStringHandler(38, 3);
            defaultInterpolatedStringHandler.AppendLiteral("Notify - Code: ");
            defaultInterpolatedStringHandler.AppendFormatted(Code);
            defaultInterpolatedStringHandler.AppendLiteral(", Property: ");
            defaultInterpolatedStringHandler.AppendFormatted(Property);
            defaultInterpolatedStringHandler.AppendLiteral(", Message: ");
            defaultInterpolatedStringHandler.AppendFormatted(Message);
            return defaultInterpolatedStringHandler.ToStringAndClear();
        }
    }
}
