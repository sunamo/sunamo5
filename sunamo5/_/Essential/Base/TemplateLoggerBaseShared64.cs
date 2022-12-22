using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace sunamo.Essential
{
    public abstract partial class TemplateLoggerBase
    {
        public TemplateLoggerBase(VoidTypeOfMessageStringParamsObject writeLineDelegate)
        {
            _writeLineDelegate = writeLineDelegate;
        }


        static Type type = typeof(TemplateLoggerBase);
        private VoidTypeOfMessageStringParamsObject _writeLineDelegate;

        /// <summary>
        /// Return true if number of counts is odd
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="nameOfCollection"></param>
        /// <param name="args"></param>
        public bool NotEvenNumberOfElements(Type type, string methodName, string nameOfCollection, object[] args)
        {
            if (args.Count() % 2 == 1)
            {
                WriteLine(TypeOfMessage.Error, Exceptions.NotEvenNumberOfElements(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection));
                return false;
            }
            return true;
        }

        private string FullNameOfExecutedCode(object type, string methodName)
        {
            return ThrowEx.FullNameOfExecutedCode(t.Item1, t.Item2);
        }

        private void WriteLine(TypeOfMessage error, string v)
        {
            _writeLineDelegate(error, v);
        }

        /// <summary>
        /// Return true if any will be null
        /// </summary>
        /// <param name="type"></param>
        /// <param name="methodName"></param>
        /// <param name="nameOfCollection"></param>
        /// <param name="args"></param>
        public bool AnyElementIsNull(Type type, string methodName, string nameOfCollection, object[] args)
        {
            List<int> nulled = CA.IndexesWithNull(args);
            if (nulled.Count > 0)
            {
                WriteLine(TypeOfMessage.Information, Exceptions.AnyElementIsNullOrEmpty(FullNameOfExecutedCode(t.Item1, t.Item2), nameOfCollection, nulled));
                return true;
            }
            return false;
        }
    }
}
