using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Text.Json.Serialization;
using JetBrains.Annotations;

// ReSharper disable once CheckNamespace
namespace BlazorApplicationInsights;

/// <summary>
/// Options for <see cref="ApplicationInsightsLogger"/>
/// </summary>
[PublicAPI]
public class ApplicationInsightsLoggerOptions
{
    /// <summary>Include the category name of the logger in customDimensions under the 'CategoryName' key</summary>
    public bool IncludeCategoryName { get; set; } = true;

    /// <summary>Include scope information in customDimensions</summary>
    public bool IncludeScopes { get; set; } = true;

    [JsonIgnore]
    [Obsolete("Not part of the stable API")]
    [EditorBrowsable(EditorBrowsableState.Never)]
    public Action<Dictionary<string, object?>>? EnrichCallback { get; set; }
}