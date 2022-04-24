using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


public class PushSolutionsData
{
    public bool mergeAndFetch = false; 
    public bool addGitignore = false;
    public List<string> onlyThese = null;
    public bool? cs = null;
    /// <summary>
    /// Když nemám očíslované, počítá od 0. tedy warning = 0, error = 1, fatal = 2, ve VS debuggeru při error | fatal vidím 3 
    /// </summary>
    public GitTypesOfMessages checkForGit = GitTypesOfMessages.error | GitTypesOfMessages.fatal;

    public void Set(bool mergeAndFetch, bool addGitignore = false)
    {
        this.mergeAndFetch = mergeAndFetch;
        this.addGitignore = addGitignore;
    }
}