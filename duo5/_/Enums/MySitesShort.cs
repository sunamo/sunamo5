/// <summary>
/// Every web should start with unique letter - checked by AddSpaceAfterFirstLetterForEveryAndSort - 
/// Používá se pro mnoho serverů pro ukládání do DB, proto hodnotu žádné z těchto výčtových hodnot nemůžeš měnit, protože by ti pak nefungovala práce s DB
/// In Short must have every entry first letter unique
/// 
/// After every add/change call from HostingManager via SuMenuItem and open :\Sync\Develop of Future\Lists-Develop of Future\ - is here list with first letters
/// </summary>
public enum MySitesShort : byte
{
    //Ggd = 0,
    Bib = 1,
    Cts = 2,
    Geo = 3,
    App = 4,
    Phs = 5,
    Lyr = 6,
    Dev = 7,
    // Hlavně neměň tuto hodnotu, neposouvej ji vždy až na poslední místo, protože pak by nefungovala práce s DB, ve které(třeba v tabulce Pages) se používá i hodnota None
    Nope = 8,
    // Cant be clc due to Cts
    Mth = 9,
    TBG = 10,
    Fth = 11,
    Sho = 12,
    Sha = 13,
    Eur = 14,
    Wid = 15,
    Var = 16,
    /// <summary>
    /// Honem to přelož aneb ChytreAplikace
    /// </summary>
    Htp = 17,
    Rps = 18,
    /// <summary>
    /// Cant be Sda, one with same first letter there is (Sha)
    /// </summary>
    Yth = 19,
    /// <summary>
    /// Cant be Sda, one with same first letter there is (Wid)
    /// </summary>
    //Wmc = 20,
    None = 255
}