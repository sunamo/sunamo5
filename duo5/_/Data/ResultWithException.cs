using System;
using System.Collections.Generic;
using System.Text;


    public class ResultWithException<T>
    {
        public T Data = default(T);
        /// <summary>
        /// only string, because Message property isn't editable after instatiate
        /// </summary>
        public string exc = null;

        public ResultWithException(T data)
        {
            Data = data;
        }

        public ResultWithException(string exc)
        {
            this.exc = exc;
        }

public ResultWithException()
        {

        }


    }