using System.Collections.Generic;

public interface IPowershellParser
{
    List<string> ParseToParts(string d, string charWhichIsNotContained);
}