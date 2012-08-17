using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Spring.Aop;
using System.Reflection;

namespace Wcf.SpringDotNetAdvice
{
    public class ThrowsAdvice : IThrowsAdvice
    {
        public void AfterThrowing(MethodInfo method, Object[] args, Object target, Exception ex)
        {
            var a = 0;
        }
        public void AfterThrowing(RemotingException ex)
        {
            var a = 0;
        }
        public void AfterThrowing(ConstraintException ex)
        {
            var a = 0;
        }

        public void AfterThrowing(NoNullAllowedException ex)
        {
            var a = 0;
        }

        public void AfterThrowing(DataException ex)
        {
            var a = 0;
        }
    }
}
