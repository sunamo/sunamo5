using System;
using System.Collections.Generic;
using System.Text;

public interface IAbstractCatalog<StorageFolder, StorageFile>
{
    AbstractCatalog<StorageFolder, StorageFile> ac { get; }
}
