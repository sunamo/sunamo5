using System;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Mám to ve rozhraní ale přišel jsem na to že to rozhraní být nemůže
/// 
/// 2 možnosti:
/// PHWin nemít statickou, jen s ci a tento ci poté vkládat v entry assembly do tříd
/// toto mít jako třídu jež bude inmplentovat rozhraní a nemuset to vkládat do žádné třídy - 
/// jak to už dělám u powershellu
/// 
/// už mě tu sere že mám milion závislostí jež se mi různě ztrácí a velké kupy projektů s různorodým obsahem
/// 
/// Takže to udělám jako ve PS:
/// ve win musím mít NS abych jej dokázal odlišit + Win.cs s metodou Init. 
/// 
/// </summary>
public interface IPHWin
{
    void Code(string e);
}