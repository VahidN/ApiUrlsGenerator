namespace ApiUrlsGenerator;

/// <summary>
///     ApiUrlGenerator's custom options
/// </summary>
public class ApiUrlsGeneratorOptions
{
    /// <summary>
    ///     The output folder of the ApiUrlsGenerator. The generated .cs file will be saved here.
    ///     If this folder doesn't exist, nothing will be generated.
    /// </summary>
    public string? OutputFolder { set; get; }

    /// <summary>
    ///     The output filename of the ApiUrlsGenerator.
    ///     Its default value is `ApiUrls.cs`
    /// </summary>
    public string? OutputFileName { set; get; } = "ApiUrls.cs";

    /// <summary>
    ///     Its default value is `StronglyTypedApiUrls`
    /// </summary>
    public string? DefaultNamespace { set; get; } = "StronglyTypedApiUrls";
}