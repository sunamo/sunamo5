using System;
using System.Collections.Generic;
using System.Text;

public interface IInitializeAfterLoaded
{
    void SetInitAfterLoaded(Action initAfterLoaded);
}