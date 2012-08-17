using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Spring.Aop;
using Core.DataTypeUtility;
using Wcf.SpringDotNetAdvice.Validate;
using Core.Enums;

namespace Wcf.SpringDotNetAdvice
{
    public class MethodBeforeAdvice : IMethodBeforeAdvice
    {
        public void Before(System.Reflection.MethodInfo method, object[] args, object target)
        {
            
        }

    }
}
