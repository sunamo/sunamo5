/// <summary>
/// Používá se pro mnoho serverů pro ukládání do DB, proto hodnotu žádné z těchto výčtových hodnot nemůžeš měnit, protože by ti pak nefungovala práce s DB
/// In Long should have same first letter as in Short
///
/// After every add/change call from HostingManager via SuMenuItem and open :\Sync\Develop of Future\Lists-Develop of Future\ - is here list with first letters
/// </summary>
public enum MySites : byte
{

    //Ggdag = 0,
    BibleServer =1,
    Cats = 2,
    GeoCaching = 3,
    Apps = 4,
    Photos = 5,
    Lyrics = 6,
    Developer = 7,
    // NID, Hlavně neměň tuto hodnotu, neposouvej ji vždy až na poslední místo, protože  pak by nefungovala práce s DB, ve které(třeba v tabulce Pages) se používá i hodnota None
    Nope = 8,
    // NID
    Calc = 9,
    ThunderBrigade = 10,
    Youth = 11,
    Shortener = 12,
    // NID
    Shared = 13,
    // NID
    Eurostrip = 14,
    Widgets = 15,
    Dart = 16,
    // NID
    AppsCs = 17,
    // NOT RepairServices, NOT RepairPhones
    RepairService = 18,
    /// <summary>
    /// Cant be Sda, one with same first letter there is in short
    /// When I delete here, must delete also from mss. otherwise when I use continue; and convert from mss and ms, continue; here wont find fight enum
    /// </summary>
    Adventist = 19,
    /// <summary>
    /// Cant be Sda, one with same first letter there is in short
    /// </summary>
    //WindowsMetroControls = 20,
    // NID
    None = 255
}