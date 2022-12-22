

/// <summary>
/// Common for Cm + Gp
/// </summary>
public interface IOAuth
{
    string ApiUri{get;}
    string ClientSecret{get;}
    /// <summary>
    /// In gp clientID
    /// </summary>
    string MerchantId{get;}


}

public interface IGoPayOAuth : IOAuth
{
    long GoID{get;}
}

public interface IComgateOAuth : IOAuth
{
    /// <summary>
    /// cm specific
    /// </summary>
    string Email { get; }
}