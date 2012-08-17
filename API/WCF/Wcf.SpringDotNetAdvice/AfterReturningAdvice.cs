using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;

namespace Wcf.SpringDotNetAdvice
{
    public class AfterReturningAdvice : IAfterReturningAdvice
    {
        public void AfterReturning(object returnValue, System.Reflection.MethodInfo method, object[] args, object target)
        {
            
        }
    }
}
