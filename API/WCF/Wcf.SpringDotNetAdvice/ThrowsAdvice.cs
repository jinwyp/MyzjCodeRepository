using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Runtime.Remoting;
using System.Text;
using Spring.Aop;
using System.Reflection;
using Core.LogUtility;
using Core.Enums;

namespace Wcf.SpringDotNetAdvice
{
    public class ThrowsAdvice : IThrowsAdvice
    {
        public void AfterThrowing(MethodInfo method, Object[] args, Object target, Exception ex)
        {
            
        }
        public void AfterThrowing(RemotingException ex)
        {
            
        }
        public void AfterThrowing(ConstraintException ex)
        {
            
        }

        public void AfterThrowing(NoNullAllowedException ex)
        {
            
        }

        public void AfterThrowing(DataException ex)
        {
           
        }
    }
}
